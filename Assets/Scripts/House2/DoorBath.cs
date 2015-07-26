using UnityEngine;
using System.Collections;

public class DoorBath : MonoBehaviour {
	// Reference to the animator component.
	private Animator anim;					
	//tiempo para Open Anim
	public float timeOpenAnim = 0.0f;
	
	Animations animations;
	Gui gui;
	Inventory inventory;
	PlayerMove playerMove;
	
	void Start () {
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
		gui = (Gui)GameObject.FindObjectOfType(typeof(Gui));
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
		playerMove = (PlayerMove)GameObject.FindObjectOfType(typeof(PlayerMove));
		anim = GetComponent<Animator>();
		anim.Play("doorBathClosed");
	}
	
	void Update () {
		if(!BtnPausa.showMenu && !gui.mostrarTexto && !inventory.showItems){
			if(animations.objeto == "BathRoom"){
				anim.Play(VerificaAnim());
			}
		}
	}

	IEnumerator IrAOtraEscena(string lugar){
		playerMove.sceneToGo = lugar;
		//This is a coroutine
		yield return new WaitForSeconds(1.8f);
		Application.LoadLevel(lugar);
	}

	private string VerificaAnim(){
		if(timeOpenAnim <= anim.GetCurrentAnimatorStateInfo(0).length){
			timeOpenAnim = timeOpenAnim +0.02f;
			return "doorBathOpen";
		}else{
			StartCoroutine(IrAOtraEscena("BathRoom"));
			return "doorBathOpened";
		}
	}
}
