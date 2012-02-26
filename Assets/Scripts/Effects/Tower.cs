using UnityEngine;
using System.Collections;

/* Developer Notes:
* You can have a tower without a zone, but a zone cannot exist without a tower.
*/

public class Tower {

	private int towerXPos;
	private int towerZPos;
	private Zone zone;
	private int direct;
	private bool pastState;
	private Effect.EffectType effect;
	private GameObject towerObj;
	private bool active;
	
	//Constructor
	public Tower (int x, int z, Effect.EffectType effect, bool pastState, int direction) {
		towerXPos = x;
		towerZPos = z;
		this.effect = effect;
		direct = direction;
		
		//direct = dir;
		active = true;
	
		towerObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		towerObj.renderer.enabled = true;
		towerObj.transform.position = new Vector3(towerXPos, 0, towerZPos);
		towerObj.transform.localScale = new Vector3(1f,0.5f,1f);
		towerObj.transform.Rotate(0,0,180);
		towerObj.transform.tag = "tower";
		
		this.pastState = pastState;
		
		if (effect == Effect.EffectType.Fire){
			if (pastState == true){
				SetTextureTower(TextureFactory.GetFireTowerPast());
			}
			else {
				SetTextureTower(TextureFactory.GetFireTowerFuture());
			}
		}
		
		if (effect == Effect.EffectType.Ice){
			if (pastState == true){
				SetTextureTower(TextureFactory.GetIceTowerPast());
			}
			else {
				SetTextureTower(TextureFactory.GetIceTowerFuture());
			}
		}
		CreateZone();
	}
	
	public void SetTextureTower(Texture tex){
		towerObj.renderer.material.mainTexture = tex;
	}
	
	public GameObject GetTowerObj(){
		return towerObj;
	}
	
	private void CreateZone() {
		if (this.direct == 1) {
			this.createZone(this.towerXPos - 2, this.towerZPos - 2);
		} else if (this.direct == 2) {
			this.createZone(this.towerXPos, this.towerZPos - 2);
		} else if (this.direct == 3) {
			this.createZone(this.towerXPos + 2, this.towerZPos - 2);
		} else if (this.direct == 4) {
			this.createZone(this.towerXPos - 2, this.towerZPos);
		} else if (this.direct == 5) {
			this.createZone(this.towerXPos, this.towerZPos);
		} else if (this.direct == 6) {
			this.createZone(this.towerXPos + 2, this.towerZPos);
		} else if (this.direct == 7) {
			this.createZone(this.towerXPos - 2, this.towerZPos + 2);
		} else if (this.direct == 8) {
			this.createZone(this.towerXPos, this.towerZPos + 2);
		}else if (this.direct == 9) {
			this.createZone(this.towerXPos + 2, this.towerZPos + 2);
		}
	}
	
	public void createZone(int newXPos, int newZPos) {
		int xPos = newXPos;
		int zPos = newZPos;
		zone = new Zone (new Effect(Effect.EffectType.Fire), new Vector2(xPos, zPos), 3, 3, pastState);
	}
	
	/*
	// Creates a zone for the effect.
	public void createBottomZone() {
		int xPos = this.towerXPos;
		int zPos = this.towerZPos - 2;
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		zone = new Zone (new Effect(Effect.EffectType.Fire), new Vector2(xPos, zPos), 3, 3, pastState);
	}
	
	public void createBottomLeftZone() {
		int xPos = this.towerXPos - 2;
		int zPos = this.towerZPos - 2;
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		zone = new Zone (new Effect(Effect.EffectType.Fire), new Vector2(xPos, zPos), 3, 3, pastState);
	}
	
	public void createBottomRightZone() {
		int xPos = this.towerXPos + 2;
		int zPos = this.towerZPos - 2;
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		zone = new Zone (new Effect("fire"), new Vector2(xPos, zPos), 3, 3, pastState);
	}
	
	public void createUpperZone() {
		int xPos = this.towerXPos;
		int zPos = this.towerZPos + 2;
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		zone = new Zone (new Effect("fire"), new Vector2(xPos, zPos), 3, 3, pastState);
	}
	
	public void createLeftZone() {
		int xPos = this.towerXPos - 2;
		int zPos = this.towerZPos;
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		zone = new Zone (new Effect("fire"), new Vector2(xPos, zPos), 3, 3, pastState);
	}
	
	public void createTopLeftZone() {
		int xPos = this.towerXPos - 2;
		int zPos = this.towerZPos + 2;
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		zone = new Zone (new Effect("fire"), new Vector2(xPos, zPos), 3, 3, pastState);
	}
	
	public void createRightZone() {
		int xPos = this.towerXPos + 2;
		int zPos = this.towerZPos;
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		zone = new Zone (new Effect("fire"), new Vector2(xPos, zPos), 3, 3, pastState);
	}
	
	public void createTopRightZone() {
		int xPos = this.towerXPos + 2;
		int zPos = this.towerZPos + 2;
		// This is just for visual test purposes, will need to replace with actual ingame animation/models
		zone = new Zone (new Effect("fire"), new Vector2(xPos, zPos), 3, 3, pastState);
	}
	*/
	
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
	
	public void FlipTime (){
		if (pastState == true){
			pastState = false;
		}
		else {
			pastState = true;
		}
		SetTextureTower(GetTowerTexture());
		this.zone.FlipTime();
	}
	
	private Texture GetTowerTexture (){
		Texture towerTex = TextureFactory.GetFireTowerPast(); // default
		
		if (effect == Effect.EffectType.Fire && pastState == true){
			towerTex = TextureFactory.GetFireTowerPast();
		}
		else if (effect == Effect.EffectType.Fire && pastState == false){
			towerTex = TextureFactory.GetFireTowerFuture();
		}
		else if (effect == Effect.EffectType.Ice && pastState == true){
			towerTex = TextureFactory.GetIceTowerPast();
		}
		else if (effect == Effect.EffectType.Ice && pastState == false){
			towerTex = TextureFactory.GetIceTowerFuture();
		}
		
		return towerTex;
	}

	/* public Zone getWall() {
		return this.wall;
	}*/
	
}
