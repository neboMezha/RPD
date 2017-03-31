using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Script : MonoBehaviour {
	public GameObject d1;

	// Use this for initialization
	void Start () {
		//d1.GetComponent<Button> ().onclick.AddListener (Attack);
		GameObject dog = Instantiate(d1, new Vector2(-50, 100), Quaternion.identity);	 		// instantiates teh prefab
		dog.GetComponent("Dog_Script").name = "God Dog";										// sets the name to the dog object
		dog.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);	// adds teh dog as child of teh canvas

		GameObject dog2 = Instantiate(d1, new Vector2(-50, 0), Quaternion.identity);	 		// instantiates teh prefab
		dog2.GetComponent("Dog_Script").name = "Dog Adam";										// sets the name to the dog object
		dog2.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);	// adds teh dog as child of teh canvas

		GameObject dog3 = Instantiate(d1, new Vector2(-50, -100), Quaternion.identity);	 		// instantiates teh prefab
		dog3.GetComponent("Dog_Script").name = "Dog Eve";										// sets the name to the dog object
		dog3.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);	// adds teh dog as child of teh canvas

	}

	// Update is called once per frame
	void Update () {
		
	}
}
