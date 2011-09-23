using UnityEngine;
using System.Collections;

public class MainMenu {
	
	int buttonWidth = 200;
	int buttonHeight = 50;
	
	int selGrindInt = 0;
	string[] levelstrings = {"Level1","Level2","Level3","Level4","Level5","Level6","Level7","Level8","Level9"};

	int toolbarInt = 0;
	string[] toolbarStrings = {"Audio","Graphics","Stats","System"};
	
	string[] aboutinfo = {"Project Reach", "Version Alpha 0.1",
									"A Final Stand Sudios Production",
									"Copyright (c) 2011-2012 Final Stand Studios"};
											
	public MainMenu () {
		
	}
	
	//public void DrawMainGUI(SettingMenu settingmen,LoadMenu loadmen,AboutMenu aboutmen,NewMenu newmen) {
	public void DrawMainGUI	() {
		if ( GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
			100,buttonWidth,buttonHeight),"New Game") ) 
		{
			DrawNewGUI();
			//newmen.DrawGUI(this);
			// New game or resume game
			//Application.LoadLevel("Win")	;
		}
		if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
			150,buttonWidth,buttonHeight),"Load Level")) {
			// Load level menu
			//loadmen.DrawGUI(this);
			DrawLoadGUI();
		}
		if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
			200,buttonWidth,buttonHeight),"Settings")) {
			// Settings menu
			//settingmen.DrawGUI();
				DrawSettingGUI();
		}
		if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
			250,buttonWidth,buttonHeight),"About")) {
			// Aboutmenu
			//aboutmen.DrawGUI();
				DrawAboutGUI();
		}
		
		if ( GUI.Button(new Rect(Screen.width - buttonWidth ,
			Screen.height - buttonHeight,buttonWidth,buttonHeight),"Exit") ) 
		{
			// Exit game
			Application.Quit();
		}
	}
	
	public void DrawLoadGUI() {
		beginPage(300,300);
		
		//for (int i = 0;i<10;i++) {
		//	stuffs[i] = stuff;
		//}
//		GUI.skin = test2;
		selGrindInt = GUILayout.SelectionGrid(selGrindInt,levelstrings,3);
		endPage();
	//	GUI.skin = null;
		if ( GUI.Button(new Rect(Screen.width - buttonWidth ,
			Screen.height - buttonHeight,buttonWidth,buttonHeight),"Back") ) 
		{
			//GUI.skin = null;
			//Back to Main Menu
			DrawMainGUI();
		}
		
	}
	
	public void DrawNewGUI() {
		if ( GUI.Button(new Rect(Screen.width - buttonWidth ,
			Screen.height - buttonHeight,buttonWidth,buttonHeight),"Back") ) 
		{
			DrawMainGUI();
			// Destroy current stuff and go back one level
		}
	}
	
	
	public void DrawSettingGUI () {
		beginPage(300,300);
		toolbarInt = GUILayout.Toolbar(toolbarInt,toolbarStrings);
		toolbarOptions(toolbarInt);
		endPage();
		if ( GUI.Button(new Rect(Screen.width - buttonWidth ,
			Screen.height - buttonHeight,buttonWidth,buttonHeight),"Back") ) 
		{
						DrawMainGUI();

//  Back button
		}
		
	}

	public void DrawAboutGUI () {
	
		beginPage(300,300);
		foreach (string credit in aboutinfo) {
			GUILayout.Label(credit);
		}
		endPage();
		if ( GUI.Button(new Rect(Screen.width - buttonWidth ,
			Screen.height - buttonHeight,buttonWidth,buttonHeight),"Back") ) 
		{
			// Go back to Main Menu
		}
	}
	
	void toolbarOptions(int option) {
		switch (option) {
			case 0: //Volume
				GUILayout.Label("Volume");
				AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume,0,1);
			break;
			case 1: //Graphics
			
			break;
			case 2: //Stats
			
			break;
			case 3: //System
				GUILayout.Label ("Unity player version "+Application.unityVersion);
				GUILayout.Label("Graphics: "+SystemInfo.graphicsDeviceName+" "+
				SystemInfo.graphicsMemorySize+"MB\n"+
				SystemInfo.graphicsDeviceVersion+"\n"+
				SystemInfo.graphicsDeviceVendor);
				GUILayout.Label("Shadows: "+SystemInfo.supportsShadows);
				GUILayout.Label("Image Effects: "+SystemInfo.supportsImageEffects);
				GUILayout.Label("Render Textures: "+SystemInfo.supportsRenderTextures);
			break;			
		}
	}
	
	
	void beginPage(int width, int height) {
		GUILayout.BeginArea(new Rect ((Screen.width-width)/2,(Screen.height - height)/2,width,height));
	}
	
	void endPage () {
		GUILayout.EndArea();
	}
	
	
}
