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
		recieveBinding();
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
		case "select":
			keyVar = KeyBindings.select;
			break;
		case "placeMvtMkr":
			keyVar = KeyBindings.placeMvtMkr;
			break;
		case "holdGround":
			keyVar = KeyBindings.holdGround;
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
		case "ability7":
			keyVar = KeyBindings.ability7;
			break;
		case "ability8":
			keyVar = KeyBindings.ability8;
			break;
		case "ability9":
			keyVar = KeyBindings.ability9;
			break;
		case "ability10":
			keyVar = KeyBindings.ability10;
			break;
		case "pause":
			keyVar = KeyBindings.pause;
			break;
		default:
			keyVar = KeyCode.None;
			break;
		}
	}

	private void assignBinding()
	{
		switch (key) {
		case "select":
			KeyBindings.select = keyVar;
			break;
		case "placeMvtMkr":
			KeyBindings.placeMvtMkr = keyVar;
			break;
		case "holdGround":
			KeyBindings.holdGround = keyVar;
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
		case "ability7":
			KeyBindings.ability7 = keyVar;
			break;
		case "ability8":
			KeyBindings.ability8 = keyVar;
			break;
		case "ability9":
			KeyBindings.ability9 = keyVar;
			break;
		case "ability10":
			KeyBindings.ability10 = keyVar;
			break;
		case "pause":
			KeyBindings.pause = keyVar;
			break;
		default:
			keyVar = KeyCode.None;
			break;
		}
	}
}
