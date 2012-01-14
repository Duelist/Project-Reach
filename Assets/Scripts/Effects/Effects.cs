using UnityEngine;
using System.Collections;

public class Effect
{	
	public enum EffectType {None, Fire, Ice, Earth, Wind};
	public enum DebuffType {None, Weaken, Slow, Slip, Burn};
	
	private EffectType effectType;
	private DebuffType debuffType;
	
	private float damage;
	private float damageIncrease;
	
	private float armour;
	private float armourReduction;
	
	private float speed;
	private float speedReduction;
	
	// Effects constructor, creates an effect object and sets variables accordingly
	// Since we are creating an effect, then we are creating a zone, thus effect and zone are the same
	public Effect (EffectType effect) {
		this.effectType = effect;
		
		if (this.effectType == EffectType.Fire) {
			damage = damage * damageIncrease;
			this.debuffType = DebuffType.Burn;
		} else if (this.effectType == EffectType.Earth) {
			armour = armour * armourReduction;
			this.debuffType = DebuffType.Weaken;
		} else if (this.effectType == EffectType.Wind) { 
			speed = speed * speedReduction;
			this.debuffType = DebuffType.Slow;
		} else if (this.effectType == EffectType.Ice) {
			this.debuffType = DebuffType.Slip;
		} else if (this.effectType == EffectType.None) {
			this.debuffType = DebuffType.None;
		}
		//this.setEffectSpecial(eff);
		// createZone(effectElement, towerXPos, towerZPos, width, length);
	}
	
	public void SetEffectType(EffectType effect) {
		this.effectType = effect;
	}
	
	public void SetDebuffType(DebuffType debuff) {
		this.debuffType = debuff;
	}
	
	public EffectType GetEffectType() {
		return this.effectType;
	}
	
	public DebuffType GetDebuffType() {
		return this.debuffType;
	}
	
	public float GetDamage(){
		return damage;
	}
	
}
