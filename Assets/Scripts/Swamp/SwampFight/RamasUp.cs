using UnityEngine;
using System.Collections;

public class RamasUp : MonoBehaviour {
	private Animator anim;
	//tiempo para Open Anim
	private float timeAnim = 0.0f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.Play("ramasUp");
	}
	
	// Update is called once per frame
	void Update () {
		anim.Play(VerificaAnim());
	}

	private string VerificaAnim(){
		if(timeAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
			return "ramasClosed";
		}else{
			timeAnim = timeAnim +0.022f;
			return "ramasUp";
		}
	}
}
