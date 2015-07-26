using UnityEngine;
using System.Collections;

public class InstanceSH2 : MonoBehaviour {
	public GameObject respawn;

	public GameObject player;
	public GameObject bullets;

	private GameObject respawnItem;
	private GameObject objeto;

	void Awake(){
		InitialInstace();
	}

	void Start () {
		PlayerPrefs.SetString("playerInstance","SwampHouse2");
	}

	void InitialInstace(){
		InstancerObject("player");
		if(PlayerPrefs.GetInt("bulletsSH2Picked") == 0){
			InstancerObject("bullets");
		}
	}

	public void InstancerObject(string name){
		if(name != "player"){
			//Se busca el respawn correspondiente
			respawnItem = respawn.transform.FindChild(name+"Respawn").gameObject;
			//Se debe instanciar el item en el lugar del respawn
			Instantiate(verificarObjeto(name), respawnItem.transform.position, Quaternion.identity);
			EliminarCloneString(verificarObjeto(name).transform.name);
		}else{
			if(PlayerPrefs.GetString("playerInstance")=="SwampHouse3"){
				//Se busca el respawn correspondiente
				respawnItem = respawn.transform.FindChild("playerUpRespawn").gameObject;
			}else{
				//respawn default
				respawnItem = respawn.transform.FindChild("playerOutRespawn").gameObject;
			}
			//Se debe instanciar el item en el lugar del respawn
			Instantiate(player, respawnItem.transform.position, Quaternion.identity);
			EliminarCloneString("Player");
		}
	}

	void EliminarCloneString(string nombre){
		//Cambiar Nombre Gameobject
		objeto = GameObject.Find(nombre+"(Clone)");
		objeto.name = nombre;
	}
	
	GameObject verificarObjeto(string nombre){
		if(nombre == "player"){
			return player;
		}else if(nombre == "bullets"){
			return bullets;
		}else{
			return player;
		}
	}
}
