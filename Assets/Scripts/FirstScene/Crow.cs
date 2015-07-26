using UnityEngine;
using System.Collections;

public class Crow : MonoBehaviour {
	Animations animations;
	private Animator anim;					// Reference to the player's animator component.
	private bool facingRight = true;
	private bool isFlying = false;
	private float speedX = 0.4f;
	private float speedY = 0.01f;

	void Start () {
		anim = GetComponent<Animator>();
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
	}
	
	// Update is called once per frame
	void Update () {
		if(animations.objeto == "crow"){
			isFlying = true;
			audio.Play();
			animations.objeto = "";
		}
		if(isFlying){
			if(facingRight){
				Flip();
			}
			this.transform.position = new Vector3(this.transform.position.x + speedX, 
			                                      this.transform.position.y + speedY,
			                                      this.transform.position.z);
			anim.Play("crowFlying");
		}
		if(this.transform.position.x > 100){
			Destroy(this.gameObject);
		}
	}

	void Flip(){
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		facingRight = false;
	}
}
