using UnityEngine;
using System.Collections;

public class BackgroundLogic : MonoBehaviour {

	int screenWidth;
	int screenHeight;

	Texture2D tex;

	// Use this for initialization
	void Start () {
		screenWidth = Screen.currentResolution.width;
		screenHeight = Screen.currentResolution.height;

		tex = new Texture2D (screenWidth, screenHeight);
		GetComponent<Renderer> ().material.mainTexture = tex;

		Color[] colors = new Color[screenWidth * screenHeight];
		for (int i = 0; i < tex.width; i++) {
			for (int j = 0; j < tex.height; j++) {
				colors [j * tex.width + i] = new Color (255, 0, 0);
			}
		}
		tex.SetPixels (colors);
		tex.Apply ();
	}

	// Update is called once per frame
	void Update () {
	}

	void onGUI() {
		Color[] colors = new Color[screenWidth * screenHeight];
		for (int i = 0; i < tex.width; i++) {
			for (int j = 0; j < tex.height; j++) {
				colors [j * tex.width + i] = new Color (0, 0, 0);
			}
		}
		tex.SetPixels (colors);
		tex.Apply ();
		GUI.DrawTexture (new Rect(0, 0, screenWidth, screenHeight), tex);
	}
}
