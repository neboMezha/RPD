using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScript : MonoBehaviour {
	public GameObject helpBoxesObj;
	public int numberOfHelpBoxes;
	private int currentBoxIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NextBox(){
		if (currentBoxIndex < numberOfHelpBoxes - 1) {
			currentBoxIndex++;
			helpBoxesObj.transform.localPosition -= new Vector3 (312, 0, 0);
		}
		Debug.Log ("current box index = " + currentBoxIndex);

	}
}
