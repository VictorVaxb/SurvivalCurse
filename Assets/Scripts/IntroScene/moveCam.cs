using UnityEngine;
using System.Collections;

public class moveCam : MonoBehaviour {
	//array de respawns
	GameObject[] respawns;
	//length de respawns
	public static int respawnsLength;
	//Velocidad de movimiento
	private float speed = 1.6f;
	//Vector final
	private Vector3 vectorFinal;

	void Awake(){
		respawnsLength = 0;
	}

	void Start () {
		respawns = GameObject.FindGameObjectsWithTag("Respawn");
		respawnsLength = respawns.Length;
	}

	void Update () {
		BuscarPos();
		transform.position = Vector3.Lerp(transform.position, 
		                                  vectorFinal, 
		                                  speed * Time.fixedDeltaTime);
		//Al estar cerca de respawn activo el texto:
		if(Vector3.Distance(transform.position,vectorFinal) < 0.07f){
			if(!GuiIntro.mostrarTexto && (GuiIntro.posicionCam <= respawns.Length)){
				GuiIntro.mostrarTexto = true;
			}
		}
	}

	void BuscarPos(){
		if(GuiIntro.posicionCam <= respawnsLength){
			foreach(GameObject respawn in respawns){
				if(respawn.name == GuiIntro.posicionCam.ToString()){
					vectorFinal = respawn.transform.position;
				}
			}
		}else{
			StartCoroutine(IrAOtraEscena("SceneSelection"));
		}
	}

	IEnumerator IrAOtraEscena(string lugar){
		//This is a coroutine
		yield return new WaitForSeconds(1.5f);
		Application.LoadLevel(lugar);
	}
}
