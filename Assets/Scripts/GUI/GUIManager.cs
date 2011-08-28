using UnityEngine;
using System.Collections;

public class GUIManager {
	
	
	ManaInvasion manaInvasion;
	// Use this for initialization
	public GUIManager () {
		/*maxMana = 100;
		manaCount = 0;
		invCount = 0;
		
		iconSize = 30;
		width = 100;
		height = 50;
		x = Screen.width - width * 2 - 10;
		y = Screen.height - 233;*/
		int width = 100;
		manaInvasion = new ManaInvasion (100,0,0,30,width,50,Screen.width - width * 2 - 10,Screen.height - 233);
	}
	
	public void DrawGUI () {
		manaInvasion.DrawGUI();
	}
}
