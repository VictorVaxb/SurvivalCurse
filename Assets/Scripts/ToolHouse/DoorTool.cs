using UnityEngine;
using System.Collections;

public class DoorTool : MonoBehaviour {
	private Animator anim;	
	private float timeAnim = 0.0f;

	PlayerMove playerMove;

	void Start () {
		anim = GetComponent<Animator>();
		playerMove = (PlayerMove)GameObject.FindObjectOfType(typeof(PlayerMove));
	}
	
	void Update () {
		VerificaAnimacion();
	}
	
	void VerificaAnimacion(){
		if(PlayerPrefs.GetInt("canEnterToolHouse")==0){
			anim.Play("doorIdle");
		}else if(PlayerPrefs.GetInt("canEnterToolHouse")==1){
			anim.Play(AnimDoor());
		}else{
			anim.Play("doorOpened");
		}
	}
	
	private string AnimDoor(){
		if(timeAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
			timeAnim = 0.0f;
			PlayerPrefs.SetInt("canEnterToolHouse",2);
			StartCoroutine(IrAOtraEscena("ToolHouse"));
			return "doorOpened";
		}else{
			timeAnim = timeAnim + 0.02f;
			return "doorOpen";
		}
	}

	IEnumerator IrAOtraEscena(string lugar){
		playerMove.sceneToGo = lugar;
		//This is a coroutine
		yield return new WaitForSeconds(1.8f);
		Application.LoadLevel(lugar);
	}
}
