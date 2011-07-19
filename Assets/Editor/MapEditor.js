@CustomEditor(Map)
class MapEditor extends Editor
{
	var mapObjects : boolean = false;
	var selectionBox : Texture2D;

    function OnInspectorGUI()
	{
		target.mapName = EditorGUILayout.TextField("Map Name",target.mapName);
		target.mapSize = EditorGUILayout.Vector2Field("Map Size",target.mapSize);
		target.pastState = EditorGUILayout.Toggle("Past State",target.pastState);
		
		mapObjects = EditorGUILayout.Foldout(mapObjects,"Map Objects");
		if (mapObjects)
		{
		}
		
		target.tileSize = EditorGUILayout.IntField("Tile Size",target.tileSize);
		
		selectionBox = EditorGUILayout.ObjectField("Selection Box",selectionBox,Texture2D);
		
		EditorGUILayout.BeginHorizontal();
		var rectPast : Rect = EditorGUILayout.BeginVertical();
		target.tilesetPast = EditorGUILayout.ObjectField("Tileset (Past)",target.tilesetPast,Texture2D);
		if (target.tilesetPast != null)
		{
			if (GUILayout.Button("Open Selector (Past)"))
			{
				var windowPast : TileSelector = EditorWindow.GetWindow(TileSelector);
				windowPast.Init(selectionBox,true,target.tilesetPast,target.tileSize);
			}
		}
		EditorGUILayout.EndVertical();
		
		var rectFuture : Rect = EditorGUILayout.BeginVertical();
		target.tilesetFuture = EditorGUILayout.ObjectField("Tileset (Future)",target.tilesetFuture,Texture2D);
		if (target.tilesetFuture != null)
		{
			if (GUILayout.Button("Open Selector (Future)"))
			{
				var windowFuture : TileSelector = EditorWindow.GetWindow(TileSelector);
				windowFuture.Init(selectionBox,false,target.tilesetFuture,target.tileSize);
			}
		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndHorizontal();
		
		if (target.tilesetPast != null || target.tilesetFuture != null)
		{
			if (GUILayout.Button("Generate Map"))
			{
				target.cleanUp();
				target.generateMap();
			}
		}
		else
		{
			if (GUILayout.Button("Generate Map"))
			{
				target.cleanUp();
				target.generateMap();
			}

		}
		
		if (GUI.changed)
			EditorUtility.SetDirty(target);
    }
	
	function OnSceneGUI()
	{
	}
}