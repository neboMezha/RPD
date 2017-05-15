using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle_Script : MonoBehaviour {
	//public GameObject d1;
	Scene_Script ss;
	public Button[] buttons;
	public List<Button> catSelectors;

	public GameObject[] dogs;		//need to create DogRoster Array in Main/Map scene to take in the selected dogs
	private float timer;
	private float[] timers;
	private float timeHolder;
	public int knockedOut;

	public GameObject areYouSureObj;	// group of objects for are you sure screen pop up
	bool battlePaused = false;

	public GameObject enemyPlaceholderObj;

	// AUDIO //
	new AudioSource audio;
	public AudioClip catDamageSound;
	public AudioClip catAttackSound;

	//temporarily hardcoded cats
	public List<GameObject> cats;
	private int targetIndex;
	public int catHP = 25;
	private int catCount;
	private Color temp = new Color (0, 0, 0, 0); 	//NECESSARY TO MAKE SELECTOR ICON INVISIBLE
	private int catsInLvl;

	public GameObject[] ui = new GameObject[2];

	// Use this for initialization
	void Start () {
		areYouSureObj.SetActive (false);
		
		//disables all but one target
		for (int k = 0; k < catSelectors.Count; k++) {
			catSelectors [k].onClick.AddListener (ChangeTargets);
			if (k > 0) {
				catSelectors [k].GetComponent<Image> ().color = temp;
			}
		}

		catCount = 0;
		targetIndex = 0;
		//cat creation and loading
		//pulls in the battle ID form the Game manager
		int battleID = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().battleID;

		//sets the IDs of the Cats for this battle localy
		List<int> catIDs = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().battles [battleID];

		//go through every cat that has an ID provided, add the correlating Cat class to the gameobjects
		for (int i = 0; i < catIDs.Count; i++) {	
			Cat c1 =GameObject.Find ("GameManager").GetComponent<Game_Manager> ().cats [catIDs[i]];
			cats [i].AddComponent<Cat> ();


			cats [i].GetComponent<Cat> ().CatName = c1.CatName;
			cats [i].GetComponent<Cat> ().AttackRate = c1.AttackRate;
			cats [i].GetComponent<Cat> ().MaxHp = c1.MaxHp;
			cats [i].GetComponent<Cat> ().Hp = c1.MaxHp;
			cats [i].GetComponent<SpriteRenderer> ().sprite = c1.Image;
			cats [i].transform.localScale /= 2f;
			catCount++;
		}

		catsInLvl = catIDs.Count-1;
		timers = new float[catsInLvl+1];
		for (int a = 0; a < timers.Length; a++) {
			timers [a] = 0;
		}
		//disable other cat slots in the scene
		for (int j = catIDs.Count; j < cats.Count; j++) {
			cats[j].SetActive(false);
			catSelectors [j].enabled = false;
		}


		// Initialize Audio Source Component
		audio = GetComponent<AudioSource>();

		//GameObject.Find ("Canvas").GetComponent<CanvasGroup> ().alpha = 0f;


		buttons = new Button[14];

		for (int j = 1; j < 15; j++) {
			buttons [j - 1] = GameObject.Find (j.ToString()).GetComponent<Button>();
			buttons [j - 1].enabled = false;
		}

		knockedOut = 0;
		int size = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().dogRoster.Count;
		dogs = new GameObject[size];
		List<GameObject> roster = GameObject.Find ("GameManager").GetComponent<Game_Manager>().dogRoster;

		//----instantiates all the dogs in the roster
		for (int i = 0; i < roster.Count; i++) {
			buttons [i].enabled = true;
			dogs [i] = Instantiate(roster[i], buttons[i].transform.position, Quaternion.identity);
			string name = dogs [i].GetComponent ("Dog_Script").name.Substring (0, dogs [i].GetComponent ("Dog_Script").name.Length - 7);
			dogs[i].GetComponent("Dog_Script").name = name;	
			buttons [i].onClick.AddListener (dogs [i].GetComponent <Dog_Script>().Attack);

		}
		ss = GameObject.Find ("SceneManager").GetComponent<Scene_Script>();

		catHP = cats[targetIndex].GetComponent<Cat> ().MaxHp;

		//--NOTE
		//should create the buttons with a "selected" sprite to show which cat is the target

	}

	// Update is called once per frame
	void Update () {
		if (!battlePaused) {
			cats [targetIndex].GetComponent<Cat> ().Hp = catHP;

			for(int n=0; n < timers.Length; n++){
				timers[n] += Time.deltaTime; 		//time for Cat to attack
			}
			//----Cat Attack
			for (int m = 0; m <= catsInLvl; m++) {
				if (timers[m] >= cats [m].GetComponent<Cat> ().AttackRate && cats[m].activeSelf) {
					timers[m] = 0;
					int rand = Random.Range (0, 30);

					int agr = 0;
					int target = 0;
					if (rand >= 10) {
						for (int j = 0; j < dogs.Length; j++) {
							if (dogs [j].GetComponent<Dog_Script> ().koed == false) {
								if (dogs [j].GetComponent<Dog_Script> ().aggro >= agr) { 	//if next dog has higher aggro, make his target
									agr = dogs [j].GetComponent<Dog_Script> ().aggro;
									target = j;
								}
							}
						}
						//Debug.Log ("Chose strongest");
					} else {
						target = Random.Range (0, dogs.Length);

						if (dogs [target].GetComponent<Dog_Script> ().koed) { 	//if the dog is KOed, pick new target //CAN CAUSE A CRASH IN FIRST LEVEL (all enemies go for the highest aggro seems to kill the game)
							//target = Random.Range (0, dogs.Length);
							target++;
							if (target >= dogs.Length)
								target = 0;
						}

						//Debug.Log ("Chose random");
					}
					//Debug.Log (cats [m].GetComponent<Cat> ().CatName + " attacked");
					dogs [target].GetComponent<Dog_Script> ().TakeDamage (); 	//dog takes damage
				}
			}

			//-----Checking for KOs
			for (int i = 0; i < dogs.Length; i++) {							//checks for KOed dogs
				if (dogs [i] != null) {
					if (dogs [i].GetComponent<Dog_Script> ().koed) {
						dogs [i].GetComponent<Renderer> ().enabled = false;		//hides KOed ones
						buttons [i].enabled = false;
					}
				}
			}

			//Change Cat targets when one dies
			if (catHP <= 0 && catCount > 0) {
				catSelectors [targetIndex].GetComponent<Image> ().color = temp; //makes current selector invisible
				cats[targetIndex].SetActive (false);	//disables that cat
				catSelectors [targetIndex].enabled = false;


				for (int j = 0; j < catsInLvl; j++) {
					if (!cats [targetIndex].activeSelf) {
						targetIndex++;
						if (targetIndex > catsInLvl) {
							targetIndex = 0;
						}
					}
					else
						break;
				}

				catHP = cats[targetIndex].GetComponent<Cat> ().Hp;
				catCount--;
				Debug.Log (catCount);
				catSelectors [targetIndex].GetComponent<Image> ().color = Color.white;
			}
			// END BATTLE STUFF
			if (knockedOut >= dogs.Length || catCount <=0) {								// if all KOed, exit battle OR kill cat
				for (int i = 0; i < dogs.Length; i++) {
					Destroy (dogs[i]);
				}
				//GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ChangeState ("map");
				//ss.UnloadScene(2);

				if (knockedOut >= dogs.Length) {
					// change gamestate SCREAMS
					GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ChangeState("lose");
				} else if (catCount <= 0) {
					GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ChangeState("win");
				}

				ss.AddScene(7);	// kEEPS GETTING CALLED INFINITELY
				Destroy(ui[0]);
				Destroy(ui[1]);
				Destroy(gameObject);	// kill self

				// this makes it go straight back to map, IF COMMENTED OUT DONT WORRY NOT AN ERROR WHEN U PLAY
				//GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ToMapScene(2);
			}

			catHP = cats [targetIndex].GetComponent<Cat> ().Hp;
		}

	}


	public void ChangeTargets(){
		Vector2 mousept = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		//turn off the selector of current target
		catSelectors [targetIndex].GetComponent<Image> ().color = temp;


		//shift the selector
		if (mousept.x >= -0.2f && mousept.x <= 0.9f) {
			targetIndex = 0;
		}
		else if (mousept.x >= 0.8f && mousept.x <= 1.9f) {
			if (mousept.y > 0) {
				targetIndex = 1;

			} else {
				targetIndex = 2;
			}
		}
		else if (mousept.x >= 1.8f && mousept.x <= 2.9f) {
			if (mousept.y > 1) {
				targetIndex = 5;
			} else if (mousept.y < -1) {
				targetIndex = 3;
			} else {
				targetIndex = 4;
			}
		}
		catSelectors [targetIndex].GetComponent<Image> ().color = Color.white;

		catHP = cats[targetIndex].GetComponent<Cat> ().Hp;
		Debug.Log (catHP);
	}


	/// <summary>
	/// To the Win or Lose scene depending on value.
	/// Only to be accessed from Battle Scene!!!
	/// </summary>
	/// <param name="value">0 - Lose. 1 - win.</param>
	void ToWinLose(int value) {
		// TO DO: play win or lose noise
		ss.AddScene(7);
		if (value == 0) {

		} else if (value == 1) {

		}

	}

	public void PromptEscape(){
		areYouSureObj.SetActive (true);
		battlePaused = true;
	}

	/// <summary>
	/// only to use from paused/areyousure screen
	/// </summary>
	public void ReturnToBattle() {
		areYouSureObj.SetActive (false);
		battlePaused = false;
	}

	public void EscapeBattle() {
		GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ToMapScene(2);
	}


}
