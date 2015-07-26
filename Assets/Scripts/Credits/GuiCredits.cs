using UnityEngine;
using System.Collections;

public class GuiCredits : MonoBehaviour {
	private float anchoPant = 0.0f;
	private float altoPant = 0.0f;

	//Estilo texto
	public GUIStyle myStyle;

	//Variables Back btn
	private float backBtnX = 0.84f;
	private float backBtnY = 0.02f;
	private float backBtnXY = 0.12f;

	public GUIStyle backStyle;

	// Texturas Back
	public Texture2D back1;
	public Texture2D back2;

	public GUISkin guiSkin;

	public Texture2D fondo;

	//Variables labels
	public float lblX = 0.05f;
	public float lblY = 0.05f;
	public float lbl1Y = 0.1f;

	//Lineas
	public float lblY2 = 0.15f;
	public float lblY3 = 0.11f;
	public float lblY4 = 0.14f;
	public float lblY5 = 0.15f;
	public float lblY6 = 0.05f;
	public float lblY7 = 0.05f;

	//Otro X
	private float lblX2 = 0.5f;
	
	void Update () {
		anchoPant = Screen.width;
		altoPant = Screen.height;
	}

	void OnGUI(){
		myStyle.fontSize = Mathf.RoundToInt(anchoPant/16);
		myStyle.contentOffset = new Vector2(anchoPant/45,altoPant/60);
		GUI.DrawTexture (new Rect(0,0,anchoPant,altoPant), fondo);
		guiSkin.label.fontSize = Mathf.RoundToInt(anchoPant/20);
		if(GUI.Button(new Rect(anchoPant*backBtnX,altoPant*backBtnY,anchoPant*backBtnXY,altoPant*backBtnXY), "", backStyle))
		{
			backStyle.normal.background = back2;
			// Sonido boton
			audio.Play();
			Application.LoadLevel ("MainMenu");
		}

		//Label Credits
		GUI.Label(new Rect(anchoPant*lblX,altoPant*lblY,anchoPant,altoPant), "Credits", guiSkin.label);
		//Segunda Linea
		GUI.Label(new Rect(anchoPant*lblX,altoPant*lblY2,anchoPant,altoPant), "Programmer", myStyle);
		GUI.Label(new Rect(anchoPant*lblX2,altoPant*lblY2,anchoPant,altoPant), "Victor Peñaloza", myStyle);
		//Tercera Linea
		GUI.Label(new Rect(anchoPant*lblX,altoPant*lblY3,anchoPant,altoPant), "Music", myStyle);
		//Cuarta linea
		GUI.Label(new Rect(anchoPant*lblX,altoPant*lblY4,anchoPant,altoPant), "Marc Teichert", myStyle);
		GUI.Label(new Rect(anchoPant*lblX2,altoPant*lblY4,anchoPant,altoPant), "David Palmero", myStyle);
		//Quinta linea
		GUI.Label(new Rect(anchoPant*lblX,altoPant*lblY5,anchoPant,altoPant), "Christoph Burghardt", myStyle);
		GUI.Label(new Rect(anchoPant*lblX2,altoPant*lblY5,anchoPant,altoPant), "LizZarDino", myStyle);
		//Sexta linea
		GUI.Label(new Rect(anchoPant*lblX,altoPant*lblY6,anchoPant,altoPant), "Lzn02", myStyle);
	}
}
