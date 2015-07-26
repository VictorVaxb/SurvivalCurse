using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
	Animations animations;
	Boss2 boss2;
	public AudioClip musicaPelea;
	private GameObject musica;

	void Start(){
		musica = GameObject.FindGameObjectsWithTag("Music")[0];
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
		boss2 = (Boss2)GameObject.FindObjectOfType(typeof(Boss2));
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.gameObject.name == "Player"){
			musica.audio.clip = musicaPelea;
			musica.audio.Play();
			animations.objeto = "ramasUp";
			boss2.ActivarBoss2();
			Destroy (this.gameObject);
		}
	}

	/*
	void OnTriggerExit2D(Collider2D coll) {
		Debug.Log ("tristay");
	}
	*/
}
