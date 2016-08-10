using UnityEngine;
using System.Collections;

public class SimpleAI : BaseAI {

	// Use this for initialization
	public override void Start () {
	}
	
	// Update is called once per frame
	public override void Update () {
		cell.addMovement (Random.Range(-speed, speed));
		cell.turn (Random.Range(-2, 2));
	}

	public override string toString() {
		return "Simple AI";
	}
}
