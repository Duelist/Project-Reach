using UnityEngine;
using System.Collections;

public class RadialMenu {
	public RadialMenu (){
	}

	public static void ShowRadialMenu (Vector3 pos, Vector2 radSize){
		//Debug.Log ("x:" + pos.x + "y: " + pos.y + "z: " + pos.z);
		GUI.DrawTexture (new Rect (pos.x, pos.y, radSize.x+2, radSize.y+2), TextureFactory.GetTileSelector());
		GUI.DrawTexture (new Rect (pos.x, pos.y - (radSize.y+2), radSize.x+2, radSize.y+2), TextureFactory.GetFireSelectorButton());
		GUI.DrawTexture (new Rect (pos.x, pos.y + (radSize.y+2), radSize.x+2, radSize.y+2), TextureFactory.GetIceSelectorButton());
	}
}
