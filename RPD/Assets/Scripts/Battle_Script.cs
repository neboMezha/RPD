using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Battle_Script : MonoBehaviour {
	//public GameObject d1;
	public Scene_Script ss;

	public GameObject[] dogs;		//need to create DogRoster Array in Main/Map scene to take in the selected dogs
	private float timer;
	private float timeHolder;
	public int knockedOut;

	// AUDIO //
	AudioSource audio;
	public AudioClip catDamageSound;
	public AudioClip catAttackSound;

	//NEED CAT CLASS


	// Use this for initialization
	void Start () {
		// Initialize Audio Source Component
		audio = GetComponent<AudioSource>();

		knockedOut = 0;
		dogs = new GameObject[3];
		GameObject[] roster = GameObject.Find ("GameManager").GetComponent<Game_Manager>().dogRoster;
		for (int i = 0; i < roster.Length; i++) {
			dogs[i] = Instantiate(roster[i], new Vector2(-50, 100-(i*100)), Quaternion.identity);	 		// instantiates teh prefab
			Debug.Log (dogs[i] + " instantiated");
			string name = dogs [i].GetComponent ("Dog_Script").name.Substring (0, dogs [i].GetComponent ("Dog_Script").name.Length - 7);
			dogs[i].GetComponent("Dog_Script").name = name;													// sets the name to the dog object
			dogs[i].transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);

		}
		ss = GameObject.Find ("SceneManager").GetComponent<Scene_Script>();
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; 		//time for Cat to attack
		if (timer >= 3.0f) {
			timer = 0;

			int target = Random.Range (0, dogs.Length);					//cat picks random dog
			while (dogs [target].GetComponent<Dog_Script> ().koed) { 	//if the dog is KOed, pick new target
				target = Random.Range (0, dogs.Length);
			}
			Debug.Log ("Cat Attacked " + dogs[target].name); 			//cat hits dog

			audio.PlayOneShot(catDamageSound, 0.5f);					// Play Cat attack sound
			dogs [target].GetComponent<Dog_Script> ().TakeDamage (); 	//dog takes damage
		}

		for (int i = 0; i < dogs.Length; i++) {							//checks for KOed dogs
			if (dogs [i].GetComponent<Dog_Script> ().koed) {
				//dogs [i].transform.position.x = 0;
				dogs[i].GetComponent<Renderer>().enabled = false;		//hides KOed ones
			}
		}

		if (knockedOut == dogs.Length) {								// if all KOed, exit battle
			GameObject.Find ("GameManager").GetComponent<Game_Manager>().battling = false;
			ss.UnloadScene(2);
		}
	}
}
