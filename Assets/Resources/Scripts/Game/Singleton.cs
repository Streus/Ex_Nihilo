using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	private static T instance;

	public static T Instance {
		get {
			//if no instance- load in the instance
			if (instance == null) {
				instance = (T) FindObjectOfType(typeof(T));
				if (instance == null) {
					Debug.LogError ("Could not find an instance of " + typeof(T) + " in the scene. Make sure" +
						" there is a GameObject of that type and try again.");
				}
			} 

			return instance;
		}
	}
}