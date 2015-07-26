using UnityEngine;
using System.Collections;

public class InstanceSFight : MonoBehaviour {
	//ramas para instanciar cuando se active el trigger
	public GameObject ramas;

	//Respawn para los objetos a instanciar
	public GameObject respawn;
	private GameObject respawnItem;
	private GameObject objeto;

	//Para verificar cuando se debe instanciar las ramas
	Animations animations;

	void Start () {
		PlayerPrefs.SetString("playerInstance","SwampFight");
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
	}

	void Update(){
		if(animations.objeto == "ramasUp"){
			InstancerObject("ramasUp");
			animations.objeto = "";
		}
	}

	public void InstancerObject(string name){
		//Se busca el respawn correspondiente
		respawnItem = respawn.transform.FindChild(name+"Respawn").gameObject;
		//Se debe instanciar el item en el lugar del respawn
		Instantiate(BuscarObjeto(name), respawnItem.transform.position, Quaternion.identity);
		EliminarCloneString(name);
	}

	GameObject BuscarObjeto(string nombre){
		if(nombre == "ramas"){
			return ramas;
		}else{
			return ramas;
		}
	}
	
	void EliminarCloneString(string nombre){
		//Cambiar Nombre Gameobject
		objeto = GameObject.Find(nombre+"(Clone)");
		objeto.name = nombre;
	}
}
