using UnityEngine;
using System.Collections;

public class SimpleAI : BaseAI {

	// Use this for initialization
	public override void Start () {
		//Random from 0-2PI
		direction = Random.value * 2 * Mathf.PI;

		//Random from 1-10
		speed = Random.value;
	}
	
	// Update is called once per frame
	public override void Update () {
		float xDiff = Mathf.Cos (direction) * speed;
		float yDiff = Mathf.Sin (direction) * speed;

		//transform.position = new Vector3 (transform.position.x + xDiff, transform.position.y + yDiff, transform.position.z);
		cell.moveHorizontal (xDiff);
		cell.moveVertical (yDiff);

		speed += Random.value * 0.2F;
		speed -= Random.value * 0.2F;

		direction += Random.value * 0.6F;
		direction -= Random.value * 0.6F;
	}

	public override string toString() {
		return "Simple AI";
	}
}
