﻿using UnityEngine;
using System.Collections;

/**
 * A class that handles all of the player's interactions
 */
public class MovementManager : Singleton<MovementManager> {
	//Selection variables
	private static Vector2 anchorPoint = new Vector2(0, 0); //Where the user right-clicks to select
	private static Vector2 clickPoint  = new Vector2(0, 0); //Where the user is currently right-clicking

	//Selection overlay options below
	private static Color selectMainColor; //internal color
	private static Color selectTrimColor; //edge color

	private static Texture2D selectMainTex;
	private static Texture2D selectTrimTex;

	public static int trimWidth; //how wide are the edges?

	//Movement variables
	public static Vector2 averagePos = new Vector2 (0, 0); //center-of-mass position
	public static Vector2 averageVel = new Vector2 (0, 0); //center-of-mass velocity

	public static Vector2 targetCoords = new Vector2(0, 0); //Where we're trying to move to

	public static GameObject flag; //movement flag

	//What we currently have selected
	private static ArrayList selection = new ArrayList();

	void Start() {
		//Selection overlay init
		setSelectMainColor(new Color(1, 1, 1, 0.3f));
		setSelectTrimColor(new Color(0, 0, 0, 0.3f));
		setSelectTrimWidth (3);
	}

	public void setSelectMainColor(Color col) {
		selectMainColor = col;

		selectMainTex = new Texture2D (1, 1);
		selectMainTex.SetPixel (0, 0, selectMainColor);
		selectMainTex.Apply ();
	}

	public void setSelectTrimColor (Color col) {
		selectTrimColor = col;

		selectTrimTex = new Texture2D (1, 1);
		selectTrimTex.SetPixel (0, 0, selectTrimColor);
		selectTrimTex.Apply ();
	}

	public void setSelectTrimWidth(int to) {
		trimWidth = to;
	}

	// Update is called once per frame
	void Update () {
		//Pause logic
		if (Input.GetKeyDown (KeyBindings.pause)) {
			Game.setPause (!Game.paused);
		}

		//selection UI and logic
		if(Input.GetKey(KeyBindings.select)) {
			Vector2 mousePoint = Input.mousePosition;
			if (anchorPoint.x == 0 && anchorPoint.y == 0)
				anchorPoint = new Vector2 (mousePoint.x, Screen.height - mousePoint.y);

			clickPoint = new Vector2 (mousePoint.x, Screen.height - mousePoint.y);

			//Reset currently selected cells
			for(int i = 0; i < selection.Count; i++) 
				(selection [i] as GameObject).GetComponent<CellBase> ().setPlayerControl (false);

			//The y input for ScreenToWorldPoint is calculated from the bottom-left instead of top-left.
			//To correct for this, we have to flip the y coordinate about the Screen height.
			Vector2 anchorFlipped = new Vector2(anchorPoint.x, Mathf.Abs (anchorPoint.y - Screen.height)); 
			Vector2 clickFlipped = new Vector2(clickPoint.x, Mathf.Abs (clickPoint.y - Screen.height)); 

			//Pull all cells within our selection
			selection = Game.getBetween (
				Camera.main.ScreenToWorldPoint (anchorFlipped), 
				Camera.main.ScreenToWorldPoint(clickFlipped));
			selection = Game.filterBy<CellBase> (selection);

			//Debug.Log ("Current selection: " + selection.Count + " cells.");

			//Clear AI from selected cells
			for (int i = 0; i < selection.Count; i++) 
				(selection [i] as GameObject).GetComponent<CellBase> ().setPlayerControl (true);

			//Change the game's status as needed
			Game.playerControllingCell = selection.Count > 0;

			//Temporary- control everything we select
			//Later, add code to check if we /can/ select this cell
			Game.controlled = selection;

			//Recompute center of mass
			computeCenter ();

			//Don't move
			targetCoords = new Vector2 (0, 0);
		} else { //unbind after click released
			//selection = new ArrayList(); //clear selection
			anchorPoint.x = 0;
			anchorPoint.y = 0;

			//If not selecting, then tell each cell to move to the requested location
			for (int i = 0; i < Game.controlled.Count; i++) {
				CellBase cb = (Game.controlled [i] as GameObject).GetComponent<CellBase> ();

				//If we have a target to go to, then move there
				if (targetCoords.x != 0 && targetCoords.y != 0) {
					cb.navigateTo (targetCoords.x, targetCoords.y);
				}
			}

			//calculate center position, velocity, and camera pos - unchanged if none selected
			computeCenter();
			//Locked camera- camera position is based on cell selection
			if (Game.controlled.Count != 0 && GameOptions.lockedCamera) 
				centerCamera ();
		}

		//Unlocked camera- pan based on distance to edge of screen
		if (!GameOptions.lockedCamera) {
			float x = Input.mousePosition.x;
			float y = Input.mousePosition.y;

			//Limit the camera pan speed
			if (x < 0)
				x = 0;
			if (x > Screen.width)
				x = Screen.width;
			if (y < 0)
				y = 0;
			if (y > Screen.height)
				y = Screen.height;

			float panX = GameOptions.panBorderSize * Screen.width;
			float panY = GameOptions.panBorderSize * Screen.height;
			float speed = GameOptions.panSpeed;

			//NOTE: When zoom is removed, change the default pan speed to compensate.
			if (x < panX)
				CameraManager.xOffset -= Mathf.Pow (2, CameraManager.zoom) * Mathf.Abs (x - panX) / panX * speed;
			else if (x > Screen.width - panX)
				CameraManager.xOffset += Mathf.Pow (2, CameraManager.zoom) * Mathf.Abs (Screen.width - x - panX) / panX * speed;

			if (y < panY)
				CameraManager.yOffset -= Mathf.Pow (2, CameraManager.zoom) * Mathf.Abs (y - panY) / panY * speed;
			else if (y > Screen.height - panY)
				CameraManager.yOffset += Mathf.Pow (2, CameraManager.zoom) * Mathf.Abs (Screen.height - y - panY) / panY * speed;
		}

		//player is in control of at least one cell
		if (Game.playerControllingCell) { 
			//movement logic
			if (Input.GetKey (KeyBindings.placeMvtMkr)) {
				Vector3 wrldpnt = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				targetCoords = new Vector2 (wrldpnt.x, wrldpnt.y);

				if (flag != null)
					Game.destroy (flag);
				flag = Game.create ("Flag", wrldpnt.x, wrldpnt.y);
			}

			Debug.Log ("Controlling " + Game.controlled.Count + " cell objects.");
		}
	}

	//Handle any GUI related to movement - RMB selection goes here
	void OnGUI() {
		if (anchorPoint.x != 0 && anchorPoint.y != 0) {
			float minX = Mathf.Min (anchorPoint.x, clickPoint.x);
			float minY = Mathf.Min (anchorPoint.y, clickPoint.y);

			float width = Mathf.Abs (anchorPoint.x - clickPoint.x);
			float height = Mathf.Abs (anchorPoint.y - clickPoint.y);

			//Draw the base
			GUI.DrawTexture (new Rect (minX, minY, width, height), selectMainTex);

			//Draw the outline
			GUI.DrawTexture(new Rect(minX, minY, trimWidth, height), selectTrimTex);
			GUI.DrawTexture(new Rect(minX + trimWidth, minY + height - trimWidth, width - (2 * trimWidth), trimWidth), selectTrimTex);
			GUI.DrawTexture(new Rect(minX + width - trimWidth, minY, trimWidth, height), selectTrimTex);
			GUI.DrawTexture(new Rect(minX + trimWidth, minY, width - (2 * trimWidth), trimWidth), selectTrimTex);
		}
	}

	/**
	 * Recalculates the values "averagePos" and "averageVel", or sets them
	 * to (0, 0) if this is impossible or undefined.
	 */
	void computeCenter() {
		averagePos = new Vector2 (0, 0);

		if (Game.controlled.Count != 0) {
			for (int i = 0; i < Game.controlled.Count; i++) {
				CellBase cb = (Game.controlled [i] as GameObject).GetComponent<CellBase> ();

				averagePos.x += cb.position.x;
				averagePos.y += cb.position.y;
			}
			averagePos.x /= Game.controlled.Count;
			averagePos.y /= Game.controlled.Count;
		}
	}

	void centerCamera() {
		//"deltaTime" is the amount of time passed since the last frame.
		//Multiplying this by the velocity smoothes out the laggy camera.
		//float del = Time.deltaTime;
		//CameraManager.xOffset = transform.position.x + (physBody.velocity.x * del);
		//CameraManager.yOffset = transform.position.y + (physBody.velocity.y * del);

		Debug.Log ("Camera recentering: " + averagePos.x + ", " + averagePos.y);

		//We set the camera to the average of the controlled cells' positions
		CameraManager.xOffset = averagePos.x;
		CameraManager.yOffset = averagePos.y;
	}
}
