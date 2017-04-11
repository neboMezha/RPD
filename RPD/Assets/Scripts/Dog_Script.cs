using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Script : MonoBehaviour {
	public bool attacking;
	public bool koed = false;
	public bool unlocked = true;
	public int hp;
	public int atk;
	public double coolDown;
	public int aggro;
	// Use this for initialization
	void Start () {
		koed = false;
		attacking = false;
		hp = 10;

		//-------------SETTING UP DOG STATS
		if (this.name == "Shinobi Inu") {
			hp = 1;
			coolDown = 1.5;
			aggro = 1;
			atk = 2;
		}
		else if (this.name == "Labracadabrador") {
			hp = 1;
			coolDown = 3;
			aggro = 3;
			atk = 4;
		}
		else if (this.name == "Saint Bernard") {
			hp = 1;
			coolDown = 5;
			aggro = 2;
			atk = 1;
		}
		else {
				hp = 3;
				coolDown = 6;
				aggro = 10;
				atk = 3;
			}
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
			//Debug.Log (this.name + " attacked"); //uses the Object's name in the heirarchy as the name
			GameObject.Find ("BattleManager").GetComponent<Battle_Script> ().catHP -= atk;
			Debug.Log ("Cat HP: " + GameObject.Find ("BattleManager").GetComponent<Battle_Script> ().catHP);
		} 
		else {																								//ELSE IN TEAM BUILDER
			GameObject.Find ("TeamManager").GetComponent<Team_Script> ().selectedDog = this.gameObject;
			int slot = GameObject.Find ("TeamManager").GetComponent<Team_Script> ().slot;
			Debug.Log (this.name + " Chosen for Slot " + slot);

		}
	}

	public void TakeDamage(){
		hp -= 1;

		Debug.Log (this.name + "'s HP = " + hp); //uses the Object's name in the heirarchy as the name

		if (hp <= 0) {
			//Debug.Log (this.name + " died"); //uses the Object's name in the heirarchy as the name
			koed = true;

			GameObject.Find("BattleManager").GetComponent<Battle_Script>().knockedOut++;
		}
	}
}
