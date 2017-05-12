using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summon_Script : MonoBehaviour {

	Scene_Script ss;
	public GameObject backB;
	public GameObject summonB;
	private int bonesOwned;
	// Use this for initialization
	void Start () {
		ss = GameObject.Find ("SceneManager").GetComponent<Scene_Script>();
		bonesOwned = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().bones;
		//backB = GameObject.Find ("Back");
		if (bonesOwned < 5) {
			summonB.SetActive (false);
		} else {
			summonB.SetActive (true);
		}

		GameObject.Find ("Currency").GetComponent<Text> ().text = "Bones: " + bonesOwned;
	}

	// Update is called once per frame
	void Update () {
	}

	public void Summon(){
		int bonesOwned = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().bones;

		if(bonesOwned >= 5){
			int rand = Random.Range (0, 100);
			int num = 0;
			if (rand < 85) {
				num = Random.Range (0, 4);
			}
			else {
				num = Random.Range (4, 7); //eventually will be 4,8
			}

			Debug.Log (num);

			GameObject dog = GameObject.Find("GameManager").GetComponent<Game_Manager>().allDogs[num];
			GameObject.Find ("GameManager").GetComponent<Game_Manager> ().owned.Add (dog);

			//take out the 5 bones for summoning
			GameObject.Find ("GameManager").GetComponent<Game_Manager> ().bones -= 5;
			bonesOwned -= 5;
			//disable the summon button if not enough $$$ left


			GameObject.Find ("Currency").GetComponent<Text> ().text = "Bones: " + bonesOwned;

			ss.AddScene (5);
		}

		if (bonesOwned < 5) {
			summonB.SetActive (false);
			summonB.GetComponent<Image> ().enabled = false;
		} else {
			summonB.SetActive (true);
			summonB.GetComponent<Image> ().enabled = true;
		}
	}

	public void Back(){
		//GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ChangeState ("map");
		//GameObject.Find ("SceneManager").GetComponent<Scene_Script> ().UnloadScene (4);
		GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ToMapScene(4);
	}
}
