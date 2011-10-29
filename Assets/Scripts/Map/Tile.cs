using UnityEngine;
using System;
using System.Collections;

public class Tile
{
	public int id;
	public bool pastState;
	public bool hasSelector;
	public bool collision;
	public GameObject tileObject;
	
	public Tile()
	{
		id = 0;
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
}
