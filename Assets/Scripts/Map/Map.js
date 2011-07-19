#pragma strict
@script ExecuteInEditMode()
class Map extends MonoBehaviour
{
	public var mapName : String;
	public var mapSize : Vector2;
	public var tileSize : int;
	public var pastState : boolean;
	
	public var tiles : GameObject [];
	
	public var tilesetPast : Texture2D;
	public var tilesetFuture : Texture2D;

	function Awake()
	{
	}
	
	function Start()
	{
		mapName = "Default";
		mapSize = new Vector2(2,2);
		pastState = true;
		tileSize = 32;
		tilesetPast = null;
		tilesetFuture = null;
	}

	function Update() 
	{
	}

	function cleanUp()
	{
		for (var i = 0; i < tiles.length; i++)
		{
			DestroyImmediate(tiles[i]);
		}
	}

	function generateMap()
	{
		tiles = new GameObject[mapSize.x*mapSize.y];
		
		var count : int = 0;
		for (var i = 0; i < mapSize.y; i++)
		{
			for (var j = 0; j < mapSize.x; j++)
			{
				var tile : GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
				
				tile.name = "Tile " + (j*mapSize.x + i + 1).ToString();
				tile.transform.parent = this.transform;
				tile.transform.position = Vector3(i,0,j);
				tile.transform.localScale = this.transform.localScale;
				
				tile.AddComponent(Tile);
				var tileComponent : Tile = tile.GetComponent(Tile) as Tile;
				
				tileComponent.position.x = j;
				tileComponent.position.x = i;
				tileComponent.pastState = pastState;
				
				tiles[count] = tile;
				count += 1;
			}
		}
	}
}