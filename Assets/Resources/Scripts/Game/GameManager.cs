using UnityEngine;
using System.Collections;
using UnityEditor;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//ensure persistence
		DontDestroyOnLoad(transform.gameObject);

		//create a developer console object if one doesn't exist
		if(GameObject.Find("/DevConsole") == null)
		{			
			PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/GameManagement/DevConsole", typeof(GameObject)));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
