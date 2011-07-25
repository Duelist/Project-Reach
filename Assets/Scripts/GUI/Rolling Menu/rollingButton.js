// Create a public variable where we can assign the GUISkin
var gearSkin : Texture;
var templateSkin : Texture;
var fireSkin : GUISkin;
var fireDecal : Texture;
var iceSkin : GUISkin;
var iceDecal : Texture;
var earthSkin : GUISkin;
var earthDecal : Texture;
var windSkin : GUISkin;
var windDecal : Texture;
var infoSkinFire : Texture;
var infoSkinIce : Texture;
var infoSkinEarth : Texture;
var infoSkinWind : Texture;
var infoTextStyle : GUIStyle;

var infoSkin = infoSkinWind;
var mouseInfo = false;
var towerInfo = "none";
var animateX = 0;
var width = 100;
var height = 90;
var winfo = height * 2;
var hinfo = height * 2;
var animateSpeed;

// Apply the Skin in our OnGUI() function
function OnGUI () {
	var width = this.width;
	var height = this.height;
	var spacing = 10;
	var x = Screen.width-width*2-spacing*2;
	var y = Screen.height-height*2-spacing*2;
	
	// Note that animation time depends on the width of the info box
	var winfo = this.winfo;
	var hinfo = this.hinfo;
	var animateSpeed = 2;
	
	var menuWidth = width*2 + spacing*3;
	var menuHeight = height*2 + spacing*3;
	
	if (animateX > 0){
		if (animateX - animateSpeed < 0){
			animateX = 0;
		}
		else {
			animateX -= animateSpeed;
		}
	}
	
	// Show Rolling Info Window
	if (this.mouseInfo){
		GUI.DrawTexture(Rect(x-(winfo-animateX),y,winfo+spacing,hinfo+spacing),infoSkin);
		// Text Label
		infoMsg = "Build "+towerInfo+" Construct! \n Cost: 5 Mana";
		GUI.Label(Rect(x-(winfo-animateX)+spacing,y+spacing*2,winfo-spacing*2,hinfo/2),infoMsg,infoTextStyle);
		// Create Tower Button
		if (GUI.Button (Rect(x-(winfo-animateX)+spacing,y+spacing*2+hinfo/2,winfo-spacing*2,hinfo/2-spacing*3),"Create!")){
			// Create Tower of type x here
		}
	}
	
	// Frame Texture
	GUI.DrawTexture(Rect(x-spacing, y-spacing, menuWidth, menuHeight),templateSkin);
		
	// Button Creation
	GUI.skin = fireSkin;
	if (GUI.Button (Rect(x,y,width,height),"")){
		setHelper("Fire");
	}
	GUI.DrawTexture(Rect(x,y,width,height),fireDecal);
	
	GUI.skin = iceSkin;
	if (GUI.Button (Rect(x + width + spacing,y,width,height),"")){
		setHelper("Ice");
	}
	GUI.DrawTexture(Rect(x + width + spacing,y,width,height),iceDecal);
	
	GUI.skin = earthSkin;
	if (GUI.Button (Rect(x,y + height + spacing,width,height),"")){
		setHelper("Earth");
	}
	GUI.DrawTexture(Rect(x,y + height + spacing,width,height),earthDecal);
	
	GUI.skin = windSkin;
	if (GUI.Button (Rect(x + width + spacing,y + height + spacing,width,height),"")){
		setHelper("Wind");
	}
	GUI.DrawTexture(Rect(x + width + spacing,y + height + spacing,width,height),windDecal);

	
	// Gear Texture
	gear1X = x - spacing-width/4;
	gear1Y = y - spacing-width/4;
	gear1Size = width/2;
	gear2X = x - spacing-width/4;
	gear2Y = y + menuHeight - width/4 - spacing;
	gear2Size = width/2;
	
	pivotPoint = Vector2(x-spacing, y-spacing);
    GUIUtility.RotateAroundPivot (-animateX*2, pivotPoint); 
	GUI.DrawTexture(Rect(gear1X, gear1Y, gear1Size, gear1Size),gearSkin);
	GUIUtility.RotateAroundPivot (animateX*2, pivotPoint); 
	
	pivotPoint = Vector2(x - spacing, y + menuHeight - spacing);
	GUIUtility.RotateAroundPivot (animateX*2, pivotPoint); 
	GUI.DrawTexture(Rect(gear2X, gear2Y,gear2Size,gear2Size),gearSkin);
	GUIUtility.RotateAroundPivot (-animateX*2, pivotPoint);
}

// Helper function to set the info window in motion
function setHelper (towerType){
	mouseInfo = true;
	towerInfo = towerType;
	animateX = winfo;
	if (towerType == "Fire"){
		infoSkin = infoSkinFire;
		infoTextStyle.normal.textColor = Color.black;
	}
	if (towerType == "Ice"){
		infoSkin = infoSkinIce;
		infoTextStyle.normal.textColor = Color.white;
	}
	if (towerType == "Earth"){
		infoSkin = infoSkinEarth;
		infoTextStyle.normal.textColor = Color.white;
	}
	if (towerType == "Wind"){
		infoSkin = infoSkinWind;
		infoTextStyle.normal.textColor = Color.black;
	}
}