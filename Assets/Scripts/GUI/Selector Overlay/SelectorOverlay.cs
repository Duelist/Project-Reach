using UnityEngine;
using System.Collections;

public class SelectorOverlay {
	private float buttonSize;
	private Vector2 firstTilePos;
	private Vector2 lastTilePos;
	
	private bool mouseDown;
	private bool mouseDrag;
	private bool mouseUp;
	
	private Vector3 mouseDownPos;
	
	private Map mapStore;
	
	Camera camera;
	
	Texture fireTex = Resources.Load ("Tower/FireTower") as Texture;
	public SelectorOverlay(Map map){
		camera = Camera.main;
		if (map.tiles.GetUpperBound(0) > 1){
			buttonSize = Mathf.Abs((camera.WorldToScreenPoint(map.tiles[0,0].tileObject.transform.position)).x - (camera.WorldToScreenPoint(map.tiles[1,0].tileObject.transform.position)).x);
		}
		else {
			Debug.Log("Failed to calculate buttonSize, defaulting to size 32");
			buttonSize = 32;
		}
		Debug.Log(buttonSize+"");
		firstTilePos = new Vector2 ((camera.WorldToScreenPoint(map.tiles[0,0].tileObject.transform.position)).x - (buttonSize/2), (camera.WorldToScreenPoint(map.tiles[0,0].tileObject.transform.position)).y - (buttonSize/2));
		lastTilePos = new Vector2 ((camera.WorldToScreenPoint(map.tiles[map.mapSizeX-1,map.mapSizeY-1].tileObject.transform.position)).x + (buttonSize/2), (camera.WorldToScreenPoint(map.tiles[map.mapSizeX-1,map.mapSizeY-1].tileObject.transform.position)).y - (buttonSize/2));
		mapStore = map;
		
		ResetButtonStates();
		
		mouseDownPos = new Vector3 (-1,-1, 0);
	}
	
	public void DrawGUI (Hashtable towerList, Player player){
		// Change of button to event
		Event e = Event.current;
		if (e.type == EventType.MouseDown){
			mouseDown = true;
			mouseDownPos.x = Input.mousePosition.x;
			mouseDownPos.y = Input.mousePosition.y;
			mouseDownPos.z = 0;
			Debug.Log("Mouse Down x:" + Input.mousePosition.x + " y:" + Input.mousePosition.y + " z:" + Input.mousePosition.z);
		}
		else if (e.type == EventType.MouseDrag){
			mouseDrag = true;
			Debug.Log("Dragging Mouse Position x:" + Input.mousePosition.x + " y:" + Input.mousePosition.y + " z:" + Input.mousePosition.z);
			Vector3 positioning = Input.mousePosition;
			positioning.z = 0;
			Debug.DrawLine(mouseDownPos, positioning, Color.black);
		}
		else if (e.type == EventType.MouseUp){
			mouseUp = true;
			Debug.Log("Current event detected: " + Event.current.type);
			Debug.Log("Input Mouse Position x:" + Input.mousePosition.x + " y:" + Input.mousePosition.y);
			if (mouseDown == true && mouseDrag == false && mouseUp == true){
				if (WithinBounds()){
					CalculateTile(towerList, player);
				}	
			}
			else if (mouseDown == true && mouseDrag == true && mouseUp == true){
				// create a wall here =)
			}
			ResetButtonStates();
		}
	}
	
	private void ResetButtonStates(){
		mouseDown = false;
		mouseDrag = false;
		mouseUp = false;
	}
	
	private void CalculateTile(Hashtable towerList, Player player){
		// Remove the extra space from the origin of the screen to the first tile.
		float storagePosX = Input.mousePosition.x - firstTilePos.x;
		float storagePosY = Input.mousePosition.y - firstTilePos.y;
		
		// Calculate which tile was clicked and generate the tableKey
		float i = buttonSize;
		int hashKeyX = 0;
		while (i < storagePosX){ // upto the size of the map.
			hashKeyX++;
			i += buttonSize;
		}
		i = buttonSize;
		int hashKeyY = 0;
		while (i < storagePosY){ // upto the size of the map.
			hashKeyY++;
			i += buttonSize;
		}
		
		string tableKey = hashKeyX + "," + hashKeyY + "Tower";
		Debug.Log (tableKey);
		
		//Check if tile is a selector
		//Then Check if tableKey is occupied in hashtable (whether there is a tower created)
		if (mapStore.tiles[hashKeyX,hashKeyY].hasSelector){
			if (towerList.ContainsKey(tableKey)){
				Debug.Log ("Key Found");
			}
			else {
				if (player.GetMana() >= 10){
					Tower tower = new Tower (hashKeyX, hashKeyY, "right");
					tower.SetTextureTower(fireTex);
					towerList.Add(tableKey, tower);
					Debug.Log ("Key Created");
					player.DecMana(10);
				}
				else {
					Debug.Log ("Out of Mana!");
				}
			}
		}
		else {
			Debug.Log ("Not a selector");
		}
	}
	
	// Calculate if the click was within map bounds
	private bool WithinBounds(){
		if (Input.mousePosition.x >= firstTilePos.x && Input.mousePosition.x <= lastTilePos.x && Input.mousePosition.y >= firstTilePos.y && Input.mousePosition.y <= lastTilePos.y){
			return true;
		}
		Debug.Log ("Not Within bounds");
		return false;
	}
}