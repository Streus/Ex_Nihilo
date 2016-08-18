using UnityEngine;
using System.Collections;

public class SaveOptionsButton : MonoBehaviour {

	private GameManager gm;

	void Start(){
		gm = (GameManager)GameObject.Find("Game Manager").GetComponent<GameManager>();
	}

	public void saveAllOptionsAndControls()
	{
		gm.saveOptions();
	}
}
