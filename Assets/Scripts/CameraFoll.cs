using UnityEngine;
using System.Collections;

public class CameraFoll : MonoBehaviour {
	public float aumentoY = 3.0f;		// Aumento en Y para descentrar camara
	private float shakeAmount = 0.25f;  	// Variable para el Shake de camara

	private Transform player;			// Reference to the player's transform.

	PlayerMove playerMove;

	void Start (){
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerMove = (PlayerMove)GameObject.FindObjectOfType(typeof(PlayerMove));
	}
	
	void FixedUpdate (){
		TrackPlayer();
	}

	void Update(){
		if(playerMove.isDamaged) {
			this.transform.position = this.transform.position + Random.insideUnitSphere * shakeAmount;
		}
	}

	void TrackPlayer ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = player.transform.position.x;
		float targetY = player.transform.position.y;
		
		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY + aumentoY, transform.position.z);
	}
}
