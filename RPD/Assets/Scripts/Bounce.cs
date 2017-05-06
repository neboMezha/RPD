using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {
	public float frequency = 10.0f;
	public float magnitude = 0.5f;
	private float originalY;
	// Use this for initialization
	void Start () {
		originalY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, originalY + Mathf.Sin (Time.time * frequency) * magnitude, transform.position.z);
	}
}
