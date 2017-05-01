using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Examine_Script : MonoBehaviour {
	public GameObject newDog;
	// Use this for initialization
	void Start () {
		//disable the background Back Button form the Summon Page
		GameObject.Find ("SummonManager").GetComponent<Summon_Script>().backB.SetActive(false);
		GameObject.Find ("SummonManager").GetComponent<Summon_Script>().summonB.SetActive(false);


		int num = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().owned.Count-1;
		newDog = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().owned[num];

		GameObject.Find ("placeholder").GetComponent<SpriteRenderer>().sprite  = newDog.GetComponent<SpriteRenderer>().sprite;
		GameObject.Find ("DogName").GetComponent<Text> ().text = newDog.name;

		GameObject.Find ("Hp").GetComponent<Text> ().text = newDog.GetComponent<Dog_Script>().hp.ToString();
		GameObject.Find ("cooldown").GetComponent<Text> ().text = newDog.GetComponent<Dog_Script>().coolDown.ToString();
		GameObject.Find ("dmg").GetComponent<Text> ().text = newDog.GetComponent<Dog_Script>().atk.ToString();
		GameObject.Find ("aggro").GetComponent<Text> ().text = newDog.GetComponent<Dog_Script>().aggro.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Back(){

		GameObject.Find ("SummonManager").GetComponent<Summon_Script>().backB.SetActive(true);
		GameObject.Find ("SummonManager").GetComponent<Summon_Script>().summonB.SetActive(true);
		//GameObject.Find ("Back").GetComponent<Button> ().enabled = false;
		//GameObject.Find ("GameManager").GetComponent<Game_Manager> ().ChangeState ();
		GameObject.Find ("SceneManager").GetComponent<Scene_Script> ().UnloadScene (5);
	}
}
