using UnityEngine;
using System.Collections;

public class InstanceSH4 : MonoBehaviour {
	public GameObject respawn;
	public GameObject enemy2;
	private GameObject respawnItem;
	private GameObject objeto;
	
	void Awake(){
		InitialInstace();
	}
	
	void Start () {
		PlayerPrefs.SetString("playerInstance","SwampHouse4");
	}
	
	void InitialInstace(){
		if(PlayerPrefs.GetInt("enemy2Akilled") == 0){
			InstancerObject("enemy2");
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
		if(nombre == "enemy2"){
			return enemy2;
		}else{
			return enemy2;
		}
	}
}
