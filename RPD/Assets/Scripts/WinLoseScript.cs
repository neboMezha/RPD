using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinLoseScript : MonoBehaviour {
	public GameObject[] dogPositions = new GameObject[3];
	public GameObject numBonesTextObj;
	private int numBoneReward = 1;	// for now just 1 per battle

	// Use this for initialization
	void Start () {
		// show dogs
		for (int i = 0; i < dogPositions.Length; i++) {
			dogPositions[i] = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().dogRoster [i];
		}

		// set and show bone reward number
		numBonesTextObj.GetComponent<Text>().text = numBoneReward.ToString();


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Back(){
		GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ToMapScene(7);
	}
}
