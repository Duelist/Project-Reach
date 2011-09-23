using UnityEngine;
using System.Collections;

public class LoadMenu {

	int buttonWidth;
	int buttonHeight;
		int selGrindInt = 0;
		string[] levelstrings = {"Level1","Level2","Level3","Level4","Level5","Level6","Level7","Level8","Level9"};

		
	public LoadMenu (int width,int height) {
		buttonWidth = width;
		buttonHeight = height;
	}
	
	public void DrawGUI () {
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
		}
		
	}
	
	void beginPage(int width, int height) {
		GUILayout.BeginArea(new Rect ((Screen.width-width)/2,(Screen.height - height)/2,width,height));
	}
	
	void endPage () {
		GUILayout.EndArea();
	}
	
	
}
