using UnityEngine;
using System.Collections;

public class InstanceSMech : MonoBehaviour {	
	public GameObject respawn;

	public GameObject gataItem;
	public GameObject hookItem;

	private GameObject respawnItem;
	private GameObject objeto;
	
	void Awake(){
		InitialInstace();
	}
	
	void Start () {
		PlayerPrefs.SetString("playerInstance","SwampMech");
	}
	
	void InitialInstace(){
		if(PlayerPrefs.GetInt("gataItemPicked") == 0){
			InstancerObject("gataItem");
		}
		if(PlayerPrefs.GetInt("hookItemPicked") == 0){
			InstancerObject("hookItem");
		}
	}
	
	public void InstancerObject(string name){
		if(name != "player"){
			//Se busca el respawn correspondiente
			respawnItem = respawn.transform.FindChild(name+"Respawn").gameObject;
			//Se debe instanciar el item en el lugar del respawn
			Instantiate(verificarObjeto(name), respawnItem.transform.position, Quaternion.identity);
			EliminarCloneString(verificarObjeto(name).transform.name);
		}
	}
	
	void EliminarCloneString(string nombre){
		//Cambiar Nombre Gameobject
		objeto = GameObject.Find(nombre+"(Clone)");
		objeto.name = nombre;
	}
	
	GameObject verificarObjeto(string nombre){
		if(nombre == "gataItem"){
			return gataItem;
		}else if(nombre == "hookItem"){
			return hookItem;
		}else{
			return gataItem;
		}
	}
}
