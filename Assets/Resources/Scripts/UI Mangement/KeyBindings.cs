using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class KeyBindings : MonoBehaviour {

	//movement set 1
	static KeyCode forward;
	static KeyCode backward;
	static KeyCode turnRight;
	static KeyCode turnLeft;

	//movement set 2
	static KeyCode placeMvtMkr;

	//abilities
	static KeyCode ability1;
	static KeyCode ability2;
	static KeyCode ability3;
	static KeyCode ability4;
	static KeyCode ability5;
	static KeyCode ability6;

	//misc
	static KeyCode interact;

	// Use this for initialization
	void Start() 
	{
		loadKeyBindings ();
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	// Fill the KeyCode fields with values from keybindings.ini
	public void loadKeyBindings() 
	{
		
	}
}
