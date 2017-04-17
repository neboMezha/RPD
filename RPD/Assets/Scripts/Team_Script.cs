using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Team_Script : MonoBehaviour {
	public Button[] buttons;
	//set the sprites to put into buttons
	public Sprite mage;
	public Sprite ninja;
	public Sprite warrior;
	public Sprite healer;

	// Use this for initialization
	void Start () {
		//buttons = GameObject.FindGameObjectsWithTag ("Button"); //saves all buttons in an array
		buttons = Button.FindObjectsOfType(typeof(Button)) as Button[];

		GameObject[] availableDogs = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ownedDogs; 

		for (int i = 0; i < buttons.Length; i++) {
			if (availableDogs [i] != null) {
				//set the sprite to what it needs to be
			} 
			else {
				//disable the button
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Back(){
		GameObject.Find ("SceneManager").GetComponent<Scene_Script> ().UnloadScene (3);
	}

	//on click
	public void SelectDog(){
		//if not selected
			//add to GameManager-> dogRoster
		//else
			//find same dog in the roster and remove it
	}
}
