using UnityEngine;
using System.Collections;

public class Booss1 : MonoBehaviour {
	//Cabeza a Lanzar
	public Transform head;
	//crowbar a instanciar
	public Transform crowbar;
	private GameObject objeto;

	private bool facingRight = false;
	public float speedWalk = 10.0f;
	private float moveSpeed = 0.9f;		// The speed the enemy moves at.
	private string estado = "buscando";
	//private int hp = 3; 
	private GameObject player;
	private Animator anim;	
	//tiempo para el Ataque
	private float timeAttackAnim = 0.0f;
	//tiempo para el Ataque
	private float timeDieAnim = 0.0f;
	//private float timeDieAnim = 0.0f;
	private int headCount = 0;
	//Variable que guarda el tiempo
	private float lastShoot = 0.0f;
	//Variable que indica cada cuanto se lanza la cabeza
	public float frecDisparo = 1.0f;
	//Numero de cabezas a disparar
	public int numHeads = 4;
	//Vida del boss
	public int vidaBoss = 1000;
	//Daño de las balas del jugador
	public int bulletDamage = 100;
	//sangre del boss, es una particula que se debe instanciar en el child blood
	public GameObject bossBlood;
	//gameobject del child blood
	private GameObject bloodPos;
	//gameobject para instanciar el crowbar
	private GameObject crowbarPos;
	
	Gui gui;
	Inventory inventory;
	
	void Awake(){
		anim = GetComponent<Animator>();
		gui = (Gui)GameObject.FindObjectOfType(typeof(Gui));
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
	}
	
	void Start () {
		bloodPos = this.transform.FindChild("blood").gameObject;
		crowbarPos = this.transform.FindChild("crowbar").gameObject;
		player = GameObject.FindGameObjectsWithTag("Player")[0];
		Flip();
	}

	void Update () {
		if(!DieMenu.showMenuDie){
			anim.Play(VerificaAnimacion());
			if(vidaBoss > 0){
				if(estado == "buscando"){
					moveSpeed = player.transform.position.x - this.transform.position.x;
				}

				VerificaAccion();
				
				if(!BtnPausa.showMenu && !gui.mostrarTexto && !inventory.showItems){
					anim.speed = 1.0f;
					//Se verifica que animacion se debe reproducir
					//anim.Play(VerificaAnimacion());
					rigidbody2D.velocity = new Vector2(transform.localScale.x * moveSpeed, rigidbody2D.velocity.y);	
				}else{
					anim.speed = 0.0f;
				}
			}else{
				moveSpeed = 0;		//Boss no avanza
			}
		}
	}

	void VerificaAccion(){
		if(estado == "buscando"){
			MovBuscarPlayer();
		}else if(estado == "atacando2"){
			if(lastShoot < Time.time && headCount < numHeads){
				//SE INSTANCIA LA CABEZA
				LanzarCabeza();
			}else if(headCount == numHeads){
				headCount = 0;
				estado = "attackBack";
			}
		}
	}

	void MovBuscarPlayer(){
		if(moveSpeed < 0){		//Player a la Izquierda
			if(moveSpeed < -16){
				if(!facingRight){
					moveSpeed = -1 * speedWalk;
				}else{
					moveSpeed = speedWalk;
				}
			}else if(moveSpeed < 0 && moveSpeed > -15){
				moveSpeed = 0;		//Boss no avanza
				estado = "atacando";
				anim.Play("boss1Attack");
			}
		}else if(!facingRight){	//Player a la Derecha
			moveSpeed = speedWalk;
		}else{
			moveSpeed = -1 * speedWalk;
		}
	}

	private string VerificaAnimacion(){
		switch (estado)
		{
		case "buscando":
			return "boss1walk";
		case "atacando":
			if(timeAttackAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
				timeAttackAnim = 0.0f;
				estado = "atacando2";
				return "boss1Attacking";
			}else{
				timeAttackAnim = timeAttackAnim + 0.02f;
				return "boss1Attack";
			}
		case "atacando2":
			return "boss1Attacking";
		case "retrocediendo":
			return "boss1WalkBack";
		case "attackBack":
			if(timeAttackAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
				timeAttackAnim = 0.0f;
				estado = "buscando";
				return "boss1walk";
			}else{
				timeAttackAnim = timeAttackAnim + 0.02f;
				return "boss1AttackBack";
			}
		case "muriendo":
			if(timeDieAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
				//se instancia crowbar para salir del nivel
				Instantiate(crowbar, crowbarPos.transform.position, Quaternion.identity);
				EliminarCloneString("chainCutterItem");
				//se cambia variable para que no se instancie nuevamente
				PlayerPrefs.SetInt("boss1Kill",1);
				Destroy(this.gameObject);
				return "";
			}else{
				timeDieAnim = timeDieAnim + 0.018f;
				return "boss1Die";
			}
		default:
			return "boss1walk";
		}
	}

	void EliminarCloneString(string nombre){
		//Cambiar Nombre Gameobject
		objeto = GameObject.Find(nombre+"(Clone)");
		objeto.name = nombre;
	}

	void LanzarCabeza(){
		//Validar para que la cabeza se instancie solo 3 veces y en un intervalo de tiempo
		if(headCount < numHeads){
			Instantiate(head, new Vector3(transform.position.x,transform.position.y + 0.5f,transform.position.z), Quaternion.identity);
			lastShoot = Time.time + frecDisparo;
			headCount++;
		}
	}
	
	void Flip (){
		if(player.transform.position.x >= this.transform.position.x){
			facingRight = true;
			// Multiply the player's x local scale by -1.
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(vidaBoss > 0){
			if(coll.gameObject.tag == "Bullet"){
				//Se eliminan otras particulas
				if(GameObject.FindGameObjectsWithTag("Particle").Length > 0){
					Destroy(GameObject.FindGameObjectsWithTag("Particle")[0]);
				}
				//se instancia la sangre
				Instantiate(bossBlood, bloodPos.transform.position, new Quaternion(-30,90,0,0));
				vidaBoss = vidaBoss - bulletDamage;
				verificarVida();
				//Instanciar alguna particula que imite la sangre de boss
			}
		}
	}

	void verificarVida(){
		if(vidaBoss < 0){
			estado = "muriendo";
		}
	}
}
