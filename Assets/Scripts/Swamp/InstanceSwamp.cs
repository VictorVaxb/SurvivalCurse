using UnityEngine;
using System.Collections;

public class InstanceSwamp : MonoBehaviour {
	public GameObject player;
	public GameObject respawn;

	private GameObject respawnItem;
	private GameObject objeto;

	//para definir que se tiene pistola
	Inventory inventory;

	void Awake() {
		InitialInstace();
	}

	void Start () {
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
		/*
		if(PlayerPrefs.GetString("playerInstance") == ""){
			inventory.llegaNuevoItem ("gunItem");
		}
		*/
		PlayerPrefs.SetString("playerInstance","Swamp1");
	}

	public void InstancerObject(string name){
		if(name != "player"){
			//Se busca el respawn correspondiente
			respawnItem = respawn.transform.FindChild(name+"Respawn").gameObject;
			//Se debe instanciar el item en el lugar del respawn
			Instantiate(BuscarObjeto(name), respawnItem.transform.position, Quaternion.identity);
			EliminarCloneString(name);
		}else{
			if(PlayerPrefs.GetString("playerInstance")=="SwampMech"){
				respawnItem = respawn.transform.FindChild("playerMechRespawn").gameObject;
			}else if(PlayerPrefs.GetString("playerInstance")=="SwampHouse1"){
				respawnItem = respawn.transform.FindChild("playerHouseRespawn").gameObject;
			}else{
				respawnItem = respawn.transform.FindChild("playerIniRespawn").gameObject;
			}
			//Se debe instanciar el item en el lugar del respawn
			Instantiate(player, respawnItem.transform.position, Quaternion.identity);
			EliminarCloneString("Player");
		}
	}

	void InitialInstace(){
		InstancerObject("player");
	}

	GameObject BuscarObjeto(string nombre){
		if(nombre == "player"){
			return player;
		}else{
			return player;
		}
	}
	
	void EliminarCloneString(string nombre){
		//Cambiar Nombre Gameobject
		objeto = GameObject.Find(nombre+"(Clone)");
		objeto.name = nombre;
	}
}
