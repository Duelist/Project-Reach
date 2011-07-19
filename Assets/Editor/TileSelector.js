class TileSelector extends EditorWindow
{
	var selectionBox : Texture2D;
	var scrollview : Vector2;
	
	var tileSize : int;
	var pastState : boolean;
	var tileset : Texture2D;
	var selectionPosition : Vector2;
	
	function Init(selectionBox,pastState,tileset,tileSize)
	{
		this.selectionBox = selectionBox;
		this.pastState = pastState;
		this.tileset = tileset;
		this.tileSize = tileSize;
	}
	
	function OnGUI()
	{	
		GUI.DrawTexture(new Rect(0,0,tileset.width,tileset.height), tileset);
		
		if (GUI.Button(new Rect(0,tileset.height,100,50),"Set Sprite"))
    	{
      		SetTile(selectionPosition.x, selectionPosition.y);
    	}
		
		var e : Event = Event.current;
		var currentPosition : Vector2;
		currentPosition.x = parseInt((e.mousePosition.x + scrollview.x)/tileSize);
		currentPosition.y = parseInt((e.mousePosition.y + scrollview.y)/tileSize);
		
		if (e.type == EventType.MouseDown && e.button == 0)
		{
			selectionPosition = currentPosition;
		}
		
		GUI.DrawTexture(new Rect(selectionPosition.x*tileSize,selectionPosition.y*tileSize,tileSize,tileSize),selectionBox);
		
		if (EditorWindow.mouseOverWindow == this)
		{
			this.Repaint();
		}
	}
	
	function SetTile(x,y)
	{
		if (!tileset)
			return;
		
		x = (tileset.width/tileSize) - x - 1;
		var pixels : Color[];
		pixels = tileset.GetPixels(x*tileSize,y*tileSize,tileSize,tileSize);
		var newTexture : Texture2D = new Texture2D(tileSize,tileSize);
		newTexture.SetPixels(pixels);
		newTexture.mipMapBias = -10;
		newTexture.Apply(false);
		
		for (obj in Selection.gameObjects)
		{
			obj.renderer.material.mainTexture = newTexture;
			var tileComp : Tile = obj.GetComponent(Tile);
			tileComp.pastState = this.pastState;
		}
	}
	
}