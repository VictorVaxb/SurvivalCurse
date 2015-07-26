using UnityEngine;
using System.Collections;

public class DestroyAnim : MonoBehaviour {
	private float timeAnim = 0.0f;
	public float incremento = 0.02f;
	private Animator anim;	

	void Start() {
		anim = GetComponent<Animator>();
	}

	void Update() {
		if(timeAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
			Destroy(this.gameObject);
		}else{
			timeAnim += incremento;
		}
	}
}
