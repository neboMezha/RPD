using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Map Button Class: Represents a button on the map that leads to a battle stage.
/// </summary>
/// 
public class MapButton : MonoBehaviour {
	public bool locked;
	bool focused;				// bool to check if tapped once, "hovered", on first click, info bubble will display. on second click WHILE focused, will go to stage.
	GameObject infoBubble;		// child of button object, pop up info bubble
	GameObject infoBubbleShadow;
	public int target;				// Integer ID of cooresponding stage

	GameObject[] infoBubbles;		//AHHH THIS IS TERRIBLE DONT DO THIS IN EVERY MAP BUTTON @self
	GameObject[] allMapButtons;		// this too stahp

	public bool Focused {
		get { return focused; }
		set { focused = value; }
	}


	// Use this for initialization
	void Start () {
		focused = false;

		// Get child bubble stuff
		infoBubble = this.gameObject.transform.GetChild(1).gameObject;
		infoBubbleShadow = this.gameObject.transform.GetChild(1).GetChild(0).gameObject;

		// Make Info Bubble and silhouette invisible at first
		infoBubble.SetActive(false);
		//infoBubbleShadow.GetComponent<SpriteRenderer>().enabled = false;


		infoBubbles = GameObject.FindGameObjectsWithTag("InfoBubble"); // ew why would I do this here, much bette rin like GM
		allMapButtons = GameObject.FindGameObjectsWithTag("MapButton");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	/// <summary>
	/// Called when button is tapped
	/// </summary>
	public void Clicked() {						///DEBUG: 1st/2nd click not working, goes traight ToBattleTest()on first click
		Debug.Log ("Tapped obj: " + this.name);

		/*
		foreach (GameObject g in allMapButtons) {
			//make focused in MapButton component of each FALSE
			g.GetComponent<MapButton>().Focused = false;
		}
		// close all other info bubbles				still ew
		foreach (GameObject g in infoBubbles) {
			g.SetActive (false);
		} // theyre currently not going UNFOCUSED when clicked off of
		*/
		if (!focused) {	// first tap
			focused = true;	// focus this button

			// TODO: perform focus animation and stuff
			infoBubble.SetActive(true);



		}
		else if (focused) {	// second tap

			// temporary
			GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ToBattleTest();

			Debug.Log ("Load battle target ID: " + target);

			// TO DO: go to cooresponding battle scene USE target variable
		}
	}
		
}
