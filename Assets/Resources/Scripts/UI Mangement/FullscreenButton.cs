using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FullscreenButton : MonoBehaviour {

	void Start()
	{
		transform.GetComponent<Toggle> ().isOn = Screen.fullScreen;
	}

	public void toggleFullscreen()
	{
		Screen.fullScreen = !Screen.fullScreen;
	}
}
