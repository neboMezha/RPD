using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Map Button Class: Represents a button on the map that leads to a battle stage.
/// </summary>
/// 
[RequireComponent(typeof(Collider2D))]
public class MapButton : MonoBehaviour {
	public bool locked;
	bool focused = false;			// bool to check if tapped once, "hovered", on first click, info bubble will display. on second click WHILE focused, will go to stage.
	Transform buttonObjectBubble;	// child of button object, pop up info bubble
	int target;						// Integer ID of cooresponding stage

	//new AudioSource Audio;
	//public AudioClip tapSound;
	//public AudioClip selectSound;
	// HANDLE AUDIO IN GM!!!!!!!!!!! so no mulicopies of sound and stuff

	// Use this for initialization
	void Start () {
		buttonObjectBubble = this.gameObject.transform.GetChild(1);	// should be the bubble
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Called when button is tapped
	/// </summary>
	void OnMouseDown() {
		Debug.Log ("Tapped obj: " + this.name);

		if (!focused) {	// first tap
			focused = true;

			// TODO: perform focus animation and stuff
			// make info bubble visible
			//buttonObjectBubble.localPosition.z = -0.3f;

		}
		if (focused) {	// second tap

			// temporary
			GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ToBattleTest();


			// TO DO: go to cooresponding battle scene
		}
	}
		
}
