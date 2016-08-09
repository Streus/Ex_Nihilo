using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyRebindButton : MonoBehaviour {

	public string key;
	private KeyCode keyVar;

	private bool rebinding;

	private Text txt;

	// Use this for initialization
	void Start () {
		Debug.Log(key);
		rebinding = false;
		recieveBinding();
		txt = transform.GetChild(0).GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		if (rebinding && Input.anyKeyDown) 
		{
			foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) {
				if(Input.GetKey(vKey))
					keyVar = vKey;
			}
			assignBinding();
			rebinding = false;
		}
		txt.text = keyVar.ToString();
	}

	public void readyToRebind()
	{
		rebinding = true;
		keyVar = KeyCode.None;
		assignBinding();
		Text txt = transform.GetChild(0).GetComponent<Text>();
		txt.text = keyVar.ToString();
	}

	private void recieveBinding()
	{
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
		default:
			keyVar = KeyCode.Q;
			break;
		}
	}

	private void assignBinding()
	{
		switch (key) {
		case "forward":
			KeyBindings.forward = keyVar;
			break;
		case "backward":
			KeyBindings.backward = keyVar;
			break;
		case "turnRight":
			KeyBindings.turnRight = keyVar;
			break;
		case "turnLeft":
			KeyBindings.turnLeft = keyVar;
			break;
		case "placeMvtMkr":
			KeyBindings.placeMvtMkr = keyVar;
			break;
		case "ability1":
			KeyBindings.ability1 = keyVar;
			break;
		case "ability2":
			KeyBindings.ability2 = keyVar;
			break;
		case "ability3":
			KeyBindings.ability3 = keyVar;
			break;
		case "ability4":
			KeyBindings.ability4 = keyVar;
			break;
		case "ability5":
			KeyBindings.ability5 = keyVar;
			break;
		case "ability6":
			KeyBindings.ability6 = keyVar;
			break;
		case "interact":
			KeyBindings.interact = keyVar;
			break;
		default:
			keyVar = KeyCode.None;
			break;
		}
	}
}
