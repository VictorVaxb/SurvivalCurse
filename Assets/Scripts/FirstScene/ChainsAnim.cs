using UnityEngine;
using System.Collections;

public class ChainsAnim : MonoBehaviour {

	private Animator anim;
	Animations animations;
	//var que da el limite de la animacion
	private float timeAnim = 0.0f;
	private bool changeName = false;
	//para que no se repita audio
	private bool canSound = true;

	void Awake(){
		anim = GetComponent<Animator>();
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
	}

	void Update () {
		if(PlayerPrefs.GetInt("chainsCutted") == 0){
			if(animations.objeto == "chains"){
				anim.Play(VerificaAnim());
			}else{
				anim.Play("chainsClosed");
			}
		}else{
			anim.Play("chainsCuted");
		}
	}

	private string VerificaAnim(){
		if(timeAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
			PlayerPrefs.SetInt("chainsCutted",1);
			if(!changeName){
				this.gameObject.name = "SceneSelection";
				this.gameObject.tag = "InOut";
			}
			return "chainsCuted";
		}else{
			//Sonido de cadenas
			if(canSound){
				audio.Play();
				canSound = false;
			}
			timeAnim = timeAnim + 0.022f;
			return "chainsCut";
		}
	}
}
