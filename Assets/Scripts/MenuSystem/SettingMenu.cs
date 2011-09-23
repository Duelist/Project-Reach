using System;
using UnityEngine;
using UnityEditor;
using System.Collections;

public class SettingMenu {
	
	int toolbarInt = 0;
	string[] toolbarStrings = {"Audio","Graphics","Stats","System"};
	
	int buttonWidth;
	int buttonHeight;
	
	public SettingMenu (int width,int height) {
		buttonWidth = width;
		buttonHeight = height;
	}
	
	public void DrawGUI () {
		beginPage(300,300);
		toolbarInt = GUILayout.Toolbar(toolbarInt,toolbarStrings);
		toolbarOptions(toolbarInt);
		endPage();
		if ( GUI.Button(new Rect(Screen.width - buttonWidth ,
			Screen.height - buttonHeight,buttonWidth,buttonHeight),"Back") ) 
		{
//  Back button
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
