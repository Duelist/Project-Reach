using UnityEngine;
using System.Collections;

public class MainMenu: MonoBehaviour {
	
	public bool visible;
	int buttonWidth = 200;
	int buttonHeight = 50;
	Rect testwin = new Rect (20,20,120,50);
	string currentmen = "none";
	bool mainmenu = true;
	bool settingsmenu = false;
	bool aboutmenu = false;
	bool loadmenu = false;
	bool newmenu = false;
	
	int selGridInt = -1;
	string[] levelstrings = {"Level1","Level2","Level3","Level4","Level5","Level6","Level7","Level8","Level9"};

	int toolbarInt = 0;
	string[] toolbarStrings = {"Audio","Graphics","Stats","System"};
	
	string[] aboutinfo = {"Project Reach", "Version Alpha 0.1",
									"A Final Stand Sudios Production",
									"Copyright (c) 2011-2012 Final Stand Studios"};
	bool ifpaused;										
									
	public MainMenu () {
		//ifpaused = paused;
	}
	
	public string getCurrentMenu() {
		return currentmen;
	}
		
	//public void Types(string men) {
	//public void Types() {
	public void OnGUI() {
		/*mainmenu = false;
		settingsmenu = false;
		aboutmenu = false;
		loadmenu = false;
		newmenu = false;
		
		if (men == "main") {
			mainmenu = true;
			currentmen = "main"; 
			}
		if (men == "setting") {
			settingsmenu = true;
			currentmen = "setting"; 
			}
		if (men == "about") {
			aboutmenu = true;
			currentmen = "about"; 
			}
		if (men == "load") {
			loadmenu = true;
			currentmen = "load"; 
			}
			
		if (men == "new") {
			newmenu = true;
			currentmen = "new"; 
			}*/
	
		if (Input.GetKeyDown("a")) {
			testwin = GUI.Window(0,new Rect(110,10,200,60),Win0,"BLAH");
		}
		if (mainmenu) DrawMainGUI();
		 if (settingsmenu) DrawSettingGUI();
		 if (aboutmenu) DrawAboutGUI();
		 if (loadmenu) DrawLoadGUI();
		 if (newmenu) DrawNewGUI();
	}
	
	public void Win0(int winindex) {
		if (GUI.Button(new Rect(10,20,100,20),"asd")) {
			Debug.Log("button pressed");
		}
	}
	
	
	//public void DrawMainGUI(SettingMenu settingmen,LoadMenu loadmen,AboutMenu aboutmen,NewMenu newmen) {
	public void DrawMainGUI	() {
		if ( GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
			100,buttonWidth,buttonHeight),"New Game") ) 
		{
			newmenu = true;
			mainmenu = false;
			currentmen = "new";
			//newmen.DrawGUI(this);
			// New game or resume game
			//Application.LoadLevel("Win")	;
		}
		if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
			150,buttonWidth,buttonHeight),"Load Level")) {
			// Load level menu
				loadmenu = true;
				mainmenu = false;
				currentmen = "load";
			//loadmen.DrawGUI(this);
			//DrawLoadGUI();
		}
		if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
			200,buttonWidth,buttonHeight),"Settings")) {
			// Settings menu
			//settingmen.DrawGUI();
			settingsmenu = true;
			mainmenu = false;
			currentmen = "setting";

				//DrawSettingGUI();
		}
		if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
			250,buttonWidth,buttonHeight),"About")) {
			// Aboutmenu
			aboutmenu = true;
			mainmenu = false;
			currentmen = "about";

			//aboutmen.DrawGUI();
			//DrawAboutGUI();
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
		selGridInt = GUILayout.SelectionGrid(selGridInt,levelstrings,3);
		endPage();
	//	GUI.skin = null;
		switch(selGridInt) {
			case 0:
				Application.LoadLevel(1);
				break;
			case 1:
				Application.LoadLevel(2);
				break;
			case 2:
				break;
			case 3:
				break;
			default:
				break;
		}
		
		if ( GUI.Button(new Rect(Screen.width - buttonWidth ,
			Screen.height - buttonHeight,buttonWidth,buttonHeight),"Back") ) 
		{
			//GUI.skin = null;
			//Back to Main Menu
			mainmenu = true;
			loadmenu = false;
			currentmen = "main";

			//DrawMainGUI();
		}
		
	}
	
	public void DrawNewGUI() {
		if ( GUI.Button(new Rect(Screen.width - buttonWidth ,
			Screen.height - buttonHeight,buttonWidth,buttonHeight),"Back") ) 
		{
			mainmenu = true;
			newmenu = false;
			currentmen = "main";

			//DrawMainGUI();
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
			mainmenu = true;
			settingsmenu = false;
			currentmen = "main";

			//DrawMainGUI();

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
			mainmenu = true;
			aboutmenu = false;
			currentmen = "main";
			
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