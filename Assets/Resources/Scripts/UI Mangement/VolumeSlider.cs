using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour {

	public AudioMixer gameMix;
	public string channel;
	Slider volSlider;

	void Start(){
		volSlider = transform.GetChild (1).GetComponent<Slider> ();
		float retval = 0f;
		gameMix.GetFloat (channel, out retval);
		volSlider.value = retval;
	}

	//update the master volume for the game.
	public void updateVolume()
	{
		gameMix.SetFloat (channel, volSlider.value);
	}
}
