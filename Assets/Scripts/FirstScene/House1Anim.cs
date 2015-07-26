using UnityEngine;
using System.Collections;

public class House1Anim : MonoBehaviour {
	private Animator anim;
	private int canSound = 0;
	
	//Variable para determinar el tiempo de duracion de animacion puerta house
	public float timeDoorAnim = 0.0f; 

	//Para verificar cuando se debe animar la puerta
	Animations animations;
	
	void Start () {
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
		anim = GetComponent<Animator>();
		anim.enabled = false;
	}
	
	void Update () {
		if(PlayerPrefs.GetInt("canEnterHouse")==0){
			if(animations.objeto == "House1"){
				anim.enabled = true;
				if(canSound == 0){
					audio.Play();
					canSound = 1;
				}
				anim.Play("doorHouseOpen");
				AnimaPuerta();
			}else{
				anim.enabled = false;
			}
		}else{
			anim.enabled = true;
			anim.Play("doorHouseClosed");
		}
	}

	void AnimaPuerta(){
		if(timeDoorAnim >= 1.0f){
			timeDoorAnim = 0.0f;
			anim.Play("doorHouseClosed");
			animations.objeto = string.Empty;
			PlayerPrefs.SetInt("canEnterHouse", 1);
		}else{
			timeDoorAnim = timeDoorAnim +0.04f;
		}
	}
}
