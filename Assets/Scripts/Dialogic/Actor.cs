using UnityEngine;
using System.Collections;

public class Actor 
{
	public int id;
	public string name;
	public GameObject gameObject;

	public Actor(int id, string name, GameObject gameObject)
	{
		this.id = id;
		this.name = name;
		this.gameObject = gameObject;
	}
}
