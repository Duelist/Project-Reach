using UnityEngine;
using System.Collections.Generic;

public class DialogicManager : MonoBehaviour
{
	public List<Actor> actors;
	public List<Dialog> dialogs;
	
	private int dialogSpeed;
	private int startDialogId;
	private GUISkin dialogSkin;
	
	public DialogicManager()
	{
		actors = new List<Actor>();
		dialogs = new List<Dialog>();
	}
	
	public void Awake()
	{
		//actors = new List<Actor>();
		//dialogs = new List<Dialog>();
	}
	
	public bool CheckAllTriggers()
	{
		bool check = true;
		
		for (int i = 0; i < this.dialogs.Count; i++)
		{
			if (this.dialogs[i].parent == -1)
			{
				if (!this.dialogs[i].CheckTriggers())
				{
					check = false;
					break;
				}
			}
		}
			
		return false;
	}
}