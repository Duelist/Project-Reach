using UnityEngine;
using System.Collections;

/* Developer Notes:
* You can have a tower without a zone, but a zone cannot exist without a tower.
*/

public class Tower {

	private int towerXPos;
	private int towerZPos;
	private Zone zone;
	private string direct; 
	//private Zone wall;
	//private Effect effect;
	private GameObject towerObj;
	
	//Constructor
	public Tower (int x, int z, Zone zOne, string dir) {
		towerXPos = x;
		towerZPos = z;
		//effect = eff;
		zone = zOne;
		direct = dir;
	
		towerObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		towerObj.renderer.enabled = false;
		towerObj.transform.position = new Vector3(towerXPos, 0, towerZPos);
		towerObj.transform.localScale = new Vector3(1f,0.1f,1f);
		towerObj.transform.Rotate(0,0,180);
		towerObj.transform.tag = "tower";
	}
	
	public void SetTextureTower(Texture tex){
		towerObj.renderer.material.mainTexture = tex;
	}
	
	public GameObject GetTowerObj(){
		return towerObj;
	}
	
	public void createZone() {
		if (this.direct == "right") {
			this.createRightZone();
		} else if (this.direct == "left") {
			this.createLeftZone();
		} else if (this.direct == "up") {
			this.createUpperZone();
		} else if (this.direct == "down") {
			this.createLowerZone();
		}
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
		cubex.transform.position = new Vector3(xPos + 2, 0, zPos);
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
