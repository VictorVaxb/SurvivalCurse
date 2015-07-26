using UnityEngine;
using System.Collections;

public class BelowDamage : MonoBehaviour {
	private float timeToDestroy = 1.5f;
	private float contador = 0.0f;
	public GameObject rama;

	void Update () { 
		contador = contador + Time.deltaTime;
		if(contador > timeToDestroy){
			Instantiate(rama, new Vector3(this.gameObject.transform.position.x,
			                              this.gameObject.transform.position.y + 3,
			                              this.gameObject.transform.position.z), new Quaternion(0,0,0,0));
			Destroy(this.gameObject);
		}
	}
}
