using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour
{
	// Effect variables
	private string effectSpecial;
	private string effectElement;
	

	// Effects constructor, creates an effect object and sets variables accordingly
	// Since we are creating an effect, then we are creating a zone, thus effect and zone are the same
	public Effect (string eff) {
		effectElement = eff;
		this.setEffectSpecial(eff);
		// createZone(effectElement, towerXPos, towerZPos, width, length);
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
	
	public void seteffectElementAndSpecial(string ele) {
		this.effectElement = ele;
		this.setEffectSpecial(ele);
	}
	
	public string getEffectElement() {
		return this.effectElement;
	}
	
	public string getEffectSpecial() {
		return this.effectSpecial;
	}
	
}
