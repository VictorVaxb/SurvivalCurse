using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.

	//Variable para determinar las animaciones que se gatillan antes de ir a otra escena
	//public string lugarAIrTrigger = string.Empty;

	public string sceneToGo = "";

	//vida del PLayer
	public int lifePlayer = 1;
	//daño de enemigo1
	public int danioEnemy1 = 30;
	//daño de cabeza de boss
	public int danioHead = 15;
	//daño de boss
	public int danioBoss1 = 20;

	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float minSpeed = 1f;				// Idle Speed
	private Animator anim;					// Reference to the player's animator component.
	public float h = 0.0f;
	private GameObject MirarSigno;
	private GameObject InOutSigno;

	//particula de sangre
	public GameObject playerBlood;

	public string objetoInvest = "";
	public string itemATomar = "";
	//BTN ACCION
	public bool isAction = false;
	//BTN SHOOT
	public bool isShoot = false;

	public string movement = "";

	private string itemEquipado = "";
	//lugar al que se entrara si se presiona action
	public string lugarInOut = "";
	public Transform bullet;
	public float frecDisparo = 1.0f;
	private float lastShoot = 0.0f;
	//tiempo para Shoot Anim
	public float timeShootAnim = 0.0f;
	//tiempo para Reload Anim
	public float timeReloadAnim = 0.0f;
	//tiempo para Damaged Anim
	public float timeDamagedAnim = 0.0f;
	//tiempo para Dead Anim
	public float timeDeadAnim = 0.0f;

	//El jugador esta disparando?
	private bool isShooting = false;
	//El jugador esta recargando?
	private bool isReload = false;
	//El jugador esta dañado?
	public bool isDamaged = false;

	//Total Num of Bullets
	public int bulletsNum;
	//Bullets in Gun
	public int gunBullets;

	//Sonidos del Player
	public AudioClip shootGunSound;
	public AudioClip reloadGunSound;
	public AudioClip damagedPlayerSound;
	public AudioClip deadPlayerSound;
	public AudioClip itemPickSound;
	public AudioClip doorSound;
	public AudioClip escaleraSound;

	Gui gui;
	Inventory inventory;
	Animations animations;

	void Awake(){
		anim = GetComponent<Animator>();
		MirarSigno = this.transform.FindChild("eyeIcon").gameObject;
		MirarSigno.gameObject.renderer.enabled = false;
		InOutSigno = this.transform.FindChild("inOut").gameObject;
		InOutSigno.gameObject.renderer.enabled = false;
		gui = (Gui)GameObject.FindObjectOfType(typeof(Gui));
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
		animations = (Animations)GameObject.FindObjectOfType(typeof(Animations));
	}

	void Start(){
		lifePlayer = PlayerPrefs.GetInt("vidaPlayer");
	}

	void Update(){
		//Si el jugador esta vivo
		if(lifePlayer > 0){
			if(inventory.itemEquipado != null){
				itemEquipado = inventory.itemEquipado.name;
			}else{
				itemEquipado = PlayerPrefs.GetString("itemEquipado");
			}

			if(anim.GetCurrentAnimatorStateInfo(0).IsName("damagedPlayer")){
				isDamaged = true;
			}else{
				isDamaged = false;
			}
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("gunShootPlayer")){
				isShooting = true;
			}else{
				isShooting = false;
			}
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("reloadGun")){
				isReload = true;
			}else{
				isReload = false;
			}

			if(!BtnPausa.showMenu && !gui.mostrarTexto && !inventory.showItems){
				anim.speed = 1.0f;
				// Cache the horizontal input.
				if(movement == "der"){
					h = 1.0f; 
				}else if(movement == "izq"){
					h = -1.0f;
				}else{
					h = Input.GetAxis("Horizontal");
					if(h > 0.05f){
						h = 1.0f;
					}else if(h < -0.05f){
						h = -1.0f;
					}
				}

				// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
				if(h * rigidbody2D.velocity.x < maxSpeed && !isShooting && !isReload && !isDamaged){
					// ... add a force to the player.
					rigidbody2D.AddForce(Vector2.right * h * moveForce);
				}
				// If the player's horizontal velocity is greater than the maxSpeed...
				if(Mathf.Abs(rigidbody2D.velocity.x) >= maxSpeed){
					if(!isShooting && !isReload && !isDamaged){
					// ... set the player's velocity to the maxSpeed in the x axis.
						rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
					}
					anim.Play(verificaAnimacion());
					
				}else{
					if(Mathf.Abs(rigidbody2D.velocity.x) < minSpeed){
						anim.Play(verificaAnimacion());
					}
				}
				// If the input is moving the player right and the player is facing left...
				if(h > 0 && !facingRight){
					// ... flip the player.
					Flip();
				}
				// Otherwise if the input is moving the player left and the player is facing right...
				else if(h < 0 && facingRight){
					// ... flip the player.
					Flip();
				}
			}else{
				anim.speed = 0.0f;
			}
			//TOMAR ITEMS O INVESTIGAR
			AccionArealizar();
			isAction = false;
			isShoot = false;
		}else{
			//Si el jugador esta muerto
			anim.Play(verificaAnimacion());
		}
	}

	void AccionArealizar(){
		if(!BtnPausa.showMenu && !inventory.showItems && isAction){
			//Investigar
			if(objetoInvest !="" && itemATomar =="" && lugarInOut == ""){
				ChequearActionInvestigar();
			}//Tomar Item
			else if(itemATomar != "" && objetoInvest == "" && lugarInOut == ""){
				ChequearActionItem(itemATomar);
			}//Entrar o Salir de algun lugar
			else if(objetoInvest == "" && itemATomar == "" && lugarInOut != ""){
				if(gui.mostrarTexto){
					ChequearActionInvestigar();
				}else{
					AccionEntrarSalir(lugarInOut);
				}
			}
		}
		//Disparar
		if(isShoot && itemEquipado == "gunItem"){
			if(lastShoot < Time.time && !isReload && !isShooting){
				Disparar();
			}
		}
	}

	void AccionEntrarSalir(string lugar){
		if(lugar == "ToolHouse"){
			if(PlayerPrefs.GetInt("canEnterToolHouse") == 0){
				if(itemEquipado == "keyItem"){
					this.gameObject.audio.clip = doorSound;
					audio.Play();
					gui.SetObjetoMostrar(lugar, itemEquipado);
					PlayerPrefs.SetInt("canEnterToolHouse", 1);
					//Se instancia Tool House desde el script de la puerta DoorTool
					//Asi se espera a que termine la animacion
					//Application.LoadLevel(lugar);
				}else{
					gui.SetObjetoMostrar(lugar, itemEquipado);
				}
			}else{
				if(PlayerPrefs.GetInt("canEnterToolHouse") == 1){
					this.gameObject.audio.clip = doorSound;
					audio.Play();
					StartCoroutine(IrAOtraEscena(lugar));
					//Application.LoadLevel(lugar);
				}else if(PlayerPrefs.GetInt("canEnterToolHouse") == 2){
					this.gameObject.audio.clip = doorSound;
					audio.Play();
					StartCoroutine(IrAOtraEscena(lugar));
					//Application.LoadLevel(lugar);
				}
			}
		}else if(lugar == "FirstScene"){
			this.gameObject.audio.clip = doorSound;
			audio.Play();
			StartCoroutine(IrAOtraEscena(lugar));
			//Application.LoadLevel(lugar);
		}else if(lugar == "SceneSelection"){
			this.gameObject.audio.clip = doorSound;
			audio.Play();
			//Verifica si se paso la escena 1 o no
			PlayerPrefs.SetInt("FirstSceneOk", 1);
			StartCoroutine(IrAOtraEscena(lugar));
		}else if(lugar == "House1"){
			if(PlayerPrefs.GetInt("canEnterHouse")==1){
				// Sonidos de puerta
				if(PlayerPrefs.GetString("playerInstance") != "House2"){
					this.gameObject.audio.clip = doorSound;
					audio.Play();
				}else{
					//Sonidos de pasos
					EjecutarSonidoPasos();
				}
				StartCoroutine(IrAOtraEscena(lugar));
				//Application.LoadLevel(lugar);
			}else{
				if(itemEquipado == "crowbarItem"){
					if(PlayerPrefs.GetInt("canEnterHouse")==0){
						animations.objeto = "House1";
						//lugarAIrTrigger = "House1";
					}else{
						PlayerPrefs.SetInt("canEnterHouse",1);
						//Sonido puerta
						if(PlayerPrefs.GetString("playerInstance") != "House2"){
							this.gameObject.audio.clip = doorSound;
							audio.Play();
						}else{
							//Sonidos de pasos
						}
						StartCoroutine(IrAOtraEscena(lugar));
						//Application.LoadLevel(lugar);
					}
					inventory.EliminarTextura(itemEquipado);
				}else{
					//Mostrar mensaje que indique hay no se puede entrar por tablas
					gui.SetObjetoMostrar(lugar, itemEquipado);
				}
			}
		}else if(lugar == "House2"){
			if(PlayerPrefs.GetString("playerInstance") != "House3" && PlayerPrefs.GetString("playerInstance") != "House1"){
				//Sonido puerta
				this.gameObject.audio.clip = doorSound;
				audio.Play();
			}else if(PlayerPrefs.GetString("playerInstance") == "House3" || PlayerPrefs.GetString("playerInstance") == "House1"){
				EjecutarSonidoPasos();
			}
			StartCoroutine(IrAOtraEscena(lugar));
			//Application.LoadLevel(lugar);
		}else if(lugar == "BathRoom"){
			//Sonido puerta
			this.gameObject.audio.clip = doorSound;
			audio.Play();
			animations.CambiarObjetoAnima(lugar);
		}else if(lugar == "SwampMech" || lugar == "SwampFight" || lugar == "Swamp1" || lugar == "House3"){
			//Sonido escalera
			EjecutarSonidoPasos();
			StartCoroutine(IrAOtraEscena(lugar));
		}else if(lugar == "SwampHouse1" && PlayerPrefs.GetString("playerInstance") != "SwampHouse2"){
			//Sonido escalera
			EjecutarSonidoPasos();
			StartCoroutine(IrAOtraEscena(lugar));
		}else{
			StartCoroutine(IrAOtraEscena(lugar));
			//Application.LoadLevel(lugar);
		}
	}

	void EjecutarSonidoPasos(){
		this.gameObject.audio.clip = escaleraSound;
		audio.Play();
	}

	IEnumerator IrAOtraEscena(string lugar){
		sceneToGo = lugar;
		//This is a coroutine
		yield return new WaitForSeconds(1.5f);
		Application.LoadLevel(lugar);
	}

	void Disparar(){
		if(PlayerPrefs.GetInt("gunBullets") > 0){
			anim.Play("gunShootPlayer");
			//SE INSTANCIA LA BALA
			Instantiate(bullet, new Vector3(transform.position.x,transform.position.y + 0.5f,transform.position.z), Quaternion.identity);
			lastShoot = Time.time + frecDisparo;
			PlayerPrefs.SetInt("gunBullets",PlayerPrefs.GetInt("gunBullets")-1);
			this.gameObject.audio.clip = shootGunSound;
			audio.Play();
		}else{
			if(PlayerPrefs.GetInt("gunBulletsInv") > 0){
				this.gameObject.audio.clip = reloadGunSound;
				audio.Play();
				anim.Play("reloadGun");
				recargar();
			}
		}
	}
	//VERIFICO SI INVESTIGO O HAGO ALGUNA ACCION DEBIDO A ITEM EQUIPADO
	private void ChequearActionInvestigar(){
		if(objetoInvest == "armario"){
			animations.CambiarObjetoAnima(objetoInvest);
		}else if(objetoInvest == "statue"){
			if(itemEquipado == "ringItem"){
				animations.CambiarObjetoAnima(objetoInvest);
			}else{
				InvestigarOCerrarInfo();
			}
		}else if(objetoInvest == "doorIn"){
			if(itemEquipado == "destornilladorItem"){
				MirarSigno.gameObject.renderer.enabled = false;
				animations.CambiarObjetoAnima(objetoInvest);
			}else{
				if(PlayerPrefs.GetInt("doorInOpen") == 0){
					InvestigarOCerrarInfo();
				}
			}
		}else if(objetoInvest == "pedestal"){
			if(itemEquipado == "candleItem" && PlayerPrefs.GetInt("velaInstanciada")==0){
				MirarSigno.gameObject.renderer.enabled = false;
				animations.CambiarObjetoAnima(objetoInvest);
			}else{
				if(PlayerPrefs.GetInt("velaInstanciada")==0){
					InvestigarOCerrarInfo();
				}
			}
		}else if(objetoInvest == "candle"){
			if(itemEquipado == "ligtherItem" && PlayerPrefs.GetInt("velaPrendida")==0){
				MirarSigno.gameObject.renderer.enabled = false;

				animations.CambiarObjetoAnima(objetoInvest);
			}else{
				if(PlayerPrefs.GetInt("velaPrendida")==0){
					InvestigarOCerrarInfo();
				}
			}
		}else if(objetoInvest == "escalera"){
			if(itemEquipado == "hookItem"){
				MirarSigno.gameObject.renderer.enabled = false;
				animations.CambiarObjetoAnima(objetoInvest);
				inventory.EliminarTextura(itemEquipado);
			}else{
				InvestigarOCerrarInfo();
			}
		}else if(objetoInvest == "retroceder"){
			InvestigarOCerrarInfo();
		}else if(objetoInvest == "chains"){
			if(itemEquipado == "chainCutterItem"){
				MirarSigno.gameObject.renderer.enabled = false;
				animations.CambiarObjetoAnima(objetoInvest);
				inventory.EliminarTextura(itemEquipado);
			}else{
				InvestigarOCerrarInfo();
			}
		}else if(objetoInvest == "comoda"){
			if(itemEquipado == "gataItem"){
				MirarSigno.gameObject.renderer.enabled = false;
				animations.CambiarObjetoAnima(objetoInvest);
				inventory.EliminarTextura(itemEquipado);
			}else{
				InvestigarOCerrarInfo();
			}
		}else if(objetoInvest == "peter"){
			InvestigarOCerrarInfo();
			animations.objeto = "askPeter";
		}else if(objetoInvest == "nonamed"){
			StartCoroutine(Esperar(0.5f));
			isAction = false;
		}else if(objetoInvest == "crow"){
			animations.objeto = "crow";
			LimpiarVariables();
		}else if(objetoInvest == "ramas"){
			if(itemEquipado == "axeItem"){
				MirarSigno.gameObject.renderer.enabled = false;
				animations.CambiarObjetoAnima(objetoInvest);
				inventory.EliminarTextura(itemEquipado);
			}else{
				InvestigarOCerrarInfo();
			}
		}else{
			InvestigarOCerrarInfo();
		}
	}
	//INVESTIGAR O CERRAR INFO QUE SE MUESTRA DESDE GUI
	private void InvestigarOCerrarInfo(){
		if(!gui.mostrarTexto){
			MirarSigno.gameObject.renderer.enabled = false;
			gui.SetObjetoMostrar(objetoInvest, itemEquipado);
			isAction = false;
		}
		else if(gui.mostrarTexto){
			StartCoroutine(Esperar(0.5f));
			isAction = false;
		}
	}

	private void ChequearActionItem(string item){
		if(item == "bulletsItem"){
			PlayerPrefs.SetInt("gunBulletsInv",PlayerPrefs.GetInt("gunBulletsInv") + 6);
			if(PlayerPrefs.GetString("playerInstance") == "SwampHouse2"){
				PlayerPrefs.SetInt("bulletsSH2Picked", 1);
			}
		}
		if(item == "candleItem"){
			PlayerPrefs.SetInt("candleItemPicked", 1);
		}
		if(item == "crowbarItem"){
			PlayerPrefs.SetInt("crowbarItemPicked", 1);
		}
		if(item == "destornilladorItem"){
			PlayerPrefs.SetInt("destornilladorItemPicked", 1);
		}
		if(item == "ligtherItem"){
			PlayerPrefs.SetInt("ligtherItemPicked", 1);
		}
		if(item == "chainCutterItem"){
			PlayerPrefs.SetInt("chainCutterItemPicked", 1);
		}
		if(item == "gunItem"){
			PlayerPrefs.SetInt("gunItemPicked", 1);
		}
		if(item == "gataItem"){
			PlayerPrefs.SetInt("gataItemPicked", 1);
		}
		if(item == "hookItem"){
			PlayerPrefs.SetInt("hookItemPicked", 1);
		}
		//TOMAR ITEMS
		itemATomar = "";
		MirarSigno.gameObject.renderer.enabled = false;
		inventory.llegaNuevoItem(item);
		this.gameObject.audio.clip = itemPickSound;
		audio.Play();
		isAction = false;
	}

	private string verificaAnimacion(){
		if(lifePlayer > 0){
			if(!isShooting && !isReload && !isDamaged){
				if(Mathf.Abs(rigidbody2D.velocity.x) <= minSpeed && itemEquipado != "gunItem"){
					return "idlePlayer";
				}
				else if(Mathf.Abs(rigidbody2D.velocity.x) >= maxSpeed && itemEquipado != "gunItem"){
					return "walkingPlayer";
				}
				else if(Mathf.Abs(rigidbody2D.velocity.x) >= maxSpeed && itemEquipado == "gunItem"){
					return "gunWalkPlayer";
				}
				else if(Mathf.Abs(rigidbody2D.velocity.x) >= maxSpeed && itemEquipado == "gunItem"){
					return "gunWalkPlayer";
				}
				else{
					return "idlePlayer";
				}
			}else{
				if(!isDamaged){
					if(isShooting && !isReload){
						if(timeShootAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
							timeShootAnim = 0.0f;
							return "idlePlayer";
						}else{
							timeShootAnim = timeShootAnim +0.01f;
							return "gunShootPlayer";
						}
					}else if(!isShooting && isReload){
						if(timeReloadAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
							timeReloadAnim = 0.0f;
							return "idlePlayer";
						}else{
							timeReloadAnim = timeReloadAnim +0.01f;
							return "reloadGun";
						}
					}else{
						return "idlePlayer";
					}
				}else{
					if(timeDamagedAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
						timeDamagedAnim = 0.0f;
						return "idlePlayer";
					}else{
						timeDamagedAnim = timeDamagedAnim +0.02f;
						return "damagedPlayer";
					}
				}
			}
		}else{
			if(timeDeadAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
				//Momento para abrir GUI de nuevo juego o continuar
				return "groundPlayer";
			}else{
				if(this.gameObject.audio.clip != deadPlayerSound){
					this.gameObject.audio.clip = deadPlayerSound;
					audio.Play();
				}
				timeDeadAnim = timeDeadAnim + 0.02f;
				return "deadPlayer";
			}
		}
	}

	IEnumerator Esperar(float sec){
		//This is a coroutine
		yield return new WaitForSeconds(sec);
		gui.mostrarTexto = false;
	}

	void Flip (){
		if(!isShooting && !isReload && !isDamaged){
			// Switch the way the player is labelled as facing.
			facingRight = !facingRight;
			// Multiply the player's x local scale by -1.
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}

	void CambiaStringsActions(string tag, string nombre){
		if (tag == "Objeto"){
			MirarSigno.gameObject.renderer.enabled = true;
			objetoInvest = nombre;
			itemATomar = "";
			lugarInOut = "";
		}else if(tag == "Item"){
			MirarSigno.gameObject.renderer.enabled = true;
			objetoInvest = "";
			lugarInOut = "";
			itemATomar = nombre;
		}else if(tag == "InOut"){
			InOutSigno.gameObject.renderer.enabled = true;
			objetoInvest = "";
			itemATomar = "";
			lugarInOut = nombre;
		}
	}

	void OnTriggerStay2D(Collider2D coll) {
		if(coll.gameObject.name == "armario" && PlayerPrefs.GetInt("armarioOpen")==0){
			CambiaStringsActions(coll.gameObject.tag, coll.gameObject.name);
		}else if(coll.gameObject.name == "armario" && PlayerPrefs.GetInt("armarioOpen")==1){
			MirarSigno.gameObject.renderer.enabled = false;
		}else if(coll.gameObject.name == "SceneSelection"){
			MirarSigno.gameObject.renderer.enabled = false;
			InOutSigno.gameObject.renderer.enabled = true;
			CambiaStringsActions(coll.gameObject.tag, coll.gameObject.name);
		}else if(coll.gameObject.name == "chains" && PlayerPrefs.GetInt("chainsCutted") == 1){
			MirarSigno.gameObject.renderer.enabled = false;
		}else if(coll.gameObject.name == "nonamed"){
			CambiaStringsActions(coll.gameObject.tag, coll.gameObject.name);
			MirarSigno.gameObject.renderer.enabled = false;
		}else{
			CambiaStringsActions(coll.gameObject.tag, coll.gameObject.name);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.gameObject.tag == "Head" && lifePlayer > 0){
			//Se eliminan otras particulas
			if(GameObject.FindGameObjectsWithTag("Particle").Length > 0){
				Destroy(GameObject.FindGameObjectsWithTag("Particle")[0]);
			}
			this.gameObject.audio.clip = damagedPlayerSound;
			audio.Play();
			//se instancia la sangre
			Instantiate(playerBlood, coll.gameObject.transform.position, new Quaternion(-30,90,0,0));
			Destroy(coll.gameObject);
			lifePlayer = lifePlayer - danioHead;
			PlayerPrefs.SetInt("vidaPlayer",lifePlayer);
			anim.Play("damagedPlayer");
		}else{
			if(coll.gameObject.tag == "Damage" && lifePlayer > 0){
				this.gameObject.audio.clip = damagedPlayerSound;
				audio.Play();
				lifePlayer = lifePlayer - danioHead;
				PlayerPrefs.SetInt("vidaPlayer",lifePlayer);
				anim.Play("damagedPlayer");
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		if(lifePlayer > 0){
			MirarSigno.gameObject.renderer.enabled = false;
			InOutSigno.gameObject.renderer.enabled = false;
			objetoInvest = "";
			itemATomar = "";
			lugarInOut = "";
		}
	}

	public void LimpiarVariables(){
		MirarSigno.gameObject.renderer.enabled = false;
		InOutSigno.gameObject.renderer.enabled = false;
		objetoInvest = "";
		itemATomar = "";
		lugarInOut = "";
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(lifePlayer > 0){
			if(coll.gameObject.tag == "Enemy"){
				if(coll.gameObject.name == "enemy1"){
					lifePlayer = lifePlayer - danioEnemy1;
					PlayerPrefs.SetInt("vidaPlayer",lifePlayer);
				}else if(coll.gameObject.name == "boss1"){
					lifePlayer = lifePlayer - danioBoss1;
					PlayerPrefs.SetInt("vidaPlayer",lifePlayer);
				}
				anim.Play("damagedPlayer");
			}
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if(lifePlayer > 0){
			if(coll.gameObject.tag == "Enemy" && !isDamaged){
				Instantiate(playerBlood, this.transform.position, new Quaternion(-30,90,0,0));
				this.gameObject.audio.clip = damagedPlayerSound;
				lifePlayer = lifePlayer - danioBoss1;
				PlayerPrefs.SetInt("vidaPlayer",lifePlayer);
				audio.Play();
				anim.Play("damagedPlayer");
			}
		}
	}

	public void GenerarDanioExterno(){
		this.gameObject.audio.clip = damagedPlayerSound;
		Instantiate(playerBlood, this.transform.position, new Quaternion(-30,90,0,0));
		lifePlayer = lifePlayer - danioBoss1;
		PlayerPrefs.SetInt("vidaPlayer",lifePlayer);
		audio.Play();
		anim.Play("damagedPlayer");
	}

	public void recargar(){
		for(int x=1;x<=6;x++){
			if(PlayerPrefs.GetInt("gunBulletsInv") > 0){
				if(PlayerPrefs.GetInt("gunBullets")<6){
					PlayerPrefs.SetInt("gunBulletsInv",PlayerPrefs.GetInt("gunBulletsInv")-1);
					PlayerPrefs.SetInt("gunBullets",PlayerPrefs.GetInt("gunBullets")+1);
				}
			}
		}
	}
}
