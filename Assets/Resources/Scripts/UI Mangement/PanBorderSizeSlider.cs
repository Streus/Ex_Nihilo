using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanBorderSizeSlider : MonoBehaviour {

	Slider borderSlider;
	Text textElement;

	void Start(){
		borderSlider = transform.GetChild (1).GetComponent<Slider> ();
		borderSlider.value = GameOptions.panBorderSize;

		textElement = transform.GetChild(0).GetComponent<Text>();
		textElement.text = "Pan Border Size: " + borderSlider.value;
	}

	//update the master volume for the game.
	public void updateBorderSize(){
		GameOptions.panBorderSize = borderSlider.value;
	}

	public void Update(){
		textElement.text = "Pan Border Size: " + borderSlider.value;
	}
}
