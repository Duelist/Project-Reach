using UnityEngine;
using System.Collections;

public class DialogBox : MonoBehaviour {

	private Rect groupRect;
	private Rect boxRect;
	private float typingPause;
	private Dialog currentDialog;
	private string currentText;
	private float currentTime;

	private GUISkin dialogSkin;

	public DialogBox(Dialog startDialog)
	{
		this.groupRect = new Rect(0,Screen.height/2,Screen.width,Screen.height/2);
		this.boxRect = new Rect(0,0,Screen.width,Screen.height/2);
		this.typingPause = 0.1f;
		this.currentDialog = startDialog;
		this.currentText = "";
		this.currentTime = Time.time;
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Check for input to progress dialog tree

		GUI.BeginGroup(this.groupRect);
			GUI.Box(this.boxRect, this.currentDialog.text);
		GUI.EndGroup();
		TypeText();
	}

	IEnumerator TypeText()
	{
		for (int i = 0; i < this.currentDialog.text.Length; i++)
		{
			this.currentText += this.currentDialog.text[i];
			currentTime = Time.time;

			Debug.Log("[Dialogic] Current Text: " + this.currentText);
			yield return new WaitForSeconds(typingPause);
		}
	}
}
