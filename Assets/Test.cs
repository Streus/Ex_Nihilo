using UnityEngine;
using System.Collections;

using System.IO;
using System.Reflection;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Assembly assembly = Assembly.GetExecutingAssembly ();
		ArrayList commands = new ArrayList();

		string help = "";

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
					CommandBase cb = assembly.CreateInstance (rawClass)) as CommandBase;
					commands.Add (cb);
					help += cb.getInvocation().ToLower() + ": " + cb.getHelpMessage();
				}
			}
		}


		//string help = "";
		//Assembly assembly = Assembly.GetExecutingAssembly ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
