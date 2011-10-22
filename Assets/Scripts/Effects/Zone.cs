using UnityEngine;
using System.Collections;

public class Zone {

	private int width;
	private int length;
	// private ArrayList effectList;
	private Effect effect;
	private string time;
	
	public Zone (Effect eff, int x, int y, string t) {
		width  = x;
		length = y;
		effect = eff;
		time = t;
	}
	
	/* Setters and Getters */
	public void setTime(string t) {
		this.time = t;
	}
	
	public void setEffect(Effect eff) {
		this.effect = eff;
	}
	
	public void setWidth(int w) {
		this.width = w;
	}
	
	public void setLength(int l) {
		this.length = l;
	}
	
	public Effect getEffect() {
		return this.effect;
	}
	
	public string getTime() {
		return this.time;
	}
	
	public Zone getZone() {
		return this;
	}
	
	public int getZoneWidth() {
		return this.width;
	}
	
	public int getZoneLength() {
		return this.length;
	}
	
	public string getZoneEffect() {
		return this.effect.getEffectElement();
	}
	
	public string getZoneSpecial() {
		return this.effect.getEffectSpecial();
	}
	
}
