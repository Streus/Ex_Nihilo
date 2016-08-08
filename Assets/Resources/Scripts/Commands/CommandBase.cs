using UnityEngine;
using System.Collections;

public abstract class CommandBase : MonoBehaviour {

	//have a ton of game-related variables here that the commands can
	//access. Stuff like access to the main menus, internal game variables,
	//just a reference to anything that might be useful. Possibly wrap them in
	//objects as "folders".

	/**
	 * Add in support for having multiple entries in the help menu- things like
	 * "console [size]" and "console [color]" should be separate and marked as such
	 * 
	 * Similarly, have an easy way to specify arguments that this needs. [brackets] are
	 * for optional parameters, [vertical | bars] signify one-of-X options, etc
	 */

	//a few methods that you must override here

	//The invocation name- how you call the command
	public abstract string getInvocation();

	//A short (<80 character) message that appears in the help menu
	public abstract string getHelpMessage();

	//Initialization
	public abstract void Start ();
	
	//Update is called once per frame
	public abstract void Update ();

	//This method is called when your command is to be executed
	//@param args: an arbitrary list of arguments
	public abstract void Execute (string[] args);
}
