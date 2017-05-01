using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle_Script : MonoBehaviour {
	//public GameObject d1;
	Scene_Script ss;
	public Button[] buttons;

	public GameObject[] dogs;		//need to create DogRoster Array in Main/Map scene to take in the selected dogs
	private float timer;
	private float timeHolder;
	public int knockedOut;

	public GameObject enemyPlaceholderObj;

	// AUDIO //
	new AudioSource audio;
	public AudioClip catDamageSound;
	public AudioClip catAttackSound;

	//temporarily hardcoded cats
	public GameObject cat1;
	public GameObject cat2;
	public GameObject cat3;
	public int catHP = 25;

	// Use this for initialization
	void Start () {
		//cat creation and loading
		Cat c1 = GameObject.Find("GameManager").GetComponent<Game_Manager> ().cats [0];
		cat1.AddComponent<Cat>();

		cat1.GetComponent<SpriteRenderer>().sprite = c1.Image;
		cat1.transform.localScale /= 2f;


		Cat c2 = GameObject.Find("GameManager").GetComponent<Game_Manager> ().cats [1];
		cat2.AddComponent<Cat>();

		cat2.GetComponent<SpriteRenderer>().sprite = c2.Image;
		cat2.transform.localScale /= 2f;

		Cat c3 = GameObject.Find("GameManager").GetComponent<Game_Manager> ().cats [2];
		cat3.AddComponent<Cat>();

		cat3.GetComponent<SpriteRenderer>().sprite = c3.Image;
		cat3.transform.localScale /= 2f;



		// Initialize Audio Source Component
		audio = GetComponent<AudioSource>();

		GameObject.Find ("Canvas").GetComponent<CanvasGroup> ().alpha = 0f;

		buttons = new Button[14];

		for (int j = 1; j < 15; j++) {
			buttons [j - 1] = GameObject.Find (j.ToString()).GetComponent<Button>();
			buttons [j - 1].enabled = false;
		}

		knockedOut = 0;
		int size = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().dogRoster.Count;
		dogs = new GameObject[size];
		List<GameObject> roster = GameObject.Find ("GameManager").GetComponent<Game_Manager>().dogRoster;

		Debug.Log (GameObject.Find ("GameManager").GetComponent<Game_Manager>().dogRoster[1]);
		//----instantiates all the dogs in the roster
		for (int i = 0; i < roster.Count; i++) {
			buttons [i].enabled = true;
			dogs [i] = Instantiate(roster[i], buttons[i].transform.position, Quaternion.identity);
			string name = dogs [i].GetComponent ("Dog_Script").name.Substring (0, dogs [i].GetComponent ("Dog_Script").name.Length - 7);
			dogs[i].GetComponent("Dog_Script").name = name;	
			buttons [i].onClick.AddListener (dogs [i].GetComponent <Dog_Script>().Attack);

		}
		ss = GameObject.Find ("SceneManager").GetComponent<Scene_Script>();
	}

	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime; 		//time for Cat to attack
		//----Cat Attack
		if (timer >= 3.0f) {
			timer = 0;
			int rand = Random.Range(0,30);

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
				Debug.Log ("Chose strongest");
			} 
			else {
				target = Random.Range (0, dogs.Length);

				while (dogs [target].GetComponent<Dog_Script> ().koed) { 	//if the dog is KOed, pick new target
					target = Random.Range (0, dogs.Length);
				}

				Debug.Log ("Chose random");
			}

			dogs [target].GetComponent<Dog_Script> ().TakeDamage (); 	//dog takes damage
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

		if (knockedOut == dogs.Length || catHP <=0) {								// if all KOed, exit battle OR kill cat
			for (int i = 0; i < dogs.Length; i++) {
				Destroy (dogs[i]);
			}

			//GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ChangeState ("map");
			//ss.UnloadScene(2);
			GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ToMapScene(2);
		}
	}



	/// <summary>
	/// Loads the specified battle--FOR NOW hardcoded single battle
	/// </summary>
	void LoadBattle(){

	}

}
