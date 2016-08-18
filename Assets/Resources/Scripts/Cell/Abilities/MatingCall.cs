using UnityEngine;
using System.Collections;

public class MatingCall : Ability {
	public MatingCall(CellBase cb) : base(cb){
		name = "Mating Call";
		desc = "Display your readiness to mate to nearby cells";
		image = (Sprite)Resources.Load("Sprites/Game/Abilities/ability_matingcall", typeof(Sprite));
		cost = 1;
		cooldown = 60;
	}

	// Use the Mating Call ability
	public override bool use()
	{
		currentCD = cooldown;
		return false;
	}
}
