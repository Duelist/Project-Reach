var invIcon : Texture;
var manaOrb : Texture;
var textStyle : GUIStyle;
var maxMana = 100;
var manaCount = 0;


function OnGUI(){
	var iconSize = 30;
	var width = 100;
	var height = 50;
	var x = Screen.width-width*2-10;
	var y = Screen.height - 230;
	var manaCount = Mathf.Floor(Time.time);
	var invCount = Mathf.Floor(Time.time/10);
	
	
	
	GUI.DrawTexture (Rect (x, y, iconSize, iconSize), manaOrb);
	GUI.Label (Rect (x + iconSize, y + iconSize / 6, width, height), manaCount + " Mana", textStyle);
	GUI.DrawTexture (Rect (x + width, y, iconSize, iconSize), invIcon);
	GUI.Label (Rect (x + width + iconSize, y + iconSize / 6, width - iconSize, height), invCount + " Invasion", textStyle);
}