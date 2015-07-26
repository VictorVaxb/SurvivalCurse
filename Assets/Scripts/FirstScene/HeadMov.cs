using UnityEngine;
using System.Collections;

public class HeadMov : MonoBehaviour {
	private bool canAddForce = true;
	//Se necesita saber donde esta el jugador
	private GameObject player;
	private Vector3 vector = new Vector3(1,1,0);

	public int xForce = 100; //de 100 a 500
	public int yForce = 1500;
	//velocidad de rotacion
	public int speedRot = 20; 


	Gui gui;
	Inventory inventory;

	void Start (){
		gui = (Gui)GameObject.FindObjectOfType(typeof(Gui));
		inventory = (Inventory)GameObject.FindObjectOfType(typeof(Inventory));
		player = GameObject.FindGameObjectsWithTag("Player")[0];
	}
	
	void Update() {
		if(!BtnPausa.showMenu && !gui.mostrarTexto && !inventory.showItems){
			this.transform.Rotate(0,0,Time.deltaTime * speedRot);
			if(this.transform.position.y < -17.0f){
				Destroy(this.gameObject);
			}
			//Destroy(this.gameObject);
			if(canAddForce){
				//xRange = Random.Range(xRange, xRange + 5);
				//yRange = Random.Range(yRange, yRange + 5);
				ModificarX();
				vector = new Vector3(vector.x * xForce, vector.y * yForce,0);
				rigidbody2D.AddForce(vector);
				//rigidbody2D.velocity = new Vector2(transform.localScale.x * -9.5f, rigidbody2D.velocity.y * -64.43f);	
				canAddForce = false;
			}
		}
	}

	void ModificarX(){
		xForce = Random.Range(100, 500);
		if(player.transform.position.x < this.transform.position.x){
			xForce = xForce * -1;
		}
	}
}
