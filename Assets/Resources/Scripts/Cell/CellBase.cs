using UnityEngine;
using System.Collections;

public class CellBase : MonoBehaviour {
	//~~Cell definition variables~~//

	public Shape shape = Shape.Circle; //one of the enums below
	public enum Shape {Circle};

	//Size in units. For a circle, this is its radius.
	public float size = 0.2f;

	public float maxSpeed = 10f;   
	public float turnSpeed = 10f;  //max turn speed in radians/frame

	public Vector2 position; //x, y position
	public float rotation;   //rotation (in radians)

	//~~Internal logic variables~~//

	private Vector2 targetCoords; //for click-to-move
	private Rigidbody2D physBody; //for physics calculations

	private Vector2 anchorPoint;   //Where the user right-clicks to select
	private Vector2 clickPoint;    //Where the user is currently right-clicking

	public GameObject[] attached = new GameObject[360]; //what is attached to us?

	//The AI this cell is using. Keep this null for player control.
	public BaseAI aiProgram = null;

	//Selection overlay options below
	public static Color selectMainColor = new Color(1, 1, 1, 0.3f); //internal color
	public static Color selectTrimColor = new Color(0, 0, 0, 0.3f); //edge color

	private static Texture2D selectMainTex;
	private static Texture2D selectTrimTex;

	public static int trimWidth = 3; //how wide are the edges?

	private static ArrayList selection;

	//~~Gameplay specific variables~~/

	public static GameObject flag; //movement flag

	// Use this for initialization
	void Start () {
		//Set up the cell-specific variables
		targetCoords = new Vector2 (0, 0);
		physBody = GetComponent<Rigidbody2D> ();

		clickPoint = new Vector2(0, 0);

		position = transform.position;
		rotation = transform.rotation.eulerAngles.z;

		//Look for any AI scripts
		aiProgram = GetComponent<BaseAI> ();

		//Set up the cell-specific variables
		physBody = GetComponent<Rigidbody2D> ();

		//If any AI scripts are found, run their init
		if (aiProgram != null) {
			aiProgram.setCell (this);
			aiProgram.Start ();
		} else {
			//If no AI, then player control init
			centerCamera ();
		}

		//Selection overlay
		selectMainTex = new Texture2D (1, 1);
		selectMainTex.SetPixel (0, 0, selectMainColor);
		selectMainTex.Apply ();

		selectTrimTex = new Texture2D (1, 1);
		selectTrimTex.SetPixel (0, 0, selectTrimColor);
		selectTrimTex.Apply ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//If there is an AI, then run that code
		if (aiProgram != null)
			aiProgram.Update ();
		else { //Player control code
			if(Input.GetKey(KeyBindings.select)) {
				Vector2 mousePoint = Input.mousePosition;
				if (anchorPoint.x == 0 && anchorPoint.y == 0)
					anchorPoint = new Vector2 (mousePoint.x, Screen.height - mousePoint.y);

				clickPoint = new Vector2 (mousePoint.x, Screen.height - mousePoint.y);

				selection = Game.getBetween (
					Camera.main.ScreenToWorldPoint(anchorPoint), 
					Camera.main.ScreenToWorldPoint(clickPoint));
				selection = Game.filterBy<CellBase> (selection);
			} else { //unbind after click released
				selection = new ArrayList(); //clear selection
				anchorPoint.x = 0;
				anchorPoint.y = 0;
			}

			Debug.Log ("Selecting " + selection.Count + " cell objects.");

			if (Input.GetKey (KeyBindings.placeMvtMkr)){
				Vector3 wrldpnt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				targetCoords = new Vector2 (wrldpnt.x, wrldpnt.y);

				if (flag != null) 
					Destroy (flag);
				flag = (GameObject)Instantiate (Resources.Load ("Prefabs/Flag"), 
					new Vector2 (wrldpnt.x, wrldpnt.y),
					Quaternion.identity);
			}

			navigateTo (targetCoords.x, targetCoords.y);
			centerCamera ();

			//Update key variables
			position = transform.position;
			rotation = transform.rotation.eulerAngles.z;

			for (int i = 0; i < 360; i++) {
				if (attached [i] != null) {
					if (shape == Shape.Circle) {
						Vector2 newPos = position;

						float del = Time.deltaTime;
						newPos.x += (physBody.velocity.x * del) + (size * Mathf.Cos (Mathf.Deg2Rad * (i + rotation)));
						newPos.y += (physBody.velocity.y * del) + (size * Mathf.Sin (Mathf.Deg2Rad * (i + rotation)));
						attached [i].transform.position = newPos;

						Quaternion newRot = Quaternion.Euler(0, 0, rotation + i + 180);
						attached [i].transform.rotation = newRot;
						//attached[i].transform.Rotate(0, 0, rotation);
					}
				}
			}
		}
	}

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
	public void move(float speed) { 
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
