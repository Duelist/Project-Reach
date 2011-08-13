using UnityEngine;
using System.Collections;

public class ManaInvasion : MonoBehaviour {

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
	void Start () {
		maxMana = 100;
		manaCount = 0;
		invCount = 0;
		
		iconSize = 30;
		width = 100;
		height = 50;
		x = Screen.width - width * 2 - 10;
		y = Screen.height - 233;
	}
	
	void OnGUI(){
		manaCount = (int) Mathf.Floor(Time.time);
		if (manaCount > maxMana){
			manaCount = maxMana;
		}
		
		invCount = (int) Mathf.Floor(Time.time/10);
		
		GUI.DrawTexture (new Rect (x, y, iconSize, iconSize), manaOrb);
		GUI.Label (new Rect (x + iconSize, y + iconSize / 6, width, height), manaCount + " Mana", textStyle);
		GUI.DrawTexture (new Rect (x + width, y, iconSize, iconSize), invIcon);
		GUI.Label (new Rect (x + width + iconSize, y + iconSize / 6, width - iconSize, height), invCount + " Invasion", textStyle);
	}
}
