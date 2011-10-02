using UnityEngine;
using System.Collections;

public class Map
{
	public string mapName;
	public Vector2 mapSize;
	public int tileSize;
	public bool pastState;
	
	public Tile[] tiles;
	
	public Texture2D tilesetPast;
	public Texture2D tilesetFuture;
	
	public ArrayList startZones;
	public ArrayList endZones;
	
	public Map()
	{
		mapName = "Default";
		mapSize = new Vector2(10,10);
		pastState = true;
		tileSize = 32;
		tilesetPast = null;
		tilesetFuture = null;
		//Debug.Log(tiles);
		//tiles = new Tile[(int)mapSize.x*(int)mapSize.y];
	}
	
	public void CleanUp()
	{
		if (tiles != null)
		{
			foreach (Tile tile in tiles)
			{
				Debug.Log("DESTROY!");
				//DestroyImmediate(tile.tileObject);
			}
		}
	}
	
	Tile GetTile(int x, int y)
	{
		int coord = y*(int)mapSize.x + x;
		return tiles[coord];
	}
	
	Tile GetTile(float x, float y)
	{
		int coord = (int)y*(int)mapSize.x + (int)x;
		return tiles[coord];
	}
	
	public ArrayList GetNeighbours(Tile curr)
	{
		ArrayList neighbours = new ArrayList();
		
		// Left
		if ((curr.tileObject.transform.position.x - 1 >= 0) && (GetTile(curr.tileObject.transform.position.x - 1,curr.tileObject.transform.position.z).collision == false))
		{
			neighbours.Add(GetTile(curr.tileObject.transform.position.x - 1,curr.tileObject.transform.position.z));
		}
		
		// Bottom
		if ((curr.tileObject.transform.position.z - 1 >= 0) && (GetTile(curr.tileObject.transform.position.x,curr.tileObject.transform.position.z - 1).collision == false))
		{
			neighbours.Add(GetTile(curr.tileObject.transform.position.x,curr.tileObject.transform.position.z - 1));
		}
		
		// Right
		if ((curr.tileObject.transform.position.x + 1 < mapSize.x) && (GetTile(curr.tileObject.transform.position.x + 1,curr.tileObject.transform.position.z).collision == false))
		{
			neighbours.Add(GetTile(curr.tileObject.transform.position.x + 1,curr.tileObject.transform.position.z));
		}
		
		// Top
		if ((curr.tileObject.transform.position.z + 1 < mapSize.y) && (GetTile(curr.tileObject.transform.position.x,curr.tileObject.transform.position.z + 1).collision == false))
		{
			neighbours.Add(GetTile(curr.tileObject.transform.position.x,curr.tileObject.transform.position.z + 1));
		}

		
		return neighbours;
	}
	
	public void GenerateMap()
	{
		tiles = new Tile[(int)mapSize.x*(int)mapSize.y];
		
		for (int i = 0; i < mapSize.y; i++)
		{
			for (int j = 0; j < mapSize.x; j++)
			{
				Tile tile = new Tile();
				GameObject tileObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

				tileObject.name = "Tile " + (j*mapSize.x + i + 1).ToString();
				tileObject.transform.position = new Vector3(i,0,j);
				tileObject.transform.localScale = new Vector3(1,0,1);

				tile.pastState = pastState;
				tile.tileObject = tileObject;

				tiles[j*(int)mapSize.x + i] = tile;
				//Debug.Log("Making tile at " + tileObject.transform.position);
			}
		}
	}
	
	// Test function -jtai
	public void GenerateLevel1(){
		GenerateMap();
		
		// ---------------------- Setting Collisions ------------------------- //
		// 4 walls surrounding the map
		for (int i=0; i < 10; i++){
			tiles[i].SetCollision(true);
		}
		for (int i=0; i < 10; i++){
			tiles[i*10].SetCollision(true);
		}
		for (int i=0; i < 10; i++){
			tiles[9+(i*10)].SetCollision(true);
		}
		for (int i=0; i < 10; i++){
			tiles[90+(i)].SetCollision(true);
		}
		
		// 2 inner walls
		for (int i = 0; i < 7; i++){
			tiles[3+(i*10)].SetCollision(true);
		}
		for (int i = 0; i < 7; i++){
			tiles[96-(i * 10)].SetCollision(true);
		}
		
		// Openings
		tiles [1].SetCollision(false);
		tiles [2].SetCollision(false);
		tiles [97].SetCollision(false);
		tiles [98].SetCollision(false);
		
		GenerateTestTextures(tiles);
	}// Test Map Class
	
	private void GenerateTestTextures(Tile[] tiles){
		// ---------------------- Setting Textures ------------------------- //
		Texture collisionTex = Resources.Load ("GUI/Rolling Menu Textures/Gear") as Texture;
		Texture pathTex = Resources.Load ("GUI/Rolling Menu Textures/IceButton") as Texture;
		
		for (int i = 0; i < 100; i++){
			if (tiles[i].GetCollision()){
				// Collision Texture
				tiles[i].SetTexture(collisionTex);
			}
			else {
				// Path Texture
				tiles[i].SetTexture(pathTex);
			}
		}
	}
}
