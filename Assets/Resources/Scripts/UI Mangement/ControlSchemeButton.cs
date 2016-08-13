using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlSchemeButton : MonoBehaviour {

	public GameObject forwardButton;
	public GameObject backwardButton;
	public GameObject turnRightButton;
	public GameObject turnLeftButton;
	public GameObject clickToMoveButton;

	// Use this for initialization
	void Start () {
		updateInteractable();
	}

	public void updateInteractable()
	{
		//movement set 1
		forwardButton.GetComponent<Button>().interactable = KeyBindings.mvtSet;
		backwardButton.GetComponent<Button>().interactable = KeyBindings.mvtSet;
		turnRightButton.GetComponent<Button>().interactable = KeyBindings.mvtSet;
		turnLeftButton.GetComponent<Button>().interactable = KeyBindings.mvtSet;

		//movement set 2
		clickToMoveButton.GetComponent<Button>().interactable = !KeyBindings.mvtSet;
	}
	
	public void switchMovementSet()
	{
		KeyBindings.mvtSet = !KeyBindings.mvtSet;
		updateInteractable();
	}
}
