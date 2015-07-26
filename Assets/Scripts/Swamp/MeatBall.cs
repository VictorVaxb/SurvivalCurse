using UnityEngine;
using System.Collections;

public class MeatBall : MonoBehaviour {
	//Objeto de explosion
	public GameObject exposion;
	public GameObject soundExposion;

	void OnTriggerEnter2D(Collider2D coll) {
		//Instanciar objeto explosion
		Instantiate(exposion, transform.position, Quaternion.identity);
		Instantiate(soundExposion, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}
}
