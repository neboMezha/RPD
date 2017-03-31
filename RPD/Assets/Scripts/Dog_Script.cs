using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Attack(){
		Debug.Log (this.name + " attacked"); //uses the Object's name in the heirarchy as the name
	}
}
