using UnityEngine;
using System.Collections;

public class Version : CommandBase {
	public override string getInvocation() {
		return "version";
	}

	public override string getHelpMessage() {
		return "prints out Unity version information";
	}

	public override void Execute(string[] args) {
		Println("Unity Version: "+ Application.unityVersion);
	}
}
