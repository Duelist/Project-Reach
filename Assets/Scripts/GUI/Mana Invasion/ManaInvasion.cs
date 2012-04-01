using UnityEngine;
using System.Collections;

public class ManaInvasion {

	public Texture invIcon;
	public Texture manaOrb;
	public Texture hpOrb;
	public GUIStyle textStyle;
	private int invCount;
	private int iconSize;
	private int width;
	private int height;
	private int x;
	private int y;
	
	// Use this for initialization
	public ManaInvasion (int x, int y, int w, int h) {
		invCount = GameStorage.level;
		this.x = x;
		this.y = y;
		width = w;
		height = h;
		iconSize = height/2;
		
		invIcon = Resources.Load ("GUI/Mana-Invasion Icons/Inv-Icon") as Texture;
		if (invIcon == null){
			Debug.Log("Load invIcon failed");
		}
		manaOrb = Resources.Load ("GUI/Mana-Invasion Icons/Mana-Orb") as Texture;
		if (manaOrb == null){
			Debug.Log("Load manaOrb failed");
		}
		hpOrb = Resources.Load ("GUI/Mana-Invasion Icons/HP-Orb") as Texture;
		if (hpOrb == null){
			Debug.Log("Load manaOrb failed");
		}
		textStyle = new GUIStyle();
	}
	
	public void DrawGUI(Player player){
		invCount = GameStorage.level;
		
		
		if (hpOrb != null){
			GUI.DrawTexture (new Rect (x, y, iconSize, iconSize), hpOrb);
		}
		GUI.Label (new Rect (x + iconSize, y + iconSize/3, width/2, height/2), player.GetHealth() + " Health", textStyle);

		if (manaOrb != null){
			GUI.DrawTexture (new Rect (x + width/2, y, iconSize, iconSize), manaOrb);
		}
		GUI.Label (new Rect (x + iconSize + width/2, y  + iconSize/3, width/2, height/2), player.GetMana() + " Mana", textStyle);
		
		if (invIcon != null){
			GUI.DrawTexture (new Rect (x + width/2, y + iconSize, iconSize, iconSize), invIcon);
		}
		GUI.Label (new Rect (x + width/2 + iconSize, y + iconSize + iconSize/3, width, height/2), "Invasion " + invCount, textStyle);
	}
}
