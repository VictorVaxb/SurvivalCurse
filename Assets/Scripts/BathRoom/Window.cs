using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour {
	private Animator anim;	
	private float timeAnim = 0.0f;
	public Transform sonidoLizard;
	private bool isAudio = false;

	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update () {
		VerificaAnimacion();
		IntstanciaAudioLizard();
	}

	void IntstanciaAudioLizard(){
		if(!isAudio && PlayerPrefs.GetInt("bulletBathroomPicked")==1){
			Instantiate(sonidoLizard, this.transform.position, Quaternion.identity);
			isAudio = true;
		}
	}

	void VerificaAnimacion(){
		if(PlayerPrefs.GetInt("bulletBathroomPicked")==0){
			anim.Play("ventanaOk");
		}else if(PlayerPrefs.GetInt("bulletBathroomPicked")==1){
			anim.Play(AnimLizard());
		}else{
			anim.Play("ventanaOk");
		}
	}

	private string AnimLizard(){
		if(timeAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
			timeAnim = 0.0f;
			PlayerPrefs.SetInt("bulletBathroomPicked",2);
			return "ventanaOk";
		}else{
			timeAnim = timeAnim + 0.02f;
			return "ventanaLizard";
		}
	}
}
