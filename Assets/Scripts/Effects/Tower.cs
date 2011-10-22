using UnityEngine;
using System.Collections;

/* Developer Notes:
* You can have a tower without a zone, but a zone cannot exist without a tower.
*/

public class Tower : MonoBehaviour {

	private int towerXPos;
	private int towerZPos;
	private Zone zone;
	//private Zone wall;
	//private Effect effect;
	
	//Constructor
	public Tower (int x, int z, Zone zOne) {
		towerXPos = x;
		towerZPos = z;
		//effect = eff;
		zone = zOne;
	
		GameObject spherex = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		spherex.transform.position = new Vector3(towerXPos, 0, towerZPos);
		spherex.transform.localScale = new Vector3(1,1,1);
		spherex.transform.tag = "tower";
	}
	
	// Creates a zone for the effect.
	public void createLowerZone() {
		int rows = this.zone.getZoneWidth();
		int cols = this.zone.getZoneLength();
		int xPos = this.towerXPos;
		int zPos = this.towerZPos;
		
		Effect eff = this.zone.getEffect();
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		GameObject cubex = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cubex.transform.position = new Vector3(xPos, 0, zPos-2);
		cubex.transform.localScale = new Vector3(rows,1,cols);
	}
	
	public void createUpperZone() {
		int rows = this.zone.getZoneWidth();
		int cols = this.zone.getZoneLength();
		int xPos = this.towerXPos;
		int zPos = this.towerZPos;
		
		Effect eff = this.zone.getEffect();
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		GameObject cubex = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cubex.transform.position = new Vector3(xPos, 0, zPos+2);
		cubex.transform.localScale = new Vector3(rows,1,cols);
	}
	
	public void createLeftZone() {
		int rows = this.zone.getZoneWidth();
		int cols = this.zone.getZoneLength();
		int xPos = this.towerXPos;
		int zPos = this.towerZPos;
		
		Effect eff = this.zone.getEffect();
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		GameObject cubex = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cubex.transform.position = new Vector3(xPos - 2, 0, zPos);
		cubex.transform.localScale = new Vector3(rows,1,cols);
	}
	
	public void createRightZone() {
		int rows = this.zone.getZoneWidth();
		int cols = this.zone.getZoneLength();
		int xPos = this.towerXPos;
		int zPos = this.towerZPos;
		
		Effect eff = this.zone.getEffect();
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		GameObject cubex = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cubex.transform.position = new Vector3(xPos, 0, zPos+2);
		cubex.transform.localScale = new Vector3(rows,1,cols);
	}
	
		// Physically/visually create a wall
	//public void createWall(int xs, int zs, int xe, int ze) {
	public void createWall(Wall wall) {
		int xs = wall.getXStart();
		int zs = wall.getZStart();
		int xe = wall.getXEnd();
		int ze = wall.getZEnd();
		int xDiff = Mathf.Abs(xs - xe);
		int zDiff = Mathf.Abs(zs - ze);
		
		// NOTE: also need a script that applies debuffs to enemies as they intersect.
		
		if (xDiff == 0) {  //Vertical wall
			GameObject cubeVert = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			cubeVert.transform.position = new Vector3(xs, 0, zs + 2);
			cubeVert.transform.localScale = new Vector3(1,1,3);
		
		} else if (zDiff == 0) {
			GameObject cubeHor = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			cubeHor.transform.position = new Vector3(xs + 2, 0, zs);
			cubeHor.transform.localScale = new Vector3(3,1,1);
		}
		
	}
	
	/* Setters and Getters */
	public void setZone(Zone z) {
		this.zone = z;
	}
	
	public void setEffect(Effect eff) {
		this.zone.setEffect(eff);
	}
	
	/*public void setWall(Zone w) {
		this.wall = w;
	}*/
	
	public void setXPos(int x) {
		this.towerXPos = x;
	}
	
	public void setZPos(int z) {
		this.towerZPos = z;
	}
	
	public int getXPos() {
		return this.towerXPos;
	}
	
	public int getZPos() {
		return this.towerZPos;
	}
	
	public Zone getZone() {
		return this.zone;
	}
	
	/* public Zone getWall() {
		return this.wall;
	}*/
}
