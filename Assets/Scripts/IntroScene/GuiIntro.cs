using UnityEngine;
using System.Collections;

public class GuiIntro : MonoBehaviour {
	public static bool mostrarTexto = false;
	public static float anchoPant = 0.0f;
	public static float altoPant = 0.0f;
	//Texto a mostrar en dialogo
	private string stringToEdit = "My name is freddy, i have 17 years old, my little brother died 3 years ago";
	//Variables para typewritr Effect
	private string texto = "";
	public int largoTexto = 0;
	//Estilo texto
	public GUIStyle myStyle;
	//Estilo boton OK
	public GUIStyle btnOkStyle;
	private float alto = 0.3f;
	public Texture2D fondoTexto;
	//Variables de Label
	private float labelX = 0.04f;
	private float labelY = 0.02f;
	private float labelX2 = 0.9f;
	//velocidad de typewriter
	private float speedWrite = 0.06f;
	private float timeVar = 0.0f;
	private bool showBtnOk = false;
	//Variables boton OK
	private float btnOkX = 0.82f;
	private float btnOkY = 0.78f;
	private float btnOkX2 = 0.14f;
	private float btnOkY2 = 0.12f;
	//variable para ver el avance
	public static int posicionCam = 1;

	void Awake(){
		posicionCam = 1;
	}

	void Start () {
		anchoPant = Screen.width;
		altoPant = Screen.height;
	}

	void Update () {
		anchoPant = Screen.width;
		altoPant = Screen.height;
	}

	void OnGUI() {
		myStyle.fontSize = Mathf.RoundToInt(anchoPant/16);
		myStyle.contentOffset = new Vector2(anchoPant/45,altoPant/60);
		if(mostrarTexto){
			verificaLargo();
			GUI.DrawTexture(new Rect(0,0,anchoPant,altoPant*alto), fondoTexto);
			GUI.Label(new Rect(anchoPant*labelX,altoPant*labelY,anchoPant*labelX2,0), texto, myStyle);
		}else{
			largoTexto = 0;
			stringToEdit = "";
			timeVar = 0.0f;
		}
		if(showBtnOk){
			if(GUI.Button(new Rect(anchoPant*btnOkX,altoPant*btnOkY,anchoPant*btnOkX2,anchoPant*btnOkY2), "", btnOkStyle)){
				audio.Play();
				posicionCam++;
				mostrarTexto = false;
				showBtnOk = false;
				largoTexto = 0;
				stringToEdit = "";
				timeVar = 0.0f;
			}
		}
	}

	void verificaLargo(){
		SetearTexto ();
		timeVar = timeVar + Time.deltaTime;
		if(largoTexto < stringToEdit.Length){
			if(timeVar > speedWrite){
				largoTexto++;
				timeVar = 0.0f;
			}
			texto = stringToEdit.Substring(0, largoTexto);
		}else{
			showBtnOk = true;
		}
	}

	void SetearTexto(){
		if(posicionCam == 1){
			stringToEdit = "My name is freddy, i have 17 years old, my little brother died 3 years ago";
		}else if(posicionCam == 2){
			stringToEdit = "In my house all was quiet until ....";
		}else if(posicionCam == 3){
			stringToEdit = "My brother showed up at my window, whispering to help";
		}else{
			stringToEdit = "The next morning, I go to the cottage, where everything happened";
		}
	}
}
