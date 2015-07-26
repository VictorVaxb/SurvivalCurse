using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	public GUISkin guiSkin;
	public GUIStyle itemStyle;
	public GUIStyle actionStyle;
	public GUIStyle shootStyle;
	public GUIStyle derStyle;
	public GUIStyle izqStyle;

	private string gunBullets = "";

	//FONDO 
	public float fondoY = 0.81f;
	public float fondoY2 = 0.2f;

	//BTN INVENTARIO
	private float itemBtnX = 0.86f;
	private float itemBtnY = 0.015f;
	private float itemBtnXY2 = 0.13f;

	//BTNS CONTROLS
	public float btnsY = 0.85f;
	public float btnsXY = 0.12f;

	//BTN ACTION
	private float actionBtnX = 0.83f;
	//BTN SHOOT
	private float shootBtnX = 0.68f;

	//BTN IZQUIERDA
	public float izqBtnY = 0.05f;

	//BTN DERECHA
	public float derBtnY = 0.2f;

	//RANGOS DE MOVIEMIENTO
	public int porcAlto = 15;
	public int porcIzq1 = 20;
	public int porcDer1 = 26;
	public int porcDer2 = 40;

	Inventory inventory;
	PlayerMove playerMove;
	Gui gui;

	void Start () {
		gui = (Gui)GameObject.FindObjectOfType(typeof(Gui));
		playerMove = (PlayerMove)GameObject.FindObjectOfType(typeof(PlayerMove));
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
		itemStyle.normal.background = inventory.BuscarTextura("normal","emptyItem2");
		actionStyle.normal.background = inventory.BuscarTextura("normal","btnBase");
		shootStyle.normal.background = inventory.BuscarTextura("normal","btnShoot");
		derStyle.normal.background = inventory.BuscarTextura("normal","btnRight");
		izqStyle.normal.background = inventory.BuscarTextura("normal","btnLeft");
	}

	void Update () {
		if(!inventory.showItems && !BtnPausa.showMenu){
			if(Input.touches.Length > 0)
			{
				foreach (Touch touch in Input.touches) 
				{
					if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Began) 
					{
						if(validarTouchPoint(touch.position.x,touch.position.y) == "der")
						{
							playerMove.movement = "der";
						}
						else if(validarTouchPoint(touch.position.x,touch.position.y) == "izq"){
							playerMove.movement = "izq";
						}else{
							playerMove.movement = "";
						}
					}else if(touch.phase == TouchPhase.Ended){
						playerMove.movement = "";
					}
				}
			}
		}
	}

	private string validarTouchPoint(float pointX, float pointY)
	{
		if((pointX*100)/Gui.anchoPant < porcIzq1 && (pointY*100)/Gui.altoPant < porcAlto)
		{
			//Debug.Log("DER: X%: "+(pointX*100)/Gui.anchoPant+"Y%:"+(pointY*100)/Gui.altoPant);
			return "izq";
		}else if((pointX*100)/Gui.anchoPant > porcDer1 && (pointX*100)/Gui.anchoPant < porcDer2 && (pointY*100)/Gui.altoPant < porcAlto){
			return "der";
		}else{
			return "No Boton";
		}
	}

	void OnGUI(){
		if (DieMenu.showMenuDie == false) {
			if (itemStyle.normal.background.name == "gunItem") {
					itemStyle.fontSize = Mathf.RoundToInt (Gui.anchoPant / 30);
					itemStyle.contentOffset = new Vector2 (Gui.anchoPant / 65, Gui.altoPant / 90);
					gunBullets = PlayerPrefs.GetInt ("gunBullets").ToString ();
			} else {
					gunBullets = "";
			}
			if (!BtnPausa.showMenu & !inventory.consultaInventory ()) {
					if (PlayerPrefs.GetString ("itemEquipado") != "") {
						if (PlayerPrefs.GetString ("itemEquipado") == "emptyItem") {
							itemStyle.normal.background = inventory.BuscarTextura ("normal", "emptyItem2");
						} else {
							itemStyle.normal.background = inventory.BuscarTextura ("normal", PlayerPrefs.GetString ("itemEquipado"));
						}
					}
					//FONDO CONtROLS
					GUI.Box (new Rect (0, Gui.altoPant * fondoY, Gui.anchoPant, Gui.altoPant * fondoY2), "", guiSkin.box);
					//BOtON DERECHA
					GUI.Box (new Rect (Gui.anchoPant * derBtnY, Gui.altoPant * btnsY, Gui.anchoPant * btnsXY, Gui.altoPant * btnsXY), "", derStyle);
					//BOtON IZQUIERDA
					GUI.Box (new Rect (Gui.anchoPant * izqBtnY, Gui.altoPant * btnsY, Gui.anchoPant * btnsXY, Gui.altoPant * btnsXY), "", izqStyle);
					if (!gui.mostrarTexto) {
						//BOtON INVENTARIO
						if (GUI.Button (new Rect (Gui.anchoPant * itemBtnX, Gui.altoPant * itemBtnY, Gui.anchoPant * itemBtnXY2, Gui.altoPant * itemBtnXY2), gunBullets, itemStyle)) {
							inventory.muestraInventory (true);
						}
					}
					VerificaActionTexture ();
					//BOtON ACCION
					if (GUI.Button (new Rect (Gui.anchoPant * actionBtnX, Gui.altoPant * btnsY, Gui.anchoPant * btnsXY, Gui.altoPant * btnsXY), "", actionStyle)) {
						if (playerMove.h < 0.9f && playerMove.h > -0.9f) {
							playerMove.isAction = true;
						} else {
							playerMove.isAction = false;
						}
					}
					//BOTON SHOOT
					if (PlayerPrefs.GetString ("itemEquipado") == "gunItem") {
						if (GUI.Button (new Rect (Gui.anchoPant * shootBtnX, Gui.altoPant * btnsY, Gui.anchoPant * btnsXY, Gui.altoPant * btnsXY), "", shootStyle)) {
							if (playerMove.h < 0.9f && playerMove.h > -0.9f) {
								playerMove.isShoot = true;
							} else {
								playerMove.isShoot = false;
							}
						}
					}
				}
			}
		}

	void VerificaActionTexture(){
		if(playerMove.objetoInvest != "" && !gui.mostrarTexto){
			actionStyle.normal.background = inventory.BuscarTextura("normal","btnLook");
		}else if(gui.mostrarTexto){
			actionStyle.normal.background = inventory.BuscarTextura("normal","btnClose");
		}else if(playerMove.itemATomar != ""){
			actionStyle.normal.background = inventory.BuscarTextura("normal","btnLook2");
		}else if(playerMove.lugarInOut != ""){
			actionStyle.normal.background = inventory.BuscarTextura("normal","btnInOut");
		}else{
			actionStyle.normal.background = inventory.BuscarTextura("normal","btnBase");
		}
	}
}
