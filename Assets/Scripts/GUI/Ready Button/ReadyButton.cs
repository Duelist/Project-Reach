using UnityEngine;
using System.Collections;

public class ReadyButton {
	int x, y, w, h;
	
	public ReadyButton (int x, int y, int w, int h){
		this.x = x;
		this.y = y;
		this.w = w;
		this.h = h;
	}
	
	public void DrawGUI () {
		if (GUI.Button (new Rect(x, y, w, h),"Ready!")){
			GameStorage.gameState = GameStorage.GameState.Playing;
			GameManager.HideSelectors();
		}
	}
}
