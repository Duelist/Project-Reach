using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	#region Fields
	public GUISkin test2;
	public Texture stuff;
	public Texture[] stuffs;
	private bool pause = false;
	private bool mainmenu = true;
	private bool settingsmenu = false;
	private bool newgamemenu = false;
	private bool levelmenu = false;
	private bool aboutmenu = false;
	
	//private SepiaToneEffect pauseFilter;
	public Texture backgroundTexture;
	private int buttonWidth = 200;
	private int buttonHeight = 50;
	private int selGrindInt = 0;
	private string[] levelstrings = {"Level1","Level2","Level3","Level4","Level5","Level6","Level7","Level8","Level9"};
	private int toolbarInt = 0;
	private string[] toolbarStrings = {"Audio","Graphics","Stats","System"};
	private string[] aboutinfo = {"Project Reach", "Version Alpha 0.1",
											"A Final Stand Sudios Production",
											"Copyright (c) 2011-2012 Final Stand Studios"};

	#endregion
	
	#region Functions
	
	void OnGUI () {
		//pauseFilter = Camera.main.GetComponent<SepiaToneEffect>();
		if (mainmenu) {
			mainMenu();
		}
		if (newgamemenu) {
			newgameMenu();
		}
		if (levelmenu) {
			levelMenu();
		}
		if (settingsmenu) {
			settingsMenu();
		}		
		if (aboutmenu) {
			aboutMenu();
		}		
	}
	
	void mainMenu () {
		if (!pause) {
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),backgroundTexture);
		}
		if ( GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
			100,buttonWidth,buttonHeight),"New Game") ) 
		{
			//mainmenu = false;
			//newgamemenu = true;
			Application.LoadLevel("Win")	;
		}
		if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
			150,buttonWidth,buttonHeight),"Load Level")) {
			mainmenu = false;
			levelmenu = true;
		}
		if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
			200,buttonWidth,buttonHeight),"Settings")) {
			mainmenu = false;
			settingsmenu = true;
		}
		if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
			250,buttonWidth,buttonHeight),"About")) {
			mainmenu = false;
			aboutmenu = true;
		}
		
		if ( GUI.Button(new Rect(Screen.width - buttonWidth ,
			Screen.height - buttonHeight,buttonWidth,buttonHeight),"Exit") ) 
		{
				Application.Quit();
		}
	}
	
	void newgameMenu () {
		if ( GUI.Button(new Rect(Screen.width - buttonWidth ,
			Screen.height - buttonHeight,buttonWidth,buttonHeight),"Back") ) 
		{
			newgamemenu = false;
			mainmenu = true;
		}
	}
	
	void levelMenu () {
		beginPage(300,300);
		
		//for (int i = 0;i<10;i++) {
		//	stuffs[i] = stuff;
		//}
		GUI.skin = test2;
		selGrindInt = GUILayout.SelectionGrid(selGrindInt,levelstrings,3);
		endPage();
		GUI.skin = null;
		if ( GUI.Button(new Rect(Screen.width - buttonWidth ,
			Screen.height - buttonHeight,buttonWidth,buttonHeight),"Back") ) 
		{
			//GUI.skin = null;
			levelmenu = false;
			mainmenu = true;
		}
	}
	
	void settingsMenu () {
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),backgroundTexture);
		beginPage(300,300);
		toolbarInt = GUILayout.Toolbar(toolbarInt,toolbarStrings);
		toolbarOptions(toolbarInt);
		endPage();
		if ( GUI.Button(new Rect(Screen.width - buttonWidth ,
			Screen.height - buttonHeight,buttonWidth,buttonHeight),"Back") ) 
		{
			settingsmenu = false;
			mainmenu = true;
		}
	}
	
	void aboutMenu () {
		//GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),backgroundTexture);
		beginPage(300,300);
		foreach (string credit in aboutinfo) {
			GUILayout.Label(credit);
		}
		endPage();
		if ( GUI.Button(new Rect(Screen.width - buttonWidth ,
			Screen.height - buttonHeight,buttonWidth,buttonHeight),"Back") ) 
		{
			aboutmenu = false;
			mainmenu = true;
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

	void pauseMenu () {
		if (!pause) {
			if ( GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
				100,buttonWidth,buttonHeight),"Pause") ) 
			{
				pause = !pause;
				AudioListener.pause = false;
				Time.timeScale = 0;
				//pauseFilter.enabled = false;
			}
		}
		if (pause) {
			 
			if ( GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
				100,buttonWidth,buttonHeight),"Resume") ) 
			{
				pause=false;
				Time.timeScale = 1;
				AudioListener.pause = true;
			//	pauseFilter.enabled = true;
			}
		}

	}
	
	#endregion
}