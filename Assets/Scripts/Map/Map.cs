using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
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
	
	void Awake()
	{
		mapName = "Default";
		mapSize = new Vector2(2,2);
		pastState = true;
		tileSize = 32;
		tilesetPast = null;
		tilesetFuture = null;
		tiles = new Tile[(int)mapSize.x*(int)mapSize.y];
	}
	
	public void CleanUp()
	{
		if (tiles != null)
		{
			foreach (Tile tile in tiles)
			{
				Debug.Log("DESTROY!");
				DestroyImmediate(tile.tileObject);
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
	
	public ArrayList GetNeighbours(Tile curr, Tile goal)
	{
		ArrayList neighbours = new ArrayList();
		
		// Left
		if ((curr.position.x - 1 >= 0) && (GetTile(curr.position.x - 1,curr.position.y).collision == false))
		{
			neighbours.Add(GetTile(curr.position.x - 1,curr.position.y));
		}
		
		// Top
		if ((curr.position.y - 1 >= 0) && (GetTile(curr.position.x,curr.position.y - 1).collision == false))
		{
			neighbours.Add(GetTile(curr.position.x,curr.position.y - 1));
		}
		
		// Right
		if ((curr.position.x + 1 < mapSize.x) && (GetTile(curr.position.x + 1,curr.position.y).collision == false))
		{
			neighbours.Add(GetTile(curr.position.x + 1,curr.position.y));
		}
		
		// Bottom
		if ((curr.position.y + 1 < mapSize.y) && (GetTile(curr.position.x,curr.position.y + 1).collision == false))
		{
			neighbours.Add(GetTile(curr.position.x,curr.position.y + 1));
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
				tileObject.transform.parent = this.transform;
				tileObject.transform.position = new Vector3(i,0,j);
				tileObject.transform.localScale = this.transform.localScale;

				tile.pastState = pastState;
				tile.tileObject = tileObject;

				tiles[j*(int)mapSize.x + i] = tile;
			}
		}
	}
}
