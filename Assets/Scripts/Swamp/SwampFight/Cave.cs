using UnityEngine;
using System.Collections;

public class Cave : MonoBehaviour {

	PlayerMove playerMove;

	void Start(){
		playerMove = (PlayerMove)GameObject.FindObjectOfType(typeof(PlayerMove));
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.gameObject.name == "Player"){
			StartCoroutine(IrAOtraEscena("Ending"));
		}
	}

	IEnumerator IrAOtraEscena(string lugar){
		playerMove.sceneToGo = lugar;
		yield return new WaitForSeconds(1.5f);
		Application.LoadLevel(lugar);
	}
}
