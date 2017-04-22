using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {
	public Scene_Script ss;
	public GameObject[] allDogs = new GameObject[4];
	//public GameObject[] ownedDogs;
	public List<GameObject> owned;
	public List<GameObject> dogRoster;

	[HideInInspector] public bool battling;

	enum GameState { map, battle, teamSelect };		// used to control input and stuff and what state the game should be in, regardless of overlapping scenes
	GameState currentState;

	// AUDIO //
	new AudioSource audio;
	public AudioClip mapBGM;
	public AudioClip battleBGM;

	public AudioClip selectionSound;
	public AudioClip cancelSound;

	// Use this for initialization
	void Start () {

		audio = GetComponent<AudioSource>();

		// Initit map defauts at first since map is the first to load GameManager object
		currentState = GameState.map;

		// GET CANVAS EVENTHANDLER OBJECTS (***need to be disabled when not on that gamestate)


		//sets the first
		owned.Add (allDogs [0]);
		owned.Add (allDogs [3]);
		owned.Add (allDogs [3]);
		owned.Add (allDogs [0]);

		owned.Add (allDogs [1]);
		owned.Add (allDogs [2]);
		owned.Add (allDogs [2]);
		owned.Add (allDogs [1]);
		/*
		//set all dogs 28 times
		for(int j=0; j<28; j++){
			owned.Add (allDogs [Random.Range(0,4)]);
		}
		*/

		ss = GameObject.Find ("SceneManager").GetComponent<Scene_Script>();

		/*
		ownedDogs = new GameObject[6];

		battling = false;

		if(ownedDogs.Length < 14)
			dogRoster = new GameObject[ownedDogs.Length];
		else
			dogRoster = new GameObject[14];

		//--Sets length of roster
		if(owned.Count < 14)
			dogRoster = new GameObject[owned.Count];
		else
			dogRoster = new GameObject[14];

		*/

		//--TEMPORARY
		//--Assign dogs to roster slots
		int teamSize = 0;
		if (owned.Count < 14)
			teamSize = owned.Count;
		else
			teamSize = 14;
		
		for(int i=0; i<4; i++){
			dogRoster.Add(owned[i]);
			Debug.Log ("Added " + owned[i].name);
		}

	}

	// Update is called once per frame
	void Update () {
		// MAP SELECT STATE-----------------------------------
		if (currentState == GameState.map) {
			if (Input.GetKeyDown (KeyCode.Return)) {		// to battle
				audio.Stop();
				audio.PlayOneShot (selectionSound, 1.0f);
				battling = true;		//
				ss.AddScene (2);
				currentState = GameState.battle;

				// TO DO: go through and make all the other eventsystems and canvas inactive, and activate the new scenes'
			}
			if (Input.GetKeyDown (KeyCode.Space)) {			// to team select
				audio.Stop();
				audio.PlayOneShot (selectionSound, 1.0f);
				ss.AddScene (3);
				currentState = GameState.teamSelect;

				// TO DO: go through and make all the other eventsystems and canvas inactive, and activate the new scenes'
			}

			if (Input.GetKeyDown (KeyCode.LeftShift)) {			// to team select
				audio.Stop();
				audio.PlayOneShot (selectionSound, 1.0f);
				ss.AddScene (4);
				currentState = GameState.teamSelect;

				// TO DO: go through and make all the other eventsystems and canvas inactive, and activate the new scenes'
			}
		}


		// BATTLING STATE--------------------------------------
		if (currentState == GameState.battle) {
			// TO DO: input handers here
		}
	}
	public void ChangeState(){
		currentState = GameState.map;
	}
}
