using UnityEngine;
using System.Collections;

public class ResetControlsButton : MonoBehaviour {

	private GameManager gm;

	void Start(){
		gm = (GameManager)GameObject.Find("Game Manager").GetComponent<GameManager>();
	}

	public void resetAllControls()
	{
		gm.resetControls();
	}
}
