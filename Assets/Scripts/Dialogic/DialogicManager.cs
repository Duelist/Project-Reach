using UnityEngine;
using System.Collections.Generic;

public class DialogicManager : MonoBehaviour
{
	public List<Actor> actors;
	public List<Dialog> dialogs;
	
	private Dialog currentDialog;
	private GUISkin dialogSkin;
	
	public DialogicManager()
	{
		actors = new List<Actor>();
		dialogs = new List<Dialog>();
		currentDialog = null;
	}
	
	void Start()
	{
	}

	void OnGUI()
	{
		currentDialog = GetActiveDialog();
		
		if (currentDialog != null)
		{
			GameStorage.gameState = GameStorage.GameState.Paused;
			DialogBox dialogBox = new DialogBox();
		}
	}
	
	public Dialog GetActiveDialog()
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
