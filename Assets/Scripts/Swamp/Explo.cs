using UnityEngine;
using System.Collections;

public class Explo : MonoBehaviour {
	//private float tiempoVida = 0.0f;
	//tiempo para Open Anim
	private float timeAnim = 0.0f;
	private Animator anim;

	void Start(){
		anim = GetComponent<Animator>();
		anim.Play("exploIdle");
	}

	void Update(){
		anim.Play(VerificaAnim ());
	}

	private string VerificaAnim(){
		if(timeAnim >= 0.04f){
			Destroy(this.gameObject);		
			return "exploIdle";
		}else{
			timeAnim = timeAnim +0.001f;
			return "exploIdle";
		}
	}
}
