using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roster_Button_Script : MonoBehaviour {
	public bool chosen = false;
	private ColorBlock col;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectDog(){
		chosen = !chosen;
		if (chosen) {
			//insert this button's component(dog) to the roster
			Debug.Log("Chose ");
			col = GetComponent <Button> ().colors;
			col.normalColor = Color.green;
			col.highlightedColor = Color.green;
			GetComponent <Button> ().colors = col;
		} 
		else {
			col = GetComponent <Button> ().colors;
			col.highlightedColor = Color.white;
			col.normalColor = Color.white;
			GetComponent <Button> ().colors = col;
			//remove this button's component(dog) from the roseter
		}
	}
}
