using UnityEngine;
using System.Collections;

public class ReadyButton {
	public int sizeDivisor = 6;
	public void DrawGUI () {
		if (GUI.Button (new Rect(Screen.width - Screen.width/sizeDivisor, Screen.height-Screen.height/sizeDivisor,Screen.width/sizeDivisor,Screen.height/sizeDivisor),"Ready!")){
			GameManager.SetGameState(1);
		}
	}
}
