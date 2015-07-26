using UnityEngine;
using System.Collections;

public class InBed : MonoBehaviour {
	private Animator anim;					// Reference to the player's animator component.
	private bool canAnimIdle = true;
	//Tiempo de animacion
	private float timeIdleAnim = 0.0f;
	void Awake(){
		// Setting up references.
		anim = GetComponent<Animator>();
	}

	void Start () {

	}

	void Update () {
		anim.Play(VerificaAnim());
	}

	private string VerificaAnim(){
		if(GuiIntro.posicionCam == 3 && canAnimIdle && GuiIntro.mostrarTexto){
			if(timeIdleAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
				timeIdleAnim = 0.0f;
				canAnimIdle = false;
				return "inBedAnim";
			}else{
				timeIdleAnim = timeIdleAnim +0.01f;
				return "inBedAnim";
			}
		}else if(!canAnimIdle){
			return "inBedEnd";
		}else{
			return "inBedIni";
		}
	}
}
