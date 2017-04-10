using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Team_Script : MonoBehaviour {
	public GameObject selectedDog;
	private bool choosingDog;
	public int slot;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Back(){
		GameObject.Find ("SceneManager").GetComponent<Scene_Script> ().UnloadScene (3);
	}


}
