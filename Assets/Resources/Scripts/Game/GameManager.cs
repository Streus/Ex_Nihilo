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
		KeyBindings.select = (KeyCode)PlayerPrefs.GetInt("keyselect", (int)KeyCode.Mouse0);
		KeyBindings.placeMvtMkr = (KeyCode)PlayerPrefs.GetInt("keyplacemvtmkr", (int)KeyCode.Mouse1);
		KeyBindings.holdGround = (KeyCode)PlayerPrefs.GetInt("keyholdground", (int)KeyCode.LeftShift);
		KeyBindings.ability1 = (KeyCode)PlayerPrefs.GetInt("keyability1", (int)KeyCode.Alpha1);
		KeyBindings.ability2 = (KeyCode)PlayerPrefs.GetInt("keyability2", (int)KeyCode.Alpha2);
		KeyBindings.ability3 = (KeyCode)PlayerPrefs.GetInt("keyability3", (int)KeyCode.Alpha3);
		KeyBindings.ability4 = (KeyCode)PlayerPrefs.GetInt("keyability4", (int)KeyCode.Alpha4);
		KeyBindings.ability5 = (KeyCode)PlayerPrefs.GetInt("keyability5", (int)KeyCode.Alpha5);
		KeyBindings.ability6 = (KeyCode)PlayerPrefs.GetInt("keyability6", (int)KeyCode.Alpha6);
		KeyBindings.ability7 = (KeyCode)PlayerPrefs.GetInt("keyability7", (int)KeyCode.Alpha7);
		KeyBindings.ability8 = (KeyCode)PlayerPrefs.GetInt("keyability8", (int)KeyCode.Alpha8);
		KeyBindings.ability9 = (KeyCode)PlayerPrefs.GetInt("keyability9", (int)KeyCode.Alpha9);
		KeyBindings.ability10 = (KeyCode)PlayerPrefs.GetInt("keyability10", (int)KeyCode.Alpha0);
		KeyBindings.pause = (KeyCode)PlayerPrefs.GetInt("keypause", (int)KeyCode.Tab);

		//control options
		string lkdCmra = PlayerPrefs.GetString("lockedcamera", "true");
		GameOptions.lockedCamera = bool.Parse(lkdCmra);
		GameOptions.panBorderSize = PlayerPrefs.GetInt("panbordersize", 11);
		GameOptions.panSpeed = PlayerPrefs.GetFloat("panspeed", 5f);

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
		PlayerPrefs.SetInt("keyselect", (int)KeyBindings.select);
		PlayerPrefs.SetInt("keyplacemvtmkr", (int)KeyBindings.placeMvtMkr);
		PlayerPrefs.SetInt("keyholdground", (int)KeyBindings.holdGround);
		PlayerPrefs.SetInt("keyability1", (int)KeyBindings.ability1);
		PlayerPrefs.SetInt("keyability2", (int)KeyBindings.ability2);
		PlayerPrefs.SetInt("keyability3", (int)KeyBindings.ability3);
		PlayerPrefs.SetInt("keyability4", (int)KeyBindings.ability4);
		PlayerPrefs.SetInt("keyability5", (int)KeyBindings.ability5);
		PlayerPrefs.SetInt("keyability6", (int)KeyBindings.ability6);
		PlayerPrefs.SetInt("keyability7", (int)KeyBindings.ability7);
		PlayerPrefs.SetInt("keyability8", (int)KeyBindings.ability8);
		PlayerPrefs.SetInt("keyability9", (int)KeyBindings.ability9);
		PlayerPrefs.SetInt("keyability10", (int)KeyBindings.ability10);
		PlayerPrefs.SetInt("keypause", (int)KeyBindings.pause);

		//control options
		PlayerPrefs.SetString("lockedcamera", GameOptions.lockedCamera.ToString());
		PlayerPrefs.SetInt("panbordersize", GameOptions.panBorderSize);
		PlayerPrefs.SetFloat("panspeed", GameOptions.panSpeed);

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

	//movement and interaction
	public static KeyCode select;
	public static KeyCode placeMvtMkr;
	public static KeyCode holdGround;

	//abilities
	public static KeyCode ability1;
	public static KeyCode ability2;
	public static KeyCode ability3;
	public static KeyCode ability4;
	public static KeyCode ability5;
	public static KeyCode ability6;
	public static KeyCode ability7;
	public static KeyCode ability8;
	public static KeyCode ability9;
	public static KeyCode ability10;

	//misc
	public static KeyCode pause;
}

public static class GameOptions{
	
	//control
	public static bool lockedCamera;
	public static int panBorderSize;
	public static float panSpeed;
}