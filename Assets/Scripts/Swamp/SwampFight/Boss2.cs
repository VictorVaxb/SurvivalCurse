using UnityEngine;
using System.Collections;

public class Boss2 : MonoBehaviour {

	private GameObject objeto;

	private float speedWalk = 0.0f;
	private float dist = 0.01f;		// diferencia entre el player y el boss2
	private string estado = "esperando";
	private GameObject player;
	private Animator anim;	
	private float timeAttackAnim = 0.0f;
	private float timeAttack2Anim = 0.0f;
	private float timeDieAnim = 0.0f;
	private int objetoCount = 0;
	//Variable que indica cada cuanto se lanza la cabeza
	public float frecInstancia = 1.0f;
	//Numero de objetos a instanciar
	public int num = 3;
	//Vida del boss
	public int vidaBoss = 1000;
	//Daño de las balas del jugador
	private int bulletDamage = 100;
	//sangre del boss, es una particula que se debe instanciar en el child blood
	public GameObject bossBlood;
	//gameobject del child blood
	private GameObject bloodPos;
	//booleano para determinar cuando activarse
	private bool canMove = false;
	//booleano para determinar si se puede cambiar de estado
	private bool canChange = false;
	//objetos de daño
	public GameObject objetoDanio;
	public GameObject objetoDanio2;

	private GameObject musica;
	//Sonidos
	public AudioClip boss2Walk;
	public AudioClip boss2Die;
	public AudioClip boss2Punch;
	public AudioClip playWhitMe;
	public AudioClip musicaEscena;

	//Externos
	Gui gui;
	Inventory inventory;
	PlayerMove playerMove;
	
	void Awake(){
		anim = GetComponent<Animator>();
		gui = (Gui)GameObject.FindObjectOfType(typeof(Gui));
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
		playerMove = (PlayerMove)GameObject.FindObjectOfType(typeof(PlayerMove));
	}
	
	void Start () {
		bloodPos = this.transform.FindChild("blood").gameObject;
		player = GameObject.FindGameObjectsWithTag("Player")[0];
	}
	
	void Update () {
		if(!DieMenu.showMenuDie && !BtnPausa.showMenu && !gui.mostrarTexto && !inventory.showItems){
			anim.Play(VerificaAnimacion());
			if(vidaBoss > 0){
				dist =  this.transform.position.x - player.transform.position.x;

				if(canMove && canChange){
					VerificaAccion();
					VerificaSonidos();
				}
				
				if(!BtnPausa.showMenu && !gui.mostrarTexto && !inventory.showItems){
					anim.speed = 1.0f;
					rigidbody2D.velocity = new Vector2(transform.localScale.x * speedWalk, rigidbody2D.velocity.y);	
				}else{
					anim.speed = 0.0f;
				}
			}else{
				speedWalk = 0;		//Boss no avanza
			}
		}else{
			anim.speed = 0.0f;
		}
	}

	void VerificaSonidos(){
		if(estado == "buscando"){
			if (audio.clip.name != "boss2Walk") {
				audio.clip = boss2Walk;
			}
		}else if(estado == "atacando2"){
			if (audio.clip.name != "playWhitMe") {
				audio.clip = playWhitMe;
			}
		}
		if(!audio.isPlaying){
			audio.Play();
		}
	}
	
	void VerificaAccion(){
		if(vidaBoss > 0){
			if(dist > 18.0f){
				estado = "buscando";
				speedWalk = -1.2f;
			}else if(dist > 10.0f && dist <= 18.0f){
				canChange = false;
				estado = "atacando2";
				speedWalk = 0.0f;
			}else if(dist <= 10.0f){
				canChange = false;
				estado = "atacando";
				speedWalk = 0.0f;
			}
		}else{
			canChange = false;
			//Se debe desabilitar el collider
			this.rigidbody2D.gravityScale = 0;
			this.rigidbody2D.collider2D.enabled = false;
			estado = "muriendo";
		}
	}
	
	private string VerificaAnimacion(){
		switch (estado)
		{
		case "esperando":
			return "boss2Idle";
		case "buscando":
			return "boss2Walk";
		case "atacando":
			if(timeAttackAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
				//llamo a player para generar daño
				playerMove.GenerarDanioExterno();
				timeAttackAnim = 0.0f;
				canChange = true;
				return "boss2Attack1";
			}else{
				if (audio.clip.name != "boss2Punch") {
					audio.clip = boss2Punch;
					audio.PlayScheduled(0.3);
				}
				timeAttackAnim += 0.02f;
				return "boss2Attack1";
			}
		case "atacando2":
			if(timeAttack2Anim >= anim.GetCurrentAnimatorStateInfo(0).length){
				timeAttack2Anim = 0.0f;
				//se instancia el objeto que daña al player
				if(Random.Range(-1.5f,1.5f) > 0){
					Instantiate(objetoDanio, new Vector3(player.transform.position.x,player.transform.position.y - 3.0f,0), new Quaternion(-30,90,0,0));
				}else{
					Instantiate(objetoDanio2, new Vector3(player.transform.position.x,player.transform.position.y + 10.0f,0), new Quaternion(-30,90,0,0));
				}
				objetoCount += 1;
				if(objetoCount > 3){
					canChange = true;
				}
				return "boss2Attack2";
			}else{
				timeAttack2Anim += 0.02f;
				return "boss2Attack2";
			}
		case "muriendo":
			if(timeDieAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
				musica = GameObject.FindGameObjectsWithTag("Music")[0];
				musica.audio.clip = musicaEscena;
				musica.audio.Play();
				Destroy(this.gameObject);
				return "boss2Die";
			}else{
				if (audio.clip.name != "boss2Die") {
					audio.clip = boss2Die;
					audio.PlayScheduled(0.2);
				}
				timeDieAnim += 0.018f;
				return "boss2Die";
			}
		default:
			return "boss2Walk";
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
				vidaBoss -= bulletDamage;
				if(vidaBoss <= 0){
					canChange = false;
					//Se debe desabilitar el collider
					this.rigidbody2D.gravityScale = 0;
					this.rigidbody2D.collider2D.enabled = false;
					estado = "muriendo";
				}
			}
		}
	}


	public void ActivarBoss2(){
		canChange = true;
		canMove = true;
	}
}
