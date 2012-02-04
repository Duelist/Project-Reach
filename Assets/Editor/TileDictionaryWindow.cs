using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileDictionaryWindow : EditorWindow
{
    private int tile_width;
    private int tile_height;
    private int tile_offset;
    private int tile_X;
    private int tile_Y;
    private int tile_preview_width = 64;
    private int tile_preview_height = 64;
    private int[,] tile_num_dict;
    private Texture2D[,] tile_tex_dict;
    private Texture2D tile_map;
    private Vector2 scroll_position = Vector2.zero;
    
    public void LoadData(int tW, int tH, int tO, int tX, int tY, string tM)
    {
        tile_width = tW;
        tile_height = tH;
        tile_offset = tO;
        tile_X = tX;
        tile_Y = tY;
        tile_num_dict = new int[tX,tY];
        tile_tex_dict = new Texture2D[tX,tY];
        tile_map = (Texture2D) Resources.Load(tM);
    }
    
    public void GenerateTiles()
    {
        int y;
        Color[] pixels;
        Texture2D tex;
        if (!tile_map)
            return;
        
        for (int i = 0; i < tile_X; i++)
        {
            for (int j = 0; j < tile_Y; j++)
            {
                y = tile_map.height/tile_height - j - 1;
                pixels = tile_map.GetPixels(i*tile_width + tile_offset,y*tile_height + tile_offset,tile_width,tile_height);
                tex = new Texture2D(tile_width,tile_height);
                tex.SetPixels(pixels);
                tex.Apply();
                tile_tex_dict[i,j] = tex;
                tile_num_dict[i,j] = i + j*tile_Y + 1;
            }
        }
    }
    
    void OnGUI()
    {
        scroll_position = EditorGUILayout.BeginScrollView(scroll_position);
        EditorGUILayout.BeginVertical();
        for (int i = 0; i < tile_X; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < tile_Y; j++)
            {
                EditorGUI.DrawPreviewTexture(new Rect(i*tile_preview_width,j*tile_preview_height,tile_preview_width,tile_preview_height),(Texture2D)tile_tex_dict[i,j]);
                tile_num_dict[i,j] = EditorGUI.IntField(new Rect(i*tile_preview_width + 5,j*tile_preview_height + 2, 20, 20), tile_num_dict[i,j]);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
        
        if (GUILayout.Button("Next"))
        {
            MapGeneratorWindow window = (MapGeneratorWindow)EditorWindow.GetWindow(typeof(MapGeneratorWindow));
            window.LoadData(tile_num_dict, tile_tex_dict);
        }
    }
}