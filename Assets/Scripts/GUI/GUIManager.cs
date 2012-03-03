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
		/*maxMana = 100;
		manaCount = 0;
		invCount = 0;
		
		iconSize = 30;
		width = 100;
		height = 50;
		x = Screen.width - width * 2 - 10;
		y = Screen.height - 233;*/
		int width = 100;
		manaInvasion = new ManaInvasion (0,30,width,50,Screen.width - width * 2 - 10,Screen.height - 233);
		
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
		readyButton = new ReadyButton ();
	}
	
	public void DrawPlayGUI (Player player) {
		manaInvasion.DrawGUI(player);
		castSpell.DrawGUI();
	}
	
	public void DrawBuildGUI (){
		readyButton.DrawGUI();
	}
}
