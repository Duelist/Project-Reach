using UnityEngine;
using System.Collections;

public class MenuManager {

	string currmenu;
	bool paused;
	
	//SettingMenu settingmen;
	//LoadMenu loadmen;
	MainMenu mainmen;
	//AboutMenu aboutmen;
	//NewMenu newmenu;
		
	public MenuManager () {
			mainmen = new MainMenu ();

		}	
	
	public void SetPaused(bool ispaused) {
		paused = ispaused;
	}
	
	// Need to get current state/button press from every type of menu so we can switch between the menus.
	// Need to know if the get mehods will be persistently checked like in Update or OnGui.
	
	public void DrawMain() {
//		mainmen = new MainMenu ();
//		mainmen.DrawMainGUI(settingmen,loadmen,aboutmen,newmenu);
		mainmen.Types();

	}
	
/* 	public void DrawNew() {
		newmenu = new NewMenu (buttonWidth,buttonHeight);	

	}
	
	public void DrawLoad() {
		loadmen = new LoadMenu (buttonWidth,buttonHeight); 

	}
	
	public void DrawSetting() {
		settingmen = new SettingMenu (buttonWidth,buttonHeight); 

	}
	
	public void DrawAbout() {
		aboutmen = new AboutMenu (buttonWidth,buttonHeight);

	} */
	
}
