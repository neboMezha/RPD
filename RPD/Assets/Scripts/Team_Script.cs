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
	public int numSelected; //used to track how many dogs are selected
	private List<GameObject> availableDogs;

	private List<GameObject> team;
	private Scene_Script ss;

	// Use this for initialization
	void Start () {
		numSelected = 0;
		//buttons = GameObject.FindGameObjectsWithTag ("Button"); //saves all butatons in an array
		ss = GameObject.Find ("SceneManager").GetComponent<Scene_Script> ();

		GameObject btns = GameObject.Find("Buttons");
		buttons = btns.GetComponentsInChildren<Button> ();

		availableDogs = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().owned; 

		//--Makes the screen only scrollable when there are more dogs than screen space
		if (availableDogs.Count <= 16) {
			GameObject.Find ("Scrollable").GetComponent<ScrollRect> ().enabled = false;
		} 
		else {
			GameObject.Find ("Scrollable").GetComponent<ScrollRect> ().enabled = true;
		}

		//--sets sprite to button if there is a dog to fill the spot, else disables the button
		for (int i = 0; i < 28; i++) {
			if (availableDogs.Count > i) {
				//Debug.Log ("Dog Available");
				buttons [i].image.sprite = availableDogs[i].GetComponent<SpriteRenderer>().sprite;
			} 
			else {
				//Debug.Log ("Button Disabled");
				buttons [i].image.enabled = false;
				buttons [i].interactable = false;
			}
		}

		//---create a copy of the roster to use for selected state
		team = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().dogRoster;

		//
		for (int j = 0; j < team.Count; j++) {
			for (int k = 0; k < availableDogs.Count; k++) {
				if (availableDogs [k].name == team [0].name) {
					//make that dog chosen
					buttons[k].GetComponent<Roster_Button_Script>().SelectDog();
					team.Remove(team[0]);
					if (team.Count == 0)
						break;
					//break;
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public void Back(){
		//--set the selected dogs to dog roster
		//empty the roster array
		GameObject.Find ("GameManager").GetComponent<Game_Manager> ().dogRoster.Clear();
		Debug.Log (availableDogs.Count);
		//populate the roster with selected dogs
		for(int i=0; i < availableDogs.Count; i++){
			if (buttons [i].GetComponent<Roster_Button_Script> ().chosen && i < 14) {
				GameObject.Find ("GameManager").GetComponent<Game_Manager> ().dogRoster.Add (availableDogs[i]);
			}
		}

		// CHANGE THIS TO ToMapScene() in GameManager
		GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ToMapScene(3);
	}

	public void ToSummon() {
		GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ToSummonScene();
		//populate the roster with selected dogs
		for(int i=0; i < availableDogs.Count; i++){
			if (buttons [i].GetComponent<Roster_Button_Script> ().chosen && i < 14) {
				GameObject.Find ("GameManager").GetComponent<Game_Manager> ().dogRoster.Add (availableDogs[i]);
			}
		}
		ss.UnloadScene (3);
	}

	//on click
	public void SelectDog(){
		//if not selected
		//add to GameManager-> dogRoster
		//else
		//find same dog in the roster and remove it
	}

}
