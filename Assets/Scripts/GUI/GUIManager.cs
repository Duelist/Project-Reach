using UnityEngine;
using System.Collections;

public class GUIManager
{
	ManaInvasion manaInvasion;
	FutureButton futureButton;
	RollingButton rollingButton;
	SelectorOverlay selectorOverlay;
	CastSpell castSpell;
	ReadyButton readyButton;
	InfoWindow infoWindow;
	private int sizeDivisor;
	
	// Use this for initialization
	public GUIManager()
	{
		// Going to make the layout of the screen in 6s
		sizeDivisor = 6;
		
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
		
		selectorOverlay = new SelectorOverlay();
		castSpell = new CastSpell();	
		readyButton = new ReadyButton (Screen.width - Screen.width/sizeDivisor, Screen.height-Screen.height/sizeDivisor, Screen.width/sizeDivisor,Screen.height/sizeDivisor);
		infoWindow = new InfoWindow (Screen.width - Screen.width/sizeDivisor, Screen.height-Screen.height/sizeDivisor*3, Screen.width/sizeDivisor, Screen.height/sizeDivisor*2);
	}
	
	public void DrawPlayGUI () {
		manaInvasion.DrawGUI(GameStorage.player);
		castSpell.DrawGUI();
		player.GetHealth ();
	}
	
	public void DrawBuildGUI (){
		manaInvasion.DrawGUI(GameStorage.player);
		readyButton.DrawGUI();
		infoWindow.DrawGUI();
	}
	
	public void DrawStoppedGUI (Player player){
		if (player.IsDead()){
			DrawDefeatedMsg(player);
		}
	}
	
	public InfoWindow GetInfoWindow (){
		return infoWindow;
	}
	
	public void DrawDefeatedMsg (Player player){
		GUI.DrawTexture(new Rect (Screen.width/sizeDivisor*2f,Screen.height/sizeDivisor*2f, Screen.width/sizeDivisor*2f, Screen.height/sizeDivisor*2f), TextureFactory.GetPlayerLoseTexture());
		if (GUI.Button (new Rect (Screen.width/sizeDivisor*2.5f, Screen.height/sizeDivisor*4.5f, Screen.width/sizeDivisor, Screen.height/sizeDivisor),"Try again")){
			GameManager.SetGameState(0);
			player.SetHealth(100);
			player.GetPlayerObj().animation.Play("WakeUp");
		}
	}
}
