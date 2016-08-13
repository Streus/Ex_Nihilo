using UnityEngine;
using System.Collections;

public class BaseAI : MonoBehaviour {

	protected CellBase cell;

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
