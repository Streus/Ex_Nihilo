using UnityEngine;
using System.Collections;

public class Help : CommandBase {
	public override string getInvocation() {
		return "help";
	}

	public override string getHelpMessage() {
		return "displays this help menu";
	}

	public override void Execute(string[] args) {
		foreach (CommandBase cb in DevConsole.commands) {
			Println(cb.getInvocation().ToLower() + ": " + cb.getHelpMessage());
		}
	}
}
