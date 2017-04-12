using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class Team_Script : MonoBehaviour {
	public GameObject selectedDog;
	private bool choosingDog;
	public int slot;

	// AUDIO //
	AudioSource audio;
	public AudioClip selectionSound;
	public AudioClip cancelSound;


	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Back(){
		audio.PlayOneShot (cancelSound);
		GameObject.Find ("SceneManager").GetComponent<Scene_Script> ().UnloadScene (3);
	}


}
