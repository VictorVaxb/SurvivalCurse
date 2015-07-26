using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
	public bool showItems = false;
	private string texto = "Select an item to equip";

	public GUISkin guiSkin;
	public GUISkin guiSkin2;
	//Estilos Botones
	public GUIStyle playerStyle;
	public GUIStyle itemsStyle1;
	public GUIStyle itemsStyle2;
	public GUIStyle itemsStyle3;
	public GUIStyle itemsStyle4;
	public GUIStyle itemsStyle5;
	public GUIStyle itemsStyle6;
	public GUIStyle itemsStyle7;
	public GUIStyle itemsStyle8;
	public GUIStyle backStyle;
	public GUIStyle detallItemStyle;
	public GUIStyle detallItemTextStyle;
	public GUIStyle imagenBullets;
	public GUIStyle lifeBarSector;
	public GUIStyle lifeBar;
	public GUIStyle reloadStyle;
	//BOTONES ITEMS
	private int contador = 1;
	public int numBotones = 8;
	public float botItemX = 0.65f;
	public float botItemY = 0.05f;
	public float separacionX = 0.17f;
	public float separacionY = 0.2f;
	public float botItemX2 = 0.16f;
	public float botItemY2 = 0.18f;
	//boton BACK
	public float backBtnX = 0.71f;
	public float backBtnY = 0.85f;
	public float backBtnX2 = 0.2f;
	public float backBtnY2 = 0.1f;
	//Imagen PERSONAJE
	public float playerX = 0.02f;
	public float playerY = 0.02f;
	public float playerX2 = 0.15f;
	public float playerY2 = 0.29f;
	//Imagen DETALLE DE ITEMS
	public float detItemX = 0.02f;
	public float detItemY = 0.33f;
	public float detItemX2 = 0.61f;
	public float detItemY2 = 0.35f;
	//Imagen Bullets
	private float bulletImgX = 0.4f;
	private float bulletImgY = 0.01f;
	private float bulletImgX2 = 0.24f;
	private float bulletImgY2 = 0.18f;
	//Label Bullets
	public float bulletLblX = 0.57f;
	public float bulletLblY = 0.08f;
	public float bulletX2Y2 = 0.2f;
	//Life bar Sector
	public float lifeBarSectorX = 0.18f;
	public float lifeBarSectorY = 0.2f;
	public float lifeBarSectorX2 = 0.45f;
	public float lifeBarSectorY2 = 0.11f;
	//Life bar
	public float lifeBarX2 = 0.18f;
	//Boton Recargar
	private float reloadBtnX = 0.45f;
	private float reloadBtnY = 0.4f;
	private float reloadBtnX2 = 0.156f;
	private float reloadBtnY2 = 0.09f;
	//Status Box
	private float statusBtnX = 0.17f;
	private float statusBtnY = 0.06f;
	private float statusBtnX2 = 0.22f;
	private float statusBtnY2 = 0.16f;

	public float detItemTextoY = 0.7f;
	public float detItemTextoY2 = 0.25f;
	public Texture2D[] textures;
	public Texture2D itemEquipado = null;
	public string nombreItemEquipado;
	private int x = 0;

	private string bulletNum = "0";
	PlayerMove playerMove;

	void Awake(){
		//Carga de Texturas
		textures = Resources.LoadAll<Texture2D>("Textures");
	}

	void Start(){
		SeteoTexturaInventory();
		playerMove = (PlayerMove)GameObject.FindObjectOfType(typeof(PlayerMove));
		detallItemStyle.normal.background = BuscarTextura("normal", "emptyItemDet");
		imagenBullets.normal.background = BuscarTextura("normal", "bulletGui");
		reloadStyle.normal.background = BuscarTextura("normal", "reload");
		nombreItemEquipado = PlayerPrefs.GetString("itemEquipado");
	}

	void OnGUI(){
		if(showItems){
			SeteoTexturaInventory();
			guiSkin.label.fontSize = Mathf.RoundToInt(Gui.anchoPant/22);
			bulletNum = PlayerPrefs.GetInt("gunBulletsInv").ToString();
			detallItemTextStyle.fontSize = Mathf.RoundToInt(Gui.anchoPant/25);
			detallItemTextStyle.contentOffset = new Vector2(Gui.anchoPant/55,Gui.altoPant/90);

			verificarVidaBar();

			//Fondo Inventory
			GUI.Box(new Rect(0,0,Gui.anchoPant,Gui.altoPant), "", guiSkin.window);
			//Status Box
			GUI.Box(new Rect(Gui.anchoPant*statusBtnX,Gui.altoPant*statusBtnY,Gui.anchoPant*statusBtnX2,Gui.altoPant*statusBtnY2), "", guiSkin2.box);
			//Boton BACK
			if(GUI.Button(new Rect(Gui.anchoPant*backBtnX,Gui.altoPant*backBtnY,Gui.anchoPant*backBtnX2,Gui.altoPant*backBtnY2), "", backStyle))
			{
				muestraInventory(false);
			}
			//Imagen PERSONAJE
			GUI.Box(new Rect(Gui.anchoPant*playerX,Gui.altoPant*playerY,Gui.anchoPant*playerX2,Gui.altoPant*playerY2), "", playerStyle);
			//botones items
			for(contador=1;contador<=numBotones;contador++){
				if(GUI.Button(new Rect(verificaXBot(contador),verificaYBot(contador),Gui.anchoPant*botItemX2,Gui.altoPant*botItemY2), "", verificaEstiloBoton(contador)))
				{
					// Audio de botton
					audio.Play();
					//DEBO CAMBIAR IMAGEN EN DETALLE
					SetearDetalleYTextoItem(contador);
				}
			}
			//Detalle de ITEM IMAGEN
			GUI.Box(new Rect(Gui.anchoPant*detItemX,Gui.altoPant*detItemY,Gui.anchoPant*detItemX2,Gui.altoPant*detItemY2), "", detallItemStyle);
			//Detalle de ITEM TEXTO, Usa Texto debido a que es info del Item
			GUI.Box(new Rect(Gui.anchoPant*detItemX,Gui.altoPant*detItemTextoY,Gui.anchoPant*detItemX2,Gui.altoPant*detItemTextoY2), texto, detallItemTextStyle);
			//Textura bullets cantidad
			GUI.Box(new Rect(Gui.anchoPant*bulletImgX,Gui.altoPant*bulletImgY,Gui.anchoPant*bulletImgX2,Gui.altoPant*bulletImgY2), "", imagenBullets);
			//Textura life bar, se debe cambiar de color segun vida tenga player
			GUI.Box(new Rect(Gui.anchoPant*(lifeBarSectorX+0.03f),Gui.altoPant*(lifeBarSectorY+0.03f),Gui.anchoPant*lifeBarX2,Gui.altoPant*(lifeBarSectorY2-0.03f)), "", lifeBar);
			//Textura life bar sector
			GUI.Box(new Rect(Gui.anchoPant*lifeBarSectorX,Gui.altoPant*lifeBarSectorY,Gui.anchoPant*lifeBarSectorX2,Gui.altoPant*lifeBarSectorY2), "", lifeBarSector);
			//Label Numero de Bullets en Inventario
			GUI.Label(new Rect(Gui.anchoPant*bulletLblX,Gui.altoPant*bulletLblY,Gui.anchoPant*bulletX2Y2,Gui.altoPant*bulletX2Y2), bulletNum, guiSkin.label);
			//Boton recargar pistola
			if(detallItemStyle.normal.background.name == "gunItemDet"){
				if(GUI.Button(new Rect(Gui.anchoPant*reloadBtnX,Gui.altoPant*reloadBtnY,Gui.anchoPant*reloadBtnX2,Gui.altoPant*reloadBtnY2), "", reloadStyle))
				{
					playerMove.recargar();
				}
			}
			if(BuscarNumGun()>0){
				guiSkin2.label.fontSize = Mathf.RoundToInt(Gui.anchoPant/25);
				guiSkin2.label.contentOffset = new Vector2(Gui.anchoPant/50,Gui.altoPant/230);
				//Label Numero de Bullets en pistola
				GUI.Label(new Rect(verificaXBot(BuscarNumGun()),verificaYBot(BuscarNumGun()),Gui.anchoPant*botItemX2,Gui.altoPant*botItemY2), PlayerPrefs.GetInt("gunBullets").ToString(), guiSkin2.label);
			}
		}
	}

	private int BuscarNumGun(){
		if(itemsStyle1.normal.background.name == "gunItem"){
			return 1;
		}else if(itemsStyle2.normal.background.name == "gunItem"){
			return 2;
		}else if(itemsStyle3.normal.background.name == "gunItem"){
			return 3;
		}else if(itemsStyle4.normal.background.name == "gunItem"){
			return 4;
		}else if(itemsStyle5.normal.background.name == "gunItem"){
			return 5;
		}else if(itemsStyle6.normal.background.name == "gunItem"){
			return 6;
		}else if(itemsStyle7.normal.background.name == "gunItem"){
			return 7;
		}else if(itemsStyle8.normal.background.name == "gunItem"){
			return 8;
		}else{
			return 0;
		}
	}

	//Se debe eliminar textura cuando se utiliza un Item
	public void EliminarTextura(string nombre){
		SetearNombreItemEquipado("emptyItem");		//Setea variable item equipado, para control
		if(itemsStyle1.normal.background.name == nombre){
			itemsStyle1.normal.background = BuscarTextura("normal", "emptyItem");
			CambiarTextoItemDet(itemsStyle1.normal.background.name);
			itemEquipado = itemsStyle1.normal.background;
			PlayerPrefs.SetString("item1", string.Empty);
		}else if(itemsStyle2.normal.background.name == nombre){
			itemsStyle2.normal.background = BuscarTextura("normal", "emptyItem");
			CambiarTextoItemDet(itemsStyle2.normal.background.name);
			itemEquipado = itemsStyle2.normal.background;
			PlayerPrefs.SetString("item2", string.Empty);
		}else if(itemsStyle3.normal.background.name == nombre){
			itemsStyle3.normal.background = BuscarTextura("normal", "emptyItem");
			CambiarTextoItemDet(itemsStyle3.normal.background.name);
			itemEquipado = itemsStyle3.normal.background;
			PlayerPrefs.SetString("item3", string.Empty);
		}else if(itemsStyle4.normal.background.name == nombre){
			itemsStyle4.normal.background = BuscarTextura("normal", "emptyItem");
			CambiarTextoItemDet(itemsStyle4.normal.background.name);
			itemEquipado = itemsStyle4.normal.background;
			PlayerPrefs.SetString("item4", string.Empty);
		}else if(itemsStyle5.normal.background.name == nombre){
			itemsStyle5.normal.background = BuscarTextura("normal", "emptyItem");
			CambiarTextoItemDet(itemsStyle5.normal.background.name);
			itemEquipado = itemsStyle5.normal.background;
			PlayerPrefs.SetString("item5", string.Empty);
		}else if(itemsStyle6.normal.background.name == nombre){
			itemsStyle6.normal.background = BuscarTextura("normal", "emptyItem");
			CambiarTextoItemDet(itemsStyle6.normal.background.name);
			itemEquipado = itemsStyle6.normal.background;
			PlayerPrefs.SetString("item6", string.Empty);
		}else if(itemsStyle7.normal.background.name == nombre){
			itemsStyle7.normal.background = BuscarTextura("normal", "emptyItem");
			CambiarTextoItemDet(itemsStyle7.normal.background.name);
			itemEquipado = itemsStyle7.normal.background;
			PlayerPrefs.SetString("item7", string.Empty);
		}else{
			itemsStyle8.normal.background = BuscarTextura("normal", "emptyItem");
			CambiarTextoItemDet(itemsStyle8.normal.background.name);
			itemEquipado = itemsStyle8.normal.background;
			PlayerPrefs.SetString("item8", string.Empty);
		}
		//Independiente de los botones el detalle imagen sera null
		detallItemStyle.normal.background = BuscarTextura("normal","emptyItemDet");
	}

	void SetearDetalleYTextoItem(int num){
		if(num == 1){
			detallItemStyle.normal.background = BuscarTextura("detalleItem", itemsStyle1.normal.background.name);
			CambiarTextoItemDet(itemsStyle1.normal.background.name);
			SetearNombreItemEquipado(itemsStyle1.normal.background.name);
			itemEquipado = itemsStyle1.normal.background;
		}else if(num == 2){
			detallItemStyle.normal.background = BuscarTextura("detalleItem", itemsStyle2.normal.background.name);
			CambiarTextoItemDet(itemsStyle2.normal.background.name);
			SetearNombreItemEquipado(itemsStyle2.normal.background.name);
			itemEquipado = itemsStyle2.normal.background;
		}else if(num == 3){
			detallItemStyle.normal.background = BuscarTextura("detalleItem", itemsStyle3.normal.background.name);
			CambiarTextoItemDet(itemsStyle3.normal.background.name);
			SetearNombreItemEquipado(itemsStyle3.normal.background.name);
			itemEquipado = itemsStyle3.normal.background;
		}else if(num == 4){
			detallItemStyle.normal.background = BuscarTextura("detalleItem", itemsStyle4.normal.background.name);
			CambiarTextoItemDet(itemsStyle4.normal.background.name);
			SetearNombreItemEquipado(itemsStyle4.normal.background.name);
			itemEquipado = itemsStyle4.normal.background;
		}else if(num == 5){
			detallItemStyle.normal.background = BuscarTextura("detalleItem", itemsStyle5.normal.background.name);
			CambiarTextoItemDet(itemsStyle5.normal.background.name);
			SetearNombreItemEquipado(itemsStyle5.normal.background.name);
			itemEquipado = itemsStyle5.normal.background;
		}else if(num == 6){
			detallItemStyle.normal.background = BuscarTextura("detalleItem", itemsStyle6.normal.background.name);
			CambiarTextoItemDet(itemsStyle6.normal.background.name);
			SetearNombreItemEquipado(itemsStyle6.normal.background.name);
			itemEquipado = itemsStyle6.normal.background;
		}else if(num == 7){
			detallItemStyle.normal.background = BuscarTextura("detalleItem", itemsStyle7.normal.background.name);
			CambiarTextoItemDet(itemsStyle7.normal.background.name);
			SetearNombreItemEquipado(itemsStyle7.normal.background.name);
			itemEquipado = itemsStyle7.normal.background;
		}else{
			detallItemStyle.normal.background = BuscarTextura("detalleItem", itemsStyle8.normal.background.name);
			CambiarTextoItemDet(itemsStyle8.normal.background.name);
			SetearNombreItemEquipado(itemsStyle8.normal.background.name);
			itemEquipado = itemsStyle8.normal.background;
		}
	}

	//Setea variable item equipado, para controls.cs
	void SetearNombreItemEquipado(string nombre){
		PlayerPrefs.SetString("itemEquipado",nombre);
	}

	public void llegaNuevoItem(string nombreItem){
		if(nombreItem != "bulletsItem"){
			//Debo cambiar la variables Save de los Items
			CambiarEstadoPickeadoItem(nombreItem);
			//SE ALMACENA TEXTURA ITEM Y SE DESTRUYE OBJEtO
			if(PlayerPrefs.GetString("item1") == string.Empty){
				itemsStyle1.normal.background = BuscarTextura("normal", nombreItem);
				PlayerPrefs.SetString("item1", nombreItem);
			}
			else if(PlayerPrefs.GetString("item2") == string.Empty){
				itemsStyle2.normal.background = BuscarTextura("normal", nombreItem);
				PlayerPrefs.SetString("item2", nombreItem);
			}
			else if(PlayerPrefs.GetString("item3") == string.Empty){
				itemsStyle3.normal.background = BuscarTextura("normal", nombreItem);
				PlayerPrefs.SetString("item3", nombreItem);
			}
			else if(PlayerPrefs.GetString("item4") == string.Empty){
				itemsStyle4.normal.background = BuscarTextura("normal", nombreItem);
				PlayerPrefs.SetString("item4", nombreItem);
			}
			else if(PlayerPrefs.GetString("item5") == string.Empty){
				itemsStyle5.normal.background = BuscarTextura("normal", nombreItem);
				PlayerPrefs.SetString("item5", nombreItem);
			}
			else if(PlayerPrefs.GetString("item6") == string.Empty){
				itemsStyle6.normal.background = BuscarTextura("normal", nombreItem);
				PlayerPrefs.SetString("item6", nombreItem);
			}
			else if(PlayerPrefs.GetString("item7") == string.Empty){
				itemsStyle7.normal.background = BuscarTextura("normal", nombreItem);
				PlayerPrefs.SetString("item7", nombreItem);
			}
			else if(PlayerPrefs.GetString("item8") == string.Empty){
				itemsStyle8.normal.background = BuscarTextura("normal", nombreItem);
				PlayerPrefs.SetString("item8", nombreItem);
			}
		}else if(nombreItem == "bulletsItem"){
			//Se debe cambiar variable para que no se instancie mas
			if(PlayerPrefs.GetString("playerInstance") == "ToolHouse"){
				PlayerPrefs.SetInt("bulletsToolHousePicked",1);
			}
			if(PlayerPrefs.GetString("playerInstance") == "BathRoom"){
				PlayerPrefs.SetInt("bulletBathroomPicked",1);
			}
		}
		Destroy(GameObject.Find(nombreItem));
	}

	void CambiarEstadoPickeadoItem(string itemPickeado){
		//Debo cambiar el estado del item itemPickeado)
		PlayerPrefs.SetInt(itemPickeado+"Picked", 1);
	}

	void SeteoTexturaInventory(){
		playerStyle.normal.background = BuscarTextura("normal", "playerFace");
		if(PlayerPrefs.GetString("item1")==string.Empty){
			itemsStyle1.normal.background = BuscarTextura("normal", "emptyItem");
		}else{
			itemsStyle1.normal.background = BuscarTextura("normal", PlayerPrefs.GetString("item1"));
		}
		if(PlayerPrefs.GetString("item2")==string.Empty){
			itemsStyle2.normal.background = BuscarTextura("normal", "emptyItem");
		}else{
			itemsStyle2.normal.background = BuscarTextura("normal", PlayerPrefs.GetString("item2"));
		}
		if(PlayerPrefs.GetString("item3")==string.Empty){
			itemsStyle3.normal.background = BuscarTextura("normal", "emptyItem");
		}else{
			itemsStyle3.normal.background = BuscarTextura("normal", PlayerPrefs.GetString("item3"));
		}
		if(PlayerPrefs.GetString("item4")==string.Empty){
			itemsStyle4.normal.background = BuscarTextura("normal", "emptyItem");
		}else{
			itemsStyle4.normal.background = BuscarTextura("normal", PlayerPrefs.GetString("item4"));
		}
		if(PlayerPrefs.GetString("item5")==string.Empty){
			itemsStyle5.normal.background = BuscarTextura("normal", "emptyItem");
		}else{
			itemsStyle5.normal.background = BuscarTextura("normal", PlayerPrefs.GetString("item5"));
		}
		if(PlayerPrefs.GetString("item6")==string.Empty){
			itemsStyle6.normal.background = BuscarTextura("normal", "emptyItem");
		}else{
			itemsStyle6.normal.background = BuscarTextura("normal", PlayerPrefs.GetString("item6"));
		}
		if(PlayerPrefs.GetString("item7")==string.Empty){
			itemsStyle7.normal.background = BuscarTextura("normal", "emptyItem");
		}else{
			itemsStyle7.normal.background = BuscarTextura("normal", PlayerPrefs.GetString("item7"));
		}
		if(PlayerPrefs.GetString("item8")==string.Empty){
			itemsStyle8.normal.background = BuscarTextura("normal", "emptyItem");
		}else{
			itemsStyle8.normal.background = BuscarTextura("normal", PlayerPrefs.GetString("item8"));
		}
	}

	void CambiarTextoItemDet(string item){
		if(item == "ringItem"){
			texto = "Old Ring, it look's very old";
		}
		else if(item == "ligtherItem"){
			texto = "Ligther, maybe i need it";
		}
		else if(item == "crowbarItem"){
			texto = "Crowbar used for tables or something";
		}
		else if(item == "gunItem"){
			texto = "Gun for defend myself";
		}
		else if(item == "candleItem"){
			texto = "Candle, looks suspicious";
		}
		else if(item == "destornilladorItem"){ 
			texto = "screwdriver, tool very useful";
		}
		else if(item == "keyItem"){ 
			texto = "old standard key";
		}
		else if(item == "chainCutterItem"){ 
			texto = "Perfect tool for cutting chains";
		}
		else if(item == "axeItem"){
			texto = "Axe, if i need to cut something";
		}
		else if(item == "gataItem"){
			texto = "hydraulic jack, in case there is something heavy to lift";
		}
		else{
			texto = "Select an item to equip";
		}
	}

	private float verificaXBot(int pos){
		if(pos%2==0){
			return Gui.anchoPant*botItemX + (separacionX * Gui.anchoPant);
		}else{
			return Gui.anchoPant*botItemX;
		}
	}
	
	private float verificaYBot(int pos){
		if(pos==1 || pos==2){
			return Gui.altoPant*botItemY;
		}
		else if(pos==3 || pos==4){
			return Gui.altoPant*botItemY + (separacionY * Gui.altoPant);
		}
		else if(pos==5 || pos==6){
			return Gui.altoPant*botItemY + (2 * (separacionY * Gui.altoPant));
		}
		else if(pos==7 || pos==8){
			return Gui.altoPant*botItemY + (3 * (separacionY * Gui.altoPant));
		}else{
			return Gui.altoPant*botItemY;
		}
	}

	public void muestraInventory(bool accion){
		if(accion){
			showItems = true;
		}else{
			showItems = false;
		}
	}
	
	public bool consultaInventory(){
		return showItems;
	}

	//Accion Normal = busqueda normal, detalle Item, es para la caja de texto con detalle sobre Item
	public Texture2D BuscarTextura(string accion, string nombreTextura){
		//Normal sol busca la textura mediante el nombre
		if(accion == "normal"){
			for(x = 0;x<textures.Length;x++){
				if(textures[x].name == nombreTextura){
					return textures[x];
				}
			}
			return textures[0];
			//detalle Item es para buscar la textura para mostrar el detalle
		}else if(accion == "detalleItem"){
			for(x = 0;x<textures.Length;x++){
				if(textures[x].name == nombreTextura + "Det"){
					return textures[x];
				}
			}
			return textures[0];
		}else{
			return textures[0];
		}
	}
	
	private GUIStyle verificaEstiloBoton(int num){
		if(num == 1){
			return itemsStyle1;
		}else if(num == 2){
			return itemsStyle2;
		}else if(num == 3){
			return itemsStyle3;
		}else if(num == 4){
			return itemsStyle4;
		}else if(num == 5){
			return itemsStyle5;
		}else if(num == 6){
			return itemsStyle6;
		}else if(num == 7){
			return itemsStyle7;
		}else{ 
			return itemsStyle8;
		}
	}

	void verificarVidaBar(){
		lifeBarX2 = 0.4f*(float.Parse(playerMove.lifePlayer.ToString())/100);
		if(playerMove.lifePlayer >= 70){
			lifeBar.normal.background = BuscarTextura("normal","lifeBarGreen");
		}else if(playerMove.lifePlayer >= 50 && playerMove.lifePlayer < 70){
			lifeBar.normal.background = BuscarTextura("normal","lifeBarYellow");
		}else if(playerMove.lifePlayer >= 30 && playerMove.lifePlayer < 50){
			lifeBar.normal.background = BuscarTextura("normal","lifeBarOrange");
		}else if(playerMove.lifePlayer < 30){
			lifeBar.normal.background = BuscarTextura("normal","lifeBarRed");
		}
	}
}
