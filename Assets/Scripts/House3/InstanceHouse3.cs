using UnityEngine;
using System.Collections;

//ESTE SCRIPT ES PARA REALIZAR LA INSTANCIA INICIAL DE OBJETOS
//TAMBIEN TIENE FUNCION PUBLICA PARA DETERMINADO MOMENTO

public class InstanceHouse3 : MonoBehaviour {
	public GameObject candle;
	public GameObject gun;
	public GameObject enemy1;
	public GameObject bullets;
	public GameObject peter;

	private bool isEnemy = false;
	private Animator anim;	

	public GameObject respawn;
	private GameObject respawnItem;
	private GameObject objeto;

	Animations animations;
	Inventory inventory;

	void Start () {
		if(PlayerPrefs.GetInt("gunItemPicked") == 0){
			InstancerObject("gunItem");
		}
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
		PlayerPrefs.SetString("playerInstance","House3");
		if(PlayerPrefs.GetInt("velaInstanciada") > 0){
			DesabilitarCollider("pedestal");
			InstancerObject("candle");
		}
		if(PlayerPrefs.GetInt("velaInstanciada") == 0){
			InstancerObject("bullets");
		}
	}

	void Update() {
		if(animations.objeto == "pedestal" && PlayerPrefs.GetInt("velaInstanciada") == 0){
			PlayerPrefs.SetInt("velaInstanciada",1);
			DesabilitarCollider("pedestal");
			animations.objeto = "";
			inventory.EliminarTextura("candleItem");
			InstancerObject("candle");
		}
		if(animations.objeto == "candle" && PlayerPrefs.GetInt("velaPrendida") == 0){
			PlayerPrefs.SetInt("velaPrendida",1);
			DesabilitarCollider("candle");
			animations.objeto = "";
			inventory.EliminarTextura("ligtherItem");
			//debo iniciar animacion de prendido y prender luz
			IniciarAnimVela();
		}
		if(PlayerPrefs.GetInt("velaPrendida") == 1 && animations.objeto == "peter"){
			InstancerObject("peter");
			//InstancerObject("enemy");
			PlayerPrefs.SetInt("velaPrendida",2);
		}
		if(animations.objeto == "enemy1" && !isEnemy){
			isEnemy = true;
			InstancerObject("enemy");
			animations.objeto = "";
		}
	}

	void DesabilitarCollider(string nombreObj){
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Objeto")) {
			if(go.name == nombreObj){
				go.gameObject.transform.collider2D.enabled = false;    
			}
		}
	}

	void IniciarAnimVela(){
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Objeto")) {
			if(go.name == "candle"){
				anim = go.GetComponent<Animator>();
				anim.Play("candleOn");
			}
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
		if(nombre == "candle"){
			return candle;
		}else if(nombre == "enemy"){
			return enemy1;
		}else if(nombre == "bullets"){
			return bullets;
		}else if(nombre == "gunItem"){
			return gun;
		}else if(nombre == "peter"){
			return peter;
		}else{
			return candle;
		}
	}
}
