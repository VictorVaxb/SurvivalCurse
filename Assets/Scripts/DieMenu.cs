using UnityEngine;
using System.Collections;

public class DieMenu : MonoBehaviour {
	public static bool showMenuDie = false;
	public GUISkin guiSkin;
	private float posBtnX = 0.3f;
	private float posX2 = 0.43f;
	private float posY2 = 0.2f;
	private float posMenuY = 0.45f;
	private float fondoX = 0.23f;
	private float fondoX2 = 0.6f;
	private float fondoY2 = 1.05f;
	
	Gui gui;
	Inventory inventory;
	PlayerMove playerMove;
	
	void Start () {
		gui = (Gui)GameObject.FindObjectOfType(typeof(Gui));
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
		playerMove = (PlayerMove)GameObject.FindObjectOfType(typeof(PlayerMove));
	}
	
	void OnGUI(){
		if(showMenuDie)
		{
			GUI.skin = guiSkin;
			GUI.DrawTexture(new Rect(Gui.anchoPant*fondoX,0,Gui.anchoPant*fondoX2, Gui.altoPant*fondoY2),inventory.BuscarTextura("normal","diedMenu"));
			
			if(GUI.Button(new Rect(Gui.anchoPant*posBtnX,Gui.altoPant*posMenuY,Gui.anchoPant*posX2,Gui.altoPant*posY2), inventory.BuscarTextura("normal","menu"), guiSkin.button))
			{
				// Auido Botton
				audio.Play();
				menuOnOf();
				//Instantiate(selectSound, gameObject.transform.position, Quaternion.identity);
				StartCoroutine(IrAOtraEscena("MainMenu"));
				//Application.LoadLevel("MainMenu");
			}
		}
	}
	
	IEnumerator IrAOtraEscena(string lugar){
		playerMove.sceneToGo = lugar;
		//This is a coroutine
		yield return new WaitForSeconds(1.8f);
		Application.LoadLevel(lugar);
	}
	
	void menuOnOf(){
		if(showMenuDie){
			showMenuDie = false;
		}else{
			showMenuDie = true;
		}
	}
}
