using UnityEngine;

public class Dialog
{
	public int id;
	public string text;
	public int parent;
	public Trigger trigger;

	public Dialog(int id, string text)
	{
		this.id = id;
		this.text = text;
		this.parent = -1;
	}

	public Dialog(int id, string text, int parent)
	{
		this.id = id;
		this.text = text;
		this.parent = parent;
	}
	
	public bool CheckTrigger()
	{
		return this.trigger.Check();
	}
}
