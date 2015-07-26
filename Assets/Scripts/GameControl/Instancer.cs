using UnityEngine;
using System.Collections;

public class Instancer : MonoBehaviour {

	public GameObject key;
	public GameObject ring;
	public GameObject candle;
	public GameObject player;
	public GameObject boss1;
	public GameObject crow;

	public GameObject respawn;
	private GameObject respawnItem;
	private GameObject objeto;

	void Awake() {
		if(PlayerPrefs.GetInt("isPlayed")==0){
			InstancerObject("crow");
			PlayerPrefs.SetInt("isPlayed",1);
		}
		InitialInstace();
	}

	void InitialInstace(){
		InstancerObject("player");
		if(PlayerPrefs.GetInt("ringItemPicked")==0){
			InstancerObject("ringItem");
		}
		if(PlayerPrefs.GetInt("candleItemPicked")==0){
			InstancerObject("candleItem");
		}
		if(PlayerPrefs.GetInt("velaPrendida")>0 && PlayerPrefs.GetInt("boss1Kill")==0){
			InstancerObject("boss1");
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
			if(PlayerPrefs.GetString("playerInstance")==""){
				//Se busca el respawn correspondiente
				respawnItem = respawn.transform.FindChild("playerIniRespawn").gameObject;
			}else if(PlayerPrefs.GetString("playerInstance")=="ToolHouse"){
				//Se busca el respawn correspondiente
				respawnItem = respawn.transform.FindChild("playerToolRespawn").gameObject;
				PlayerPrefs.SetString("playerInstance","");
			}else if(PlayerPrefs.GetString("playerInstance")=="House1"){
				//Se busca el respawn correspondiente
				respawnItem = respawn.transform.FindChild("playerHouseRespawn").gameObject;
				PlayerPrefs.SetString("playerInstance","");
			}else{
				//respawn default
				respawnItem = respawn.transform.FindChild("playerIniRespawn").gameObject;
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
		if(nombre == "keyItem"){
			return key;
		}else if(nombre == "ringItem"){
			return ring;
		}else if(nombre == "candleItem"){
			return candle;
		}else if(nombre == "boss1"){
			return boss1;
		}else if(nombre == "crow"){
			return crow;
		}else{
			return key;
		}
	}
}
