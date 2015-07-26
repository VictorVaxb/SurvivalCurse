using UnityEngine;
using System.Collections;

public class BtnPausa : MonoBehaviour {
	public static bool showMenu = false; 
	//Boton pausa
	private float xFactor = 0.015f;
	private float yFactor = 0.02f;
	private float x2Factor = 0.08f;
	private float y2Factor = 0.12f;
	private string texto = "";
	public GUIStyle myStyle;
	public GUISkin guiSkin;
	private float posGroupX = 0.28f;
	private float posGroupY = 0.18f;
	private float posBtnX = 0.078f;
	private float posContinueY = 0.18f;
	private float posX2 = 0.3f;
	private float posY2 = 0.2f;
	private float posMenuY = 0.42f;
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
		myStyle.normal.background = inventory.BuscarTextura("normal","btnPausa");
	}
	
	void OnGUI(){
		if(DieMenu.showMenuDie == false){
			//Boton pausa
			if(!gui.mostrarTexto & !inventory.consultaInventory()){
				if(GUI.Button(new Rect(Gui.anchoPant*xFactor,Gui.altoPant*yFactor,Gui.anchoPant*x2Factor,Gui.altoPant*y2Factor), texto, myStyle))
				{
					menuOnOf();
				}
			}
		}
		
		if(showMenu && DieMenu.showMenuDie == false)
		{
			GUI.skin = guiSkin;
			GUI.DrawTexture(new Rect(Gui.anchoPant*fondoX,0,Gui.anchoPant*fondoX2, Gui.altoPant*fondoY2),inventory.BuscarTextura("normal","pauseMenu"));
			GUI.BeginGroup (new Rect(Gui.anchoPant*posGroupX, Gui.altoPant*posGroupY,Gui.anchoPant, Gui.altoPant));
			if(GUI.Button(new Rect(Gui.anchoPant*posBtnX,Gui.altoPant*posContinueY,Gui.anchoPant*posX2,Gui.altoPant*posY2), inventory.BuscarTextura("normal","continue"), guiSkin.button))
			{
				//Instantiate(selectSound, gameObject.transform.position, Quaternion.identity);
				//print("Resume");
				// Auido Botton
				audio.Play();
				menuOnOf();
			}
			
			if(GUI.Button(new Rect(Gui.anchoPant*posBtnX,Gui.altoPant*posMenuY,Gui.anchoPant*posX2,Gui.altoPant*posY2), inventory.BuscarTextura("normal","menu"), guiSkin.button))
			{
				// Auido Botton
				audio.Play();
				menuOnOf();
				//Instantiate(selectSound, gameObject.transform.position, Quaternion.identity);
				StartCoroutine(IrAOtraEscena("MainMenu"));
				//Application.LoadLevel("MainMenu");
			}
			GUI.EndGroup ();
		}
	}

	IEnumerator IrAOtraEscena(string lugar){
		playerMove.sceneToGo = lugar;
		//This is a coroutine
		yield return new WaitForSeconds(1.8f);
		Application.LoadLevel(lugar);
	}

	void menuOnOf(){
		if(showMenu){
			showMenu = false;
			myStyle.normal.background = inventory.BuscarTextura("normal","btnPausa");
		}else{
			showMenu = true;
			myStyle.normal.background = inventory.BuscarTextura("normal","btnPausaDes");
		}
	}
}
