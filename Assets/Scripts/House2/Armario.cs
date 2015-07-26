using UnityEngine;
using System.Collections;

public class Armario : MonoBehaviour {
	private Animator anim;					// Reference to the animator component.

	//tiempo para Open Anim
	public float timeOpenAnim = 0.0f;

	Animations animations;
	Gui gui;
	Inventory inventory;
	InstancerHouse2 instancerHouse2;

	void Start () {
		instancerHouse2 = (InstancerHouse2)GameObject.FindObjectOfType(typeof(InstancerHouse2));
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
		gui = (Gui)GameObject.FindObjectOfType(typeof(Gui));
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
		anim = GetComponent<Animator>();
		anim.Play("armarioClosed");
	}

	void Update () {
		if(!BtnPausa.showMenu && !gui.mostrarTexto && !inventory.showItems){
			if(PlayerPrefs.GetInt("armarioOpen") == 0 && animations.objeto == "armario"){
				anim.Play(VerificaAnim());
			}else if(PlayerPrefs.GetInt("armarioOpen") == 1){
				anim.Play("armarioOpened");
			}
		}
	}

	private string VerificaAnim(){
		if(timeOpenAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
			timeOpenAnim = 0.0f;
			PlayerPrefs.SetInt("armarioOpen",1);
			instancerHouse2.InstancerObject("ligtherItem");
			audio.Play();
			return "armarioOpened";
		}else{
			timeOpenAnim = timeOpenAnim +0.022f;
			return "armarioOpen";
		}
	}
}
