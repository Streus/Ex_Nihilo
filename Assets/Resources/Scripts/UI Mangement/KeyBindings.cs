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
	public static int mvtSet;

	private string keyPath;

	// Use this for initialization
	void Start() 
	{
		keyPath = Application.persistentDataPath + "/keybindings.ini";
		loadKeyBindings ();
	}

	void Awake()
	{
		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	// Fill the KeyCode fields with values from keybindings.ini
	public void loadKeyBindings()
	{
		//save file doesn't exist, set to defaults and save the defaults to a new keybindings.ini
		if (!File.Exists (keyPath)) 
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

			mvtSet = 0;

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

		mvtSet = bindings [12];
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

	public string convKeyCodeToString(KeyCode kc)
	{
		switch (kc) 
		{
		case KeyCode.A:
			return "A";
		case KeyCode.Alpha0:
			return "0";
		case KeyCode.Alpha1:
			return "1";
		case KeyCode.Alpha2:
			return "2";
		case KeyCode.Alpha3:
			return "3";
		case KeyCode.Alpha4:
			return "4";
		case KeyCode.Alpha5:
			return "6";
		case KeyCode.Alpha7:
			return "7";
		case KeyCode.Alpha8:
			return "8";
		case KeyCode.Alpha9:
			return "9";
		case KeyCode.Ampersand:
			return "&";
		case KeyCode.Asterisk:
			return "*";
		case KeyCode.At:
			return "@";
		case KeyCode.B:
			return "B";
		case KeyCode.BackQuote:
			return "`";
		case KeyCode.Backslash:
			return "\\";
		case KeyCode.Backspace:
			return "Backspace";
		case KeyCode.C:
			return "C";
		case KeyCode.CapsLock:
			return "CAPSLOCK";
		case KeyCode.Comma:
			return ",";
		case KeyCode.LeftCommand:
			return "L Cmd";
		case KeyCode.LeftControl:
			return "L Ctrl";
		case KeyCode.RightCommand:
			return "R Cmd";
		case KeyCode.RightControl:
			return "R Ctrl";
		case KeyCode.D:
			return "D";
		case KeyCode.Delete:
			return "Delete";
		case KeyCode.DownArrow:
			return "Down";
		case KeyCode.KeypadDivide:
			return "Numpad /";
		case KeyCode.PageDown:
			return "Page Down";
		case KeyCode.E:
			return "E";
		case KeyCode.End:
			return "End";
		case KeyCode.Equals:
			return "=";
		case KeyCode.Escape:
			return "Esc";
		case KeyCode.KeypadEnter:
			return "Numpad Enter";
		case KeyCode.KeypadEquals:
			return "Numpad =";
		case KeyCode.F:
			return "F";
		case KeyCode.F1:
			return "F1";
		case KeyCode.F2:
			return "F2";
		case KeyCode.F3:
			return "F3";
		case KeyCode.F4:
			return "F4";
		case KeyCode.F5:
			return "F5";
		case KeyCode.F6:
			return "F6";
		case KeyCode.F7:
			return "F7";
		case KeyCode.F8:
			return "F8";
		case KeyCode.F9:
			return "F9";
		case KeyCode.F10:
			return "F10";
		case KeyCode.F11:
			return "F11";
		case KeyCode.F12:
			return "F12";
		case KeyCode.G:
			return "G";
		case KeyCode.H:
			return "H";
		case KeyCode.Home:
			return "Home";
		case KeyCode.I:
			return "I";
		case KeyCode.Insert:
			return "Insert";
		case KeyCode.J:
			return "J";
		case KeyCode.K:
			return "K";
		case KeyCode.Keypad0:
			return "Numpad 0";
		case KeyCode.Keypad1:
			return "Numpad 1";
		case KeyCode.Keypad2:
			return "Numpad 2";
		case KeyCode.Keypad3:
			return "Numpad 3";
		case KeyCode.Keypad4:
			return "Numpad 4";
		case KeyCode.Keypad5:
			return "Numpad 5";
		case KeyCode.Keypad6:
			return "Numpad 6";
		case KeyCode.Keypad7:
			return "Numpad 7";
		case KeyCode.Keypad8:
			return "Numpad 8";
		case KeyCode.Keypad9:
			return "Numpad 9";
		case KeyCode.KeypadMinus:
			return "Numpad -";
		case KeyCode.KeypadMultiply:
			return "Numpad *";
		case KeyCode.KeypadPeriod:
			return "Numpad .";
		case KeyCode.KeypadPlus:
			return "Numpad +";
		case KeyCode.L:
			return "L";
		case KeyCode.LeftAlt:
			return "L Alt";
		case KeyCode.LeftArrow:
			return "Left";
		case KeyCode.LeftBracket:
			return "[";
		case KeyCode.LeftShift:
			return "L Shift";
		case KeyCode.LeftWindows:
			return "Windows";
		case KeyCode.ScrollLock:
			return "Scroll Lock";
		case KeyCode.M:
			return "M";
		case KeyCode.Menu:
			return "Menu";
		case KeyCode.Minus:
			return "-";
		case KeyCode.Mouse0:
			return "Mouse0";
		case KeyCode.Mouse1:
			return "Mouse1";
		case KeyCode.Mouse2:
			return "Mouse2";
		case KeyCode.Mouse3:
			return "Mouse3";
		case KeyCode.Mouse4:
			return "Mouse4";
		case KeyCode.Mouse5:
			return "Mouse5";
		case KeyCode.Mouse6:
			return "Mouse6";
		case KeyCode.N:
			return "N";
		case KeyCode.Numlock:
			return "Numlock";
		case KeyCode.O:
			return "O";
		case KeyCode.P:
			return "P";
		case KeyCode.PageUp:
			return "Page Up";
		case KeyCode.Pause:
			return "Pause";
		case KeyCode.Period:
			return ".";
		case KeyCode.Print:
			return "Print";
		default: 
			return "Unknown";
		}
	}
}
