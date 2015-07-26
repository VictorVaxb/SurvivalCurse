using UnityEngine;
using System.Collections;

public class DestroyTime : MonoBehaviour {
	public float timeToDestroy = 1.0f;
	private float contador = 0.0f;

	void Update () { 
		contador = contador + Time.deltaTime;
		if(contador > timeToDestroy){
			Destroy(this.gameObject);
		}
	}
}
