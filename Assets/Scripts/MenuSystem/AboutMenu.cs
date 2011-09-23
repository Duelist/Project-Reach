using UnityEngine;
using System.Collections;

public class AboutMenu {

	int buttonWidth;
	int buttonHeight;
	string[] aboutinfo = {"Project Reach", "Version Alpha 0.1",
											"A Final Stand Sudios Production",
											"Copyright (c) 2011-2012 Final Stand Studios"};
											
	public AboutMenu (int width,int height) {
		buttonWidth = width;
		buttonHeight = height;
	}
	
	public void DrawGUI () {
	
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
	
	void beginPage(int width, int height) {
		GUILayout.BeginArea(new Rect ((Screen.width-width)/2,(Screen.height - height)/2,width,height));
	}
	
	void endPage () {
		GUILayout.EndArea();
	}
	
}
