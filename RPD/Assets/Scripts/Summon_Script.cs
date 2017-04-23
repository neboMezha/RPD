using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_Script : MonoBehaviour {

	public Scene_Script ss;
	public GameObject backB;
	// Use this for initialization
	void Start () {
		ss = GameObject.Find ("SceneManager").GetComponent<Scene_Script>();
		//backB = GameObject.Find ("Back");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Summon(){
		int rand = Random.Range (0, 100);
		int num = 0;
		if (rand < 75) {
			num = Random.Range (0, 4);
		}
		else {
			num = Random.Range (0, 4); //eventually will be 4,8
		}

		GameObject dog = GameObject.Find("GameManager").GetComponent<Game_Manager>().allDogs[num];
		GameObject.Find ("GameManager").GetComponent<Game_Manager> ().owned.Add (dog);

		ss.AddScene (5);
	}

	public void Back(){

		GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ChangeState ("map");
		GameObject.Find ("SceneManager").GetComponent<Scene_Script> ().UnloadScene (4);
	}
}
