using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	//A reference to the main camera
	private Camera mainCamera;

	//The x, y offset of the game arena
	private float xOffset;
	private float yOffset;

	//Height above the game arena
	private float zoom;

	private float xDamp;
	private float yDamp;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;

		xOffset = mainCamera.transform.position.x;
		yOffset = mainCamera.transform.position.y;
		zoom  = Mathf.Log(mainCamera.orthographicSize, 2);

		xDamp = 0.2F;
		yDamp = 0.2F;
	}
	
	// Update is called once per frame
	void Update () {
		//If the RMB is down, pan around
		if (Input.GetKey ("mouse 1")) {
			float x = Input.GetAxis ("Mouse X") * zoom * xDamp;
			float y = Input.GetAxis ("Mouse Y") * zoom * yDamp;

			xOffset -= x;
			yOffset -= y;

			updateOrientation ();
		}

		//If the scrollwheel is moved, zoom
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		if (scroll != 0) {
			zoom -= scroll;
			updateOrientation ();
		}
	}

	private void updateOrientation() {
		float x = xOffset;
		float y = yOffset;

		mainCamera.transform.position = new Vector3 (x, y, -10);
		mainCamera.orthographicSize = Mathf.Pow(2, zoom);
	}
}
