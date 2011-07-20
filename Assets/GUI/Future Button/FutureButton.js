var futureSkin : GUISkin;
var futureLoadSkin : Texture;
var pastSkin : GUISkin;
var pastLoadSkin : Texture;
var timeZone = "Future";
var loadCount = 0;

function OnGUI () {
	width = 300;
	height = 120;
	loadTime = 5;
	x = 0;
	y = Screen.height-height;
	
	if (timeZone == "Future"){
		GUI.skin = futureSkin;
		if (GUI.Button (Rect(x,y,width,height),"")){
			timeZone = "LoadingPast";
			loadCount = Time.time;
		}
	}
	else if (timeZone == "Past"){
		GUI.skin = pastSkin;
		if (GUI.Button (Rect(x,y,width,height),"")){
			timeZone = "LoadingFuture";
			loadCount = Time.time;
		}
	}
	else if (timeZone == "LoadingPast"){
		// loads for loadTime seconds
		if (Time.time - loadCount < loadTime){
			pivotPoint = Vector2(x + width/2,y + height/2);
			if (Mathf.Floor((Time.time - loadCount) % 2) == 1){
				GUIUtility.RotateAroundPivot (5, pivotPoint); 
			}
			else{
				GUIUtility.RotateAroundPivot (-5, pivotPoint); 
			}
			GUI.DrawTexture(Rect(x,y,width,height),futureLoadSkin);
		}
		else {
			timeZone = "Past";
		}
	}
	else if (timeZone == "LoadingFuture"){
		if (Time.time - loadCount < loadTime){
			pivotPoint = Vector2(x + width/2,y + height/2);
			if (Mathf.Floor((Time.time - loadCount) % 2) == 1){
				GUIUtility.RotateAroundPivot (5, pivotPoint); 
			}
			else{
				GUIUtility.RotateAroundPivot (-5, pivotPoint); 
			}
			GUI.DrawTexture(Rect(x,y,width,height),pastLoadSkin);
		}
		else {
			timeZone = "Future";
		}
	}
}