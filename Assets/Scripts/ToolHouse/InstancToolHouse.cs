using UnityEngine;
using System.Collections;

public class InstancToolHouse : MonoBehaviour {
	public GameObject crowbar;
	public GameObject bullets;
	public GameObject destornillador;
	
	public GameObject respawn;
	private GameObject respawnItem;
	private GameObject objeto;
	
	void Start () {
		PlayerPrefs.SetString("playerInstance","ToolHouse");
		InitialInstace();
	}
	
	void InitialInstace(){
		if(PlayerPrefs.GetInt("crowbarItemPicked")==0){
			InstancerObject("crowbarItem");
		}
		if(PlayerPrefs.GetInt("bulletsToolHousePicked")==0){
			InstancerObject("bulletsItem");
		}
		if(PlayerPrefs.GetInt("destornilladorItemPicked")==0){
			InstancerObject("destornilladorItem");
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
		if(nombre == "crowbarItem"){
			return crowbar;
		}else if(nombre == "bulletsItem"){
			return bullets;
		}else if(nombre == "destornilladorItem"){
			return destornillador;
		}else{
			return bullets;
		}
	}
}
