﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class DevConsole : MonoBehaviour {

	public static bool consoleEnabled = false;

	private CanvasGroup canvasgroup;
	private Text overflowField;
	private InputField inputField;
	private Scrollbar ofScroll;

	private HistoryBuffer cmdHistory;
	public int historySize;
	public int hisIndex;

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
	}

	// Toggle the state of the Developer Console
	public void toggleEnabled()
	{
		consoleEnabled = !consoleEnabled;
		canvasgroup.blocksRaycasts = canvasgroup.interactable = consoleEnabled;
		if (consoleEnabled)
			canvasgroup.alpha = 1;
		else
			canvasgroup.alpha = 0;
	}

	// Attempt to parse the input text into a command and run it
	public void enterCommand()
	{
		//save command to history buffer
		cmdHistory.add(inputField.text);
		hisIndex = cmdHistory.getSize ();

		//bump entered text into the overflow field
		overflowField.text += ">" + inputField.text + "\n";
		ofScroll.value = 0;

		//parse the command text
		string command = inputField.text + " ";
		int argumentEnd = 0;
		string[] arguments = new string[10];
		int argPos = 0;
		while (command.Length > 0 && argPos < arguments.Length) {
			argumentEnd = command.IndexOf (' ');
			arguments [argPos] = command.Substring (0, argumentEnd);
			argPos++;
			command = command.Substring (argumentEnd + 1);
		}

		//find a command that matches
		switch (arguments[0]) {
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
				transform.GetChild (0).GetComponent<Image> ().color = new Color ((float)r, (float)g, (float)b, 255f);
				transform.GetChild (1).GetComponent<Image> ().color = new Color ((float)r, (float)g, (float)b, 255f);
			}
			break;
		default:
			Debug.Log ("Invalid command or empty command line!");
			break;
		}
		//clear the input line text
		inputField.text = "";
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