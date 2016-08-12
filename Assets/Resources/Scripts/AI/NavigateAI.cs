using UnityEngine;
using System.Collections;

public class NavigateAI : BaseAI {

	private float targetX, targetY;

	// Use this for initialization
	public override void Start () {
		targetX = 0;
		targetY = 0;

		cell.turn (Random.Range (0f, Mathf.PI * 2));

		cell.maxSpeed *= 0.4f;
		//cell.turnSpeed *= 0.4f;
	}

	// Update is called once per frame
	public override void Update () {
		if (cell.navigateTo (targetX, targetY)) {
			targetX = Random.Range (-10f, 10f) + cell.position.x;
			targetY = Random.Range (-10f, 10f) + cell.position.y;
		}
	}

	public override string toString() {
		return "Navigate AI";
	}
}
