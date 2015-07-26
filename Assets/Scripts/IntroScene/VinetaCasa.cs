using UnityEngine;
using System.Collections;

public class VinetaCasa : MonoBehaviour {
	private float aumentoXY = 0.002f;
	private float aumentoY = 0.01f;

	void Update () {
		if(GuiIntro.posicionCam == 2 && GuiIntro.mostrarTexto && transform.localScale.x < 1.8f){
		transform.localScale = new Vector3(transform.localScale.x + aumentoXY,
		                                   transform.localScale.y + aumentoXY,0);
			transform.position = new Vector3(transform.position.x,
			                                 transform.position.y - aumentoY,
			                                 transform.position.z);
		}
	}
}
