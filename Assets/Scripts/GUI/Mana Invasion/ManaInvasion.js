var InvIcon : Texture;
var ManaOrb : Texture;
var ManaCount;
var InvCount;

function OnGUI(){
	iconSize = 30;
	width = 90;
	height = 50;
	iconSize = 30;
	x = Screen.width-width*2-25;
	y = Screen.height - 230;
	ManaCount = Mathf.Floor(Time.time);
	InvCount = Mathf.Floor(Time.time/10);
	GUI.DrawTexture (Rect (x, y, iconSize, iconSize), ManaOrb);
	GUI.Label (Rect (x + iconSize, y + iconSize / 6, width, height), ManaCount + " Mana");
	GUI.DrawTexture (Rect (x + width, y, iconSize, iconSize), InvIcon);
	GUI.Label (Rect (x + width + iconSize, y + iconSize / 6, width - iconSize, height), InvCount + " Invasion");
}