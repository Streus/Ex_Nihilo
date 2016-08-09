using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(GameObject.Find("DevConsole") == null)
		{
			GameObject dvcnsl = (GameObject)Instantiate(Resources.Load("Prefabs/GameManagement/DevConsole.prefab", typeof(GameObject)));
			GameObject canvas = (GameObject)GameObject.Find("Canvas");
			dvcnsl.transform.parent = canvas.transform;
		}
		if(GameObject.Find("Bindings Manager") == null)
		{
			Instantiate(Resources.Load("Prefabs/GameManagement/BindingsManager.prefab", typeof(GameObject)));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
