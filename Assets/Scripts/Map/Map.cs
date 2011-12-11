using UnityEngine;
using System.Collections;

public class Map
{
	public string mapName;
	public int mapSizeX;
	public int mapSizeY;
	public Vector2 mapSize;
	public int tileSize;
	public bool pastState;
	
	public Tile[,] tiles;
	
	public Texture2D tilesetPast;
	public Texture2D tilesetFuture;
	
	public ArrayList startZones;
	public ArrayList endZones;
	
	public int selectorNum;
	private Vector2 [] selectorPositionList;
	
	public Map()
	{
		mapName = "Default";
		mapSizeX = 15;
		mapSizeY = 15;
		mapSize = new Vector2(mapSizeX,mapSizeY);
		pastState = true;
		tileSize = 32;
		tilesetPast = null;
		tilesetFuture = null;
		selectorNum = 0;
		//Debug.Log(tiles);
		//tiles = new Tile[(int)mapSize.x*(int)mapSize.y];
	}
	
	public Tile GetTile(int x, int y)
	{
		//int coord = y*(int)mapSize.x + x;
		return tiles[x, y];
	}
	
	public Tile GetTile(float x, float y)
	{
		//int coord = (int)y*(int)mapSize.x + (int)x;
		return tiles[(int)x, (int)y];
	}
	
	public ArrayList GetNeighbours(Tile curr)
	{
		ArrayList neighbours = new ArrayList();
		float x = curr.tileObject.transform.position.x;
		float y = curr.tileObject.transform.position.z;
		
		// Left
		if ((x-1 >= 0) && (GetTile(x-1,y).GetCollision() == false))	neighbours.Add(GetTile(x-1,y));
		// Bottom
		if ((y-1 >= 0) && (GetTile(x,y-1).GetCollision() == false))	neighbours.Add(GetTile(x,y-1));
		// Right
		if ((x+1 < mapSize.x) && (GetTile(x+1,y).GetCollision() == false))	neighbours.Add(GetTile(x+1,y));
		// Top
		if ((y+1 < mapSize.y) && (GetTile(x,y+1).GetCollision() == false))	neighbours.Add(GetTile(x,y+1));
		
		// Comment/Uncomment the following to remove/add diagonal movements
		/*
		// Top-Left
		if ((x-1 >= 0) && (y+1 < mapSize.y) && (GetTile(x-1,y+1).GetCollision() == false))	neighbours.Add(GetTile(curr.tileObject.transform.position.x - 1,curr.tileObject.transform.position.z + 1));
		// Bottom-Left
		if ((x-1 >= 0) && (y-1 >= 0) && (GetTile(x-1,y-1).GetCollision() == false))	neighbours.Add(GetTile(x-1,y-1));
		// Top-Right
		if ((x+1 < mapSize.x) && (y+1 < mapSize.y) && (GetTile(x+1,y+1).GetCollision() == false))	neighbours.Add(GetTile(x+1,y+1));
		// Bottom-Right
		if ((x+1 < mapSize.x) && (y-1 >= 0) && (GetTile(x+1,y-1).GetCollision() == false))	neighbours.Add(GetTile(x+1,y-1));
		*/
		
		return neighbours;
	}
	
	public void GenerateMap()
	{
		tiles = new Tile[(int)mapSize.x, (int)mapSize.y];
		
		for (int i = 0; i < mapSize.x; i++)
		{
			for (int j = 0; j < mapSize.y; j++)
			{
				Tile tile = new Tile();
				GameObject tileObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
				tileObject.name = "Tile " + i + "," + j;
				tileObject.transform.position = new Vector3(i,0,j);
				tileObject.transform.localScale = new Vector3(1,0,1);
				
				tile.id = i + j*(int)mapSize.y;
				tile.pastState = pastState;
				tile.tileObject = tileObject;
				
				tiles[i, j] = tile;
				//Debug.Log("Making tile at " + tileObject.transform.position);
			}
		}
	}
	
	// Test function -jtai
	public void GenerateLevel1(){
		GenerateMap();
		
		// ---------------------- Setting Collisions ------------------------- //
		// 4 walls surrounding the map
		for (int i=0; i < (int)mapSize.x; i++){
			tiles[i,0].SetCollision(true);
			// Set selectors for every other tile.
			if (i % 2 == 0){
				tiles[i,0].SetSelector(true);
			}
		}
		for (int i=0; i < (int)mapSize.y; i++){
			tiles[0,i].SetCollision(true);
			if (i % 2 == 0){
				tiles[0,i].SetSelector(true);
			}
		}
		for (int i=0; i < (int)mapSize.x; i++){
			tiles[i,(int)mapSize.y-1].SetCollision(true);
			
			if (i % 2 == 0){
				tiles[i,(int)mapSize.y-1].SetSelector(true);
			}
		}
		for (int i=0; i < (int)mapSize.y; i++){
			tiles[(int)mapSize.x-1, i].SetCollision(true);
			
			if (i % 2 == 0){
				tiles[(int)mapSize.x-1,i].SetSelector(true);
			}
		}
		
		// 2 inner walls
		for (int i = 0; i < 11; i++){
			tiles[4, i].SetCollision(true);
			tiles[5, i].SetCollision(true);
			
			if (i % 2 == 0){
				tiles[4,i].SetSelector(true);
				tiles[5,i].SetSelector(true);
			}
		}
		for (int i = 0; i < 11; i++){
			tiles[9, 14-i].SetCollision(true);
			tiles[10, 14-i].SetCollision(true);
			
			
			if (i % 2 == 0){
				tiles[9, 14-i].SetSelector(true);
				tiles[10, 14-i].SetSelector(true);
			}
		}
		
		// Openings
		tiles [1,0].SetCollision(false);
		tiles [2,0].SetCollision(false);
		tiles [3,0].SetCollision(false);
		tiles [1,0].SetSelector(false);
		tiles [2,0].SetSelector(false);
		tiles [3,0].SetSelector(false);
		tiles [11,14].SetCollision(false);
		tiles [12,14].SetCollision(false);
		tiles [13,14].SetCollision(false);
		tiles [11,14].SetSelector(false);
		tiles [12,14].SetSelector(false);
		tiles [13,14].SetSelector(false);
		
		GenerateTestTextures(tiles);
	}// Test Map Class
	
	private void GenerateLevel(string textureMap, string collisionMap)
	{
		for (int i = 0; i < mapSize.x; i++)
		{
			for (int j = 0; j < mapSize.y; j++)
			{
				if (collisionMap[i+j*((int)mapSize.y)] == '0')
					tiles[i,j].SetCollision(false);
				else
					tiles[i,j].SetCollision(true);
			}
		}
	}
	
	private void GenerateTestTextures(Tile[,] tiles){
		// ---------------------- Setting Textures ------------------------- //
		Texture selectorTex = Resources.Load ("GUI/Rolling Menu Textures/Gear") as Texture;
		Texture collisionTex = Resources.Load ("GUI/Rolling Menu Textures/EarthButton") as Texture;
		Texture pathTex = Resources.Load ("GUI/Rolling Menu Textures/InfoWindow") as Texture;
		
		for (int i = 0; i < mapSize.x; i++){
			for (int j = 0; j < mapSize.y; j++){
				if (tiles[i,j].GetCollision()){
					// Collision Texture
					if (tiles[i,j].GetSelector()){
						tiles[i,j].SetTexture(selectorTex);
						selectorNum++;
					}
					else{
						tiles[i,j].SetTexture(collisionTex);
					}
				}
				else {
					// Path Texture
					tiles[i,j].SetTexture(pathTex);
				}
			}
		}
		
		// Setting selector positions for easy retrieval
		selectorPositionList = new Vector2[selectorNum];
		int counter = 0;
		for (int i = 0; i < mapSize.x; i++){
			for (int j = 0; j < mapSize.y; j++){
				if (tiles[i,j].GetCollision()){
					// Collision Texture
					if (tiles[i,j].GetSelector()){
						selectorPositionList[counter] = new Vector2(i,j);
						counter++;
					}
				}
			}
		}
	}
	
	public Vector2 [] GetSelectorPositionList (){
		return selectorPositionList;
	}
	
	public int GetMapSizeX() {
		return (int) this.mapSize.x;
	}
	
	public int GetMapSizeY() {
		return (int) this.mapSize.y;
	}
}
