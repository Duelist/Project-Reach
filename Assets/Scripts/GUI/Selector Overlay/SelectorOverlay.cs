using UnityEngine;
using System.Collections;

public class SelectorOverlay {
	private float buttonSize;
	private Vector2 firstTilePos;
	private Vector2 lastTilePos;
	
	private bool mouseDown;
	private bool mouseDrag;
	private bool mouseUp;
	
	private bool mouseDownOnSelector;
	private bool mouseDownOnSpellSelector;
	
	public bool drawMen = false;
	public int menx;
	public int meny;
	private Vector3 mouseDownPos;
	
	private Map mapStore;
	
	Camera camera;
	
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
		lastTilePos = new Vector2 ((camera.WorldToScreenPoint(map.tiles[map.mapSizeX-1,map.mapSizeY-1].tileObject.transform.position)).x + (buttonSize/2), (camera.WorldToScreenPoint(map.tiles[map.mapSizeX-1,map.mapSizeY-1].tileObject.transform.position)).y + (buttonSize/2));
		mapStore = map;
		
		ResetButtonStates();
		
		mouseDownPos = new Vector3 (-1,-1, 0);
	}
	
	public void DrawGUI (Hashtable towerList, Player player){
		Event e = Event.current;
		if (e.type == EventType.MouseDown){
			mouseDown = true;
			mouseDownPos.x = Input.mousePosition.x;
			mouseDownPos.y = Input.mousePosition.y;
			mouseDownPos.z = 0;
			
			Debug.Log("Mouse Down x:" + Input.mousePosition.x + " y:" + Input.mousePosition.y + " z:" + Input.mousePosition.z);
			if (WithinBounds() && OnSelector((int)mouseDownPos.x,(int)mouseDownPos.y,towerList)){
				Debug.Log ("Mouse Down On Selector");
				mouseDownOnSelector = true;
			} 
			else if (WithinBounds() && OnSpellSelector((int)mouseDownPos.x,(int)mouseDownPos.y)) {
				Debug.Log("SPELL SELECTOR");
				mouseDownOnSpellSelector = true;
			}
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
			// Click Events
			if (mouseDown == true && mouseDrag == false && mouseUp == true){
				if (WithinBounds() && OnSelector((int)mouseDownPos.x,(int)mouseDownPos.y,towerList)){
					Debug.Log ("On Selector");
					//drawMen = true;
					//menx = (int) Input.mousePosition.x;
					//meny = Screen.height - (int) Input.mousePosition.y;	

					// will need to select a tower to build later before creating tower.
							
				}
				else if (WithinBounds() && OnTower((int)mouseDownPos.x,(int)mouseDownPos.y,towerList)){
					//Tower Select Flip
					int tableKey = GenerateKeyFromMouseClick();
					((Tower)towerList[tableKey]).FlipTime();
					
					// Zone Select Flip
					/*ArrayList zoneList = GetZones((int)mouseDownPos.x, (int)mouseDownPos.y, towerList);
					foreach (Zone zone in zoneList){
						zone.FlipTime();
					}*/
				}
			}
			// Drag Events
			else if (mouseDown == true && mouseDrag == true && mouseUp == true){
				if (mouseDownOnSelector == true){
					// Create Fire Tower
					float onScreenXPos = mouseDownPos.x - buttonSize/2;
					float onScreenYPos = mouseDownPos.y - buttonSize/2;
					Debug.Log(onScreenXPos + "|" + Input.mousePosition.x);
					Debug.Log(onScreenYPos + "|" + Input.mousePosition.y);
					// Fire button condition
					if (Input.mousePosition.x > onScreenXPos && Input.mousePosition.x < onScreenXPos + buttonSize && Input.mousePosition.y > onScreenYPos + buttonSize && Input.mousePosition.y < (onScreenYPos + buttonSize * 2)){
						CreateTower((int)mouseDownPos.x,(int)mouseDownPos.y, towerList, player, Effect.EffectType.Fire);	
						Debug.Log ("Create Splash Tower");
					}
					// Fire button condition
					else if (Input.mousePosition.x > onScreenXPos && Input.mousePosition.x < onScreenXPos + buttonSize && Input.mousePosition.y > (onScreenYPos - buttonSize) && Input.mousePosition.y < onScreenYPos){
						CreateTower((int)mouseDownPos.x,(int)mouseDownPos.y, towerList, player, Effect.EffectType.Ice);	
						Debug.Log ("Create Single Fire Tower");
					}
				}
				// create a wall here =)
			}
			ResetButtonStates();
		}
		
		// Radial Menu
		// Change of button to event
		if (mouseDownOnSelector == true){
			float onScreenXPos = mouseDownPos.x - (buttonSize/2);
			float onScreenYPos = (lastTilePos.y - firstTilePos.y) - (mouseDownPos.y - (buttonSize/2));
			GUI.skin = GUISkinFactory.GetFireButtonSkin();
			if (GUI.Button (new Rect(onScreenXPos,onScreenYPos - buttonSize,buttonSize,buttonSize),"")){}
			GUI.DrawTexture(new Rect(onScreenXPos,onScreenYPos - buttonSize,buttonSize,buttonSize),TextureFactory.GetFireButtonDecal());
				
			GUI.skin = GUISkinFactory.GetIceButtonSkin();
			if (GUI.Button (new Rect(onScreenXPos,onScreenYPos + buttonSize,buttonSize,buttonSize),"")){}
			GUI.DrawTexture(new Rect(onScreenXPos,onScreenYPos + buttonSize,buttonSize,buttonSize),TextureFactory.GetIceButtonDecal());
			
			/*
			GUI.skin = GUISkinFactory.GetEarthButtonSkin();
			if (GUI.Button (new Rect(onScreenXPos + buttonSize,onScreenYPos,buttonSize,buttonSize),"")){}
			GUI.DrawTexture(new Rect(onScreenXPos + buttonSize,onScreenYPos,buttonSize,buttonSize),TextureFactory.GetEarthButtonDecal());
				
			GUI.skin = GUISkinFactory.GetWindButtonSkin();
			if (GUI.Button (new Rect(onScreenXPos - buttonSize,onScreenYPos,buttonSize,buttonSize),"")){}
			GUI.DrawTexture(new Rect(onScreenXPos - buttonSize,onScreenYPos,buttonSize,buttonSize),TextureFactory.GetWindButtonDecal());
			*/
		} else if (mouseDownOnSpellSelector == true) {
			CreateSpell((int)mouseDownPos.x,(int)mouseDownPos.y, player, "ice");
		}
	}
	
	private void ResetButtonStates(){
		mouseDown = false;
		mouseDrag = false;
		mouseUp = false;
		mouseDownOnSelector = false;
	}
	
	// Need to use this method twice to get x and y
	public int CalculateTile (float Position){
		float i = buttonSize;
		int hashKey = 0;
		while (i < Position){ // upto the size of the map.
			hashKey++;
			i += buttonSize;
		}
		return hashKey;
	}
	
	// Generate the key for store/search purposes
	public int GenerateKey (int x, int y){
		return (int)(0.5*(x + y)*(x + y + 1) + y);
	}
	
	public int GenerateKeyFromMouseClick (){
		// Remove the extra space from the origin of the screen to the first tile.
		float storagePosX = Input.mousePosition.x - firstTilePos.x;
		float storagePosY = Input.mousePosition.y - firstTilePos.y;
		
		// Calculate which tile was clicked and generate the tableKey
		int hashKeyX = CalculateTile(storagePosX);
		int hashKeyY = CalculateTile(storagePosY);
		
		// Generate the key for store/search purposes
		int tableKey = GenerateKey(hashKeyX, hashKeyY);
		return tableKey;
	}
	
	private void CreateSpell(int tilex, int tiley, Player player, string element) {
		float storagePosX = tilex - firstTilePos.x;
		float storagePosY = tiley - firstTilePos.y;
		
		// Calculate which tile was clicked and generate the tableKey
		int hashKeyX = CalculateTile(storagePosX);
		int hashKeyY = CalculateTile(storagePosY);
		
		// Generate the key for store/search purposes
		int tableKey = GenerateKey(hashKeyX, hashKeyY);
		
		if (player.GetMana() >= 10){
			Zone zone = new Zone (new Effect(Effect.EffectType.Fire), new Vector2(hashKeyX, hashKeyY), 3, 3, false);
			player.DecMana(10);
		}
		else {
			Debug.Log ("Out of Mana!");
		}
	}

	private void CreateTower(int tilex, int tiley, Hashtable towerList, Player player, Effect.EffectType effect){
		// Remove the extra space from the origin of the screen to the first tile.
		float storagePosX = tilex - firstTilePos.x;
		float storagePosY = tiley - firstTilePos.y;
		
		// Calculate which tile was clicked and generate the tableKey
		int hashKeyX = CalculateTile(storagePosX);
		int hashKeyY = CalculateTile(storagePosY);
		
		// Generate the key for store/search purposes
		int tableKey = GenerateKey(hashKeyX, hashKeyY);
		
		if (player.GetMana() >= 10){
			Tower tower = new Tower (hashKeyX, hashKeyY, mapStore, effect, false);
			towerList.Add(tableKey, tower);
			Debug.Log ("Key Created");
			player.DecMana(10);
		}
		else {
			Debug.Log ("Out of Mana!");
		}
	}
	
	// Calculate if the click was within map bounds
	private bool WithinBounds(){
		if (Input.mousePosition.x >= firstTilePos.x && Input.mousePosition.x <= lastTilePos.x && Input.mousePosition.y >= firstTilePos.y && Input.mousePosition.y <= lastTilePos.y)
			return true;
		Debug.Log ("Not Within bounds");
		return false;
	}
	
	private bool OnSelector(int tilex,int tiley,Hashtable towerList) {
		float storagePosX = tilex - firstTilePos.x;
		float storagePosY = tiley - firstTilePos.y;
		
		// Calculate which tile was clicked and generate the tableKey
		int hashKeyX = CalculateTile(storagePosX);
		int hashKeyY = CalculateTile(storagePosY);
		int tableKey = GenerateKey(hashKeyX, hashKeyY);

		if (mapStore.tiles[hashKeyX,hashKeyY].hasSelector){
			if (!towerList.ContainsKey(tableKey)){
				return true;
			}
		}
		return false;
	}
	
	private bool OnSpellSelector(int tilex,int tiley) {
		float storagePosX = tilex - firstTilePos.x;
		float storagePosY = tiley - firstTilePos.y;
		
		// Calculate which tile was clicked and generate the tableKey
		int hashKeyX = CalculateTile(storagePosX);
		int hashKeyY = CalculateTile(storagePosY);

		if (mapStore.tiles[hashKeyX,hashKeyY].hasSpellSelector){
			return true;
		}
		return false;
	}
	
	private bool OnTower(int tilex,int tiley,Hashtable towerList){
		float storagePosX = tilex - firstTilePos.x;
		float storagePosY = tiley - firstTilePos.y;
		
		// Calculate which tile was clicked and generate the tableKey
		int hashKeyX = CalculateTile(storagePosX);
		int hashKeyY = CalculateTile(storagePosY);
		
		int tableKey = GenerateKey (hashKeyX, hashKeyY);
		if (towerList.ContainsKey(tableKey)){
			return true;
		}
		return false;
	}
	
	// Returns an arraylist of zones at the location
	private ArrayList GetZones (int tilex, int tiley, Hashtable towerList){
		// Remove the extra space from the origin of the screen to the first tile.
		float storagePosX = tilex - firstTilePos.x;
		float storagePosY = tiley - firstTilePos.y;
		
		// Calculate which tile was clicked and generate the tableKey
		int tileClickedX = CalculateTile(storagePosX);
		int tileClickedY = CalculateTile(storagePosY);
		
		ArrayList zoneList = new ArrayList ();
		foreach (Tower tower in towerList.Values){
			Zone newZone = tower.GetZone();
			Vector2 zonePos = newZone.GetPosition();
			// if the enemy is within a zone
			if (zonePos.x - tileClickedX <= 1 
				&& zonePos.x - tileClickedX >= -1
				&& zonePos.y - tileClickedY <= 1
				&& zonePos.y - tileClickedY >= -1){
					
				zoneList.Add(newZone);
			}
		}
		
		return zoneList;
	}
}