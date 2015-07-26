using UnityEngine;
using System.Collections;

public class Enemy1 : MonoBehaviour {
	private bool facingRight = false;
	public float speedWalk = 10.0f;
	private float moveSpeed = 0.9f;		// The speed the enemy moves at.
	private int hp = 3; 
	private GameObject player;
	private Animator anim;	
	//esta siendo dañado??
	private bool isDamaged = false; 
	//esta atacando??
	private bool isAttack = false; 
	//esta muriendo??
	private bool isDying = false; 
	//tiempo para el daño
	private float timeDamagetAnim = 0.0f;
	//tiempo para el Ataque
	private float timeAttackAnim = 0.0f;
	private float timeDieAnim = 0.0f;
	//factor de termino de Ataque Anim
	public float incAttackAnim = 0.26f;
	//Sonido breath
	private GameObject breath;

	//Sonidos del Enemy
	public AudioClip ataque;
	public AudioClip dead;

	Gui gui;
	Inventory inventory;

	void Awake(){
		// Setting up references.
		anim = GetComponent<Animator>();
		gui = (Gui)GameObject.FindObjectOfType(typeof(Gui));
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
		breath = this.transform.FindChild("breathSound").gameObject;
	}

	void Start () {
		player = GameObject.FindGameObjectsWithTag("Player")[0];
		Flip();
	}

	void Update () {
		if(!BtnPausa.showMenu && !DieMenu.showMenuDie){
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("damagedEnemy1")){
				isDamaged = true;
			}else{
				isDamaged = false;
			}
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("attackEnemy1")){
				isAttack = true;
			}else{
				isAttack = false;
			}

			moveSpeed = player.transform.position.x - this.transform.position.x;

			if(moveSpeed < 0){
				if(!facingRight){
					moveSpeed = -1 * speedWalk;
				}else{
					moveSpeed = speedWalk;
				}
			}else{
				if(!facingRight){
					moveSpeed = speedWalk;
				}else{
					moveSpeed = -1 * speedWalk;
				}
			}

			if(!gui.mostrarTexto && !inventory.showItems){
				anim.speed = 1.0f;
				//Se verifica que animacion se debe reproducir
				anim.Play(VerificaAnimacion());
				if(!isDamaged && !isAttack){
					rigidbody2D.velocity = new Vector2(transform.localScale.x * moveSpeed, rigidbody2D.velocity.y);	
				}
			}else{
				anim.speed = 0.0f;
			}
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

	private string VerificaAnimacion(){
		if(!isAttack){
			if(isDamaged && hp > 0){
				if(timeDamagetAnim >= anim.GetCurrentAnimatorStateInfo(0).length){
					timeDamagetAnim = 0.0f;
					isDamaged = false;
					return "walkEnemy1";
				}else{
					timeDamagetAnim = timeDamagetAnim + 0.02f;
					return "damagedEnemy1";
				}
			}else{
				if(!isDamaged && hp > 0){
					return "walkEnemy1";
				}else{
					if(hp <= 0){
						if(timeDieAnim <= anim.GetCurrentAnimatorStateInfo(0).length){
							timeDieAnim = timeDieAnim + 0.02f;
							//Se debe desabilitar el collider
							this.rigidbody2D.gravityScale = 0;
							this.rigidbody2D.collider2D.enabled = false;
							breath.transform.audio.enabled = false;
							return "dieEnemy1";
						}else{
							Destroy(this.gameObject);
							return "dieEnemy1";
						}
					}else{
						return "walkEnemy1";
					}
				}
			}
		}else{
			if(hp > 0){
				if(timeAttackAnim <= anim.GetCurrentAnimatorStateInfo(0).length){
					timeAttackAnim = timeAttackAnim + incAttackAnim;
					return "attackEnemy1";
				}else{
					timeAttackAnim = 0.0f;
					isAttack = false;
					return "walkEnemy1";
				}
			}else{
				return "walkEnemy1";
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Bullet"){
			if(hp > 0){
				isDamaged = true;
				anim.Play("damagedEnemy1");
				Destroy(coll.gameObject);
				hp = hp - 1;
				if(hp <= 0){
					if(!isDying){
						this.gameObject.audio.clip = dead;
						audio.Play();
						isDying = true;
					}
					//Se cambia de tag para no generar nuevas acciones
					this.gameObject.tag = "Untagged";
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "Player" && !isAttack && !isDamaged && hp > 0){
			this.gameObject.audio.clip = ataque;
			audio.Play();
			anim.Play("attackEnemy1");
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if(coll.gameObject.tag == "Player" && !isAttack && !isDamaged && hp > 0){
			this.gameObject.audio.clip = ataque;
			audio.Play();
			anim.Play("attackEnemy1");
		}
	}
}
