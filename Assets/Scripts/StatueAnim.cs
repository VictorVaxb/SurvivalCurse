using UnityEngine;
using System.Collections;

public class StatueAnim : MonoBehaviour {
	private Animator anim;					// Reference to the animator component.
	
	//tiempo para Open Anim
	public float timeAnim = 0.0f;
	
	Animations animations;
	Gui gui;
	Inventory inventory;
	Instancer instancer;
	
	void Start () {
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
		gui = (Gui)GameObject.FindObjectOfType(typeof(Gui));
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
		instancer = (Instancer)GameObject.FindObjectOfType(typeof(Instancer));
		anim = GetComponent<Animator>();
		anim.Play("statueIdle");
	}
	
	void Update () {
		if(!BtnPausa.showMenu && !gui.mostrarTexto && !inventory.showItems){
			if(PlayerPrefs.GetInt("statueGive") == 0 && animations.objeto == "statue"){
				anim.Play(VerificaAnim());
			}else{
				if(PlayerPrefs.GetInt("statueGive") == 1){
					anim.Play("statueIdle");
				}
			}
		}
	}

	private string VerificaAnim(){
		if(timeAnim <= anim.GetCurrentAnimatorStateInfo(0).length){
			timeAnim = timeAnim +0.019f;
			return "statueGive";
		}else{
			inventory.EliminarTextura("ringItem");
			//Se debe Instanciar el item KEY, se llama a Instanciador 
			instancer.InstancerObject("keyItem");
			PlayerPrefs.SetInt("statueGive",1);
			return "statueIdle";
		}
	}
}
