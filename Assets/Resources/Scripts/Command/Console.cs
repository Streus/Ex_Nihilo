﻿using UnityEngine;
using System.Collections;

using System;
using UnityEngine.UI;
using UnityEditor;

public class Console : CommandBase {
	public override string getInvocation() {
		return "console";
	}

	public override string getHelpMessage() {
		return "provides various console commands";
	}

	public override void Execute(string[] args) {
		switch (args [1]) {
		case "color":
			float r = DevConsole._transform.GetChild (0).GetComponent<Image> ().color.r;
			float g = DevConsole._transform.GetChild (0).GetComponent<Image> ().color.g;
			float b = DevConsole._transform.GetChild (0).GetComponent<Image> ().color.b;
			float a = DevConsole._transform.GetChild (0).GetComponent<Image> ().color.a;

			//resiliant input- if any fail, then use a default value
			try {
				r = (int.Parse (args [2])) / 255F;
			} catch (Exception) {
			}
			try {
				g = (int.Parse (args [3])) / 255F;
			} catch (Exception) {
			}
			try {
				b = (int.Parse (args [4])) / 255F;
			} catch (Exception) {
			}
			try {
				a = (int.Parse (args [5])) / 255F;
			} catch (Exception) {
			}

			DevConsole._transform.GetChild (0).GetComponent<Image> ().color = new Color (r, g, b, a);
			DevConsole._transform.GetChild (1).GetComponent<Image> ().color = new Color (r, g, b, a);

			//try to match foreground text color against the color chosen
			if ((r * 0.299 + g * 0.587 + b * 0.114) > 0.729) {
				DevConsole.inputField.textComponent.color = Color.black;
				DevConsole.overflowField.color = Color.black;
			} else {
				DevConsole.inputField.textComponent.color = Color.white;
				DevConsole.overflowField.color = Color.white;
			}
			break;
		case "size":
			//if bad input, default to no change
			int change = 0;

			try {
				change = int.Parse (args [2]);
			} catch (Exception) {
			}

			Vector2 textMin = DevConsole._transform.GetChild (1).GetComponent<RectTransform> ().offsetMin;
			Vector2 inputMin = DevConsole._transform.GetChild (0).GetComponent<RectTransform> ().offsetMin;
			Vector2 inputMax = DevConsole._transform.GetChild (0).GetComponent<RectTransform> ().offsetMax;

			textMin.y -= change;
			inputMin.y -= change;
			inputMax.y -= change;

			DevConsole._transform.GetChild (1).GetComponent<RectTransform> ().offsetMin = textMin;
			DevConsole._transform.GetChild (0).GetComponent<RectTransform> ().offsetMin = inputMin;
			DevConsole._transform.GetChild (0).GetComponent<RectTransform> ().offsetMax = inputMax;
			break;
		}
	}
}
