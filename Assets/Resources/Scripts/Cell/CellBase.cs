using UnityEngine;
using System.Collections;

public class CellBase : MonoBehaviour {
	//~~Cell definition variables~~//
	public bool isPlayerControlling;

	public Shape shape = Shape.Circle; //one of the enums below
	public enum Shape {Circle};

	//Size in units. For a circle, this is its radius.
	public float size = 0.2f;

	public float maxSpeed = 10f;   
	public float turnSpeed = 10f;  //max turn speed in radians/frame

	public Vector2 targetCoords = new Vector2(0, 0);

	public Vector2 position; //x, y position
	public float rotation;   //rotation (in radians)

	//~~Internal logic variables~~//

	//private Vector2 targetCoords; //for click-to-move
	private Rigidbody2D physBody; //for physics calculations

	public GameObject[] attached = new GameObject[360]; //what is attached to us?

	//The AI this cell is using. Keep this null for player control.
	public BaseAI aiProgram = null;

	//~~Gameplay specific variables~~/

	// Use this for initialization
	void Start () {
		//Set up the cell-specific variables
		//targetCoords = new Vector2 (0, 0);
		physBody = GetComponent<Rigidbody2D> ();

		position = transform.position;
		rotation = transform.rotation.eulerAngles.z;

		//Look for any AI scripts
		aiProgram = GetComponent<BaseAI> ();

		//Set up the cell-specific variables
		physBody = GetComponent<Rigidbody2D> ();

		//If any AI scripts are found, pass them this cell as a reference
		if (aiProgram != null) {
			isPlayerControlling = false;
			aiProgram.setCell (this);
		} else {
			//If no AI, then player control init
			isPlayerControlling = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//If an AI is attached, it will update itself as needed.

		//Update key variables every frame
		position = transform.position;
		rotation = transform.rotation.eulerAngles.z;

		for (int i = 0; i < attached.Length; i++) {
			if (attached [i] != null) {
				if (shape == Shape.Circle) {
					Vector2 newPos = position;

					float del = Time.deltaTime;
					newPos.x += (physBody.velocity.x * del) + (size * Mathf.Cos (Mathf.Deg2Rad * (i + rotation)));
					newPos.y += (physBody.velocity.y * del) + (size * Mathf.Sin (Mathf.Deg2Rad * (i + rotation)));

					//Reposition and rotate attached objects
					attached [i].transform.position = newPos;
					attached [i].transform.rotation = Quaternion.Euler (0, 0, rotation + i + 180);
				}
			}
		}
	}

	/**
	 * Is a player controlling this cell?
	 * Set to true if so, false otherwise. 
	 */
	public void setPlayerControl(bool to) {
		isPlayerControlling = to;
		if (aiProgram != null) //disable AI if needed
			aiProgram.enabled = !to;
	}

	//Control scheme based stuff below

	/**
	 * Forward and backward movement. Valid range: [-1, 1]
	 * Positive values move forward, negative move backward;
	 */
	public void move(float speed) { 
		if (Game.paused)
			return;
		
		if (speed < -1)
			speed = -1;
		if (speed > 1)
			speed = 1;
		physBody.AddForce (transform.right * maxSpeed * speed);
	}

	/**
	 * Turn. Valid ranges: [-1, 1].
	 * Negative values turn right, positive turn left.
	 */
	public void turn(float deltaRotation) {
		if (Game.paused)
			return;
		
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

		if (x == 0 && y == 0)
			return true;

		targetCoords = new Vector2 (x, y); 

		/*
		 * Get the distance between our current position and the targer.
		 * From there, move faster the further we are, with a deadzone.
		 * 
		 * The deadzone is dynamic, and based on the most we can move in a
		 * given frame. We stop when our inertia will carry us.
		 */
		float dist = Vector2.Distance (physBody.position, targetCoords);
		//if (dist > 0.1f) {
		if(dist > physBody.velocity.magnitude && dist > (maxSpeed * Time.deltaTime)) {
			//move forward
			//move (0.03f * Mathf.Pow(2.3f, dist)); //exponential gradient
			move(1); //move as fast as possible to get there
			toReturn = true;
		}

		//Get our posiion and the target position
		//Vector2 pos = position;
		Vector2 movementVector = new Vector2 (targetCoords.x - position.x, targetCoords.y - position.y);

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
		/*
		 * The deadzone here is based on the maximum we can turn in a frame.
		 * This means that when we hit a point where turning at a speed of +/- 1
		 * would overshoot us, we just slow our turn and "snap" to the angle.
		 * 
		 * This prevents exploits for slow turn speed users by keeping the flag
		 * close by and snapping to the deadzone's range faster than the cell can turn.
		 */
		if (absTurnAmt > (turnSpeed * Time.deltaTime)) 
			turn (Mathf.Sign (turnAmount)); //turn as fast as we can towards the angle
		else //"snap" to the exact angle when within range of it
			turn (Mathf.Rad2Deg * turnAmount / turnSpeed);

		return toReturn;
	}

	/**
	 * Attach the given GameObject at the provided angle.
	 * Valid values are from [0, 359], and will be modulo'ed into this range.
	 */
	public void attach(GameObject obj, int angle) {
		if (angle < 0 || angle > 359)
			return;
		attached [angle] = obj;

		//For other cell base shapes, we need a different calculation (+ a variable
		//for storing the type)
		if (shape == Shape.Circle) {
			Vector2 newPos = position;
			newPos.x += Mathf.Cos (Mathf.Deg2Rad * angle);
			newPos.y += Mathf.Sin (Mathf.Deg2Rad * angle);
			obj.transform.position = newPos;
		}
	}

	public void attach(string name, int angle) {
		attach (Game.create (name), angle);
	}
}
