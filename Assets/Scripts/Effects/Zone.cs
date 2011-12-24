using UnityEngine;
using System.Collections;

public class Zone {

	private Vector2 position;
	private int width;
	private int length;
	// private ArrayList effectList;
	private Effect effect;
	private string time;
	
	private GameObject zoneObj;
	
	public Zone (Effect eff, Vector2 pos, int w, int l, string t) {
		width  = w;
		length = l;
		effect = eff;
		time = t;
		position = pos;
		
		zoneObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		zoneObj.transform.position = new Vector3(position.x, 0, position.y);
		zoneObj.transform.localScale = new Vector3(width,0.5f,length);
		zoneObj.renderer.enabled = true;
		
		Texture fireZone = Resources.Load ("WallZone/FireZonePast") as Texture;
		if (time == "future"){
			fireZone = Resources.Load ("WallZone/FireZoneFuture") as Texture;
		}
		zoneObj.renderer.material.mainTexture = fireZone;
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
	
	public Effect GetEffect() {
		return this.effect;
	}
	
	public string getTime() {
		return this.time;
	}
	
	public Zone GetZone() {
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

	public Vector2 GetPosition (){
		return position;
	}
	public void SetPosition(Vector2 pos){
		position = pos;
	}
	
	public void SetActive (){
		zoneObj.renderer.enabled = true;
	}
	
	public void FlipTime (){
		Texture fireZone = Resources.Load ("WallZone/FireZonePast") as Texture;
		if (time == "past"){
			time = "future";
			fireZone = Resources.Load ("WallZone/FireZoneFuture") as Texture;
		}
		else {
			time = "past";
		}
		zoneObj.renderer.material.mainTexture = fireZone;
	}
}
