﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {
	string catName;
	int maxHP;				// max HP value
	int hp;					// current HP
	int attackRange;		// Int: num of units attackable
	float attackRate;		// Float: wait time for attack
	Sprite image;			// sprite prefab

	public Cat(string name, int mh, int range, float rate, Sprite sprt){
		catName = name;
		maxHP = mh;
		hp = mh;
		attackRange = range;
		attackRate = rate;
		image = sprt;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public string CatName {
		get { return catName; }
	}
	public int MaxHp {
		get { return maxHP; }
	}
	public int Hp {
		get { return hp; }
	}
	public int AttackRange {
		get { return attackRange; }
	}
	public float AttackRate {
		get { return attackRate; }
	}
}
