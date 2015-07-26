using UnityEngine;
using System.Collections;

public class Peter : MonoBehaviour {
	//tiempo para el daño
	private float timePointAnim = 0.0f;
	private Animator anim;					// Reference to the player's animator component.
	Animations animations;

	void Start () {
		anim = GetComponent<Animator>();
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
	}
	
	// Update is called once per frame
	void Update () {
		if (animations.objeto == "askPeter") {
			anim.Play (VerificaAnim ());
		}
	}

	string VerificaAnim(){
		if(timePointAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
			timePointAnim = 100.0f;
			animations.objeto = "enemy1";
			//Desabilitar renderer
			this.transform.renderer.enabled = false;
			this.gameObject.name = "nonamed";
			this.gameObject.tag = "Untagged";
			//Destroy(this.gameObject);
			return "pointingAnim";
		}else{
			timePointAnim = timePointAnim + 0.015f;
			return "pointAnim";
		}
	}
}
