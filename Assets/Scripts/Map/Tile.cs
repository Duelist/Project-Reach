using UnityEngine;
using System.Collections;

public class Tile
{
	public bool pastState;
	public bool hasSelector;
	public bool collision;
	public float estimatedTotalCost;
	public float costSoFar;
	public Tile parentTile;
	public GameObject tileObject;
	
	public Tile()
	{
		estimatedTotalCost = 0.0f;
		costSoFar = 1.0f;
		parentTile = null;
		tileObject = null;
	}
	
	public void SetCollision(bool collision)
	{
		this.collision = collision;
	}
	
	public bool GetCollision(){
		return collision;
	}
	//public bool get
}
