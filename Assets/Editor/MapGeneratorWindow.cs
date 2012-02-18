using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class MapGeneratorWindow : EditorWindow
{
    private int[,] tile_num_dict;
    private Texture2D[,] tile_tex_dict;
    private Hashtable tile_tex_table;
    private string map_text;
    private string map_file;
    private TextAsset map_data;
    private char file_sep;
    
    public void LoadData(int[,] nD, Texture2D[,] tD)
    {
        tile_num_dict = nD;
        tile_tex_dict = tD;
        tile_tex_table = new Hashtable();
        map_text = "";
        map_file = "Maps/test";
        map_data = new TextAsset();
        file_sep = ',';
        for (int i = 0; i < tile_num_dict.GetLength(0); i++)
            for (int j = 0; j < tile_num_dict.GetLength(1); j++)
                tile_tex_table.Add(tile_num_dict[i,j],tile_tex_dict[i,j]);
    }
    
    void GenerateMap()
    {
        GameObject map = new GameObject("Map");
        Map mapComponent = map.AddComponent<Map>();
        
        StringReader reader = new StringReader(map_text);
        if (reader == null)
        {
            Debug.Log(map_file + " could not be found, or is unreadable.");
        }
        else
        {
            string[] lines = reader.ReadToEnd().Split('\n');
            int x = 0;
            int y = 0;
            
            for (int i = lines.Length - 1; i >= 0; i--)
            {
                string[] data = lines[i].Split(file_sep);
                x = 0;
                foreach (string token in data)
                {
                    GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Tile tileComponent = tile.AddComponent<Tile>();
                    tileComponent.name = "Tile [" + (x+1).ToString() + ", " + (y+1).ToString() + "]";
                    tileComponent.id = System.Convert.ToInt32(token.Trim());
                    tile.name = tileComponent.name;
                    tile.transform.parent = map.transform;
                    tile.transform.position = new Vector3((float)x,0,(float)y);
                    tile.transform.localScale = new Vector3(1.0f,0.1f,1.0f);
                    tile.renderer.material.mainTexture = (Texture2D) tile_tex_table[tileComponent.id];
                    tile.renderer.material.mainTextureScale = new Vector2(-1.0f,-1.0f);
                    
                    x++;
                }
                y++;
            }
            
            mapComponent.width = x;
            mapComponent.height = y;
        }
    }
    
    void OnGUI()
    {
        map_text = EditorGUILayout.TextArea(map_text, GUILayout.Height(256));
        map_file = EditorGUILayout.TextField("Map File", map_file);
        if (GUILayout.Button("Load Map from Resources"))
        {
            map_data = (TextAsset) Resources.Load(map_file);
            map_text = map_data.text;
        }
        if (GUILayout.Button("Create Map"))
        {
            MapCreatorWindow window = (MapCreatorWindow)EditorWindow.GetWindow(typeof(MapCreatorWindow));
            window.LoadData(tile_num_dict, tile_tex_table);
        }
        if (GUILayout.Button("Generate Map!"))
        {
            GenerateMap();
        }
    }
}