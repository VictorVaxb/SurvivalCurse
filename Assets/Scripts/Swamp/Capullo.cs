using UnityEngine;
using System.Collections;

public class Capullo : MonoBehaviour {
	private Animator anim;					// Reference to the player's animator component.
	//tiempo para Open Anim
	private float timeAnim = 0.0f;
	//variable de tiempo entre animaciones
	private float tiempoEspera = 1.0f;
	//variable para ver si se puede animar o no
	private bool canAnim = false;
	//Objeto bola
	public GameObject bola;
	//Objeto de respawn bola
	private GameObject ballSpawn;
	//boolean esta sonando?
	private bool isSound = false;

	Gui gui;
	Inventory inventory;

	void Start () {
		anim = GetComponent<Animator>();
		anim.Play("capulloWait");
		ballSpawn = this.transform.FindChild("ballSpawn").gameObject;
		gui = (Gui)GameObject.FindObjectOfType(typeof(Gui));
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
	}

	void Update () {
		if(!BtnPausa.showMenu && !gui.mostrarTexto && !inventory.showItems){
			verificaTiempo();
			anim.Play(VerificaAnim ());
		}
	}

	private void verificaTiempo(){
		if(Time.time > tiempoEspera){
			canAnim = true;
		}
	}

	private string VerificaAnim(){
		if(canAnim){
			if(timeAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
				timeAnim = 0.0f;
				tiempoEspera = Time.time + 1.0f;
				//Instanciar bola
				Instantiate(bola, ballSpawn.transform.position, Quaternion.identity);
				canAnim = false;
				isSound = false;
				return "capulloWait";
			}else{
				if(!isSound){
					audio.Play ();
					isSound = true;
				}
				timeAnim = timeAnim +0.022f;
				return "capulloIdle";
			}
		}else{
			return "capulloWait";
		}
	}
}
