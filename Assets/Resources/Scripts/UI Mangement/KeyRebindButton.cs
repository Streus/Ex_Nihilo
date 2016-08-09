using UnityEngine;
using System.Collections;

public class KeyRebindButton : MonoBehaviour {

	public string key;
	private KeyCode keyVar;

	bool rebinding = false;

	// Use this for initialization
	void Start () {
		key = "default";
		switch (key) {
		case "forward":
			keyVar = KeyBindings.forward;
			break;
		case "backward":
			keyVar = KeyBindings.backward;
			break;
		case "turnRight":
			keyVar = KeyBindings.turnRight;
			break;
		case "turnLeft":
			keyVar = KeyBindings.turnLeft;
			break;
		case "placeMvtMkr":
			keyVar = KeyBindings.placeMvtMkr;
			break;
		case "ability1":
			keyVar = KeyBindings.ability1;
			break;
		case "ability2":
			keyVar = KeyBindings.ability2;
			break;
		case "ability3":
			keyVar = KeyBindings.ability3;
			break;
		case "ability4":
			keyVar = KeyBindings.ability4;
			break;
		case "ability5":
			keyVar = KeyBindings.ability5;
			break;
		case "ability6":
			keyVar = KeyBindings.ability6;
			break;
		case "interact":
			keyVar = KeyBindings.interact;
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		if (rebinding && Input.anyKeyDown) 
		{
			KeyCode newKey = KeyCode.None;
			foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) {
				if(Input.GetKey(vKey))
					newKey = vKey;
			}
			rebinding = false;
		}
	}

	public void readyToRebind()
	{
		rebinding = true;
	}
}
