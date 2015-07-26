using UnityEngine;
using System.Collections;

//ESTE SCRIPT CAMBIA EL MATERIAL DE LA VELA UNA VEZ QUE SE PRENDE

public class CandleItemMat : MonoBehaviour {

	public Material matLuz;
	public Material matDark;

	private Animator anim;	

	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update () {
		if(PlayerPrefs.GetInt("velaPrendida") > 0){
			transform.renderer.material = matLuz;
			anim.Play("candleOn");
		}else{
			transform.renderer.material = matDark;
			anim.Play("candleIdle");
		}
	}
}
