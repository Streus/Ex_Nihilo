using UnityEngine;
using System.Collections;

public class SimpleAI : BaseAI {

	// Use this for initialization
	public override void Start () {
		cell.turn (Random.Range (0, Mathf.PI * 2));
	}
	
	// Update is called once per frame
	public override void Update () {
		cell.addMovement (1);
		cell.turn (Random.Range(-1f, 1f));
	}

	public override string toString() {
		return "Simple AI";
	}
}
