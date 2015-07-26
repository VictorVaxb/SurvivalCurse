using UnityEngine;
using System.Collections;

public class GuiSS : MonoBehaviour {
	//variable que indica cuando hacer fade
	public static bool fading = false;
	private float anchoPant = 0.0f;
	private float altoPant = 0.0f;

	public GUIStyle backStyle;
	public GUIStyle okStyle;
	//SCENES
	public GUIStyle forestStyle;
	public GUIStyle swampStyle;
	//Boton Back
	private float backBtnX = 0.08f;
	private float backBtnY = 0.78f;
	private float backBtnX2 = 0.14f;
	private float backBtnY2 = 0.17f;
	//Boton Ok
	private float okBtnX = 0.78f;
	//FOREST Scene
	private float forestBtnX = 0.15f;
	private float forestBtnY = 0.3f;
	private float forestBtnX2 = 0.3f;
	private float forestBtnY2 = 0.3f;
	//SWAMP Scene
	private float swampBtnX = 0.55f;
	private float swampBtnY = 0.3f;
	private float swampBtnX2 = 0.3f;
	private float swampBtnY2 = 0.3f;
	//Variable para saber que escena se selecciono
	private string sceneSelect = "";

	//TEXTURAS
	public Texture2D forest;
	public Texture2D forestOk;
	public Texture2D swamp;
	public Texture2D swampNo;
	public Texture2D swampOk;

	void Start () {
		//////////////////////////////////////////////////////////////////////////////////////////
		PlayerPrefs.SetInt ("FirstSceneOk", 1);	// SACAR LUEGO; ES SOLO PARA PROBAR //////////////
		if(PlayerPrefs.GetInt("isPlayed")==0){
			PlayerPrefs.SetInt("isPlayed",1);
		}
		//IF PlayerPrefs.SetInt("FirstSceneOk", 1); Cambiar texturas
		if(PlayerPrefs.GetInt("FirstSceneOk") == 0){
			forestStyle.normal.background = forest;
		}else{
			forestStyle.normal.background = forestOk;
			swampStyle.normal.background = swamp;
		}
	}

	void Update () {
		anchoPant = Screen.width;
		altoPant = Screen.height;
	}

	void OnGUI(){
		if(sceneSelect == "forest"){
			AgrandarForest();
		}else if(sceneSelect == "swamp"){
			AgrandarSwamp();
		}
		//Boton BACK
		if (GUI.Button (new Rect (anchoPant * backBtnX, altoPant * backBtnY, anchoPant * backBtnX2, altoPant * backBtnY2), "", backStyle)) {
			// Sonido boton
			audio.Play();
			Application.LoadLevel ("MainMenu");
		}
		//Boton OK
		if(sceneSelect != ""){	//Se muestra solo si el jugador pincha algun boton de escena
			if(GUI.Button(new Rect(anchoPant*okBtnX,altoPant*backBtnY,anchoPant*backBtnX2,altoPant*backBtnY2), "", okStyle))
			{
				// Sonido boton
				audio.Play();
				if(sceneSelect == "forest"){
					borrarDatosForest();
					fading = true;
					StartCoroutine(IrAOtraEscena("FirstScene"));
					//Application.LoadLevel ("FirstScene");
				}else if(sceneSelect == "swamp" && (swampStyle.normal.background.name == "swamp" || swampStyle.normal.background.name == "swampOk")){
					borrarDatosSwamp();
					fading = true;
					StartCoroutine(IrAOtraEscena("Swamp1"));
					//Application.LoadLevel ("Swamp1");
				}
			}
		}
		//Boton Scena FOREST
		if(GUI.Button(new Rect(anchoPant*forestBtnX,altoPant*forestBtnY,anchoPant*forestBtnX2,altoPant*forestBtnY2), "", forestStyle))
		{
			// Sonido boton
			audio.Play();
			sceneSelect = "forest";
		}
		//Boton Scena SWAMP
		if(GUI.Button(new Rect(anchoPant*swampBtnX,altoPant*swampBtnY,anchoPant*swampBtnX2,altoPant*swampBtnY2), "", swampStyle))
		{
			// Sonido boton
			audio.Play();
			sceneSelect = "swamp";
		}
	}

	IEnumerator IrAOtraEscena(string lugar){
		//This is a coroutine
		yield return new WaitForSeconds(1.5f);
		Application.LoadLevel(lugar);
	}

	void AgrandarForest(){
		if(forestBtnX >= 0.1f){
			forestBtnX = forestBtnX - 0.01f;
		}
		if(forestBtnY >= 0.2f){
			forestBtnY = forestBtnY - 0.01f;
		}
		if(forestBtnX2 <= 0.42f){
			forestBtnX2 = forestBtnX2 + 0.01f;
		}
		if(forestBtnY2 <= 0.42f){
			forestBtnY2 = forestBtnY2 + 0.01f;
		}
		AchicarSwamp();
	}

	void AgrandarSwamp(){
		if(swampBtnX >= 0.5f){
			swampBtnX = swampBtnX - 0.01f;
		}
		if(swampBtnY >= 0.2f){
			swampBtnY = swampBtnY - 0.01f;
		}
		if(swampBtnX2 <= 0.42f){
			swampBtnX2 = swampBtnX2 + 0.01f;
		}
		if(swampBtnY2 <= 0.42f){
			swampBtnY2 = swampBtnY2 + 0.01f;
		}
		AchicarForest();
	}

	void AchicarSwamp(){
		swampBtnX = 0.55f;
		swampBtnY = 0.3f;
		swampBtnX2 = 0.3f;
		swampBtnY2 = 0.3f;
	}

	void AchicarForest(){
		forestBtnX = 0.15f;
		forestBtnY = 0.3f;
		forestBtnX2 = 0.3f;
		forestBtnY2 = 0.3f;
	}

	void borrarDatosSwamp(){
		//vida del player
		PlayerPrefs.SetInt("vidaPlayer",100);
		//variable que muetsra el menu die
		DieMenu.showMenuDie = false;
		//variable que muestra el menu del btn pausa
		BtnPausa.showMenu = false;
		//Variable de instanciamiento del player
		PlayerPrefs.SetString("playerInstance","");
		//variable para escalera en SwampHouse2
		PlayerPrefs.SetString("escaleraOpen","closed");
		//variable de la comoda en SwampHouse4
		PlayerPrefs.SetInt("comodaIsDown",0);
		//gataItem en swampMech
		PlayerPrefs.SetInt("gataItemPicked", 0);
		//hookItem en swampMech
		PlayerPrefs.SetInt("hookItemPicked", 0);
		//balas en swamphouse2
		PlayerPrefs.SetInt("bulletsSH2Picked", 0);
		//enemy2 en swamphouse4
		PlayerPrefs.SetInt("enemy2Akilled", 0);
		//Variables para los 8 slots del inventory
		PlayerPrefs.SetString("item1", "gunItem");
		PlayerPrefs.SetString("item2", string.Empty);
		PlayerPrefs.SetString("item3", string.Empty);
		PlayerPrefs.SetString("item4", string.Empty);
		PlayerPrefs.SetString("item5", string.Empty);
		PlayerPrefs.SetString("item6", string.Empty);
		PlayerPrefs.SetString("item7", string.Empty);
		PlayerPrefs.SetString("item8", string.Empty);
		PlayerPrefs.SetString("itemEquipado", string.Empty);
	}

	void borrarDatosForest(){
		//vida del player
		PlayerPrefs.SetInt("vidaPlayer",100);
		//variable que muetsra el menu die
		DieMenu.showMenuDie = false;
		//variable que muestra el menu del btn pausa
		BtnPausa.showMenu = false;
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
