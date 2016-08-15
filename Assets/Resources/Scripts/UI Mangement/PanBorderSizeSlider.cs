using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanBorderSizeSlider : MonoBehaviour {

	Slider borderSlider;
	Text textElement;

	void Start(){
		borderSlider = transform.GetChild (1).GetComponent<Slider> ();
		borderSlider.value = GameOptions.panBorderSize;
		borderSlider.maxValue = Mathf.Min(Screen.width, Screen.height)/2;

		textElement = transform.GetChild(0).GetComponent<Text>();
		textElement.text = "Pan Border Size: " + borderSlider.value;
	}

	//update the master volume for the game.
	public void updateBorderSize(){
		GameOptions.panBorderSize = (int)borderSlider.value;
	}

	public void Update(){
		borderSlider.maxValue = Mathf.Min(Screen.width, Screen.height)/2;
		textElement.text = "Pan Border Size: " + borderSlider.value;
	}
}
