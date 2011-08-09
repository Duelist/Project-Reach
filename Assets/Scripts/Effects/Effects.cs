using UnityEngine;
using System.Collections;

public class effect : MonoBehaviour
{
	// Effect variables
	public string effectSpecial;
	public string effectElement;
	// global variables
	/*public int damage = 1;
	public int armour = 1;
	public float armourReduction = 0.5;
	public float speedReduction = 0.5;
	public float speedIncrease = 2;
	public float damageIncrease = 2;
*/
	// Position and size variables
	public int width;
	public int length;
	public int towerXPos;
	public int towerZPos;
	
	//Time Variables
	public bool inPast;
	

	// Effects constructor, creates an effect object and sets variables accordingly
	// Since we are creating an effect, then we are creating a zone, thus effect and zone are the same
	
	public effect (string eff, int numRow, int numCol, int x, int z, bool isPast) {
		effectElement = eff;
		inPast = isPast;
		
		this.setEffectSpecial(eff);
		
		width = numRow;
		length = numCol;
		towerXPos = x;
		towerZPos = z;
		
		createZone(effectElement, towerXPos, towerZPos, width, length);
	}
	
	// Creates a zone for the effect.
	public void createZone(string ele, int xPos, int zPos, int rows, int cols) {
			for (int i = -1; i < rows - 1; i++) {
				for (int j = 1; j < cols + 1; j++) {
					// This is just for visual test purposes, will need to replace with actual ingame animation/models
					GameObject cubex = GameObject.CreatePrimitive(PrimitiveType.Cube);
					cubex.transform.position = new Vector3(xPos + i, 0, zPos + j);
					cubex.transform.localScale = new Vector3(1,1,1);
				}
			}
	}
	
	
	// Setters and Getters
	public void setEffectSpecial(string eff) {
		
		if (eff == "fire") {
			//damage = damage * damageIncrease;
			this.effectSpecial = "damage increase";
		} else if (eff == "earth") {
			//armour = armour * armourReduction;
			this.effectSpecial = "armour decrease";
		} else if (eff == "wind") { 
			//speed = speed * speedReduction;
			this.effectSpecial = "slow";
		} else if (eff == "ice") {
			this.effectSpecial = "Slip & Slide";
		}
	}
	
	public void setXPos(int x) {
		this.towerXPos = x;
	}
	
	public void setZPos(int z) {
		this.towerZPos = z;
	}
	
	public void setRows(int rows) {
		this.width = rows;
	}
	
	public void setCols(int cols) {
		this.length = cols;
	}
	
	public void setZoneElementAndSpecial(string ele) {
		this.effectElement = ele;
		this.setEffectSpecial(ele);
	}
	
	public int getXPos() {
		return this.towerXPos;
	}
	
	public int getZPos() {
		return this.towerZPos;
	}
	public int getZoneWidth() {
		return this.width;
	}
	
	public int getZoneLength() {
		return this.length;
	}
	
	public string getZoneEffect() {
		return this.effectElement;
	}
	
	public string getZoneSpecial() {
		return this.effectSpecial;
	}
}
