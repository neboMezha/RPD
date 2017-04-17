using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Team_Script : MonoBehaviour {
	public Button[] buttons;
	//set the sprites to put into buttons
	public Image mage;
	public Image ninja;
	public Image warrior;
	public Image healer;

	// Use this for initialization
	void Start () {
		//buttons = GameObject.FindGameObjectsWithTag ("Button"); //saves all butatons in an array
		buttons = Button.FindObjectsOfType(typeof(Button)) as Button[];

		List<GameObject> availableDogs = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().owned; 

		for (int i = 0; i < 28; i++) {
			if (availableDogs [i] != null) {
				Debug.Log ("Dog Available");
				buttons [i].image = ninja;
			} 
			else {
				buttons [i].enabled = false;
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
