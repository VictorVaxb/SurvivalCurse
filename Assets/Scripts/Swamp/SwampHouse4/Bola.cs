using UnityEngine;
using System.Collections;

public class Bola : MonoBehaviour {
	private float moveSpeed = 60.0f;
	private GameObject player;
	private float dist = 0.0f;
	
	void Start () {
		player = GameObject.FindGameObjectsWithTag("Player")[0];
		dist = player.transform.position.x - this.transform.position.x;
		if(dist <= 0){
			moveSpeed = moveSpeed * -1;
		}
	}
	
	void Update () {
		rigidbody2D.velocity = new Vector2(transform.localScale.x * moveSpeed, rigidbody2D.velocity.y);	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.name == "Player"){
			Destroy (this.gameObject);
		}
	}
}
