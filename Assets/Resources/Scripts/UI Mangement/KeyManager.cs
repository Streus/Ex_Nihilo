using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class KeyManager : MonoBehaviour 
{
	private string keyPath;

	// Use this for initialization
	void Start() 
	{
		keyPath = Application.persistentDataPath + "/keybindings.ini";
		loadKeyBindings ();
	}

	// Fill the KeyCode fields with values from keybindings.ini
	public void loadKeyBindings()
	{
		//save file doesn't exist, set to defaults and save the defaults to a new keybindings.ini
		if (!System.IO.File.Exists (keyPath)) 
		{
			KeyBindings.forward = KeyCode.W;
			KeyBindings.backward = KeyCode.S;
			KeyBindings.turnRight = KeyCode.D;
			KeyBindings.turnLeft = KeyCode.A;
			KeyBindings.placeMvtMkr = KeyCode.Mouse0;
			KeyBindings.ability1 = KeyCode.Alpha1;
			KeyBindings.ability2 = KeyCode.Alpha2;
			KeyBindings.ability3 = KeyCode.Alpha3;
			KeyBindings.ability4 = KeyCode.Alpha4;
			KeyBindings.ability5 = KeyCode.Alpha5;
			KeyBindings.ability6 = KeyCode.Alpha6;
			KeyBindings.interact = KeyCode.E;

			KeyBindings.mvtSet = false;

			File.Create (keyPath);
			saveKeyBindings ();
			return;
		}
			
		StreamReader keystream = new StreamReader (keyPath);
		string line;
		int[] bindings = new int[13];
		int count = 0;
		while(!string.IsNullOrEmpty(line = keystream.ReadLine ()))
		{
			line.Trim ();
			if (!(line.StartsWith ("[") && line.EndsWith ("]"))) 
			{
				string[] ln = line.Split (new char[] { '=' });
				bindings [count] = int.Parse (ln [1]);
				count++;
			}
		}
		keystream.Close ();
		KeyBindings.forward = (KeyCode)bindings [0];
		KeyBindings.backward = (KeyCode)bindings [1];
		KeyBindings.turnRight = (KeyCode)bindings [2];
		KeyBindings.turnLeft = (KeyCode)bindings [3];
		KeyBindings.placeMvtMkr = (KeyCode)bindings [4];
		KeyBindings.ability1 = (KeyCode)bindings [5];
		KeyBindings.ability2 = (KeyCode)bindings [6];
		KeyBindings.ability3 = (KeyCode)bindings [7];
		KeyBindings.ability4 = (KeyCode)bindings [8];
		KeyBindings.ability5 = (KeyCode)bindings [9];
		KeyBindings.ability6 = (KeyCode)bindings [10];
		KeyBindings.interact = (KeyCode)bindings [11];

		if(bindings[12] == 0)
			KeyBindings.mvtSet = false;
		else if(bindings[12] == 1)
			KeyBindings.mvtSet = true;
	}

	public void saveKeyBindings()
	{
		System.IO.File.WriteAllText (keyPath, 
			"[KeyBindings]\n" +
			"forward = " + (int)KeyBindings.forward + "\n" +
			"backward = " + (int)KeyBindings.backward + "\n" +
			"turnRight = " + (int)KeyBindings.turnRight + "\n" +
			"turnLeft = " + (int)KeyBindings.turnLeft + "\n" +
			"placeMvtMkr = " + (int)KeyBindings.placeMvtMkr + "\n" +
			"ability1 = " + (int)KeyBindings.ability1 + "\n" +
			"ability2 = " + (int)KeyBindings.ability2 + "\n" +
			"ability3 = " + (int)KeyBindings.ability3 + "\n" +
			"ability4 = " + (int)KeyBindings.ability4 + "\n" +
			"ability5 = " + (int)KeyBindings.ability5 + "\n" +
			"ability6 = " + (int)KeyBindings.ability6 + "\n" +
			"interact = " + (int)KeyBindings.interact + "\n" +
			"mvtSet = " + KeyBindings.mvtSet + "\n"
		);
	}
}

public static class KeyBindings{
	//movement set 1
	//mvtSet = true
	public static KeyCode forward;
	public static KeyCode backward;
	public static KeyCode turnRight;
	public static KeyCode turnLeft;

	//movement set 2
	//mvtSet = false;
	public static KeyCode placeMvtMkr;

	//abilities
	public static KeyCode ability1;
	public static KeyCode ability2;
	public static KeyCode ability3;
	public static KeyCode ability4;
	public static KeyCode ability5;
	public static KeyCode ability6;

	//misc
	public static KeyCode interact;
	public static bool mvtSet;
}