using UnityEngine;
using System.Collections;

public class CellBase : MonoBehaviour {
	//The AI this cell is using. Keep this null for player control.
	public BaseAI aiProgram = null;

	//Below are cell-specific variables.
	public bool controlSet;
	public float maxSpeed;
	public float turnSpeed;
	private Vector2 targetCoords;
	private Rigidbody2D physBody;

	// Use this for initialization
	void Start () {
		//find KeyBindings object

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
		physBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//If there is an AI, then run that code
		if (aiProgram != null)
			aiProgram.Update ();
		else {
			//Player control code
			if (controlSet) {
				//control set 1
				if (Input.GetKey (KeyCode.W))
					addMovement (maxSpeed);
				if (Input.GetKey (KeyCode.S))
					addMovement (-maxSpeed);

				float dRot = 0;
				if (Input.GetKey (KeyCode.A))
					dRot = 1;
				if (Input.GetKey (KeyCode.D))
					dRot = -1;
				turn (dRot);
			} else {
				//control set 2
				if (Input.GetKey (KeyCode.Mouse0))
					targetCoords = Input.mousePosition;

				if (Vector2.Distance (physBody.position, targetCoords) > maxSpeed) {
					//move forward
					addMovement (maxSpeed);
					//rotate to face targetCoords
					Vector2 dPos = physBody.position - targetCoords;
					float targetRot = Mathf.Atan2 (dPos.y, dPos.x) * Mathf.Rad2Deg;
					physBody.rotation = Mathf.MoveTowardsAngle (physBody.rotation, targetRot, turnSpeed);
				}
			}
			centerCamera ();
		}
	}

	void centerCamera() {
		CameraControl.xOffset = transform.position.x;
		CameraControl.yOffset = transform.position.y;
	}

	//Control scheme based stuff below

	/**
	 * Forward and backward movement. Valid range: [-inf, inf]
	 * Positive values move forward, negative move backward;
	 */
	public void addMovement(float speed) {
		physBody.AddForce (transform.right * speed);
		//transform.position = new Vector2 (transform.position.x + (power * maxSpeed), transform.position.y);
	}

	/**
	 * Turn. Valid ranges: [-2pi, 2pi].
	 * Negative values turn right, positive turn left.
	 */
	public void turn(float deltaRotation) {
		physBody.rotation += deltaRotation * turnSpeed;
		//transform.position = new Vector2 (transform.position.x, transform.position.y + (power * maxSpeed));
	}
}
