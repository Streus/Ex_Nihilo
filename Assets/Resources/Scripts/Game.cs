using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	//Is the player controlling a cell?
	public static bool playerControllingCell;

	//public static GameObject cell = (GameObject)Resources.Load ("Prefabs/Round Cell Base", typeof(GameObject));

	// Use this for initialization
	void Start () {
		spawn (new Vector2 (0, 0), Quaternion.identity);

		for (int i = 0; i < 100; i++) {
			spawnAI ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject spawnAI() {
		GameObject obj = (GameObject)Instantiate (
			Resources.Load ("Prefabs/Round Cell Base"),
			new Vector2 (-50 + (Random.value * 100), -50 + (Random.value * 100)),
			Quaternion.identity);
		obj.AddComponent<SimpleAI> ();
		return obj;
	}

	public GameObject spawn(Vector2 pos, Quaternion rot) {
		GameObject obj = (GameObject)Instantiate (Resources.Load ("Prefabs/Round Cell Base"), pos, rot);
		return obj;
	}
}
