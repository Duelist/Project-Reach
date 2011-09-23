using UnityEngine;
using System.Collections;

public class NewMenu {
	int buttonWidth;
	int buttonHeight;
	
	public NewMenu (int width,int height) {
		buttonWidth = width;
		buttonHeight = height;
	}
	
	public void DrawGUI(MainMenu mainmen) {
		if ( GUI.Button(new Rect(Screen.width - buttonWidth ,
			Screen.height - buttonHeight,buttonWidth,buttonHeight),"Back") ) 
		{
			
			// Destroy current stuff and go back one level
		}
	}
	
}
