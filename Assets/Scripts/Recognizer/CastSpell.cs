using UnityEngine;
using System.Collections;

public class CastSpell {

	public Rect textwin =  new Rect (Screen.height - 10,Screen.width - 10,10,10);
	private bool castwindow = false;
	public CastSpell () {
	
	}
	
	public void DrawGUI () {
		if (GUI.Button (new Rect(Screen.width - Screen.width/8, Screen.height-Screen.height/8,Screen.width/8,Screen.height/8),"Cast Spell")){
			Debug.Log ("spell cast");
			castwindow = true;
		}
		if (castwindow) {
			GUI.color = Color.clear;
			textwin = GUI.Window(0,textwin,DoMyWindow,"Spell Window");
		}
		
	}
	
	public void DoMyWindow(int windowID) {
		if (GUI.Button (new Rect (10,10,10,10),"Hello")) {
			
		}
	}
	
}
