using UnityEngine;
using System.Collections;

public class KeyRebindButton : MonoBehaviour {

	public string key;

	bool rebinding = false;

	// Use this for initialization
	void Start () {
		key = "default";
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
			switch (key) {
			case "forward":
				KeyBindings.forward = newKey;
				break;
			case "backward":
				KeyBindings.backward = newKey;
				break;
			case "turnRight":
				KeyBindings.turnRight = newKey;
				break;
			case "turnLeft":
				KeyBindings.turnLeft = newKey;
				break;
			case "placeMvtMkr":
				KeyBindings.placeMvtMkr = newKey;
				break;
			case "ability1":
				KeyBindings.ability1 = newKey;
				break;
			case "ability2":
				KeyBindings.ability2 = newKey;
				break;
			case "ability3":
				KeyBindings.ability3 = newKey;
				break;
			case "ability4":
				KeyBindings.ability4 = newKey;
				break;
			case "ability5":
				KeyBindings.ability5 = newKey;
				break;
			case "ability6":
				KeyBindings.ability6 = newKey;
				break;
			case "interact":
				KeyBindings.interact = newKey;
				break;
			}
			rebinding = false;
		}
	}

	public void readyToRebind()
	{
		rebinding = true;
	}
}
