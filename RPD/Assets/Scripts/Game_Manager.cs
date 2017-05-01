using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(AudioSource))]
public class Game_Manager : MonoBehaviour {
	public Scene_Script ss;
	public GameObject[] allDogs = new GameObject[4];
	public List<GameObject> owned;
	public List<GameObject> dogRoster;
	public List<Cat> cats;
	public Sprite[] allCats = new Sprite[4];

	Loader enemyLoader;

	public GameObject canvas;	// store the canvas object to be set active and inactive


	enum GameState { map, battle, teamSelect, summon };		// used to control input and stuff and what state the game should be in, regardless of overlapping scenes
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
		enemyLoader = new Loader();
		cats = new List<Cat>();

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
		//ISSUE --> creates and instantiated the cats into the scene (the cat = new GameObject part)
		//after adding the cat to the list, the gameobject cat is also altered when altering cats[index]
		enemyLoader.readTextFile(Application.dataPath + "/ListLoading/enemylist.txt");
		//foreach (Cat c in enemies) {}
		for (int i = 0; i < enemyLoader.Lines.Count; i++) {	// not count-1 right this is same as length?
			// make new cat if the loader.Lines[i] has with "ID:"
			if (enemyLoader.Lines [i].Contains ("Id:")) {
				Cat cat;
				//to be used to assign sprite from AllCats
				int num = int.Parse (enemyLoader.Lines [i].Substring (enemyLoader.Lines [i].Length - 1));

				//save line info as propper types
				string name = enemyLoader.Lines [i + 1];
				int hp = int.Parse (enemyLoader.Lines [i + 2]);
				int atkRange = int.Parse (enemyLoader.Lines [i + 3]);
				float atkRate = float.Parse (enemyLoader.Lines [i + 4]);
				string img = enemyLoader.Lines [i + 1];
				Sprite sprit = allCats [num];

				cat = new Cat (name, hp, atkRange, atkRate, sprit);

				cats.Add (cat);
			}
		}

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
	/// SCENE CHANGE FUNCTIONS: Public, handles everything that happens when the scene changes
	/// USE THESE TO CHANGE SCENES ONLY!!!
	/// </summary>
	public void ToTeamScene() {
		audio.PlayOneShot (selectionSound, 1.0f);
		ss.AddScene (3);
		ChangeState ("teamSelect");
		canvas.SetActive (false);
	}

	public void ToSettingsScene() {

	}

	/// <summary>
	/// TEMPORARY TEST FUNCTION, goes to randomized battle scene
	/// </summary>
	public void ToBattleTest(){
		audio.Stop();
		audio.PlayOneShot (selectionSound, 1.0f);			
		ss.AddScene (2);
		ChangeState ("battle");
		canvas.SetActive (false);
	}

	/// <summary>
	/// Tos the map scene.
	/// </summary>
	/// <param name="currentScene">Current scene from which this is being called (so it can be closed).</param>
	public void ToMapScene(int currentScene){
		audio.Play();
		ss.UnloadScene(currentScene);	// unload current scene since map is always in bg
		ChangeState ("map");
		canvas.SetActive (true);
	}

	public void ToSummonScene() {
		audio.PlayOneShot (selectionSound, 1.0f);
		ss.AddScene (4);
		ChangeState ("summon");
		canvas.SetActive (false);
	}




	/// <summary>
	/// Changes the state, to be used from outside classes when adding/changing scenes.
	/// Inner helper function.
	/// </summary>
	/// <param name="stateName">State name: "map", "battle", "teamSelect"</param>
	private void ChangeState(string stateName) {
		if (stateName == "map") {
			currentState = GameState.map;
		}
		else if (stateName == "battle") {
			currentState = GameState.battle;
		}
		else if (stateName == "teamSelect") {
			currentState = GameState.teamSelect;
		}
		else if (stateName == "summon") {
			currentState = GameState.summon;
		}
	}
}
