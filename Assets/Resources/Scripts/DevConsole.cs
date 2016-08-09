using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

using System.IO;
using System.Reflection;

public class DevConsole : MonoBehaviour {

	public static bool consoleEnabled = false;

	private static CanvasGroup canvasgroup;
	private static Text overflowField;
	private static InputField inputField;
	private static Scrollbar ofScroll;

	private static HistoryBuffer cmdHistory;
	public int historySize;
	public int hisIndex;
<<<<<<< HEAD

	//Command related variables
	public static Assembly assembly = Assembly.GetExecutingAssembly ();
	public static ArrayList commands = new ArrayList ();
=======
>>>>>>> origin/staging

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
		string baseDir = Directory.GetCurrentDirectory ();
		string[] files = Directory.GetFiles(baseDir + "/Assets/Resources/Scripts/Commands");
		foreach(string s in files) {
			if (s.EndsWith ("cs")) {
				Debug.Log ("Found file: " + s);

				int start = 1 + Mathf.Max (s.LastIndexOf ('/'), s.LastIndexOf ('\\'));
				int end = s.LastIndexOf ('.');

				string rawClass = s.Substring (start, end - start);

				if (rawClass.Equals ("CommandBase")) {
					Debug.Log ("Found command base- skipping");
				} else {
					Debug.Log ("Raw class: " + rawClass);
					CommandBase cb = assembly.CreateInstance (rawClass) as CommandBase;
					commands.Add (cb);
				}
			}
		}
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
			toggleEnabled ();
		}
	}

	// Toggle the state of the Developer Console
	public void toggleEnabled() {
		setEnabled (!getEnabled ());
	}

	public void setEnabled(bool to) {
		consoleEnabled = to;
		canvasgroup.blocksRaycasts = canvasgroup.interactable = consoleEnabled;
		if (consoleEnabled)
			canvasgroup.alpha = 1;
		else
			canvasgroup.alpha = 0;
	}

	public bool getEnabled() {
		return consoleEnabled;
	}

	// Attempt to parse the input text into a command and run it
	public void enterCommand()
	{
		//save command to history buffer
		cmdHistory.add(inputField.text);
		hisIndex = cmdHistory.getSize ();

		//bump entered text into the overflow field
		println (">" + inputField.text);
		ofScroll.value = 0;


		//parse the command text
		string command = inputField.text.ToLower() + " ";
		string[] arguments = command.Split (new char[]{ ' ', ',' });

		//find a command that matches
		switch (arguments[0]) {
		case "help":
			string text = "Here are all the commands (case insensitive):\n" +
			              "help: Prints out this help menu\n" +
			              "version: Returns Unity version information\n" +
						  "console [size] (amount): New size in pixels\n" +
			              "console [color] (r) (g) (b): Changes the color of the console\n";

			overflowField.text += text;
			break;
		case "checkUnityVersion":
			overflowField.text += "Unity Version " + Application.unityVersion + "\n";
			break;
		case "console":
			if (arguments [1] == "size") {
				
			}
			if (arguments [1] == "color") {
				int r = int.Parse(arguments [2]);
				int g = int.Parse(arguments [3]);
				int b = int.Parse(arguments [4]);
				transform.GetChild (0).GetComponent<Image> ().color = new Color (((float)r)/255F, ((float)g)/255F, ((float)b)/255F, 1);
				transform.GetChild (1).GetComponent<Image> ().color = new Color (((float)r)/255F, ((float)g)/255F, ((float)b)/255F, 1);
			}
			break;
		default:
			Debug.Log ("Invalid command or empty command line!");
			break;
		}
		//clear the input line text
		inputField.text = "";
	}

	//Print to the main text field
	public static void print(string str) {
		overflowField.text += str;
	}

	//Print (with extra newline) to the main text field
	public static void println(string str) {
		overflowField.text += str + "\n";
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
