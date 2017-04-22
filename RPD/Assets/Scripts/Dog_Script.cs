using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Dog_Script : MonoBehaviour {
	public bool attacking;
	public bool koed = false;
	public bool unlocked = true;
	public int hp;
	public int atk;
	public double coolDown;
	public int aggro;

	private int timer2;
	private Vector3 pos;
	private Vector3 posOr;
	private float mover;

	// AUDIO //
	new AudioSource audio;
	public AudioClip attackSound;
	public AudioClip damageSound;
	public AudioClip readySound;

	// Use this for initialization
	void Start () {
		pos = transform.position;
		posOr = transform.position;
		timer2 = 0;
		mover = 0;

		koed = false;
		attacking = false;
		//hp = 10;

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
		else if (this.name == "Chihuawarrior"){
			hp = 3;
			coolDown = 6;
			aggro = 10;
			atk = 3;
		}
	}

	// Update is called once per frame
	//--GLITCHY
	void Update () {
		//--Attack Animation
		if (attacking) {
			//after 14 frames, turn off attacking
			if (timer2 == 14) {
				attacking = false;
				timer2 = 0;
			}
			//move forward
			if (timer2 < 7) {
				transform.position = new Vector3 (pos.x + mover, pos.y, pos.z);
			} 
			//at midpoint, set the surent position as pos
			else if (timer2 == 7) {
				pos = transform.position;
				mover = 0;
			} 
			//move backwards
			else {
				transform.position = new Vector3 (pos.x - mover, pos.y, pos.z);
			}

			mover += 0.1f;
			timer2++;

		} 
		//if not attacking, set everything to original pos
		else {
			pos = posOr;
			transform.position = posOr;
		}
	}

	public void Attack(){			//IF IN BATTLE
		attacking = true;
		if (this.name == "Saint Bernard") {
			Battle_Script gms = GameObject.Find ("BattleManager").GetComponent<Battle_Script> ();
			int rand = Random.Range (0, gms.dogs.Length);
			//gms.dogRoster [0].GetComponent<Dog_Script> ().Heal ();
			gms.dogs [rand].GetComponent<Dog_Script>().hp += 1;
			Debug.Log ("Healed " + gms.dogs [rand].name);

		}
		//attacking = true;
		//Debug.Log (this.name + " attacked"); //uses the Object's name in the heirarchy as the name
		else {
			GameObject.Find ("BattleManager").GetComponent<Battle_Script> ().catHP -= atk;
			Debug.Log ("Cat HP: " + GameObject.Find ("BattleManager").GetComponent<Battle_Script> ().catHP);
		}
	}

	public void Heal(){
		hp += 1;
	}

	public void TakeDamage(){
		hp -= 1;

		Debug.Log (this.name + "'s HP = " + hp); //uses the Object's name in the heirarchy as the name

		if (hp <= 0) {
			//Debug.Log (this.name + " died"); //uses the Object's name in the heirarchy as the name
			koed = true;

			GameObject.Find("BattleManager").GetComponent<Battle_Script>().knockedOut++;
			Debug.Log (GameObject.Find("BattleManager").GetComponent<Battle_Script>().knockedOut);
		}
	}
}
