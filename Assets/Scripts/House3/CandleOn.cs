using UnityEngine;
using System.Collections;

public class CandleOn : MonoBehaviour {
	private Animator anim;					// Reference to the animator component.
	
	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update () {
		anim.Play("candleOn");
	}
}
