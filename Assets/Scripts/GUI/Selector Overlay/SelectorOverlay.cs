using UnityEngine;
using System.Collections;

public class SelectorOverlay {
	private Vector2 [] selectorButtonList;
	private bool [] selectorActive;
	private int listSize;
	private float buttonSize;
	Texture fireTex = Resources.Load ("Tower/FireTower") as Texture;
	public SelectorOverlay(Map map){
		Camera camera = Camera.main;
		if (map.tiles.GetUpperBound(0) > 1){
			buttonSize = Mathf.Abs((camera.WorldToScreenPoint(map.tiles[0,0].tileObject.transform.position)).x - (camera.WorldToScreenPoint(map.tiles[1,0].tileObject.transform.position)).x);
		}
		else {
			buttonSize = 32;
		}
		Debug.Log(buttonSize+"");
		listSize = map.selectorNum;
		
		selectorButtonList = new Vector2[listSize];
		selectorActive = new bool[listSize];
		int counter = 0;
		for (int i = 0; i < map.mapSize.x; i++){
			for (int j = 0; j < map.mapSize.y; j++){
				if (map.tiles[i,j].GetSelector()){
					Vector3 meepo = camera.WorldToScreenPoint(map.tiles[i,j].tileObject.transform.position);
					//Debug.Log(meepo.x + ", " + meepo.y +", "+ meepo.z);
					//Need to add modifications to the x and y values to get the corner of the tile.
					//Need to invert the Y numbers to match world space locations for buttons but not Events
					//float yStore = Screen.height - meepo.y - (buttonSize/2);
					float xStore = meepo.x - (buttonSize/2);
					float yStore = meepo.y - (buttonSize/2);
					
					selectorButtonList[counter] = new Vector2 (xStore, yStore);
					selectorActive[counter] = true;
					counter++;
				}
			}
		}
	}
	
	public void DrawGUI (Tower [] towerList, Player player){
		// Change of button to event
		Event e = Event.current;
		if (e.type == EventType.MouseDrag){
			Debug.Log("Current event detected: " + Event.current.type);
		}
		if (e.type == EventType.MouseUp){
			Debug.Log("Current event detected: " + Event.current.type);
			Debug.Log("Input Mouse Position x:" + Input.mousePosition.x + " y:" + Input.mousePosition.y); 
			for (int i = 0; i < listSize; i++){
				if (selectorActive[i]){
					if (selectorButtonList[i].x + buttonSize > Input.mousePosition.x && selectorButtonList[i].x < Input.mousePosition.x ){
						if (selectorButtonList[i].y + buttonSize > Input.mousePosition.y && selectorButtonList[i].y < Input.mousePosition.y ){ 
							if (player.GetMana() >= 10){	
								towerList[i].SetActive(fireTex);
								selectorActive[i] = false;
								player.DecMana(10);
							}
							else {
								Debug.Log ("Insufficient Mana to create a tower");
							}
						}
					}
				}
				
			}
		}
		/*
		for (int i = 0; i < listSize; i++){
			if (selectorActive[i]){
				if (GUI.Button (new Rect(selectorButtonList[i].x,selectorButtonList[i].y,buttonSize,buttonSize), i+"")){
					if (player.GetMana() >= 10){	
						towerList[i].SetActive(fireTex);
						selectorActive[i] = false;
						player.DecMana(10);
					}
					else {
						Debug.Log ("Insufficient Mana to create a tower");
					}
				}
			}
		}*/
	}
}