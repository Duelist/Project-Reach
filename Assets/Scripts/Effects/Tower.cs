using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	private int towerXPos;
	private int towerZPos;
	private Zone zone;
	private Zone wall;
	
	//Constructor
	public Tower (int x, int z, Zone zOne, Zone w) {
		towerXPos = x;
		towerZPos = z;
		zone = zOne;
		wall = w;
	}
	
	/* Setters and Getters */
	public void setZone(Zone z) {
		this.zone = z;
	}
	
	public void setWall(Zone w) {
		this.wall = w;
	}
	
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
	
	public Zone getWall() {
		return this.wall;
	}
}
