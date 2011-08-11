using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
	public string mapName;
	public Vector2 mapSize;
	public int tileSize;
	public bool pastState;
	
	public GameObject[] tiles;
	
	public Texture2D tilesetPast;
	public Texture2D tilesetFuture;
	
	public Rect[] startZones;
	public Rect[] endZones;
	
	void Awake ()
	{
		
	}
	
	void Start ()
	{
		mapName = "Default";
		mapSize = new Vector2(2,2);
		pastState = true;
		tileSize = 32;
		tilesetPast = null;
		tilesetFuture = null;
	}
	
	void Update ()
	{
	
	}
	
	public void CleanUp()
	{
		foreach (GameObject tile in tiles)
		{
			DestroyImmediate(tile);
		}
	}
	
	GameObject GetTile(int x, int y)
	{
		int coord = y*(int)mapSize.x + x;
		return tiles[coord];
	}
	
	public void GenerateMap()
	{
		tiles = new GameObject[(int)mapSize.x*(int)mapSize.y];
		
		int count = 0;
		for (int i = 0; i < mapSize.y; i++)
		{
			for (int j = 0; j < mapSize.x; j++)
			{
				GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
				
				tile.name = "Tile " + (j*mapSize.x + i + 1).ToString();
				tile.transform.parent = this.transform;
				tile.transform.position = new Vector3(i,0,j);
				tile.transform.localScale = this.transform.localScale;
				
				tile.AddComponent<Tile>();
				Tile tileComponent = tile.GetComponent<Tile>();
				
				tileComponent.position.x = j;
				tileComponent.position.x = i;
				tileComponent.pastState = pastState;
				
				tiles[count] = tile;
				count += 1;
			}
		}
	}
}
