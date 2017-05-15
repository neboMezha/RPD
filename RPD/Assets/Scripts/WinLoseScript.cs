using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class WinLoseScript : MonoBehaviour {
	public GameObject[] dogPositions = new GameObject[3];
	public GameObject numBonesTextObj;
	private int numBoneReward;	// for now just 1 per battle

	public GameObject winObjs;		// groups for winlose scene
	public GameObject loseObjs;

	new AudioSource audio;
	public AudioClip winBGM;
	public AudioClip loseBGM;


	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();

		// show dogs
		for (int i = 0; i < dogPositions.Length; i++) {
			dogPositions[i] = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().dogRoster [i];
		}

		// load stuff according to game state win or lose
		if (GameObject.Find("GameManager").GetComponent<Game_Manager>().GetWinLoseState() == 1) {	// WIN
			audio.clip = winBGM;
			audio.Play();
			numBoneReward = Random.Range(3, 7);
			GameObject.Find ("GameManager").GetComponent<Game_Manager> ().bones += numBoneReward;

			// set and show bone reward number
			numBonesTextObj.GetComponent<Text>().text = numBoneReward.ToString();

			winObjs.SetActive(true);
			loseObjs.SetActive (false);
		}
		else if (GameObject.Find("GameManager").GetComponent<Game_Manager>().GetWinLoseState() == 2) {	// LOSE
			audio.clip = loseBGM;
			audio.Play();
			numBoneReward = 2;
			GameObject.Find ("GameManager").GetComponent<Game_Manager>().bones += numBoneReward;

			// set and show bone reward number
			numBonesTextObj.GetComponent<Text>().text = numBoneReward.ToString();

			winObjs.SetActive(false);
			loseObjs.SetActive (true);
		}



	


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Back(){
		audio.Stop ();
		GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ToMapScene(7);
		GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ToMapScene(2);

	}
}
