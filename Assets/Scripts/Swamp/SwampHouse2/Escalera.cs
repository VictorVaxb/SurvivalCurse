using UnityEngine;
using System.Collections;

public class Escalera : MonoBehaviour {
	private Animator anim;

	Animations animations;
	PlayerMove playerMove;

	private float timeAnim = 0.0f;
	private bool canAnim = false;
	private bool canSound = true;

	void Start () {
		anim = GetComponent<Animator>();
		if(PlayerPrefs.GetString("escaleraOpen") == "closed"){
			anim.Play ("escaleraClosed");
		}else{
			anim.Play ("escaleraOpened");
			HabilitarSubida();
			this.collider2D.enabled = false;
		}
		//PlayerPrefs.SetString("playerInstance","SwampHouse3");
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
		playerMove = (PlayerMove)GameObject.FindObjectOfType(typeof(PlayerMove));
	}

	void Update () {
		if(animations.objeto == "escalera"){
			PlayerPrefs.SetString("escaleraOpen","opened");
			animations.objeto = "";
			anim.Play ("escaleraOpen");
			this.collider2D.enabled = false;
			playerMove.LimpiarVariables();
			canAnim = true;
		}
		if(canAnim){
			if(canSound){
				audio.Play();
				canSound = false;
			}
			anim.Play(VerificaAnim());
		}
	}

	private void HabilitarSubida(){
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("InOut")) {
			if(go.name == "SwampHouse3"){
				go.transform.collider2D.enabled = true;
			}
		}
	}

	private string VerificaAnim(){
		if(timeAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
			HabilitarSubida();
			canAnim = false;
			return "escaleraOpened";
		}else{
			timeAnim += 0.022f;
			return "escaleraOpen";
		}
	}
}
