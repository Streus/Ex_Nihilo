using UnityEngine;
using System.Collections;

public class CellBase : MonoBehaviour {

	//The AI this cell is using. Keep this null for player control.
	public BaseAI aiProgram = null;

	//Below are cell-specific variables.
	public float maxSpeed;

	// Use this for initialization
	void Start () {
		//Look for any AI scripts
		aiProgram = GetComponent<BaseAI> ();

		//If any AI scripts are found, run their init
		if (aiProgram != null) {
			Game.playerControllingCell = false;
			aiProgram.setCell (this);
			aiProgram.Start ();
		} else {
			//If no AI, then player control init
			Game.playerControllingCell = true;
			centerCamera ();
		}

		//Set up the cell-specific variables
		maxSpeed = 0.075F;
	}
	
	// Update is called once per frame
	void Update () {
		//If there is an AI, then run that code
		if (aiProgram != null)
			aiProgram.Update ();
		else {
			//Player control code
			float x = transform.position.x;
			float y = transform.position.y;

			float xDiff = Input.GetAxis ("Horizontal");
			float yDiff = Input.GetAxis ("Vertical");

			moveHorizontal (xDiff);
			moveVertical (yDiff);

			centerCamera ();
		}
	}

	void centerCamera() {
		CameraControl.xOffset = transform.position.x;
		CameraControl.yOffset = transform.position.y;
	}

	//Control scheme based stuff below

	/**
	 * Horizontal movement. Valid ranges: [-1, 1].
	 * Negative values move left, positive values right.
	 */
	public void moveHorizontal(float power) {
		if (power < -1)
			power = -1;
		if (power > 1)
			power = 1;

		transform.position = new Vector2 (transform.position.x + (power * maxSpeed), transform.position.y);
	}

	/**
	 * Vertical movement. Valid ranges: [-1, 1].
	 * Negative values move down, positive values up.
	 */
	public void moveVertical(float power) {
		if (power < -1)
			power = -1;
		if (power > 1)
			power = 1;

		transform.position = new Vector2 (transform.position.x, transform.position.y + (power * maxSpeed));
	}
}
