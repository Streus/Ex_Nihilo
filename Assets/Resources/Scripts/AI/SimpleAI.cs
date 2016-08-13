using UnityEngine;
using System.Collections;

public class SimpleAI : BaseAI {

	private float speed;
	private float direction;

	// Use this for initialization
	public override void Start () {
		speed = Random.Range (0f, 1f);
		direction = Random.Range (0, Mathf.PI * 2);
		cell.turn (direction);
	}
	
	// Update is called once per frame
	public override void Update () {

		speed += Random.Range (-0.1f, 0.1f);
		direction += Random.Range (-0.4f, 0.4f);

		cell.move (speed);
		cell.turn (direction);
	}

	public override string toString() {
		return "Simple AI";
	}
}
