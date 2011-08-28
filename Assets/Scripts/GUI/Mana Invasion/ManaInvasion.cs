using UnityEngine;
using System.Collections;

public class ManaInvasion {

	public Texture invIcon;
	public Texture manaOrb;
	public GUIStyle textStyle;
	private int maxMana;
	private int manaCount;
	private int invCount;
	private int iconSize;
	private int width;
	private int height;
	private int x;
	private int y;
	
	// Use this for initialization
	public ManaInvasion (int mMana, int mCount, int iCount, int iSize, int w, int h, int xx, int yy) {
		maxMana = mMana;
		manaCount = mCount;
		invCount = iCount;
		iconSize = iSize;
		width = w;
		height = h;
		x = xx;
		y = yy;
		
		invIcon = Resources.Load ("Inv-Icon") as Texture;
		if (invIcon == null){
			Debug.Log("Load invIcon failed");
		}
		manaOrb = Resources.Load ("Mana-Orb") as Texture;
		if (manaOrb == null){
			Debug.Log("Load manaOrb failed");
		}
		textStyle = new GUIStyle();
	}
	
	public void DrawGUI(){
		manaCount = (int) Mathf.Floor(Time.time);
		if (manaCount > maxMana){
			manaCount = maxMana;
		}
		
		invCount = (int) Mathf.Floor(Time.time/10);
		
		
		if (manaOrb != null){
			GUI.DrawTexture (new Rect (x, y, iconSize, iconSize), manaOrb);
		}
		
		GUI.Label (new Rect (x + iconSize, y + iconSize / 6, width, height), manaCount + " Mana", textStyle);
		if (invIcon != null){
			GUI.DrawTexture (new Rect (x + width, y, iconSize, iconSize), invIcon);
		}
		GUI.Label (new Rect (x + width + iconSize, y + iconSize / 6, width - iconSize, height), invCount + " Invasion", textStyle);
	}
}
