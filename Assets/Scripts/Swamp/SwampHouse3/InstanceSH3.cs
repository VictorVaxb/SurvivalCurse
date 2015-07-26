using UnityEngine;
using System.Collections;

public class InstanceSH3 : MonoBehaviour {
	public GameObject respawn;

	public GameObject player;
	public GameObject enemy2;

	private GameObject respawnItem;
	private GameObject objeto;
	
	void Awake(){
		InitialInstace();
	}
	
	void Start () {
		PlayerPrefs.SetString("playerInstance","SwampHouse3");
	}
	
	void InitialInstace(){
		InstancerObject("player");
		if(PlayerPrefs.GetInt("comodaIsDown") == 1){
			InstancerObject("enemy2");
			PlayerPrefs.SetInt("comodaIsDown",2);
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
			if(PlayerPrefs.GetString("playerInstance")=="SwampHouse4"){
				//Se busca el respawn correspondiente
				respawnItem = respawn.transform.FindChild("playerSideRespawn").gameObject;
			}else{
				//respawn default
				respawnItem = respawn.transform.FindChild("playerDownRespawn").gameObject;
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
		}else if(nombre == "enemy2"){
			return enemy2;
		}else{
			return player;
		}
	}
}
