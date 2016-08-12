using UnityEngine;
using System.Collections;

public class BackgroundLogic : MonoBehaviour {

	/**
	 * These are the center coordinates in "background tile units"- 
	 * every tile takes up 10x10 real units, but only a single 1x1
	 * square in this coordinate system.
	 */
	public static float cenX, cenY;

	/**
	 * How large is the background? The background array will be of
	 * size NxN, where N = (DIM * 2) + 1. This ensures that the
	 * array is always odd. 
	 * 
	 * If this is larger, you can zoom out further without losing the 
	 * appearance of a constant background. Default value is 3.
	 */
	public static int DIM = 3;
	private static int N;

	/**
	 * An array of background tile objects
	 */
	public static GameObject[,] bgArr;

	// Use this for initialization
	void Start () {
		cenX = 0;
		cenY = 0;

		setDim (3);
	}

	public static void setDim(int to) {
		if (to < 1)
			return;

		N = (2 * DIM) + 1;
		bgArr = new GameObject[N, N];

		for (int i = 0; i < N; i++) {
			for (int j = 0; j < N; j++) {
				//-20 is behind the camera, and so invisible.
				//100 is far into the distance, and so is rendered in the background.
				Vector3 quadPos = new Vector3 (10 * (cenX + i - DIM), 10 * (cenY + j - DIM), -20);
				Vector3 imagePos = new Vector3 (10 * (cenX + i - DIM), 10 * (cenY + j - DIM), 100);

				bgArr[i, j] = (GameObject)Instantiate(
					Resources.Load("Prefabs/Background"),
					quadPos,
					Quaternion.identity);
				bgArr [i, j].transform.GetChild (0).position = imagePos;
			}
		}
	}

	// Update is called once per frame
	void Update () {
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
				Vector3 quadPos = new Vector3 (10 * (cenX + DIM), 10 * (cenY + i - DIM), -20);
				Vector3 imagePos = new Vector3 (10 * (cenX + DIM), 10 * (cenY + i - DIM), 100);

				bgArr [N - 1, i] = (GameObject)Instantiate (
					Resources.Load ("Prefabs/Background"),
					quadPos,
					Quaternion.identity);
				bgArr [N - 1, i].transform.GetChild (0).position = imagePos;
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
				Vector3 quadPos = new Vector3 (10 * (cenX - DIM), 10 * (cenY + i - DIM), -20);
				Vector3 imagePos = new Vector3 (10 * (cenX - DIM), 10 * (cenY + i - DIM), 100);

				bgArr [0, i] = (GameObject)Instantiate (
					Resources.Load ("Prefabs/Background"),
					quadPos,
					Quaternion.identity);
				bgArr [0, i].transform.GetChild (0).position = imagePos;
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
				Vector3 quadPos = new Vector3 (10 * (cenX + i - DIM), 10 * (cenY + DIM), -20);
				Vector3 imagePos = new Vector3 (10 * (cenX + i - DIM), 10 * (cenY + DIM), 100);

				bgArr [i, N - 1] = (GameObject)Instantiate (
					Resources.Load("Prefabs/Background"),
					quadPos,
					Quaternion.identity);
				bgArr [i, N - 1].transform.GetChild (0).position = imagePos;
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
				Vector3 quadPos = new Vector3 (10 * (cenX + i - DIM), 10 * (cenY - DIM), -20);
				Vector3 imagePos = new Vector3 (10 * (cenX + i - DIM), 10 * (cenY - DIM), 100);

				bgArr [i, 0] = (GameObject)Instantiate (
					Resources.Load ("Prefabs/Background"),
					quadPos,
					Quaternion.identity);
				bgArr [i, 0].transform.GetChild (0).position = imagePos;
			}
		}
	}
}
