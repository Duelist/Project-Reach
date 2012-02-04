using UnityEngine;
using UnityEditor;

public class TileEditorWindow : EditorWindow
{
    private int tile_width = 16;
    private int tile_height = 16;
    private int tile_offset = 0;
    private int tile_X = 6;
    private int tile_Y = 6;
    private string tile_map = "Tilesets/test";
    
    [MenuItem("Window/Tile Map Editor")]
    static void init()
    {
        TileEditorWindow window = (TileEditorWindow)EditorWindow.GetWindow(typeof(TileEditorWindow));
    }
    
    void OnGUI()
    {
        GUILayout.Label("Tile Settings", EditorStyles.boldLabel);
        
        tile_width = EditorGUILayout.IntField("Tile Width", tile_width);
        tile_height = EditorGUILayout.IntField("Tile Height", tile_height);
        tile_offset = EditorGUILayout.IntField("Tile Offset", tile_offset);
        tile_X = EditorGUILayout.IntField("Tile X", tile_X);
        tile_Y = EditorGUILayout.IntField("Tile Y", tile_Y);
        tile_map = EditorGUILayout.TextField("Tile Map in Resources", tile_map);
        
        if (GUILayout.Button("Next"))
        {
            TileDictionaryWindow window = (TileDictionaryWindow)EditorWindow.GetWindow(typeof(TileDictionaryWindow));
            window.LoadData(tile_width, tile_height, tile_offset, tile_X, tile_Y, tile_map);
            window.GenerateTiles();
        }
    }
}