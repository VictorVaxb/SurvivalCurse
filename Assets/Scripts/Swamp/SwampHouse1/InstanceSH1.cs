using UnityEngine;
using System.Collections;

public class InstanceSH1 : MonoBehaviour {
	public GameObject player;
	public GameObject respawn;
	
	private GameObject respawnItem;
	private GameObject objeto;
	
	void Awake() {
		InitialInstace();
	}
	
	void Start () {

	}
	
	public void InstancerObject(string name){
		if(name != "player"){
			//Se busca el respawn correspondiente
			respawnItem = respawn.transform.FindChild(name+"Respawn").gameObject;
			//Se debe instanciar el item en el lugar del respawn
			Instantiate(BuscarObjeto(name), respawnItem.transform.position, Quaternion.identity);
			EliminarCloneString(name);
		}else{
			if(PlayerPrefs.GetString("playerInstance")=="SwampFight"){
				respawnItem = respawn.transform.FindChild("playerFightRespawn").gameObject;
			}else if(PlayerPrefs.GetString("playerInstance")=="SwampHouse2"){
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
		PlayerPrefs.SetString("playerInstance","SwampHouse1");
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
