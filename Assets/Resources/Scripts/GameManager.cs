using UnityEngine;
using System.Collections;
using UnityEditor;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
		if(GameObject.Find("/DevConsole") == null)
		{			
			PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/GameManagement/DevConsole", typeof(GameObject)));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
