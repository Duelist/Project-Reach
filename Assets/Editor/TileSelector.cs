using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileSelector : EditorWindow
{
	Texture2D selectionBox;
	Vector2 scrollview;
	
	int tileSize;
	Texture2D tileset;
	Vector2 selectionPosition;
	
	public void Init(Texture2D selectionBox, Texture2D tileset, int tileSize)
	{
		this.selectionBox = selectionBox;
		this.tileSize = tileSize;
		this.tileset = tileset;
	}
	
	void OnGUI()
	{	
		GUI.DrawTexture(new Rect(0,0,tileset.width,tileset.height), tileset);
		
		if (GUI.Button(new Rect(0,tileset.height,100,50),"Set Sprite"))
    	{
      		SetTile((int)selectionPosition.x, (int)selectionPosition.y);
    	}
		
		Event e = Event.current;
		Vector2 currentPosition;
		currentPosition.x = (int)((e.mousePosition.x + scrollview.x)/tileSize);
		currentPosition.y = (int)((e.mousePosition.y + scrollview.y)/tileSize);
		
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
	
	void SetTile(int x, int y)
	{
		if (!tileset)
			return;
		
		x = (tileset.width/tileSize) - x - 1;
		Color[] pixels = tileset.GetPixels(x*tileSize,y*tileSize,tileSize,tileSize);
		Texture2D newTexture = new Texture2D(tileSize,tileSize);
		newTexture.SetPixels(pixels);
		newTexture.mipMapBias = -10;
		newTexture.Apply(false);
		
		foreach (GameObject obj in Selection.gameObjects)
		{
			obj.renderer.material.mainTexture = newTexture;
		}
	}
}
