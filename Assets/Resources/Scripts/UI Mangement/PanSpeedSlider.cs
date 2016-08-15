using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanSpeedSlider : MonoBehaviour {

	Slider panSlider;
	Text textElement;

	void Start(){
		panSlider = transform.GetChild (1).GetComponent<Slider> ();
		panSlider.value = GameOptions.panSpeed;

		textElement = transform.GetChild(0).GetComponent<Text>();
		textElement.text = "Pan Speed: " + panSlider.value;
	}

	//update the master volume for the game.
	public void updatePanSpeed(){
		GameOptions.panSpeed = panSlider.value;
	}

	public void Update(){
		textElement.text = "Pan Speed: " + panSlider.value;
	}
}
