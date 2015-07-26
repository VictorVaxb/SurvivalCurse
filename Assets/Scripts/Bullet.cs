using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public float moveSpeed = 180.0f;
	PlayerMove playerMove;
	private GameObject player;
	private float dist = 0.0f;

	void Start () {
		player = GameObject.FindGameObjectsWithTag("Player")[0];
		playerMove = (PlayerMove)GameObject.FindObjectOfType(typeof(PlayerMove));
		if(!playerMove.facingRight){
			moveSpeed = moveSpeed * -1;
		}
	}
		
	void Update () {
		dist = Vector3.Distance(player.transform.position, this.transform.position);
		if(dist >= 14){
			Destroy(this.gameObject);
		}
		rigidbody2D.velocity = new Vector2(transform.localScale.x * moveSpeed, rigidbody2D.velocity.y);	
	}
}
