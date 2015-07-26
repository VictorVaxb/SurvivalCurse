using UnityEngine;
using System.Collections;

public class fadeDoor : MonoBehaviour {
	private float incremento = 0.2f;
	private Color colorA = Color.black;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("doorInOpen") == 0){
			colorA.a = 1.0f;
		}else{
			colorA.a = 0.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("doorInOpen") == 1) {
			colorA.a = colorA.a - incremento * Time.deltaTime;
			this.renderer.material.color = colorA;
		}else{
			this.renderer.material.color = colorA;
		}
	}
}
