using UnityEngine;
using System.Collections;

public class RollingButton{

	public GUISkin emptySkin;
	public Texture gearSkin;
	public Texture templateSkin;
	public GUISkin fireSkin;
	public Texture fireDecal;
	public GUISkin iceSkin;
	public Texture iceDecal;
	public GUISkin earthSkin;
	public Texture earthDecal;
	public GUISkin windSkin;
	public Texture windDecal;
	public Texture infoSkinFire;
	public Texture infoSkinIce;
	public Texture infoSkinEarth;
	public Texture infoSkinWind;
	public GUIStyle infoTextStyle;

	private Texture infoSkin;
	private bool mouseInfo;
	private string towerInfo;
	private string infoMsg;
	private int animateX;
	private int animateSpeed;
	private Vector2 pivotPoint;

	private int width;
	private int height;
	private int winfo;
	private int hinfo;
	private int x;
	private int y;
	private int oldx;
	private int oldy;
	private int spacing;
	private int menuWidth;
	private int menuHeight;
	
	public bool active;

	private int gear1X;
	private int gear1Y;
	private int gear1Size;
	private int gear2X;
	private int gear2Y;
	private int gear2Size;

	// Use this for initialization
	public RollingButton (int w, int h, int s) {
		
		width = w;
		height = h;
		spacing = s;
		
		// Note that animation time depends on the width of the info box
		winfo = height * 2;
		hinfo = height * 2;
		
		x = Screen.width-width*2-spacing*2;
		y = Screen.height-height*2-spacing*2;

		menuWidth = width*2 + spacing*3;
		menuHeight = height*2 + spacing*3;

		gear1X = x - spacing-width/4;
		gear1Y = y - spacing-width/4;
		gear1Size = width/2;
		gear2X = x - spacing-width/4;
		gear2Y = y + menuHeight - width/4 - spacing;
		gear2Size = width/2;
		
		emptySkin = Resources.Load("GUI/Rolling Menu Textures/EmptyButtonSkin") as GUISkin;
		gearSkin = Resources.Load("GUI/Rolling Menu Textures/Gear") as Texture;
		templateSkin = Resources.Load("GUI/Rolling Menu Textures/RollingMenuFrame") as Texture;
		fireSkin = Resources.Load("GUI/Rolling Menu Textures/FireButtonSkin") as GUISkin;
		fireDecal = Resources.Load("GUI/Decals/Fire-Decal") as Texture;
		iceSkin = Resources.Load("GUI/Rolling Menu Textures/IceButtonSkin") as GUISkin;
		iceDecal = Resources.Load("GUI/Decals/Ice-Decal") as Texture;
		earthSkin = Resources.Load("GUI/Rolling Menu Textures/EarthButtonSkin") as GUISkin;
		earthDecal = Resources.Load("GUI/Decals/Earth-Decal") as Texture;
		windSkin = Resources.Load("GUI/Rolling Menu Textures/WindButtonSkin") as GUISkin;
		windDecal = Resources.Load("GUI/Decals/Wind-Decal") as Texture;
		infoSkinFire = Resources.Load("GUI/Rolling Menu Textures/InfoWindow_Fire") as Texture;
		infoSkinIce = Resources.Load("GUI/Rolling Menu Textures/InfoWindow_Ice") as Texture;
		infoSkinEarth = Resources.Load("GUI/Rolling Menu Textures/InfoWindow_Earth") as Texture;
		infoSkinWind = Resources.Load("GUI/Rolling Menu Textures/InfoWindow_Air") as Texture;
		infoTextStyle = new GUIStyle ();
		
		infoSkin = infoSkinWind;
		mouseInfo = false;
		towerInfo = "none";
		infoMsg = "";
		animateX = 0;
		animateSpeed = 2;
	}

	public void setActive(int px,int py) {
		active = true;
		updatePos(px,py);
	}
	
	public void setDeActive() {
		active = false;
		oldx = x;
		oldy = y;
	}
	
	private void updatePos(int px, int py) {
		x = px;
		y = py;		
	}
	
	public void DrawGUI (){
		if (animateX > 0){
			if (animateX - animateSpeed < 0){
				animateX = 0;
			}
			else {
				animateX -= animateSpeed;
			}
		}

		if (mouseInfo){
			GUI.DrawTexture(new Rect(x-(winfo-animateX),y,winfo+spacing,hinfo+spacing),infoSkin);
			// Text Label
			infoMsg = "Build "+towerInfo+" Construct! \nCost: 5 Mana";
			GUI.Label(new Rect(x-(winfo-animateX)+spacing,y+spacing*2,winfo-spacing*2,hinfo/2),infoMsg,infoTextStyle);
			// Create Tower Button
			GUI.skin = emptySkin;
			if (GUI.Button (new Rect(x-(winfo-animateX)+spacing,y+spacing*2+hinfo/2,winfo-spacing*2,hinfo/2-spacing*3),"Create!")){
				// Create Tower of type x here
			}
		}

		// Frame Texture
		GUI.DrawTexture(new Rect(x-spacing, y-spacing, menuWidth, menuHeight),templateSkin);

		// Button Creation
		GUI.skin = fireSkin;
		if (GUI.Button (new Rect(x,y,width,height),"")){
			SetHelper("Fire");
		}
		GUI.DrawTexture(new Rect(x,y,width,height),fireDecal);

		GUI.skin = iceSkin;
		if (GUI.Button (new Rect(x + width + spacing,y,width,height),"")){
			SetHelper("Ice");
		}
		GUI.DrawTexture(new Rect(x + width + spacing,y,width,height),iceDecal);

		GUI.skin = earthSkin;
		if (GUI.Button (new Rect(x,y + height + spacing,width,height),"")){
			SetHelper("Earth");
		}
		GUI.DrawTexture(new Rect(x,y + height + spacing,width,height),earthDecal);

		GUI.skin = windSkin;
		if (GUI.Button (new Rect(x + width + spacing,y + height + spacing,width,height),"")){
			SetHelper("Wind");
		}
		GUI.DrawTexture(new Rect(x + width + spacing,y + height + spacing,width,height),windDecal);

		// Gear Texture


		pivotPoint = new Vector2(x-spacing, y-spacing);
		GUIUtility.RotateAroundPivot (-animateX*2, pivotPoint); 
		GUI.DrawTexture(new Rect(gear1X, gear1Y, gear1Size, gear1Size),gearSkin);
		GUIUtility.RotateAroundPivot (animateX*2, pivotPoint); 

		pivotPoint = new Vector2(x - spacing, y + menuHeight - spacing);
		GUIUtility.RotateAroundPivot (animateX*2, pivotPoint); 
		GUI.DrawTexture(new Rect(gear2X, gear2Y,gear2Size,gear2Size),gearSkin);
		GUIUtility.RotateAroundPivot (-animateX*2, pivotPoint);
		
		GUI.skin = emptySkin;
	}

	// Helper function to set the info window in motion
	private void SetHelper (string towerType){
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
}
