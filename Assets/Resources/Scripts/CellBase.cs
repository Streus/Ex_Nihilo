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

	public Vector2 position;

	private float actualSpeed;
	private float actualTurn;
	private float t;

	public static GameObject flag;

	// Use this for initialization
	void Start () {
		//Set up the cell-specific variables
		physBody = GetComponent<Rigidbody2D> ();
		targetCoords = new Vector2 (0, 0);
		position = transform.position;

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

				addMovement(Input.GetAxis("Vertical") * maxSpeed);
				turn(-Input.GetAxis("Horizontal") * turnSpeed);
			} else {
				//control set 2

				//Pull in mouse input and assign the variables
				if (Input.GetKey (KeyCode.Mouse0)){
					//Vector3 mspos = Input.mousePosition;
					//mspos.z = 10f;
					Vector3 wrldpnt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					targetCoords = new Vector2 (wrldpnt.x, wrldpnt.y);

					if (flag != null) 
						Destroy (flag);
					flag = (GameObject)Instantiate (Resources.Load ("Prefabs/Flag"), 
						new Vector2 (wrldpnt.x, wrldpnt.y),
						Quaternion.identity);
				}

				navigateTo (targetCoords.x, targetCoords.y);
			}
			centerCamera ();

			position = transform.position;
		}
	}

	void centerCamera() {
		//"deltaTime" is the amount of time passed since the last frame.
		//Multiplying this by the velocity smoothes out the laggy camera.
		float del = Time.deltaTime;
		CameraControl.xOffset = transform.position.x + (physBody.velocity.x * del);
		CameraControl.yOffset = transform.position.y + (physBody.velocity.y * del);
	}

	//Control scheme based stuff below

	/**
	 * Forward and backward movement. Valid range: [-1, 1]
	 * Positive values move forward, negative move backward;
	 */
	public void addMovement(float speed) { 
		if (speed < -1)
			speed = -1;
		if (speed > 1)
			speed = 1;
		physBody.AddForce (transform.right * maxSpeed * speed);
	}

	/**
	 * Turn. Valid ranges: [-2pi, 2pi].
	 * Negative values turn right, positive turn left.
	 */
	public void turn(float deltaRotation) {
		physBody.rotation += deltaRotation * turnSpeed;
	}
		
	/**
	 * Navigates to the given position.
	 * This is how control scheme 2 primarily functions.
	 * 
	 * @return: true if position is at the given coords (or close)
	 */
	public bool navigateTo(float x, float y) {
		bool toReturn = false;
		targetCoords = new Vector2 (x, y); 

		/*
		 * Get the distance between our current position and the targer.
		 * From there, move faster the further we are, with a deadzone.
		 * 
		 * TO-DO: make the deadzone dynamic, based on current velocity
		 * (that will make it stop so inertia perfectly drops you off)
		 */
		float dist = Vector2.Distance (physBody.position, targetCoords);
		//if (dist > 0.1f) {
		if(dist > physBody.velocity.magnitude && dist > 0.2f) {
			//move forward
			//addMovement (0.03f * Mathf.Pow(2.3f, dist)); //exponential gradient
			addMovement(1); //move as fast as possible to get there
			toReturn = true;
		}

		//Get our posiion and the target position
		Vector2 pos = physBody.position;
		Vector2 movementVector = new Vector2 (targetCoords.x - pos.x, targetCoords.y - pos.y);

		//Calculate angle using arctangent
		float angle = Mathf.Atan2 (movementVector.y, movementVector.x);

		/*
		 * Default behavior makes 0-PI positive, PI-2PI negative. 
		 * This corrects that to a better range of 0-2PI.
		 */
		if (angle < 0)
			angle += 2 * Mathf.PI;

		//Get our current facing angle
		float myAngle = transform.eulerAngles.z / 180 * Mathf.PI;

		//Check if it's better to turn clockwise or counterclockwise
		float turnAmount = 0;
		if (Mathf.Abs (angle - myAngle) > Mathf.PI)
			turnAmount = myAngle - angle;
		else 
			turnAmount = angle - myAngle;

		/*
		 * This next calculation prevents errors around the 0 radian zone- 
		 * if you try to cross from near-0 to near-2PI radians without this bit
		 * of code, then you might sometimes get a turn value of near-2PI rather
		 * than near-0 as it should be, causing an odd jump.
		 * 
		 * This essentially checks which of the clockwise or counterclockwise
		 * turns is less distance by treating distances from 0 and 2PI as equal.
		 */
		float absTurnAmt = Mathf.Abs (turnAmount);
		turnAmount = (turnAmount < 0 ? -1 : 1) * Mathf.Min(Mathf.Abs (absTurnAmt - 2 * Mathf.PI), absTurnAmt);

		//Do the turn
		//turn (turnAmount); //floaty
		turn(turnSpeed * Mathf.Sign(turnAmount)); //maximum turning capabilities

		return toReturn;
	}
}
