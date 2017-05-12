using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roster_Button_Script : MonoBehaviour {
	public bool chosen = true;
	private ColorBlock col;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectDog(){
		if (GameObject.Find ("TeamManager").GetComponent<Team_Script> ().numSelected < 14) {
			chosen = !chosen;
			if (chosen) {																	//IF CLICK SELECTS IT
				//insert this button's component(dog) to the roster
				Debug.Log ("Chose ");
				col = GetComponent <Button> ().colors;
				col.normalColor = Color.green;
				col.highlightedColor = Color.green;
				GetComponent <Button> ().colors = col;
				GameObject.Find ("TeamManager").GetComponent<Team_Script> ().numSelected++;
			} else {																		//IF CLICK DESELECTS IT
				col = GetComponent <Button> ().colors;
				col.highlightedColor = Color.white;
				col.normalColor = Color.white;
				GetComponent <Button> ().colors = col;
				GameObject.Find ("TeamManager").GetComponent<Team_Script> ().numSelected--;
				//remove this button's component(dog) from the roseter
			} 
		} else if (chosen) {																//DESELECT IF ROSTER ALREADY FULL
			col = GetComponent <Button> ().colors;
			col.highlightedColor = Color.white;
			col.normalColor = Color.white;
			GetComponent <Button> ().colors = col;
			GameObject.Find ("TeamManager").GetComponent<Team_Script> ().numSelected--;
			chosen = !chosen;
			//remove this button's component(dog) from the roseter
		} 
	}
}
