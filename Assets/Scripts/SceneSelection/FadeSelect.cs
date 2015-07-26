using UnityEngine;
using System.Collections;

public class FadeSelect : MonoBehaviour {
	private float incrementoOut = 0.7f;
	private float incrementoIn = 0.25f;
	private Color colorA = Color.black;
	private bool isStart = true;

	void Start () {
		colorA.a = 1.0f;
	}

	void Update () {
		if(isStart){
			colorA.a = colorA.a - incrementoIn * Time.deltaTime;
			this.renderer.material.color = colorA;
			if(colorA.a < 0.02f){
				colorA.a = 0.0f;
				isStart = false;
			}
		}

		if(GuiSS.fading){
			this.gameObject.renderer.enabled = true;
			colorA.a = colorA.a + incrementoOut * Time.deltaTime;
			this.renderer.material.color = colorA;
		}
	}
}
