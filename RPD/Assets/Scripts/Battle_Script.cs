using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle_Script : MonoBehaviour {
	//public GameObject d1;
	public Scene_Script ss;
	public Button[] buttons;
	public int catHP = 15;

	public GameObject[] dogs;		//need to create DogRoster Array in Main/Map scene to take in the selected dogs
	private float timer;
	private float timeHolder;
	public int knockedOut;

	// AUDIO //
	new AudioSource audio;
	public AudioClip catDamageSound;
	public AudioClip catAttackSound;


	// Use this for initialization
	void Start () {
		// Initialize Audio Source Component
		audio = GetComponent<AudioSource>();

		GameObject.Find ("Canvas").GetComponent<CanvasGroup> ().alpha = 0f;

		buttons = new Button[14];

		for (int j = 1; j < 15; j++) {
			buttons [j - 1] = GameObject.Find (j.ToString()).GetComponent<Button>();
			buttons [j - 1].enabled = false;
		}

		knockedOut = 0;
		int size = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().dogRoster.Length;
		dogs = new GameObject[size];
		GameObject[] roster = GameObject.Find ("GameManager").GetComponent<Game_Manager>().dogRoster;

		for (int i = 0; i < roster.Length; i++) {

			buttons [i].enabled = true;
			dogs [i] = Instantiate(roster[i], buttons[i].transform.position, Quaternion.identity);
			string name = dogs [i].GetComponent ("Dog_Script").name.Substring (0, dogs [i].GetComponent ("Dog_Script").name.Length - 7);
			dogs[i].GetComponent("Dog_Script").name = name;	
			buttons [i].onClick.AddListener (dogs [i].GetComponent <Dog_Script>().Attack);

		}
		ss = GameObject.Find ("SceneManager").GetComponent<Scene_Script>();
		//Debug.Log (dogs[0]);
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
					//if (dogs[j] != null)		////////////////////////////<- test by Aiden
					//{
						if (dogs [j].GetComponent<Dog_Script> ().koed == false) {
							if (dogs [j].GetComponent<Dog_Script> ().aggro >= agr) { 	//if next dog has higher aggro, make his target
								agr = dogs [j].GetComponent<Dog_Script> ().aggro;
								target = j;
							}
						}
					//}

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
			GameObject.Find ("GameManager").GetComponent<Game_Manager>().battling = false;
			for (int i = 0; i < dogs.Length; i++) {
				Destroy (dogs[i]);
			}
			ss.UnloadScene(2);
		}
	}

}
