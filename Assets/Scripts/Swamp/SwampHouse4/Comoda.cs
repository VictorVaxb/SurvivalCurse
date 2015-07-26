using UnityEngine;
using System.Collections;

public class Comoda : MonoBehaviour {
	private Animator anim;
	public GameObject axeItem;
	private Vector3 respawnAxe;

	Animations animations;
	PlayerMove playerMove;
	
	private float timeAnim = 0.0f;
	private bool canAnim = false;
	private bool canSound = true;
	
	void Start () {
		PlayerPrefs.SetInt("comodaIsDown",0);
		anim = GetComponent<Animator>();
		if(PlayerPrefs.GetInt("comodaIsDown") == 0){
			anim.Play ("comodaDown");
		}else{
			anim.Play ("comodaOk");
			Destroy(transform.FindChild("shine").gameObject);  
			this.collider2D.enabled = false;
		}
		//PlayerPrefs.SetString("playerInstance","SwampHouse3");
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
		playerMove = (PlayerMove)GameObject.FindObjectOfType(typeof(PlayerMove));
		respawnAxe = transform.FindChild ("spawn").gameObject.transform.position;
	}
	
	void Update () {
		if(animations.objeto == "comoda"){
			PlayerPrefs.SetInt("comodaIsDown",1);
			animations.objeto = "";
			anim.Play ("comodaUp");
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
	
	private string VerificaAnim(){
		if(timeAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
			canAnim = false;
			Instantiate(axeItem, respawnAxe, Quaternion.identity);
			Destroy(transform.FindChild("shine").gameObject);  
			return "comodaOk";
		}else{
			timeAnim += 0.022f;
			return "comodaUp";
		}
	}
}
