using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveGameButton : MonoBehaviour {

	public void loadGame()
	{
		GameObject gamemanager = GameObject.Find("Game Manager");
		GameManager gm = gamemanager.GetComponent<GameManager>();
		gm.loadGame(transform.GetChild(0).GetComponent<Text>().text);
	}
}
