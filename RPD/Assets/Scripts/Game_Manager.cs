using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Game_Manager : MonoBehaviour {
	public Scene_Script ss;
	public GameObject[] allDogs = new GameObject[4];
	public List<GameObject> owned;
	public List<GameObject> dogRoster;

	Loader enemyLoader;

	enum GameState { map, battle, teamSelect };		// used to control input and stuff and what state the game should be in, regardless of overlapping scenes
	GameState currentState;

	// AUDIO //
	new AudioSource audio;
	public AudioClip mapBGM;
	public AudioClip battleBGM;

	public AudioClip selectionSound;
	public AudioClip cancelSound;

	// For mapscene
	public List<GameObject> mapButtons;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();

		// TO DO: Init map buttons


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


		// LOAD AND ASSIGN IN ENEMY LIST----------------------------------------------
		//enemyLoader.readTextFile("/Assets/ListLoading/enemylist.txt");
		//foreach (Cat c in enemies) {}
		/*for (int i = 0; i < enemyLoader.Lines.Count; i++) {	// not count-1 right this is same as length?
			// make new cat if the loader.Lines[i] has with "ID:"
			if (enemyLoader.Lines[i].Contains("ID:")) {

			}
		}*/

	}

	// Update is called once per frame
	void Update () {
		// MAP SELECT STATE-----------------------------------
		if (currentState == GameState.map) {
			if (Input.GetKeyDown (KeyCode.Return)) {		// to battle		// MAKE CLICK ON A BAD CAT
				ToBattleTest();

				// TO DO: go through and make all the other eventsystems and canvas inactive, and activate the new scenes'
			}
			if (Input.GetKeyDown (KeyCode.Space)) {							// to team select
				ToTeamScene();

				// TO DO: go through and make all the other eventsystems and canvas inactive, and activate the new scenes'
			}
		}

		// BATTLING STATE--------------------------------------
		if (currentState == GameState.battle) {
			// TO DO: input handers here
		}
	}


	/// <summary>
	/// Changes the state, to be used from outside classes when adding/changing scenes
	/// </summary>
	/// <param name="stateName">State name: map, battle, teamSelect</param>
	public void ChangeState(string stateName) {
		if (stateName == "map") {
			currentState = GameState.map;
		}
		else if (stateName == "battle") {
			currentState = GameState.battle;
		}
		else if (stateName == "teamSelect") {
			currentState = GameState.teamSelect;
		}
	}

	// Temporary?? methods to add scenes, used with buttons
	void ToTeamScene() {
		audio.Stop();
		audio.PlayOneShot (selectionSound, 1.0f);
		ss.AddScene (3);
		currentState = GameState.teamSelect;
	}

	void ToSettingsScene() {

	}

	/// <summary>
	/// TEMPORARY TEST FUNCTION, goes to randomized battle scene
	/// </summary>
	public void ToBattleTest(){
		audio.Stop();
		audio.PlayOneShot (selectionSound, 1.0f);			
		ss.AddScene (2);
		currentState = GameState.battle;
	}


	/// <summary>
	/// Loads the specified battle
	/// </summary>
	void LoadBattle(){

	}
}
