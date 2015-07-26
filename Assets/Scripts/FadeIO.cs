using UnityEngine;
using System.Collections;

public class FadeIO : MonoBehaviour {
	private float incrementoOut = 0.7f;
	private float incrementoIn = 0.2f;
	private Color colorA = Color.black;
	private bool isStart = true;

	PlayerMove playerMove;
	DieMenu dieMenu;

	void Start () {
		playerMove = (PlayerMove)GameObject.FindObjectOfType(typeof(PlayerMove));
		dieMenu = (DieMenu)GameObject.FindObjectOfType(typeof(DieMenu));
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
		if(playerMove.sceneToGo != "" || playerMove.lifePlayer <= 0){
			this.gameObject.renderer.enabled = true;
			colorA.a = colorA.a + incrementoOut * Time.deltaTime;
			this.renderer.material.color = colorA;
			if(playerMove.lifePlayer <= 0 && colorA.a >= 0.99f){
				DieMenu.showMenuDie = true;
			}
		}
	}
}
