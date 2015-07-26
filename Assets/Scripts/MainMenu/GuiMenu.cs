using UnityEngine;
using System.Collections;

public class GuiMenu : MonoBehaviour {
	private float anchoPant = 0.0f;
	private float altoPant = 0.0f;

	public GUIStyle playStyle;
	public GUIStyle exitStyle;
	public GUIStyle newGameStyle;
	public GUIStyle areYouSureStyle;
	public GUIStyle okBtnStyle;
	public GUIStyle dontBtnStyle;
	public GUIStyle creditsBtnStyle;

	//Variables Play btn
	public float playBtnX = 0.75f;
	public float playBtnY = 0.78f;
	public float playBtnXY = 0.2f;

	//Variables Exit btn
	public float exitBtnX = 0.02f;
	public float exitBtnY = 0.77f;
	public float exitBtnXY = 0.2f;

	//Variables RESET btn
	public float resBtnX = 0.9f;
	public float resBtnXY = 0.2f;

	//Variables RESET btn
	public float newGameY = 0.9f;
	public float newGameX2 = 0.9f;
	public float newGameY2 = 0.9f;

	//Variables areYouSure box
	private float sureX = 0.15f;
	private float sureY = 0.2f;
	private float sureX2 = 0.65f;
	private float sureY2 = 0.6f;

	// variables botones Ok y Dont de are you sure
	private float okX 	= 0.26f;
	private float dontX 	= 0.57f;
	private float okDontY = 0.56f;
	private float okDontX2 = 0.14f;
	private float okDontY2 = 0.23f;

	// Boton CREDITS
	private float creditX = 0.86f;
	private float creditY = 0.02f;
	private float creditX2 = 0.13f;
	private float creditY2 = 0.1f;

	// Texturas al pinchar
	public Texture2D texturePlay;
	public Texture2D play2;
	public Texture2D textureContinue;
	public Texture2D continue2;
	public Texture2D exit;
	public Texture2D exit2;
	public Texture2D newGame;
	public Texture2D newGame2;
	public Texture2D credits;
	public Texture2D credits2;

	// Variable que muestra la ventana Are you Sure
	private bool areYouSure = false;

	/*
	void Start(){
		borrarDatos ();
	}
	*/

	void Update () {
		anchoPant = Screen.width;
		altoPant = Screen.height;
	}

	void OnGUI(){
		VerificarTexturaPlay();
		//Boton PLAY
		if (!areYouSure) {
			if (GUI.Button (new Rect (anchoPant * playBtnX, altoPant * playBtnY, anchoPant * playBtnXY, altoPant * playBtnXY), "", playStyle)) {
				// Sonido boton
				audio.Play();
				if(playStyle.normal.background.name == "play"){
					playStyle.normal.background = play2;
					Application.LoadLevel ("IntroScene");
				}else if(playStyle.normal.background.name == "continue"){
					playStyle.normal.background = continue2;
					Application.LoadLevel ("SceneSelection");
				}
			}
		}
		//Boton Exit
		if(!areYouSure){
			if(GUI.Button(new Rect(anchoPant*exitBtnX,altoPant*exitBtnY,anchoPant*exitBtnXY,altoPant*exitBtnXY), "", exitStyle))
			{
				exitStyle.normal.background = exit2;
				// Sonido boton
				audio.Play();
				//DESCOMENTAR AL FINAL DEL DESARROLLO
				System.Diagnostics.Process.GetCurrentProcess().Kill();
			}
		}
		// NEW GAME: Verificacion para mostrar el boton New Game
		if(PlayerPrefs.GetInt("isPlayed")==1 && !areYouSure){
			// Boton new game, abre ventana de pregunta Are you Sure
			if(GUI.Button(new Rect(anchoPant*exitBtnX,altoPant*newGameY,anchoPant*newGameX2,altoPant*newGameY2), "", newGameStyle))
			{
				newGameStyle.normal.background = newGame2;
				// Sonido boton
				audio.Play();
				areYouSure = true;
			}
		}
		// ARE YOU SURE ventana:
		if(areYouSure){
			GUI.Box(new Rect(anchoPant*sureX,altoPant*sureY,anchoPant*sureX2,altoPant*sureY2),"", areYouSureStyle);
			if(GUI.Button(new Rect(anchoPant*okX,altoPant*okDontY,anchoPant*okDontX2,altoPant*okDontY2), "", okBtnStyle))
			{
				// Sonido boton
				audio.Play();
				borrarDatosGenerales();
				Application.LoadLevel ("IntroScene");
			}
			if(GUI.Button(new Rect(anchoPant*dontX,altoPant*okDontY,anchoPant*okDontX2,altoPant*okDontY2), "", dontBtnStyle))
			{
				newGameStyle.normal.background = newGame;
				// Sonido boton
				audio.Play();
				areYouSure = false;
			}
		}

		if(GUI.Button(new Rect(anchoPant*creditX,altoPant*creditY,anchoPant*creditX2,altoPant*creditY2), "", creditsBtnStyle))
		{
			creditsBtnStyle.normal.background = credits2;
			// Sonido boton
			audio.Play();
			Application.LoadLevel ("Credits");
		}
	}

	void VerificarTexturaPlay(){
		if(PlayerPrefs.GetInt("isPlayed")==0){
			playStyle.normal.background = texturePlay;
		}else{
			playStyle.normal.background = textureContinue;
		}
	}

	void borrarDatosGenerales(){
		//Verifica si se paso la escena 1 o no
		PlayerPrefs.SetInt("FirstSceneOk", 0);
		//Para verificar que antes ha jugado
		PlayerPrefs.SetString("itemEquipado", string.Empty);
		//Para verificar que antes ha jugado
		PlayerPrefs.SetInt("isPlayed",0);
		//Var que verifica si las cadenas se han cortado
		PlayerPrefs.SetInt("chainsCutted",0);
		//Verificacion si la vela se ha prendido o no en House3
		PlayerPrefs.SetInt("velaPrendida",0);
		//Vela que se instancia en House 3
		PlayerPrefs.SetInt("velaInstanciada",0);
		//Balas en ToolHouse
		PlayerPrefs.SetInt("bulletsToolHousePicked",0);
		//Balas de bathroom
		PlayerPrefs.SetInt("bulletBathroomPicked",0);
		//Puerta en House2, que se abre con destornillador
		PlayerPrefs.SetInt("doorInOpen",0);
		//animacion de estatua que entrega llave
		PlayerPrefs.SetInt("statueGive",0);
		//animacion de armario
		PlayerPrefs.SetInt("armarioOpen",0);
		PlayerPrefs.SetInt("gunBullets", 0);
		PlayerPrefs.SetInt("gunBulletsInv", 0);
		//Variables para los 8 slots del inventory
		PlayerPrefs.SetString("item1", string.Empty);
		PlayerPrefs.SetString("item2", string.Empty);
		PlayerPrefs.SetString("item3", string.Empty);
		PlayerPrefs.SetString("item4", string.Empty);
		PlayerPrefs.SetString("item5", string.Empty);
		PlayerPrefs.SetString("item6", string.Empty);
		PlayerPrefs.SetString("item7", string.Empty);
		PlayerPrefs.SetString("item8", string.Empty);
		//Variables para ver el estado de los items
		PlayerPrefs.SetInt("gunItemPicked", 0);
		PlayerPrefs.SetInt("keyItemPicked", 0);
		PlayerPrefs.SetInt("ringItemPicked", 0);
		PlayerPrefs.SetInt("crowbarItemPicked", 0);
		PlayerPrefs.SetInt("candleItemPicked", 0);
		PlayerPrefs.SetInt("destornilladorItemPicked", 0);
		PlayerPrefs.SetInt("ligtherItemPicked", 0);
		PlayerPrefs.SetInt("chainCutterItemPicked", 0);
		//Variable para ver si ya se ingreso en ToolHouse
		PlayerPrefs.SetInt("canEnterToolHouse", 0);
		//Variable para ver si ya se ingreso en House
		PlayerPrefs.SetInt("canEnterHouse", 0);
		//Variable de instanciamiento del player
		PlayerPrefs.SetString("playerInstance","");
		//Variable para verificar si el boss1 se ha matado o no
		PlayerPrefs.SetInt("boss1Kill",0);
	}
}
