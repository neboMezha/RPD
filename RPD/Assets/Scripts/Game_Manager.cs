using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// my god please center everything in the scenes including all game object script holders 0,0,0

[RequireComponent(typeof(AudioSource))]
public class Game_Manager : MonoBehaviour {
	public Scene_Script ss;
	public GameObject[] allDogs = new GameObject[5];
	public GameObject[] dogRoster;
	public bool battling;

	enum GameState { map, battle, teamSelect };		// used to control input and stuff and what state the game should be in, regardless of overlapping scenes
	GameState currentState;

	// AUDIO //
	AudioSource audio;
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

		battling = false;
		ss = GameObject.Find ("SceneManager").GetComponent<Scene_Script>();
		dogRoster = new GameObject[3];

		//allDogs[0] = d1;
		//allDogs[1] = d2;
		//allDogs[2] = d3;

		dogRoster [0] = allDogs[Random.Range(0, allDogs.Length)];		//adds random dogs from all dogs
		dogRoster [1] = allDogs[Random.Range(0, allDogs.Length)];
		dogRoster [2] = allDogs[Random.Range(0, allDogs.Length)];
		//dogRoster [3] = allDogs[Random.Range(0, allDogs.Length)];

		//for (int i = 0; i < dogRoster.Length; i++) {
		//	Debug.Log (dogRoster[i].name);
		//}

		//Debug.Log (ss);
	}

	// Update is called once per frame
	void Update () {
		// MAP SELECT STATE-----------------------------------
		if (currentState == GameState.map) {
			if (Input.GetKeyDown (KeyCode.Return)) {		// to battle
				audio.Stop();
				audio.PlayOneShot (selectionSound);
				battling = true;		//
				ss.AddScene (2);
				currentState = GameState.battle;
			}
			if (Input.GetKeyDown (KeyCode.Space)) {			// to team select
				audio.Stop();
				audio.PlayOneShot (selectionSound);
				ss.AddScene (3);
				currentState = GameState.teamSelect;
			}
		}


		// BATTLING STATE--------------------------------------
		if (currentState == GameState.battle) {
			
		}
	}
}
