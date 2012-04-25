using UnityEngine;
using System.Collections;

public class DialogBox : MonoBehaviour {

	private Rect groupRect;
	private Rect boxRect;
	private Dialog currentDialog;
	private GUISkin dialogSkin;

	public DialogBox(Dialog startDialog)
	{
		this.groupRect = new Rect(0,Screen.height/2,Screen.width,Screen.height/2);
		this.boxRect = new Rect(0,0,Screen.width,Screen.height/2);
		this.currentDialog = startDialog;
	}

	// Use this for initialization
	void Start ()
	{
		GUI.BeginGroup(this.groupRect);
			GUI.Box(this.boxRect, this.currentDialog.text);
		GUI.EndGroup();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Check for input to progress dialog tree
		
	}
}
