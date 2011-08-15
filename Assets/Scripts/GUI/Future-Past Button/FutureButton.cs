using UnityEngine;
using System.Collections;

public class FutureButton : MonoBehaviour {

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
	void Start () {
		timeZone = "Future";
		loadCount = 0;
		
		width = 300;
		height = 120;
		loadTime = 5;
		x = 0;
		y = Screen.height-height;
	}
	
	// Update is called once per frame
	void OnGUI () {
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
				}
				else{
					GUIUtility.RotateAroundPivot (-5, pivotPoint); 
				}
				GUI.DrawTexture(new Rect(x,y,width,height),futureLoadSkin);
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
				}
				else{
					GUIUtility.RotateAroundPivot (-5, pivotPoint); 
				}
				GUI.DrawTexture(new Rect(x, y, width, height), pastLoadSkin);
			}
			else {
				timeZone = "Future";
			}
		}
	}
}
