using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class SaveGameList : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject gamemanager = GameObject.Find("Game Manager");
		GameManager gm = gamemanager.GetComponent<GameManager>();
		string[] savegames = gm.getSaveGames();
		for(int i = 0; i < savegames.Length; i++)
		{
			addSaveGameButton(savegames[i]);
		}
		transform.parent.parent.GetChild(1).GetComponent<Scrollbar>().value = 1;
	}
	
	private void addSaveGameButton(string name)
	{
		RectTransform rect = transform.GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + 70f);

		GameObject newButt = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/GameManagement/SaveGameButton", typeof(GameObject)));
		newButt.transform.GetChild(0).GetComponent<Text>().text = name;
		newButt.transform.SetParent(transform, false);
	}
}
