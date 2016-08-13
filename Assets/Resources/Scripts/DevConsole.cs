using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

using System.IO;
using System.Reflection;

using UnityEngine.EventSystems;

public class DevConsole : MonoBehaviour {

	public static bool consoleEnabled = false;

	public static CanvasGroup canvasgroup;
	public static Text overflowField;
	public static InputField inputField;
	public static Scrollbar ofScroll;

	private static HistoryBuffer cmdHistory;
	public int historySize;
	public int hisIndex;

	//Command related variables
	public static Assembly assembly = Assembly.GetExecutingAssembly ();
	public static ArrayList commands;

	public static Transform _transform;

	// Use this for initialization
	void Start () {
		canvasgroup = GetComponent<CanvasGroup> ();
		consoleEnabled = !consoleEnabled;
		toggleEnabled ();

		overflowField = transform.GetChild (1).GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ();
		inputField = transform.GetChild (0).GetComponent<InputField> ();

		ofScroll = transform.GetChild (1).GetChild (2).GetComponent<Scrollbar> ();
		ofScroll.value = 0;

		cmdHistory = new HistoryBuffer(historySize);
		hisIndex = -1;

		//Load in the commands
		commands = new ArrayList();

		string baseDir = Directory.GetCurrentDirectory ();
		string[] files = Directory.GetFiles(baseDir + "/Assets/Resources/Scripts/Command");
		foreach(string s in files) {
			if (s.EndsWith ("cs")) {
				int start = 1 + Mathf.Max (s.LastIndexOf ('/'), s.LastIndexOf ('\\'));
				int end = s.LastIndexOf ('.');

				string rawClass = s.Substring (start, end - start);

				if (rawClass.Equals ("CommandBase")) {
					//Skip over
				} else {
					CommandBase cb = assembly.CreateInstance (rawClass) as CommandBase;
					commands.Add (cb);
				}
			}
		}

		//Create some static stuff
		_transform = transform;
	}

	void Awake () {
		//snap console to the screen
		RectTransform rect = GetComponent<RectTransform> ();
		rect.offsetMax = rect.offsetMin = new Vector2 (0, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			hisIndex++;
			if (hisIndex >= cmdHistory.getSize())
				hisIndex = 0;
			inputField.text = cmdHistory.get (hisIndex);
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			hisIndex--;
			if (hisIndex < 0)
				inputField.text = "";
			else
				inputField.text = cmdHistory.get (hisIndex);
		}
			
		if (Input.GetKeyDown (KeyCode.Escape)) {
			setEnabled (false);
		}

		if (Input.GetKeyDown (KeyCode.BackQuote)) {
			//remove stray backticks
			inputField.text = inputField.text.Replace("`", "");

			//allow tildes
			if (!(Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))) {
				toggleEnabled ();
			}
		}
	}

	// Toggle the state of the Developer Console
	public void toggleEnabled() {
		setEnabled (!getEnabled ());
	}

	public void setEnabled(bool to) {
		consoleEnabled = to;
		canvasgroup.blocksRaycasts = canvasgroup.interactable = consoleEnabled;
		if (consoleEnabled) {
			canvasgroup.alpha = 1;
			focus ();
		} else {
			canvasgroup.alpha = 0;
		}
	}

	public bool getEnabled() {
		return consoleEnabled;
	}

	//Focuses on the dev console, so you can start typing
	public void focus() {
		EventSystem.current.SetSelectedGameObject (inputField.gameObject);
		inputField.OnPointerClick (null);
	}

	// Attempt to parse the input text into a command and run it
	public void enterCommand()
	{
		//save command to history buffer
		cmdHistory.add(inputField.text);
		hisIndex = cmdHistory.getSize ();

		//parse the command text
		string command = inputField.text.ToLower() + " ";
		string[] arguments = command.Split (new char[]{ ' ', ',' });

		//find a command that matches
		bool foundCommand = false;
		for (int i = 0; i < commands.Count; i++) {
			CommandBase com = commands [i] as CommandBase;
			if (com.getInvocation ().Equals (arguments [0])) {
				com.Execute (arguments);
				foundCommand = true;
			}
		}

		//If no command found, report that
		if (!foundCommand) {
			Println ("invalid command: \"" + arguments [0] + "\"");
		}
			
		//clear the input line text
		inputField.text = "";
		ofScroll.value = 0;

		//retain focus
		focus();
	}

	//Print to the main text field
	public static void Print(string str) {
		overflowField.text += ">" + str;
	}

	//Print (with extra newline) to the main text field
	public static void Println(string str) {
		overflowField.text += ">" + str + "\n";
	}

	private class HistoryBuffer {

		private string[] ringbuffer;
		private int size;
		private bool looping;

		public HistoryBuffer(int bufferSize)
		{
			ringbuffer = new string[bufferSize];
			size = 0;
		}

		public void add(string str)
		{
			ringbuffer [size] = str;
			size++;
			if (size >= ringbuffer.Length) {
				size = 0;
				looping = true;
			}
		}

		public string get(int index)
		{
			return ringbuffer [index];
		}

		public int getFront()
		{
			if (!looping)
				return 0;
			else 
				return size;
		}

		public int getSize()
		{
			return size;
		}
	}
}
