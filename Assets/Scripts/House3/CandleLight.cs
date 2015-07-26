using UnityEngine;
using System.Collections;

//ESTE SCRIPT ES PARA MOVER Y AGRANDAR LA LUZ, UNA VEZ QUE LA VELA ITEM SE HAYA
//PRENDIDO

public class CandleLight : MonoBehaviour {
	private Vector3 lugarFinal;
	public float velocidad = 0.02f;
	private bool canPeter = true;
	Animations animations;

	void Start () {
		BuscarLightEnd();
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
	}

	void Update () {
		if(PlayerPrefs.GetInt("velaPrendida")>0){
			this.transform.position = new Vector3(
				VerificarX(),
				this.transform.position.y,
				this.transform.position.z);
			AumentarRango();
		}
	}

	void AumentarRango(){
		if(this.transform.light.spotAngle < 110){
			this.transform.light.spotAngle = this.transform.light.spotAngle + 0.5f;
		}
	}

	float VerificarX(){
		if(this.transform.position.x > lugarFinal.x){
			return this.transform.position.x - velocidad;
		}else{
			if(canPeter){
				animations.objeto = "peter";
				canPeter = false;
			}
			return this.transform.position.x;
		}
	}

	void BuscarLightEnd(){
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Respawn")) {
			if(go.name == "LightEnd"){
				lugarFinal = go.gameObject.transform.position;    
			}
		}
	}


}
