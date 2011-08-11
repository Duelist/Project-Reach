using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
	bool mapObjects = false;
	Texture2D selectionBox;
	
	public override void OnInspectorGUI()
	{
		Map map = (Map)target;
		
		map.mapName = EditorGUILayout.TextField("Map Name", map.mapName);
		map.mapSize = EditorGUILayout.Vector2Field("Map Size", map.mapSize);
		map.pastState = EditorGUILayout.Toggle("Past State", map.pastState);
		
		mapObjects = EditorGUILayout.Foldout(mapObjects,"Map Objects");
		if (mapObjects)
		{
		}
		
		map.tileSize = EditorGUILayout.IntField("Tile Size",map.tileSize);
		
		selectionBox = (Texture2D) EditorGUILayout.ObjectField("Selection Box",selectionBox,typeof(Texture2D));
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.BeginVertical();
		map.tilesetPast = (Texture2D) EditorGUILayout.ObjectField("Tileset (Past)",map.tilesetPast,typeof(Texture2D));
		if (map.tilesetPast != null)
		{
			if (GUILayout.Button("Open Selector (Past)"))
			{
				TileSelector windowPast = (TileSelector) EditorWindow.GetWindow(typeof(TileSelector));
				windowPast.Init(selectionBox,true,map.tilesetPast,map.tileSize);
			}
		}
		EditorGUILayout.EndVertical();
		
		EditorGUILayout.BeginVertical();
		map.tilesetFuture = (Texture2D) EditorGUILayout.ObjectField("Tileset (Future)",map.tilesetFuture,typeof(Texture2D));
		if (map.tilesetFuture != null)
		{
			if (GUILayout.Button("Open Selector (Future)"))
			{
				TileSelector windowFuture = (TileSelector) EditorWindow.GetWindow(typeof(TileSelector));
				windowFuture.Init(selectionBox,false,map.tilesetFuture,map.tileSize);
			}
		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndHorizontal();
		
		if (map.tilesetPast != null || map.tilesetFuture != null)
		{
			if (GUILayout.Button("Generate Map"))
			{
				map.CleanUp();
				map.GenerateMap();
			}
		}
		else
		{
			if (GUILayout.Button("Generate Map"))
			{
				map.CleanUp();
				map.GenerateMap();
			}

		}
		
		if (GUI.changed)
			EditorUtility.SetDirty(map);
	}
}
