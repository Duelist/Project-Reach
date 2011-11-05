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
	public ManaInvasion (int iCount, int iSize, int w, int h, int xx, int yy) {
		invCount = iCount;
		iconSize = iSize;
		width = w;
		height = h;
		x = xx;
		y = yy;
		
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
		invCount = (int) Mathf.Floor(Time.time/10);
		
		if (hpOrb != null){
			GUI.DrawTexture (new Rect (x, y - iconSize, iconSize, iconSize), hpOrb);
		}
		if (manaOrb != null){
			GUI.DrawTexture (new Rect (x, y, iconSize, iconSize), manaOrb);
		}
		
		GUI.Label (new Rect (x + iconSize, y + iconSize / 6 - iconSize, width, height), player.GetHealth() + " Health", textStyle);
		GUI.Label (new Rect (x + iconSize, y + iconSize / 6, width, height), player.GetMana() + " Mana", textStyle);
		if (invIcon != null){
			GUI.DrawTexture (new Rect (x + width, y, iconSize, iconSize), invIcon);
		}
		GUI.Label (new Rect (x + width + iconSize, y + iconSize / 6, width - iconSize, height), invCount + " Invasion", textStyle);
	}
}
