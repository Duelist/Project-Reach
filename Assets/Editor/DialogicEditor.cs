using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogicManager))]
public class DialogicEditor : Editor
{
	private DialogicManager dm;
	private int dialogId;
	
	public void Awake()
	{
		dm = (DialogicManager)target;
		dialogId = 0;
	}
	
	public override void OnInspectorGUI()
	{
		// Actors
		EditorGUILayout.BeginVertical();
		
		EditorGUILayout.LabelField("Actors");
		
		EditorGUILayout.BeginHorizontal();
		
		EditorGUILayout.LabelField("ID");
		EditorGUILayout.LabelField("Character Name");
		EditorGUILayout.LabelField("GameObject");
		
		EditorGUILayout.EndHorizontal();
		
		for (int i = 0; i < dm.actors.Count; i++)
		{
			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.LabelField(dm.actors[i].id.ToString());
			dm.actors[i].name = EditorGUILayout.TextField(dm.actors[i].name);
			dm.actors[i].gameObject = (GameObject)EditorGUILayout.ObjectField(dm.actors[i].gameObject, typeof(GameObject),true);
			if (GUILayout.Button("Delete Character"))
			{
				dm.actors.RemoveAt(i);
			}
			EditorGUILayout.EndHorizontal();
		}
		
		if (GUILayout.Button("Add Character"))
		{
			if (dm.actors.Count > 0)
				dm.actors.Add(new Actor(dm.actors[dm.actors.Count - 1].id + 1, "Character", null));
			else
				dm.actors.Add(new Actor(0, "Character", null));
		}
		
		EditorGUILayout.EndVertical();
		
		// Dialogs
		EditorGUILayout.BeginVertical();
		
		EditorGUILayout.LabelField("Dialogs");
		
		EditorGUILayout.BeginHorizontal();
		
		//EditorGUILayout.LabelField("Delete dialog:");
		//dialogId = System.Convert.ToInt32(EditorGUILayout.TextField(dialogId.ToString()));
		
		EditorGUILayout.EndHorizontal();
		
		for (int i = 0; i < dm.dialogs.Count; i++)
		{
			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.LabelField(dm.dialogs[i].id.ToString());
			dm.dialogs[i].text = EditorGUILayout.TextField(dm.dialogs[i].text);
			if (GUILayout.Button("Delete"))
			{
				dm.dialogs.RemoveAt(i);
			}
			EditorGUILayout.EndHorizontal();
		}
		
		if (GUILayout.Button("Add Dialog"))
		{
			if (dm.dialogs.Count > 0)
				dm.dialogs.Add(new Dialog(dm.dialogs[dm.dialogs.Count - 1].id + 1, "Default", -1));
			else
				dm.dialogs.Add(new Dialog(0, "Default", -1));
		}
		
		EditorGUILayout.EndVertical();
	}
}