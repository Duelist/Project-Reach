using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class MapCreatorWindow : EditorWindow
{
    private int map_X;
    private int map_Y;
    
    private Vector2 map_scroll;
    private Vector2 tile_scroll;
    
    private int selected_tile;
    private int[,] map;
    private string map_file;
    private Texture current_texture;
    
    private int[,] tile_num_dict;
    private Hashtable tile_tex_table;
    
    public void LoadData(int[,] nD, Hashtable tT)
    {
        map_X = 15;
        map_Y = 15;
        
        map_scroll = Vector2.zero;
        tile_scroll = Vector2.zero;
        
        selected_tile = 1;
        current_texture = null;
        map = new int[map_X, map_Y];
        map_file = "";
        
        for (int i = 0; i < map.GetLength(0); i++)
            for (int j = 0; j < map.GetLength(1); j++)
                map[i,j] = 0;
        
        tile_num_dict = nD;
        tile_tex_table = tT;
    }
    
    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        
        EditorGUILayout.BeginHorizontal();
            map_scroll = EditorGUILayout.BeginScrollView(map_scroll);
                EditorGUILayout.BeginVertical();
                    for (int i = 0; i < map_X; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                            for (int j = 0; j < map_Y; j++)
                            {
                                if (map[i,j] == 0)
                                    current_texture = new Texture();
                                else
                                    current_texture = (Texture)tile_tex_table[map[i,j]];
                                
                                if (GUILayout.Button(current_texture,GUILayout.Width(50),GUILayout.Height(50)))
                                {
                                    map[i,j] = selected_tile;
                                }
                            }
                        EditorGUILayout.EndHorizontal();
                    }
                EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
            
            tile_scroll = EditorGUILayout.BeginScrollView(tile_scroll);
                EditorGUILayout.BeginVertical();
                    for (int i = 0; i < tile_num_dict.GetLength(0); i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                            for (int j = 0; j < tile_num_dict.GetLength(1); j++)
                            {
                                if (GUILayout.Button((Texture)tile_tex_table[tile_num_dict[j,i]],GUILayout.Width(50),GUILayout.Height(50)))
                                {
                                    selected_tile = tile_num_dict[j,i];
                                }
                            }
                        EditorGUILayout.EndHorizontal();
                    }
                    
                    if (GUILayout.Button("Fill map with selected tile"))
                    {
                        for (int i = 0; i < map.GetLength(0); i++)
                            for (int j = 0; j < map.GetLength(1); j++)
                                map[i,j] = selected_tile;
                    }
                EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        EditorGUILayout.EndHorizontal();
        
        map_file = EditorGUILayout.TextField("Map filename", map_file);
        
        if (GUILayout.Button("Create map file"))
        {
            CreateMap();
        }
        
        EditorGUILayout.EndVertical();
    }
    
    void CreateMap()
    {
        string map_text = "";
        
        for (int i = 0; i < map_X; i++)
        {
            for (int j = 0; j < map_Y; j++)
            {
                if (j == 0)
                    map_text = map_text + map[i,j];
                else
                    map_text = map_text + ',' + map[i,j];
            }
            
            if (i < map_X-1)
                map_text = map_text + '\n';
        }
        
        File.WriteAllText("Assets/Resources/Maps/"+map_file+".txt", map_text);
    }
}