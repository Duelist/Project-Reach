using UnityEngine;
using System.Collections;

public class GUIManager {
	
	
	ManaInvasion manaInvasion;
	FutureButton futureButton;
	RollingButton rollingButton;
	SelectorOverlay selectorOverlay;
	CastSpell castSpell;
	ReadyButton readyButton;
	
	// Use this for initialization
	public GUIManager (Map map, Player player) {
		// Going to make the layout of the screen in 6s
		int sizeDivisor = 6;
		
		manaInvasion = new ManaInvasion (Screen.width - Screen.width/sizeDivisor, 0, Screen.width/sizeDivisor,Screen.height/sizeDivisor);
		
		/*
		timeZone = "Future";
		loadCount = 0;
		
		width = 300;
		height = 120;
		loadTime = 5;
		x = 0;
		y = Screen.height-height;*/
		int height = 120;
		futureButton = new FutureButton("Future", 0, 300,  height, 5, 0, Screen.height - height);
		
		/*
		width = 100;
		height = 90;
		spacing = 10;*/
		rollingButton = new RollingButton (100,90,10);
		
		selectorOverlay = new SelectorOverlay(map);
		castSpell = new CastSpell();	
		readyButton = new ReadyButton (Screen.width - Screen.width/sizeDivisor, Screen.height-Screen.height/sizeDivisor, Screen.width/sizeDivisor,Screen.height/sizeDivisor);
	}
	
	public void DrawPlayGUI (Player player) {
		manaInvasion.DrawGUI(player);
		castSpell.DrawGUI();
	}
	
	public void DrawBuildGUI (Player player){
		manaInvasion.DrawGUI(player);
		readyButton.DrawGUI();
	}
}
