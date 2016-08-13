using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour {

	public AudioMixer gameMix;

	void Start () 
	{
		//ensure persistence
		DontDestroyOnLoad(transform.gameObject);
			
		//make devconsole
		PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/GameManagement/DevConsole", typeof(GameObject)));

		loadOptions ();
	}
	
	//load key, screen, and volume options
	public void loadOptions()
	{
		//keybindings
		KeyBindings.forward = (KeyCode)PlayerPrefs.GetInt("keyforward", (int)KeyCode.W);
		KeyBindings.backward = (KeyCode)PlayerPrefs.GetInt("keybackward", (int)KeyCode.S);
		KeyBindings.turnRight = (KeyCode)PlayerPrefs.GetInt("keyturnR", (int)KeyCode.D);
		KeyBindings.turnLeft = (KeyCode)PlayerPrefs.GetInt("keyturnL", (int)KeyCode.A);
		KeyBindings.placeMvtMkr = (KeyCode)PlayerPrefs.GetInt("keyplacemvtmkr", (int)KeyCode.Mouse0);
		KeyBindings.ability1 = (KeyCode)PlayerPrefs.GetInt("keyability1", (int)KeyCode.Alpha1);
		KeyBindings.ability2 = (KeyCode)PlayerPrefs.GetInt("keyability2", (int)KeyCode.Alpha2);
		KeyBindings.ability3 = (KeyCode)PlayerPrefs.GetInt("keyability3", (int)KeyCode.Alpha3);
		KeyBindings.ability4 = (KeyCode)PlayerPrefs.GetInt("keyability4", (int)KeyCode.Alpha4);
		KeyBindings.ability5 = (KeyCode)PlayerPrefs.GetInt("keyability5", (int)KeyCode.Alpha5);
		KeyBindings.ability6 = (KeyCode)PlayerPrefs.GetInt("keyability6", (int)KeyCode.Alpha6);
		KeyBindings.interact = (KeyCode)PlayerPrefs.GetInt("keyinteract", (int)KeyCode.E);
		string moveSet = PlayerPrefs.GetString("movementconfiguration", "false");
		KeyBindings.mvtSet = bool.Parse(moveSet);

		//fullscreen
		string fllscrn = PlayerPrefs.GetString("fullscreen", "false");
		Screen.fullScreen = bool.Parse(fllscrn);

		//volume
		float masterVol = PlayerPrefs.GetFloat("mastervolume", 0f);
		gameMix.SetFloat("MasterVolume", masterVol);
		float musicVol = PlayerPrefs.GetFloat("musicvolume", 0f);
		gameMix.SetFloat("MusicVolume", musicVol);
		float effVol = PlayerPrefs.GetFloat("effectsvolume", 0f);
		gameMix.SetFloat("EffectsVolume", effVol);
	}

	//save key, screen, and volume options
	public void saveOptions()
	{
		//keybindings
		PlayerPrefs.SetInt("keyforward", (int)KeyBindings.forward);
		PlayerPrefs.SetInt("keybackward", (int)KeyBindings.backward);
		PlayerPrefs.SetInt("keyturnR", (int)KeyBindings.turnRight);
		PlayerPrefs.SetInt("keyturnL", (int)KeyBindings.turnLeft);
		PlayerPrefs.SetInt("keyplacemvtmkr", (int)KeyBindings.backward);
		PlayerPrefs.SetInt("keyability1", (int)KeyBindings.ability1);
		PlayerPrefs.SetInt("keyability2", (int)KeyBindings.ability2);
		PlayerPrefs.SetInt("keyability3", (int)KeyBindings.ability3);
		PlayerPrefs.SetInt("keyability4", (int)KeyBindings.ability4);
		PlayerPrefs.SetInt("keyability5", (int)KeyBindings.ability5);
		PlayerPrefs.SetInt("keyability6", (int)KeyBindings.ability6);
		PlayerPrefs.SetInt("keyinteract", (int)KeyBindings.interact);
		PlayerPrefs.SetString("movementconfiguration", KeyBindings.mvtSet.ToString());

		//fullscreen
		PlayerPrefs.SetString("fullscreen", Screen.fullScreen.ToString());

		//volume
		float masterVol;
		gameMix.GetFloat("MasterVolume", out masterVol);
		PlayerPrefs.SetFloat("mastervolume", masterVol);
		float musicVol;
		gameMix.GetFloat("MusicVolume", out musicVol);
		PlayerPrefs.SetFloat("musicvolume", musicVol);
		float effVol;
		gameMix.GetFloat("EffectsVolume", out effVol);
		PlayerPrefs.SetFloat("effectsvolume", effVol);

		PlayerPrefs.Save();
	}

	//load the save game names from the saved games folder
	public string[] getSaveGames()
	{
		return new string[] {"A", "B", "C", "D", "E", "F", "G", "H"};
	}

	//load a game from the saved games folder
	public void loadGame(string name)
	{

	}

	//save a game to the saved games folder
	public void saveGame(string name)
	{

	}
}

public static class KeyBindings{
	//movement set 1
	//mvtSet = true
	public static KeyCode forward;
	public static KeyCode backward;
	public static KeyCode turnRight;
	public static KeyCode turnLeft;

	//movement set 2
	//mvtSet = false;
	public static KeyCode placeMvtMkr;

	//abilities
	public static KeyCode ability1;
	public static KeyCode ability2;
	public static KeyCode ability3;
	public static KeyCode ability4;
	public static KeyCode ability5;
	public static KeyCode ability6;

	//misc
	public static KeyCode interact;
	public static bool mvtSet;
}
