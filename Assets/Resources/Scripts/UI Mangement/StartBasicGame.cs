using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartBasicGame : MonoBehaviour 
{
	public void gotoGameScene()
	{
		SceneManager.LoadScene("Game");
	}
}
