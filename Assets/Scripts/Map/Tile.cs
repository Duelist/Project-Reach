using UnityEngine;
using System;
using System.Collections;

public class Tile : IComparable
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
		collision = false;
		hasSelector = false;
	}
	
	public bool GetCollision(){
		return collision;
	}
	public void SetCollision(bool collision)
	{
		this.collision = collision;
	}
	
	public bool GetSelector(){
		return this.hasSelector;
	}
	public void SetSelector(bool select){
		hasSelector = select;
	}
	
	public void SetTexture(Texture tex){
		tileObject.renderer.material.mainTexture = tex;
	}
	
	public Texture GetTexture(){
		return tileObject.renderer.material.mainTexture;
	}

	public int CompareTo(object other)
	{
		if (this.estimatedTotalCost < ((Tile) other).estimatedTotalCost) return -1;
		else if (this.estimatedTotalCost > ((Tile) other).estimatedTotalCost) return 1;
		else return 0;
	}
	//public bool get
}
