using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Team_Script : MonoBehaviour {
	public GameObject[] doggos;
	public GameObject selectedDog;
	private bool choosingDog;
	public int slot;
	// Use this for initialization
	void Start () {
		choosingDog = false;
		doggos = GameObject.Find ("GameManager").GetComponent<Game_Manager> ().allDogs;
		int spacer = 1;
		int num = 0;
		for (int i = 0; i < doggos.Length; i++) {
			if (num > 1) {
				spacer++;
				num = 0;
			}
			if (i % 2 == 0) {

				doggos [i] = Instantiate (doggos [i], new Vector2 (-50, 150 - (spacer * 65)), Quaternion.identity);	
				doggos[i].GetComponent("Dog_Script").name = i.ToString();
			} else {
				doggos[i] = Instantiate(doggos[i], new Vector2(50, 150-(spacer * 65)), Quaternion.identity);
				doggos[i].GetComponent("Dog_Script").name = i.ToString();	
			}
			Debug.Log (doggos[i] + " instantiated");												// sets the name to the dog object
			doggos[i].transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
//			doggos [i].GetComponent<Button> ().onclick.AddListener (SelectDog);
			num++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Back(){
		GameObject.Find ("SceneManager").GetComponent<Scene_Script> ().UnloadScene (3);
	}

	public void SelectSlot(){
		string name = EventSystem.current.currentSelectedGameObject.name;
		slot = int.Parse(name);
		choosingDog = true;
		Debug.Log ("SLOT " + slot +" CHOSEN");
	}

}
