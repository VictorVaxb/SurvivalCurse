using UnityEngine;
using System.Collections;

public class PeterIntro : MonoBehaviour {
	private float incrementoIn = 0.1f;
	private Color colorA = Color.black;

	void Start () {
		colorA.a = 0.0f;
		this.renderer.material.color = colorA;
	}

	void Update () {
		if(GuiIntro.posicionCam == 3 && GuiIntro.mostrarTexto && colorA.a < 0.55f){
			this.gameObject.renderer.enabled = true;
			colorA.a = colorA.a + incrementoIn * Time.deltaTime;
			this.renderer.material.color = colorA;
		}
	}
}
