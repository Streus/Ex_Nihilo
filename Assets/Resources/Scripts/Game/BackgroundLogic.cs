using UnityEngine;
using System.Collections;

public class BackgroundLogic : MonoBehaviour {
	//Height of the background image- should be far in BG (>> 0)
	private static int bgHeight = 100;

	//Rotation of the plane - needed for proper instantiation
	private static Quaternion rotation = Quaternion.Euler(0, -90, 90);

	/**
	 * These are the center coordinates in "background tile units"- 
	 * every tile takes up 10x10 real units, but only a single 1x1
	 * square in this coordinate system.
	 */
	public static float cenX, cenY;

	public static GameObject bg;
	private static Texture2D tex;

	private static int textureRes = 256;

	public static Gradient gradient = new Gradient();

	// Use this for initialization
	void Start () {
		cenX = 0;
		cenY = 0;

		/*
		GradientColorKey[] gck = new GradientColorKey[2];
		gck [0].color = new Color (0, 0, 0);
		gck [0].time = 0;
		gck [1].color = new Color (0, 255, 255);
		gck [1].time = 1;
		gradient.colorKeys = gck;
		*/

		Vector3 imagePos = new Vector3 (10 * cenX, 10 * cenY, bgHeight);
		bg = Game.create ("Background", imagePos, rotation);

		generate (1);
	}

	private static void generate(int steps) {
		tex = new Texture2D (textureRes, textureRes);
		tex.wrapMode = TextureWrapMode.Clamp;
		//tex.filterMode = FilterMode.Point;

		//start at 1x and go up by 2 per step

		Color[] colorArr = new Color[textureRes * textureRes];

		float[] values = new float[textureRes * textureRes];

		float correction = 0;
		float freq = 4f;
		float ampl = 2f;
		float scale = freq / textureRes;

		float offsetX = (Random.value * 2000) - 1000;
		float offsetY = (Random.value * 2000) - 1000;
		for (int s = 0; s < steps; s++) {
			freq *= 2;
			ampl *= 0.5f;
			scale *= 0.5f;
			correction += ampl;

			for (int i = 0; i < textureRes; i++) {
				for (int j = 0; j < textureRes; j++) {
					float val = ampl * Mathf.PerlinNoise (
						offsetX + (scale * i),
						offsetY + (scale * j));
					//colorArr [(i * textureRes) + j] = sum(
					//	new Color (val, val, val), colorArr[(i * textureRes) + j]);
					values[(i * textureRes) + j] += val;
				}
			}
		}

		correction = 1 / correction;
		
		for (int i = 0; i < textureRes * textureRes; i++) {
			values [i] *= correction;
			colorArr [i] = new Color (values [i], values [i], values [i]);
		}

		tex.SetPixels (colorArr);
		tex.Apply ();

		bg.GetComponent<Renderer> ().material.mainTexture = tex;
	}

	private static Color sum(Color a, Color b) {
		return new Color ((a.r + b.r), (a.g + b.g), (a.b + b.b));
	}

	// Update is called once per frame
	void Update () {
		/*
		//get main camera's position
		Vector3 pos = Camera.main.transform.position;

		//the camera is > 1 background unit to the right; shift right
		if (pos.x - (10 * cenX) > 10) { 
			for (int i = 0; i < N; i++)
				Destroy (bgArr [0, i]); //delete left col

			//shift array elements left by one
			for (int i = 0; i < N - 1; i++)
				for (int j = 0; j < N; j++) 
					bgArr [i, j] = bgArr [i + 1, j];

			//change the center coordinates
			cenX++;

			//create new array elements
			for (int i = 0; i < N; i++) {
				Vector3 imagePos = new Vector3 (10 * (cenX + DIM), 10 * (cenY + i - DIM), bgHeight);

				bgArr [N - 1, i] = Game.create ("Background", imagePos, rotation);
				bgArr [N - 1, i].GetComponent<Renderer> ().material.mainTexture = tex;
			}
		}

		//the idea is the same for the other 3 directions, so comments are kept to a min

		//Camera is too far left; shift left
		if ((10 * cenX) - pos.x > 10) {
			for (int i = 0; i < N; i++)
				Destroy (bgArr [N - 1, i]); //destroy right col

			//shift array elements right by one
			for (int i = N - 1; i > 0; i--)
				for (int j = 0; j < N; j++)
					bgArr [i, j] = bgArr [i - 1, j];

			//change the center coordinates
			cenX--;

			//create new array elements
			for (int i = 0; i < N; i++) {
				Vector3 imagePos = new Vector3 (10 * (cenX - DIM), 10 * (cenY + i - DIM), bgHeight);

				bgArr [0, i] = Game.create ("Background", imagePos, rotation);
				bgArr [0, i].GetComponent<Renderer> ().material.mainTexture = tex;
			}
		}

		//Camera is too far up; shift up
		if (pos.y - (10 * cenY) > 10) {
			for (int i = 0; i < N; i++)
				Destroy(bgArr[i, 0]);

			for (int i = 0; i < N; i++)
				for (int j = 0; j < N - 1; j++)
					bgArr [i, j] = bgArr [i, j + 1];

			cenY++;

			for (int i = 0; i < N; i++) {
				Vector3 imagePos = new Vector3 (10 * (cenX + i - DIM), 10 * (cenY + DIM), bgHeight);

				bgArr [i, N - 1] = Game.create ("Background", imagePos, rotation);
				bgArr [i, N - 1].GetComponent<Renderer> ().material.mainTexture = tex;
			}
		}

		//Camera is too far down; shift down
		if ((10 * cenY) - pos.y > 10) {
			for (int i = 0; i < N; i++) 
				Destroy (bgArr [i, N - 1]);

			for (int i = 0; i < N; i++) 
				for (int j = N - 1; j > 0; j--)
					bgArr [i, j] = bgArr [i, j - 1];

			cenY--;

			for (int i = 0; i < N; i++) {
				Vector3 imagePos = new Vector3 (10 * (cenX + i - DIM), 10 * (cenY - DIM), bgHeight);

				bgArr [i, 0] = Game.create ("Background", imagePos, rotation);
				bgArr [i, 0].GetComponent<Renderer> ().material.mainTexture = tex;
			}
		}
		*/
	}
}
