using UnityEngine;
using System.Collections;

public abstract class Ability {

	// The ability's display name
	public string name;

	// The ability's description flavor-text
	public string desc;

	// The graphic associated with this ability
	public Sprite image;

	// The amount of energy this ability consumes
	public int cost;

	// The time the invokee must wait in 
	public int cooldown;

	// The current cooldown
	public int currentCD;

	// The invoker using the ability
	public CellBase invoker;

	// A basic, empty constructor
	public Ability(CellBase cb) {
		invoker = cb;
		currentCD = 0;
	}

	// Decrement the cooldown variable
	public void updateCooldown(int cooldownRate) {
		cooldown -= cooldownRate;
		if(currentCD < 0)
			currentCD = 0;
	}

	// Invoke the ability
	// Param: the amount of energy available to the invoker
	// Returns true if the ability was successfully used
	// Returns false if the ability was on cooldown, or the invoker has insufficent energy
	public abstract bool use();
}
