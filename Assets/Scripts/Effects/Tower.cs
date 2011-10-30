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
	private bool active;
	
	//Constructor
	public Tower (int x, int z, string dir) {
		towerXPos = x;
		towerZPos = z;
		//effect = eff;
		direct = dir;
		active = false;
	
		towerObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		towerObj.renderer.enabled = false;
		towerObj.transform.position = new Vector3(towerXPos, 0, towerZPos);
		towerObj.transform.localScale = new Vector3(1f,0.1f,1f);
		towerObj.transform.Rotate(0,0,180);
		towerObj.transform.tag = "tower";
		
		createZone();
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
		int xPos = this.towerXPos;
		int zPos = this.towerZPos - 2;
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		zone = new Zone (new Effect("fire"), new Vector2(xPos, zPos), 3, 3, "past");
	}
	
	public void createUpperZone() {
		int xPos = this.towerXPos;
		int zPos = this.towerZPos + 2;
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		zone = new Zone (new Effect("fire"), new Vector2(xPos, zPos), 3, 3, "past");
	}
	
	public void createLeftZone() {
		int xPos = this.towerXPos - 2;
		int zPos = this.towerZPos;
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		zone = new Zone (new Effect("fire"), new Vector2(xPos, zPos), 3, 3, "past");
	}
	
	public void createRightZone() {
		int xPos = this.towerXPos + 2;
		int zPos = this.towerZPos;
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		zone = new Zone (new Effect("fire"), new Vector2(xPos, zPos), 3, 3, "past");
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
	
	public void SetActive(Texture tex){
		SetTextureTower(tex);
		towerObj.renderer.enabled = true;
		zone.SetActive();
		active = true;
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
	
	public Zone GetZone() {
		return this.zone;
	}
	
	public bool GetActive(){
		return active;
	}
	/* public Zone getWall() {
		return this.wall;
	}*/
}
