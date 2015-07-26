using UnityEngine;
using System.Collections;

public class InstancerHouse2 : MonoBehaviour {
	public GameObject player;
	public GameObject ligther;
	public GameObject enemy1;
	
	
	public GameObject respawn;
	private GameObject respawnItem;
	private GameObject objeto;
	
	void Awake() {
		InitialInstace();
	}
	
	void InitialInstace(){
		InstancerObject("player");
		verificaEnemySpawn();
		//InstancerObject("gunItem");
		PlayerPrefs.SetString("playerInstance","House2");
	}

	void verificaEnemySpawn(){
		if(PlayerPrefs.GetString("playerInstance")=="House3" && PlayerPrefs.GetInt("velaPrendida") > 0){
			InstancerObject("enemy1");
		}
	}
	
	public void InstancerObject(string name){
		if(name != "player"){
			//Se busca el respawn correspondiente
			respawnItem = respawn.transform.FindChild(name+"Respawn").gameObject;
			//Se debe instanciar el item en el lugar del respawn
			Instantiate(BuscarObjeto(name), respawnItem.transform.position, Quaternion.identity);
			EliminarCloneString(name);
		}else{
			if(PlayerPrefs.GetString("playerInstance")=="House1"){
				respawnItem = respawn.transform.FindChild("playerIniRespawn").gameObject;
			}else if(PlayerPrefs.GetString("playerInstance")=="BathRoom"){
				respawnItem = respawn.transform.FindChild("playerBathRespawn").gameObject;
			}else if(PlayerPrefs.GetString("playerInstance")=="House3"){
				respawnItem = respawn.transform.FindChild("playerAtticRespawn").gameObject;
			}else{
				respawnItem = respawn.transform.FindChild("playerAtticRespawn").gameObject;
			}
			//Se debe instanciar el item en el lugar del respawn
			Instantiate(player, respawnItem.transform.position, Quaternion.identity);
			EliminarCloneString("Player");
		}
	}

	GameObject BuscarObjeto(string nombre){
		if(nombre == "player"){
			return player;
		}else if(nombre == "ligtherItem"){
			return ligther;
		}else if(nombre == "enemy1"){
			return enemy1;
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
