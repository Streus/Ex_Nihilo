using UnityEngine;
using System.Collections;

using System.IO;
using System.Reflection;

public class Help : CommandBase {
	public override string getInvocation() {
		return "help";
	}

	public override string getHelpMessage() {
		string help = "help: Displays this help menu\n";
		foreach (CommandBase cb in DevConsole.commands) {
			if (!(cb.getInvocation ().Equals ("help"))) {
				help += cb.getInvocation ().ToLower () + ": " + cb.getHelpMessage ();
			}
		}

		help = help.Substring(0, help.LastIndexOf("\n"));
		return help;
	}

	public override void Execute(string[] args) {
		print (getHelpMessage ());
	}
}
