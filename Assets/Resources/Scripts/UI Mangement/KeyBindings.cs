using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class KeyBindings : MonoBehaviour {

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

	private string keyPath;

	// Use this for initialization
	void Start() 
	{
		keyPath = Application.persistentDataPath + "/keybindings.ini";
		loadKeyBindings ();
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	// Fill the KeyCode fields with values from keybindings.ini
	public void loadKeyBindings()
	{
		//save file doesn't exist, set to defaults and save the defaults to a new keybindings.ini
		if (!System.IO.File.Exists (keyPath)) 
		{
			forward = KeyCode.W;
			backward = KeyCode.S;
			turnRight = KeyCode.D;
			turnLeft = KeyCode.A;
			placeMvtMkr = KeyCode.Mouse0;
			ability1 = KeyCode.Alpha1;
			ability2 = KeyCode.Alpha2;
			ability3 = KeyCode.Alpha3;
			ability4 = KeyCode.Alpha4;
			ability5 = KeyCode.Alpha5;
			ability6 = KeyCode.Alpha6;
			interact = KeyCode.E;

			mvtSet = false;

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
		forward = (KeyCode)bindings [0];
		backward = (KeyCode)bindings [1];
		turnRight = (KeyCode)bindings [2];
		turnLeft = (KeyCode)bindings [3];
		placeMvtMkr = (KeyCode)bindings [4];
		ability1 = (KeyCode)bindings [5];
		ability2 = (KeyCode)bindings [6];
		ability3 = (KeyCode)bindings [7];
		ability4 = (KeyCode)bindings [8];
		ability5 = (KeyCode)bindings [9];
		ability6 = (KeyCode)bindings [10];
		interact = (KeyCode)bindings [11];

		if(bindings[12] == 0)
			mvtSet = false;
		else if(bindings[12] == 1)
			mvtSet = true;
	}

	public void saveKeyBindings()
	{
		System.IO.File.WriteAllText (keyPath, 
			"[KeyBindings]\n" +
			"forward = " + (int)forward + "\n" +
			"backward = " + (int)backward + "\n" +
			"turnRight = " + (int)turnRight + "\n" +
			"turnLeft = " + (int)turnLeft + "\n" +
			"placeMvtMkr = " + (int)placeMvtMkr + "\n" +
			"ability1 = " + (int)ability1 + "\n" +
			"ability2 = " + (int)ability2 + "\n" +
			"ability3 = " + (int)ability3 + "\n" +
			"ability4 = " + (int)ability4 + "\n" +
			"ability5 = " + (int)ability5 + "\n" +
			"ability6 = " + (int)ability6 + "\n" +
			"interact = " + (int)interact + "\n" +
			"mvtSet = " + mvtSet + "\n"
		);
	}
}
