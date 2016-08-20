using UnityEngine;
using System.Collections;

public class Game : Singleton<Game> {

	//Is the player controlling a cell?
	public static bool playerControllingCell = false;

	//Pause control variable
	public static bool paused = false;

	//What is the player currently in control of?
	public static ArrayList controlled = new ArrayList();

	//A list of all the gameobjects loaded in via create()
	public static ArrayList objects = new ArrayList();

	// Use this for initialization
	void Start () {
		//CellBase mover = spawn (new Vector2 (0, 0), Quaternion.identity).GetComponent<CellBase>();
		//mover.attach ((GameObject)Instantiate (Resources.Load ("Prefabs/Cell Flagella")), 180);

		CellBase mover = create ("Round Cell Base", 0, 0).GetComponent<CellBase>();
		mover.attach ("Cell Flagella", 180);

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
	 * Toggles the pause state of the game. 
	 */
	public static void setPause(bool to) {
		paused = to;
		Time.timeScale = to? 0 : 1;
	}

	//Pauses the game
	public static void pause() { setPause (true); }

	//Unpauses the game
	public static void unpause() { setPause (false); }

	/**
	 * Creates a GameObject of type "name"
	 */
	public static GameObject create(string name) {
		GameObject obj = (GameObject)Instantiate (Resources.Load ("Prefabs/" + name), new Vector2(0, 0), Quaternion.identity);
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
	 * Removes the provided GameObject
	 */
	public static void destroy(GameObject to) {
		objects.Remove (to);
		Destroy (to);
		objects.Remove (null); //just in case
	}

	/**
	 * Searches by the given Component and returns all GameObjects that include it
	 */
	public static ArrayList filterBy(Component com) {
		ArrayList toReturn = new ArrayList ();
		for (int i = 0; i < objects.Count; i++)
			if (((GameObject)objects [i]).GetComponent(com.GetType()) != null)
				toReturn.Add (objects [i]);
		return toReturn;
	}

	/**
	 * Searches by the given Component and returns all GameObjects that include it
	 */
	public static ArrayList filterBy<T>(ArrayList list) {
		ArrayList toReturn = new ArrayList ();
		for (int i = 0; i < list.Count; i++)
			if (((GameObject)list [i]).GetComponent<T>() != null)
				toReturn.Add (list [i]);
		return toReturn;
	}

	/**
	 * Gets all of the GameObjects of a type within a rectangle defined by
	 * the two points "first" and "second"
	 */
	public static ArrayList getBetween(Vector2 first, Vector2 second) {
		return getBetween (
			Mathf.Min (first.x, second.x),
			Mathf.Min (first.y, second.y),
			Mathf.Max (first.x, second.x),
			Mathf.Max (first.y, second.y));
	}

	public static ArrayList getBetween(float xMin, float yMin, float xMax, float yMax) {
		Debug.Log ("Searching in " + xMin + ", " + yMin + " and " + xMax + ", " + yMax);
		ArrayList toReturn = new ArrayList ();
		for (int i = 0; i < objects.Count; i++) {
			Vector2 pos = ((GameObject)objects [i]).transform.position;
			if (pos.x > xMin && pos.x < xMax && pos.y > yMin && pos.y < yMax) {
				//Debug.Log ("Object at position " + i + " is " + pos);
				toReturn.Add (objects[i]);
			}
		}
		return toReturn;
	}
}
