using UnityEngine;
using System.Collections;

public class ResetOptionsButton : MonoBehaviour {

	private GameManager gm;

	void Start(){
		gm = (GameManager)GameObject.Find("Game Manager").GetComponent<GameManager>();
	}

	public void resetAllOptions()
	{
		gm.resetOptions();
	}
}
