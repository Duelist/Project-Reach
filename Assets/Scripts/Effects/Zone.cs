using UnityEngine;
using System.Collections;

public class Zone {

	private Vector2 position;
	private int width;
	private int length;
	// private ArrayList effectList;
	private Effect effect;
	private bool pastState;
	
	private GameObject zoneObj;
	
	public Zone (Effect eff, Vector2 pos, int w, int l, bool t) {
		width  = w;
		length = l;
		effect = eff;
		pastState = t;
		position = pos;
		
		zoneObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		zoneObj.transform.position = new Vector3(position.x, 0, position.y);
		zoneObj.transform.localScale = new Vector3(width,0.5f,length);
		zoneObj.renderer.enabled = true;
		
		Texture fireZone = Resources.Load ("WallZone/FireZonePast") as Texture;
		if (pastState == false){
			fireZone = Resources.Load ("WallZone/FireZoneFuture") as Texture;
		}
		zoneObj.renderer.material.mainTexture = fireZone;
	}
	
	/* Setters and Getters */
	public void setTime(bool t) {
		this.pastState = t;
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
	
	public bool GetTime() {
		return this.pastState;
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
	
	public Effect.EffectType getZoneEffect() {
		return this.effect.GetEffectType();
	}
	
	public Effect.DebuffType getZoneSpecial() {
		return this.effect.GetDebuffType();
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
		Texture fireZone = TextureFactory.GetFireZonePast();
		if (pastState == true){
			pastState = false;
			fireZone = TextureFactory.GetFireZoneFuture();
		}
		else {
			pastState = true;
		}
		zoneObj.renderer.material.mainTexture = fireZone;
	}
}
