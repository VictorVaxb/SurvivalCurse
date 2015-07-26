using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour {
	//Texto a mostrar en dialogo
	public string stringToEdit = "";
	//Texto a modificar para que sea typewrite
	private string texto = "";
	//velocidad de typewriter
	private float speedWrite = 0.06f;
	private float timeVar = 0.0f;
	public static float anchoPant = 0.0f;
	public static float altoPant = 0.0f;
	public float alto = 0.0f;
	//Variables para typewritr Effect
	public int largoTexto = 0;
	public bool mostrarTexto = false;
	public float textoSpeed = 0.1f;
	//Variables de Label
	private float labelX = 0.02f;
	private float labelY = 0.03f;
	private float labelX2 = 0.9f;

	public GUIStyle myStyle;
	//Se usa Inventory para eliminar textura en el inventario
	Inventory inventory;

	void Start () {
		anchoPant = Screen.width;
		altoPant = Screen.height;
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
	}

	void Update () {
		myStyle.fontSize = Mathf.RoundToInt(anchoPant/18);
		myStyle.contentOffset = new Vector2(anchoPant/45,altoPant/60);
		anchoPant = Screen.width;
		altoPant = Screen.height;
		/*
		if(intervaloText < Time.time && largoTexto < 100 && mostrarTexto){
			largoTexto++;
			intervaloText = Time.time + textoSpeed;
		}else{
			if(!mostrarTexto){
				largoTexto = 0;
				intervaloText = 0.0f;
			}
		}
		*/
	}

	void OnGUI() {
		if(mostrarTexto){
			verificaLargo();
			GUI.DrawTexture(new Rect(0,0,anchoPant,altoPant*alto), inventory.BuscarTextura("normal","boxInfo"));
			GUI.Label(new Rect(anchoPant*labelX,altoPant*labelY,anchoPant*labelX2,0), texto, myStyle);
			//GUI.TextArea(new Rect(0,0,anchoPant,altoPant*alto), stringToEdit, largoTexto, myStyle);
		}else{
			texto = "";
			timeVar = 0.0f;
			largoTexto = 0;
		}
	}

	void verificaLargo(){
		timeVar = timeVar + Time.deltaTime;
		if(largoTexto < stringToEdit.Length){
			if(timeVar > speedWrite){
				largoTexto++;
				timeVar = 0.0f;
			}
			if(largoTexto == stringToEdit.Length){
				texto = stringToEdit;
			}else{
				texto = stringToEdit.Substring(0, largoTexto);
			}
		}
	}

	public void SetObjetoMostrar(string nombreObj, string itemEquipado){
		if(mostrarTexto){
			mostrarTexto = false;
			stringToEdit = "";
		}else{
			if(nombreObj == "auto"){		//AUTO
				stringToEdit = "I could not avoid the crash with tree, my car is ruined";
				mostrarTexto = true;
			}else if(nombreObj == "statue"){
				if(itemEquipado == "ringItem"){
					mostrarTexto = false;
					stringToEdit = "";
					//Se Cambia textura de Item equipado a null
					//Se debe eliminar la textura de Anillo en el Inventory
					inventory.EliminarTextura(itemEquipado);
				}else{
					stringToEdit = "Statue of a woman, it is as if asking something";
					mostrarTexto = true;
				}
			}else if(nombreObj == "doorIn"){		//PUERTA DE HOUSE1
				stringToEdit = "This door is locked, maybe i can open with someting";
				mostrarTexto = true;
			}else if(nombreObj == "ToolHouse"){		//TOOL HOUSE
				if(itemEquipado == "keyItem"){
					mostrarTexto = false;
					stringToEdit = "";
					//Se Cambia textura de Item equipado a null,Se debe eliminar la textura de Anillo en el Inventory
					inventory.EliminarTextura(itemEquipado);
				}else{
					stringToEdit = "The door is locked, apparently is a simple lock";
					mostrarTexto = true;
				}
			}else if(nombreObj == "House1"){		//ENTRADA A HOUSE1
				stringToEdit = "I can not enter the house, there are boards nailed on the door";
				mostrarTexto = true;
			}else if(nombreObj == "mascara"){		//MASCARA AFRICANA
				stringToEdit = "it's just an African mask, nothing to worry about";
				mostrarTexto = true;
			}else if(nombreObj == "pedestal"){		//PEDESTAL PARA VELA
				stringToEdit = "base to put something on";
				mostrarTexto = true;
			}else if(nombreObj == "candle"){		//VELA EN PEDESTAL
				stringToEdit = "Maybe I can turn it with something";
				mostrarTexto = true;
			}else if(nombreObj == "chains"){		//CADENAS EN PUERTA
				if(itemEquipado == "chainCutterItem"){
					mostrarTexto = false;
					stringToEdit = "";
					inventory.EliminarTextura(itemEquipado);
				}else{
					stringToEdit = "Chains do not allow me to open the door";
					mostrarTexto = true;
				}
			}else if(nombreObj == "peter"){		//PETER
				stringToEdit = "Help me, they are in the swamp, the monsters are coming";
				mostrarTexto = true;
			}else if(nombreObj == "ramas"){		//RAMAS
				stringToEdit = "This things are wood, i need something for cut";
				mostrarTexto = true;
			}else if(nombreObj == "rustyCar"){		//AUTO OXIDADO
				stringToEdit = "I wonder why this car ended up in this place";
				mostrarTexto = true;
			}else if(nombreObj == "puertaHS2"){		//PUERTA CASA SWAMP
				stringToEdit = "the door is locked, there are many things on the floor";
				mostrarTexto = true;
			}else if(nombreObj == "escalera"){		//ESCALERA SWAMP HOUSE 2
				stringToEdit = "There is a ladder up, I need something to reach the rope";
				mostrarTexto = true;
			}else if(nombreObj == "retroceder"){		//SWAMP1 PARA NO RETROCEDER
				stringToEdit = "I need not go back, I have to cross this swamp";
				mostrarTexto = true;
			}else if(nombreObj == "comoda"){		//COMODA DE SWAMP HOUSE 4
				stringToEdit = "there is something underneath, but it is very heavy to lift myself";
				mostrarTexto = true;
			}else{
				stringToEdit = "Error, agrega texto";
				mostrarTexto = true;			
			}
		}
	}
}
