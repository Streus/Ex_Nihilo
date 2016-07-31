using UnityEngine;
using System.Collections;

public class BaseAI : MonoBehaviour {

	protected CellBase cell;

	/*
	 * The direction we are facing in radians.
	 */
	protected float direction;

	/*
	 * The speed (from 0.0 to 1.0) determining the percentage of
	 * our max speed we should go at. 1.0 corresponds to the cell's
	 * maximum possible speed.
	 */
	protected float speed;

	/*
	 * Use this for initialization of your own data.
	 */
	public virtual void Start () {
	}
	
	/*
	 * Update is called once per frame - use this for making
	 * your AI do something every tick.
	 */
	public virtual void Update () {
	}

	/**
	 * Used internally to pass a reference of the specific
	 * cell we're working with - do not override, or your 
	 * AI will not work!
	 */
	public void setCell(CellBase cb) {
		cell = cb;
	}

	/**
	 * Returns the name of this AI script, and any
	 * other useful identifying information.
	 */
	public virtual string toString() {
		return "Base AI";
	}
}
