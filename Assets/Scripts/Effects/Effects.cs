using UnityEngine;
using System.Collections;

public class Effect
{
	// Effect variables
	private string effectSpecial;
	private string effectElement; 
	
	private int damage = 1;
	private int damageIncrease = 3;
	
	private float armour;
	private float armourReduction = 0.5f;
	
	private float speed;
	private float speedReduction = 0.5f;
	
	

	// Effects constructor, creates an effect object and sets variables accordingly
	// Since we are creating an effect, then we are creating a zone, thus effect and zone are the same
	public Effect (string eff) {
		effectElement = eff;
		
		if (eff == "fire") {
			damage = damage * damageIncrease;
			this.effectSpecial = "damage increase";
		} else if (eff == "earth") {
			armour = armour * armourReduction;
			this.effectSpecial = "armour decrease";
		} else if (eff == "wind") { 
			speed = speed * speedReduction;
			this.effectSpecial = "slow";
		} else if (eff == "ice") {
			this.effectSpecial = "Slip & Slide";
		} else if (eff == "default") {
			this.effectSpecial = "none";
			damage = damage + damage;
		}
		//this.setEffectSpecial(eff);
		// createZone(effectElement, towerXPos, towerZPos, width, length);
	}
	
	
	// Setters and Getters
	//public void setEffectSpecial(string eff) {
		
	//}
	
	public void seteffectElementAndSpecial(string ele) {
		this.effectElement = ele;
		//this.setEffectSpecial(ele);
	}
	
	public string getEffectElement() {
		return this.effectElement;
	}
	
	public string getEffectSpecial() {
		return this.effectSpecial;
	}
	
	public int GetDamage(){
		return damage;
	}
	
}
