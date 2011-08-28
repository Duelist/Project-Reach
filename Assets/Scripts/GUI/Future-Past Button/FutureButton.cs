using UnityEngine;
using System.Collections;

public class FutureButton {

	public GUISkin futureSkin;
	public Texture futureLoadSkin;
	public GUISkin pastSkin;
	public Texture pastLoadSkin;
	
	private string timeZone;
	private float loadCount;
	
	private int width;
	private int height;
	private float loadTime;
	private int x;
	private int y;
	private Vector2 pivotPoint;
	
	// Use this for initialization
	public FutureButton (string tz, int lc, int w, int h, int lt, int xx, int yy) {
		timeZone = tz;
		loadCount = lc;
		width = w;
		height = h;
		loadTime =lt;
		x = xx;
		y = yy;
		
		futureSkin = Resources.Load("GUI/Future-Past Button Textures/Future Button Skin") as GUISkin;
		futureLoadSkin = Resources.Load ("GUI/Future-Past Button Textures/Future-Over") as Texture;
		pastSkin = Resources.Load("GUI/Future-Past Button Textures/Past Button Skin") as GUISkin;
		pastLoadSkin = Resources.Load ("GUI/Future-Past Button Textures/Past-Over") as Texture;
	}
	
	public void DrawGUI () {
		bool anti = false;
		if (timeZone == "Future"){
			GUI.skin = futureSkin;
			if (GUI.Button (new Rect(x,y,width,height),"")){
				timeZone = "LoadingPast";
				loadCount = Time.time;
			}
		}
		else if (timeZone == "Past"){
			GUI.skin = pastSkin;
			if (GUI.Button (new Rect(x,y,width,height),"")){
				timeZone = "LoadingFuture";
				loadCount = Time.time;
			}
		}
		else if (timeZone == "LoadingPast"){
			// loads for loadTime seconds
			if (Time.time - loadCount < loadTime){
				pivotPoint = new Vector2(x + width/2,y + height/2);
				if (Mathf.Floor((Time.time - loadCount) % 2) == 1){
					GUIUtility.RotateAroundPivot (5, pivotPoint);
					anti = true;
				}
				else{
					GUIUtility.RotateAroundPivot (-5, pivotPoint); 
					anti = false;
				}
				GUI.DrawTexture(new Rect(x,y,width,height),futureLoadSkin);
				if (anti){
					GUIUtility.RotateAroundPivot (-5, pivotPoint); 
				}
				else {
					GUIUtility.RotateAroundPivot (5, pivotPoint);
				}
			}
			else {
				timeZone = "Past";
			}
		}
		else if (timeZone == "LoadingFuture"){
			if (Time.time - loadCount < loadTime){
				pivotPoint = new Vector2(x + width/2,y + height/2);
				if (Mathf.Floor((Time.time - loadCount) % 2) == 1){
					GUIUtility.RotateAroundPivot (5, pivotPoint);
					anti = true;
				}
				else{
					GUIUtility.RotateAroundPivot (-5, pivotPoint);
					anti = false;
				}
				GUI.DrawTexture(new Rect(x, y, width, height), pastLoadSkin);
				if (anti){
					GUIUtility.RotateAroundPivot (-5, pivotPoint); 
				}
				else {
					GUIUtility.RotateAroundPivot (5, pivotPoint);
				}
			}
			else {
				timeZone = "Future";
			}
		}
	}
}
