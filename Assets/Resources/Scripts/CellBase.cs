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

	private float actualSpeed;
	private float actualTurn;
	private float t;

	// Use this for initialization
	void Start () {
<<<<<<< HEAD
=======
		//Set up the cell-specific variables
		physBody = GetComponent<Rigidbody2D> ();

>>>>>>> origin/staging
		//Look for any AI scripts
		aiProgram = GetComponent<BaseAI> ();

		//Set up the cell-specific variables
		physBody = GetComponent<Rigidbody2D> ();

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
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//If there is an AI, then run that code
		if (aiProgram != null)
			aiProgram.Update ();
		else {
			//Player control code
			if (controlSet) {
<<<<<<< HEAD
				//control set 1
				if (Input.GetKey (KeyCode.W))
					addMovement (maxSpeed);
				if (Input.GetKey (KeyCode.S))
					addMovement (-maxSpeed);
				
				float dRot = 0;
				if (Input.GetKey (KeyCode.A))
					dRot = turnSpeed;
				if (Input.GetKey (KeyCode.D))
					dRot = -turnSpeed;
				turn (dRot);
=======
				addMovement(Input.GetAxis("Vertical") * maxSpeed);
				turn(-Input.GetAxis("Horizontal") * turnSpeed);
>>>>>>> origin/staging
			} else {
				//control set 2
				if (Input.GetKey (KeyCode.Mouse0)){
					Vector3 mspos = Input.mousePosition;
					mspos.z = 10f;
					Vector3 wrldpnt = Camera.main.ScreenToWorldPoint(mspos);
					targetCoords = new Vector2(wrldpnt.x, wrldpnt.y);
					Debug.Log(targetCoords);
				}

				if (Vector2.Distance (physBody.position, targetCoords) > 0.1f) {
					//move forward
					addMovement (maxSpeed);

					//rotate to face targetCoords
					float targetRot = Vector2.Angle(physBody.position, targetCoords);
					physBody.rotation = targetRot;
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
	}

	/**
	 * Turn. Valid ranges: [-2pi, 2pi].
	 * Negative values turn right, positive turn left.
	 */
	public void turn(float deltaRotation) {
		physBody.rotation += deltaRotation * turnSpeed;
	}
}
