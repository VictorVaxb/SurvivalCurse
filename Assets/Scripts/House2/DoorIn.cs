using UnityEngine;
using System.Collections;

public class DoorIn : MonoBehaviour {
	// Reference to the animator component.
	private Animator anim;	
	private bool canSound = true;
	
	//tiempo para Open Anim
	public float timeOpenAnim = 0.0f;
	
	Animations animations;
	Gui gui;
	Inventory inventory;
	PlayerMove playerMove;
	
	void Start () {
		playerMove = (PlayerMove)GameObject.FindObjectOfType(typeof(PlayerMove));
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
		gui = (Gui)GameObject.FindObjectOfType(typeof(Gui));
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
		anim = GetComponent<Animator>();
		if(PlayerPrefs.GetInt("doorInOpen") == 0){
			anim.Play("doorInClosed");
		}else{
			this.collider2D.enabled = false;
			this.gameObject.tag = "Untagged";
			anim.Play("doorInOpened");
		}
	}
	
	void Update () {
		if(!BtnPausa.showMenu && !gui.mostrarTexto && !inventory.showItems){
			if(PlayerPrefs.GetInt("doorInOpen") == 0 && animations.objeto == "doorIn"){
				if(canSound){
					playerMove.LimpiarVariables();
					audio.Play();
					canSound = false;
				}
				anim.Play(VerificaAnim());
			}else{
				if(PlayerPrefs.GetInt("doorInOpen") == 1){
					anim.Play("doorInOpened");
				}
			}
		}
	}
	
	private string VerificaAnim(){
		if(timeOpenAnim <= anim.GetCurrentAnimatorStateInfo(0).length){
			PlayerPrefs.SetInt("doorInOpen",1);
			this.collider2D.enabled = false;
			this.gameObject.tag = "Untagged";
			inventory.EliminarTextura("destornilladorItem");
			return "doorInOpen";
		}else{
			timeOpenAnim = timeOpenAnim +0.01f;
			return "doorInOpen";
		}
	}
}
