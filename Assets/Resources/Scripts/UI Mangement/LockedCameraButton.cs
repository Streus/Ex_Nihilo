using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LockedCameraButton : MonoBehaviour {

	public void Start(){
		GetComponent<Toggle>().isOn = GameOptions.lockedCamera;
	}

	public void toggleLockedCamera(){
		GameOptions.lockedCamera = !GameOptions.lockedCamera;
	}
}
