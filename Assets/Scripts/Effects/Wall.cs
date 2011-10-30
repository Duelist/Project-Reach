using UnityEngine;
using System.Collections;


public class Wall {
	private int xStart;
	private int zStart;
	private int xEnd; 
	private int zEnd;
	
	private ArrayList elementList;
	private Vector3 Direction;
	
	private Tower towerOne;
	private Tower towerTwo;
	
	public Wall(Tower One, Tower Two) {
		towerOne = One;
		towerTwo = Two;
		
		xStart = towerOne.getXPos();
		zStart = towerOne.getZPos();
		xEnd = towerTwo.getXPos();
		zEnd = towerTwo.getZPos();
		
		elementList = new ArrayList();
		elementList.Add(towerOne.GetZone().GetEffect());
		elementList.Add(towerTwo.GetZone().GetEffect());
		/*if (ele != null) {
			elementList = ele;
		}*/
		
		// Physically/visually make the wall.
		//createWall(xStart, zStart, xEnd, zEnd);
		
	}
	
	
	
	// Physically/visually create a wall
/*	public void createWall(int xs, int zs, int xe, int ze) {
		int xDiff = Mathf.Abs(xs - xe);
		int zDiff = Mathf.Abs(zs - ze);
		
		// NOTE: also need a script that applies debuffs to enemies as they intersect.
		
		if (xDiff == 0) {  //Vertical wall
			for (int i = 0;  i <= zDiff; i++) {
				GameObject cubeVert = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cubeVert.transform.position = new Vector3(xs, 0, zs + i);
				cubeVert.transform.localScale = new Vector3(1,1,1);
			}
		} else if (zDiff == 0) {
			for (int j = 0;  j <= zDiff; j++) {
				GameObject cubeHor = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cubeHor.transform.position = new Vector3(xs + j, 0, zs);
				cubeHor.transform.localScale = new Vector3(1,1,1);
			}
		}
	}
	*/
	// Setters and getters
	
	// Sets the x coordinate starting position of the wall, as an int
	public void setXStart(int x) {
		this.xStart = x;
	}
	
	// sets the z coordinate starting position of the wall, as an int
	public void setZStart(int z) {
		this.zStart = z;
	}
	
	// sets the x coordinate ending position of the wall, as an int
	public void setXEnd(int x) {
		this.xEnd = x;
	}
	
	// Sets the z coordinate ending position of the wall, as an int
	public void setZEnd(int z) {
		this.zEnd = z;
	}
	
	
	// Returns the x coordinate starting position of the wall, as an int
	public int getXStart() {
		return this.xStart;
	}
	
	// Returns the z coordinate starting position of the wall, as an int
	public int getZStart() {
		return this.zStart;
	}
	
	// Returns the x coordinate ending position of the wall, as an int
	public int getXEnd() {
		return this.xEnd;
	}
	
	// Returns the z coordinate ending position of the wall, as an int
	public int getZEnd() {
		return this.zEnd;
	}
	
	// Methods for the elementList field.
	// Returns the element list that the wall is effected by
	public ArrayList getElementList() {
		return elementList;
	}
	
	// Adds an element to the list, must change this so it can take "effect" objects
	public ArrayList addEffect(Effect eff) {
		if (!this.elementList.Contains(eff)) {
			this.elementList.Add(eff);
		}
		return this.elementList;
	}
	
	public ArrayList removeEffect(Effect eff) {
		if (this.elementList.Contains(eff)) {
			this.elementList.Remove(eff);
		}
		return this.elementList;
	}
	
	public ArrayList changeEffect(Effect oldEffect, Effect newEffect) {
		this.removeEffect(oldEffect);
		this.addEffect(newEffect);
		return this.elementList;
	}
}
