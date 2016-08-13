﻿using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	//Is the player controlling a cell?
	public static bool playerControllingCell = false;	

	//A list of all the gameobjects loaded in via create()
	public static ArrayList objects = new ArrayList();

	// Use this for initialization
	void Start () {
		//CellBase mover = spawn (new Vector2 (0, 0), Quaternion.identity).GetComponent<CellBase>();
		//mover.attach ((GameObject)Instantiate (Resources.Load ("Prefabs/Cell Flagella")), 180);

		CellBase mover = create ("Round Cell Base").GetComponent<CellBase>();
		mover.attach ("Cell Flagella", 180);
		

		playerControllingCell = true;

		for (int i = 0; i < 100; i++) {
			create ("Round Cell Base",
				new Vector2 (-50 + (Random.value * 100), -50 + (Random.value * 100)))
				.AddComponent<NavigateAI>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	/**
	 * Creates a GameObject of type "name"
	 */
	public static GameObject create(string name) {
		GameObject obj = (GameObject)Instantiate (Resources.Load ("Prefabs/" + name));
		objects.Add (obj);
		return obj;
	}

	/**
	 * Creates a GameObject of type "name" and a given transform
	 */
	public static GameObject create(string name, Transform trans) {
		GameObject obj = (GameObject)Instantiate (Resources.Load ("Prefabs/" + name), trans);
		objects.Add (obj);
		return obj;
	}

	/**
	 * Creates a GameObject of type "name" and a given position
	 */
	public static GameObject create(string name, Vector2 pos) {
		GameObject obj = (GameObject)Instantiate (Resources.Load ("Prefabs/" + name), pos, Quaternion.identity);
		objects.Add (obj);
		return obj;
	}

	/**
	 * Creates a GameObject of type "name", a given position and rotation
	 */
	public static GameObject create(string name, Vector2 pos, Quaternion rot) {
		GameObject obj = (GameObject)Instantiate (Resources.Load ("Prefabs/" + name), pos, rot);
		objects.Add (obj);
		return obj;
	}
		
	/**
	 * Creates a GameObject of type "name", at a point and a given rotation
	 */
	public static GameObject create(string name, Vector2 pos, float rot) {
		Quaternion rotQ = Quaternion.Euler(0, 0, rot);
		return create (name, pos, rotQ);
	}
		
	/**
	 * Creates a GameObject of type "name", at the point (x, y) and a given rotation
	 */
	public static GameObject create(string name, float x, float y) {
		return create (name, new Vector2 (x, y));
	}

	/**
	 * Creates a GameObject of type "name", at the point (x, y) and a given rotation
	 */
	public static GameObject create(string name, float x, float y, float rot) {
		return create (name, new Vector2 (x, y), rot);
	}

	/**
	 * Searches by the given Component and returns all GameObjects that include it
	 */
	public static ArrayList search(Component com) {
		ArrayList toReturn = new ArrayList ();
		for (int i = 0; i < objects.Count; i++)
			if (((GameObject)toReturn [i]).GetComponent(com.GetType()) != null)
				toReturn.Add (toReturn [i]);
		return toReturn;
	}
}
