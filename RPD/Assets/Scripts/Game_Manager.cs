﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {
	public Scene_Script ss;
	public GameObject[] allDogs = new GameObject[4];
	public GameObject[] ownedDogs = new GameObject[28];
	public GameObject[] dogRoster;
	public bool battling;
	// Use this for initialization
	void Start () {
		battling = false;
		ss = GameObject.Find ("SceneManager").GetComponent<Scene_Script>();
		dogRoster = new GameObject[14];

		//allDogs[0] = d1;
		//allDogs[1] = d2;
		//allDogs[2] = d3;
		for(int i=0; i<dogRoster.Length; i++){
			dogRoster [i] = allDogs[Random.Range(0, allDogs.Length)];
		}

		//dogRoster [0] = allDogs[Random.Range(0, allDogs.Length)];		//adds random dogs from all dogs
		//dogRoster [1] = allDogs[Random.Range(0, allDogs.Length)];
		//dogRoster [2] = allDogs[Random.Range(0, allDogs.Length)];
		//dogRoster [3] = allDogs[Random.Range(0, allDogs.Length)];

		//for (int i = 0; i < dogRoster.Length; i++) {
		//	Debug.Log (dogRoster[i].name);
		//}

		//Debug.Log (ss);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)){
			battling = true;
			ss.AddScene(2);
		}
		if (Input.GetKeyDown(KeyCode.Space)){
			ss.AddScene(3);
		}
	}
}
