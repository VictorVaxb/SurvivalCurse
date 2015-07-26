using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour {
	private GameObject player;
	private Animator anim;	
	private float distPlayer;
	private bool canAttack = true;
	private string estado = "idle";

	public GameObject blood;
	public GameObject bola;

	private int alcance = 15;
	private float timeAnim = 0.0f;
	private float timeAttack = 0.0f;
	private int hp = 2;

	Gui gui;
	Inventory inventory;

	void Start () {
		anim = GetComponent<Animator>();
		gui = (Gui)GameObject.FindObjectOfType(typeof(Gui));
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
		player = GameObject.FindGameObjectsWithTag("Player")[0];
	}

	void Update () {
		anim.Play(VerificaAnim());
		distPlayer = this.transform.position.x - player.transform.position.x;

		if(distPlayer < alcance && (Time.time - timeAttack) > 4.0f){
			if(canAttack && estado == "idle"){
				estado = "atacando";
				timeAttack = Time.time;
				timeAnim = 0.0f;
				canAttack = false;
			}
		}
	}

	private string VerificaAnim(){
		if(hp > 0){
			if(distPlayer > alcance  && estado == "idle"){
				return "enemy2Idle";
			}else if(estado == "atacando"){
				return verificaAtaque();
			}else if(estado == "volviendo"){
				return verificaVolver();
			}else{
				return "enemy2Idle";
			}
		}else{
			return verificaDie();
		}
	}

	private string verificaDie(){
		if(timeAnim <= anim.GetCurrentAnimatorStateInfo(0).length){
			timeAnim += 0.02f;
			return "enemy2Die";
		}else{
			PlayerPrefs.SetInt("enemy2Akilled", 1);
			Destroy(this.gameObject);
			return "enemy2Die";
		}
	}

	private string verificaAtaque(){
		if(timeAnim <= anim.GetCurrentAnimatorStateInfo(0).length){
			timeAnim += 0.02f;
			return "enemy2Attack";
		}else{
			Instantiate(bola, this.transform.position, Quaternion.identity);
			timeAnim = 0.0f;
			estado = "volviendo";
			return "enemy2AttackB";
		}
	}

	private string verificaVolver(){
		if(timeAnim <= anim.GetCurrentAnimatorStateInfo(0).length){
			timeAnim += 0.02f;
			return "enemy2AttackB";
		}else{
			estado = "idle";
			canAttack = true;
			return "enemy2Idle";
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Bullet"){
			Instantiate(blood, this.transform.position, Quaternion.identity);
			if(hp > 0){
				Destroy(coll.gameObject);
				hp = hp - 1;
				if(hp <= 0){
					if(estado != "muriendo"){
						//this.gameObject.audio.clip = dead;
						//audio.Play();
						timeAnim = 0.0f;
						estado = "muriendo";
					}
				}
			}
		}
	}
}
