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
	
	void Start()
	{
	}

	void OnGUI()
	{
		if (CheckAllTriggers() != null)
		{
			GameStorage.gameState = GameStorage.GameState.Paused;
		}
	}
	
	public Dialog CheckAllTriggers()
	{
		Dialog dialog = null;
		
		for (int i = 0; i < this.dialogs.Count; i++)
		{
			if (this.dialogs[i].parent == -1)
			{
				if (this.dialogs[i].CheckTrigger())
				{
					dialog = this.dialogs[i];
					break;
				}
			}
		}
			
		return dialog;
	}
}
