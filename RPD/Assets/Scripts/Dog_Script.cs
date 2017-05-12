using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Dog_Script : MonoBehaviour {
	public bool attacking;
	public bool takingDamage;
	public bool koed = false;
	public bool unlocked = true;
	public int hp;
	public int atk;
	public double coolDown;
	public int aggro;

	private float timer;
	private bool able; //dog is able to attack

	private Vector3 posRight;
	private Vector3 posLeft;
	private Vector3 posOr;
	private float mover = 0.1f;

	// AUDIO //
	new AudioSource audio;
	public AudioClip attackSound;
	public AudioClip damageSound;
	public AudioClip readySound;

	// Use this for initialization
	void Start () {
		able = true;
		timer = 0;

		//posRight = new Vector3 (-0.43f, 1.729f, -0.008f);
		posRight = this.transform.position + new Vector3 (0.7884f, 0, 0);
		//posLeft = new Vector3 (-1.68f, -0.617f, -0.008f);
		posLeft = this.transform.position - new Vector3 (0.7884f, 0, 0);
		posOr = transform.position;
		mover = 0.1f;

		koed = false;
		attacking = false;
		takingDamage = false;
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
		//MAdE RARES HAVE X2 HP AND ATK
		else if (this.name == "shiba_R") {
			hp = 2;
			coolDown = 1.5;
			aggro = 1;
			atk = 4;
		}
		else if (this.name == "labra_R") {
			hp = 2;
			coolDown = 3;
			aggro = 3;
			atk = 8;
		}
		else if (this.name == "chihua_R"){
			hp = 6;
			coolDown = 6;
			aggro = 10;
			atk = 6;
		}
	}

	// Update is called once per frame
	//--GLITCHY
	void Update () {
		timer += Time.deltaTime;

		if (timer >= this.coolDown) {
			timer = 0;
			able = true;
			this.GetComponent<SpriteRenderer> ().color = Color.white;
		}

		//--Attack Animation
		if (attacking) {
			//after 14 frames, turn off attacking
			if (this.transform.position.x >= posRight.x) {
				attacking = false;
			} else {
				//Debug.Log (mover);
				this.transform.position = new Vector3 (this.transform.position.x + mover, this.transform.position.y, this.transform.position.z);
			} 
		} 
		else {
			if (this.transform.position.x > posOr.x)
				this.transform.position = new Vector3 (this.transform.position.x - mover, this.transform.position.y, this.transform.position.z);
		}

		//--Taking Damage Animation
		if(takingDamage){
			if (this.transform.position.x <= posLeft.x) {
				takingDamage = false;
			} else {
				//Debug.Log (mover);
				this.transform.position = new Vector3 (this.transform.position.x - mover, this.transform.position.y, this.transform.position.z);
			} 
		}
		else {
			if (this.transform.position.x < posOr.x)
				this.transform.position = new Vector3 (this.transform.position.x + mover, this.transform.position.y, this.transform.position.z);
		}
	

	}

	public void Attack(){			//IF IN BATTLE
		if (able) {
			attacking = true;
			able = false;
		}
		if(!able) {
			this.GetComponent<SpriteRenderer> ().color = Color.gray;
		}
		if (attacking) {
			if (this.name == "Saint Bernard") {
				Battle_Script gms = GameObject.Find ("BattleManager").GetComponent<Battle_Script> ();
				int rand = Random.Range (0, gms.dogs.Length);
				//gms.dogRoster [0].GetComponent<Dog_Script> ().Heal ();
				gms.dogs [rand].GetComponent<Dog_Script> ().hp += 1;
				Debug.Log ("Healed " + gms.dogs [rand].name);

			} else {
				GameObject.Find ("BattleManager").GetComponent<Battle_Script> ().catHP -= atk;
				Debug.Log ("Cat HP: " + GameObject.Find ("BattleManager").GetComponent<Battle_Script> ().catHP);
			}
		}
	}

	public void Heal(){
		hp += 1;
	}

	public void TakeDamage(){
		takingDamage = true;

		//----
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
