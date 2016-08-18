using UnityEngine;
using System.Collections;

public class ConsoleCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
		Canvas canvasElement = GetComponent<Canvas>();
		canvasElement.worldCamera = (Camera)GameObject.Find("Camera").GetComponent<Camera>();
	}

	void OnLevelWasLoaded() {
		Canvas canvasElement = GetComponent<Canvas>();
		canvasElement.worldCamera = (Camera)GameObject.Find("Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
