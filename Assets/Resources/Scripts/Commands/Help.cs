using UnityEngine;
using System.Collections;

using System.IO;
using System.Reflection;

public class Help : CommandBase {
	public override string getInvocation() {
		return "help";
	}

	public override string getHelpMessage() {
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
				} if (rawClass.Equals ("Help")) {
					Debug.Log ("Found help command- skipping to avoid recursion");
				} else {
					Debug.Log ("Raw class: " + rawClass);
					CommandBase cb = assembly.CreateInstance (rawClass) as CommandBase;
					commands.Add (cb);
					help += cb.getInvocation().ToLower() + ": " + cb.getHelpMessage() + "\n";
				}
			}
		}

		help = "help: Displays this help menu\n" + help;
		help = help.Substring(0, help.LastIndexOf("\n"));
		return help;
	}

	public override void Execute(string[] args) {
		print (getHelpMessage ());
	}
}
