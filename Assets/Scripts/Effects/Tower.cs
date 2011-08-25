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
