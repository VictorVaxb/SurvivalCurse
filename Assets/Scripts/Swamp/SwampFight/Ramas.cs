using UnityEngine;
using System.Collections;

public class Ramas : MonoBehaviour {
	Animations animations;
	PlayerMove playerMove;
	private Animator anim;					// Reference to the player's animator component.
	private bool destroyObj = false;
	private bool cansound = true;

	//tiempo para Open Anim
	private float timeOpenAnim = 0.0f;

	void Start () {
		anim = GetComponent<Animator>();
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
		playerMove = (PlayerMove)GameObject.FindObjectOfType(typeof(PlayerMove));
		anim.Play("ramasIdle");
	}

	void Update () {
		if(animations.objeto == "ramas"){
			if(cansound){
				audio.Play();
				cansound = false;
			}
			anim.Play(VerificaAnim());
		}
		if(destroyObj){
			playerMove.LimpiarVariables();
			Destroy(this.gameObject);
		}
	}

	private string VerificaAnim(){
		if(timeOpenAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
			timeOpenAnim = 0.0f;
			animations.objeto = "";
			this.gameObject.name = "nonamed";
			this.gameObject.tag = "Untagged";
			//PlayerPrefs.SetInt("armarioOpen",1);
			destroyObj = true;
			return "ramasNothing";
		}else{
			timeOpenAnim = timeOpenAnim +0.022f;
			return "ramasCut";
		}
	}
}
