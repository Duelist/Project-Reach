using UnityEngine;
using System.Collections;

public class InfoWindow {
	int x, y, w, h;
	Texture objTex;
	Texture windowTex;
	string desc;
	Font textFont;
	GUIStyle textStyle;
	
	
	public InfoWindow (int x, int y, int w, int h){
		this.x = x;
		this.y = y;
		this.w = w;
		this.h = h;
		
		// Empty texture
		objTex = TextureFactory.GetTileSelector();
		windowTex = TextureFactory.GetWindowTexture();
		desc = "Nothing Selected";
		textStyle = new GUIStyle ();
	}
	
	public void DrawGUI () {
		GUI.DrawTexture(new Rect(x, y, w/2, w/2), objTex);
		GUI.DrawTexture(new Rect(x+w/2 + 2, y, w/2, w/2), windowTex);
		GUI.Label(new Rect(x+w/2 + 2, y, w/2, w/2), desc, textStyle);
		/*if (GUI.Button (new Rect(x, y, w, h),"Ready!")){
			GameManager.SetGameState(1);
			GameManager.HideSelectors();
		}*/
	}
	
	public void SetTex (Texture targTex){
		objTex = targTex;
	}
	
	public void SetDesc (string targDesc){
		desc = targDesc;
	}
}

