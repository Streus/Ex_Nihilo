using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour {

	//The direction in radians
	float direction;

	//The speed in units per tick
	float speed;

	// Use this for initialization
	void Start () {
		//Random from 0-2PI
		direction = Random.value * 2 * Mathf.PI;

		//Random from 1-10
		speed = Random.value * 0.1F;
	}
	
	// Update is called once per frame
	void Update () {
		float xDiff = Mathf.Cos (direction) * speed;
		float yDiff = Mathf.Sin (direction) * speed;

		transform.position = new Vector3 (transform.position.x + xDiff, transform.position.y + yDiff, transform.position.z);

		direction += Random.value * 0.6F;
		direction -= Random.value * 0.6F;
	}
}
