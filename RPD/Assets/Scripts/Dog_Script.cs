using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Script : MonoBehaviour {
	public bool attacking;
	public bool koed = false;
	public bool unlocked = true;
	public int hp;
	// Use this for initialization
	void Start () {
		koed = false;
		attacking = false;
		hp = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (attacking) {
			for (int i = 0; i <= 30; i++) {
				if (i == 30)
					attacking = false;
			}
		}

	}

	public void Attack(){
		if (GameObject.Find ("GameManager").GetComponent<Game_Manager> ().battling == true) {				//IF IN BATTLE
			attacking = true;
			Debug.Log (this.name + " attacked"); //uses the Object's name in the heirarchy as the name
		} 
		else {																								//ELSE IN TEAM BUILDER
			GameObject.Find ("TeamManager").GetComponent<Team_Script> ().selectedDog = this.gameObject;
			int slot = GameObject.Find ("TeamManager").GetComponent<Team_Script> ().slot;
			Debug.Log (this.name + " Chosen for Slot " + slot);

		}
	}

	public void TakeDamage(){
		hp -= 5;

		Debug.Log (this.name + "'s HP = " + hp); //uses the Object's name in the heirarchy as the name

		if (hp <= 0) {
			Debug.Log (this.name + " died"); //uses the Object's name in the heirarchy as the name
			koed = true;

			GameObject.Find("BattleManager").GetComponent<Battle_Script>().knockedOut++;
		}
	}
}
