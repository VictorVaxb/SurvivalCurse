using UnityEngine;
using System.Collections;

public class ShineItems : MonoBehaviour {
	public float speedRot = 20.0f;
	public float maxSize = 1.0f;
	public float minSize = 0.7f;
	public float speedScale = 3.0f;
	private string accion = "achicar"; 

	void Update () {
		Animar();
	}

	void Animar(){
		this.transform.Rotate(0,0,Time.deltaTime * speedRot);
		if(this.transform.localScale.x > maxSize){
			accion = "achicar";
		}else if(this.transform.localScale.x < minSize){
			accion = "agrandar";
		}

		if(accion == "agrandar"){
			this.transform.localScale = new Vector3(this.transform.localScale.x + Time.deltaTime * speedScale,this.transform.localScale.y + Time.deltaTime * speedScale,0);
		}else if(accion == "achicar"){
			this.transform.localScale = new Vector3(this.transform.localScale.x - Time.deltaTime * speedScale,this.transform.localScale.y - Time.deltaTime * speedScale,0);
		}
	}
}
