using UnityEngine;
using System.Collections;

public class InstancerHouse1 : MonoBehaviour {
	public GameObject player;
	//public GameObject ligther;

	
	public GameObject respawn;
	private GameObject respawnItem;
	private GameObject objeto;
	
	void Awake() {
		InitialInstace();
	}

	void Start(){
		PlayerPrefs.SetString("playerInstance","House1");
	}
	
	void InitialInstace(){
		InstancerObject("Player");
		/*
		if(PlayerPrefs.GetInt("ligtherItemPicked")==0){
			InstancerObject("ligtherItem");
		}
		*/
	}
	
	public void InstancerObject(string name){
		if(name != "Player"){
			//Se busca el respawn correspondiente
			respawnItem = respawn.transform.FindChild(name+"Respawn").gameObject;
			//Se debe instanciar el item en el lugar del respawn
			//Instantiate(verificarObjeto(name), respawnItem.transform.position, Quaternion.identity);
			//EliminarCloneString(verificarObjeto(name).transform.name);
		}else{
			if(PlayerPrefs.GetString("playerInstance")==""){
				//Se busca el respawn correspondiente
				respawnItem = respawn.transform.FindChild("playerIniRespawn").gameObject;
			}else{
				if(PlayerPrefs.GetString("playerInstance")=="House2"){
					respawnItem = respawn.transform.FindChild("playerUpRespawn").gameObject;
				}else{
					//Se busca el respawn correspondiente
					respawnItem = respawn.transform.FindChild("playerIniRespawn").gameObject;
				}
			}
		}
			//Se debe instanciar el item en el lugar del respawn
			Instantiate(BuscarObjeto(name), respawnItem.transform.position, Quaternion.identity);
			EliminarCloneString(name);
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
