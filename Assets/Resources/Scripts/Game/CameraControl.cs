using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	//A reference to the main camera
	private static Camera mainCamera;

	//The x, y offset of the game arena
	public static float xOffset;
	public static float yOffset;

	//Height above the game arena
	public static float zoom;

	private static float xDamp;
	private static float yDamp;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;

		xOffset = mainCamera.transform.position.x;
		yOffset = mainCamera.transform.position.y;
		zoom  = Mathf.Log(mainCamera.orthographicSize, 2);

		xDamp = 0.5F;
		yDamp = 0.5F;
	}
	
	// Update is called once per frame
	void Update () {
		//If the RMB is down, and player is not in a cell, pan around
		//if (!Game.playerControllingCell) {
		/*
			if (Input.GetKey (KeyBindings.placeMvtMkr)) {
				float x = Input.GetAxis ("Mouse X") * zoom * xDamp;
				float y = Input.GetAxis ("Mouse Y") * zoom * yDamp;

				xOffset += x;
				yOffset += y;
			}
			*/
		//}

		//If the scrollwheel is moved, zoom
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		if (scroll != 0) {
			zoom -= scroll;
		}

		updateOrientation ();
	}

	private void updateOrientation() {
		float x = xOffset;
		float y = yOffset;

		mainCamera.transform.position = new Vector3 (x, y, -10);
		mainCamera.orthographicSize = Mathf.Pow(2, zoom);
	}
}
