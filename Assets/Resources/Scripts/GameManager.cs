using UnityEngine;
using System.Collections;
using UnityEditor;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(GameObject.Find("/DevConsole") == null)
		{			
			PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/GameManagement/DevConsole", typeof(GameObject)));
		}
		if(GameObject.Find("BindingsManager") == null)
		{
			PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/GameManagement/BindingsManager", typeof(GameObject)));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
