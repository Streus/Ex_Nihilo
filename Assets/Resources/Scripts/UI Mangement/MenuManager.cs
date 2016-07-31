using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour 
{
	public Menu currentMenu;

	public void Start()
	{
		showMenu(currentMenu);
	}

	public void showMenu(Menu menu)
	{
		if(currentMenu != null)
		{
			currentMenu.IsOpen = false;
		}

		currentMenu = menu;
		currentMenu.IsOpen = true;
	}
}