using UnityEngine;
using System.Collections;

public class SimpleAI : BaseAI {

	// Use this for initialization
	public override void Start () {
		cell.turn (Random.Range (0, Mathf.PI * 2));
	}
	
	// Update is called once per frame
	public override void Update () {

		speed += Random.Range (-0.1f, 0.1f);
		direction += Random.Range (-0.4f, 0.4f);

		cell.addMovement (speed);
		cell.turn (direction);
	}

	public override string toString() {
		return "Simple AI";
	}
}
