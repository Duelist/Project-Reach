using UnityEngine;
using System.Collections;

public class Zone : MonoBehaviour {

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
	
	// Creates a zone for the effect.
	public void createZone(Effect ele, int xPos, int zPos, int rows, int cols) {
		for (int i = -1; i < rows - 1; i++) {
			for (int j = 1; j < cols + 1; j++) {
				// This is just for visual test purposes, will need to replace with actual ingame animation/models
				GameObject cubex = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cubex.transform.position = new Vector3(xPos + i, 0, zPos + j);
				cubex.transform.localScale = new Vector3(1,1,1);
			}
		}
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
