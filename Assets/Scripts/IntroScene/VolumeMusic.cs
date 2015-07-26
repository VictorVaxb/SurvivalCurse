using UnityEngine;
using System.Collections;

public class VolumeMusic : MonoBehaviour {
	private float disminucion = 0.01f;

	void Update () {
		if(GuiIntro.posicionCam > moveCam.respawnsLength){
			audio.volume = audio.volume - disminucion;
		}
	}
}
