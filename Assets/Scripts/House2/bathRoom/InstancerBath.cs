using UnityEngine;
using System.Collections;

public class InstancerBath : MonoBehaviour {
	//balas para instanciar
	public GameObject bullets;

	public GameObject respawn;
	private GameObject respawnItem;
	private GameObject objeto;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetString("playerInstance","BathRoom");
		InitialInstace();
	}

	void InitialInstace(){
		if(PlayerPrefs.GetInt("bulletBathroomPicked")==0){
			InstancerObject("bullets");
		}
	}

	public void InstancerObject(string name){
		//Se busca el respawn correspondiente
		respawnItem = respawn.transform.FindChild(name+"Respawn").gameObject;
		//Se debe instanciar el item en el lugar del respawn
		Instantiate(verificarObjeto(name), respawnItem.transform.position, Quaternion.identity);
		EliminarCloneString(verificarObjeto(name).transform.name);
	}
	
	void EliminarCloneString(string nombre){
		//Cambiar Nombre Gameobject
		objeto = GameObject.Find(nombre+"(Clone)");
		objeto.name = nombre;
	}
	
	GameObject verificarObjeto(string nombre){
		if(nombre == "bullets"){
			return bullets;
		}else{
			return bullets;
		}
	}
}
