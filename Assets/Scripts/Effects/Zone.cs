using UnityEngine;
using System.Collections;

public class Zone {

	private Vector2 position;
	private int width;
	private int length;
	// private ArrayList effectList;
	private Effect effect;
	private bool pastState;
	private bool reverseZone;
	
	private GameObject zoneObj;
	private GameObject bar1;
	private GameObject bar2;
	private GameObject bar3;
	private GameObject circle;
	
	public Zone (Effect eff, Vector2 pos, int w, int l, bool t, bool rz) {
		width  = w;
		length = l;
		effect = eff;
		pastState = t;
		position = pos;
		reverseZone = rz;
		
		GameObject zonePrefab = PrefabFactory.GetZonePrefab();
		GameManager.InstantiateModel(zonePrefab, new Vector3(pos.x, 0, pos.y));
		zoneObj = GameObject.Find("ZonePrefab(Clone)");
		zoneObj.name = "Zone of Effect";
		
		if (reverseZone){
			zoneObj.transform.Rotate(0,180,0);
		}
		if (eff.GetEffectType() == Effect.EffectType.Fire){
			// Change lighting to Red
			GameObject effectLight = zoneObj.transform.Find("EffectLight").gameObject;
			effectLight.light.color = Color.red;
			
			// Remove Snow Particle Effect
			GameObject snowEffect = zoneObj.transform.Find("SnowEffect").gameObject;
			GameManager.DestroyObject(snowEffect);
		}
		
		bar1 = zoneObj.transform.Find("CircleBar1").gameObject;
		bar2 = zoneObj.transform.Find("CircleBar2").gameObject;
		bar3 = zoneObj.transform.Find("CircleBar3").gameObject;
		circle = zoneObj.transform.Find("CircleOut").gameObject;
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
		if (pastState == true){
			pastState = false;
			SetZoneObjColor(Color.white);
		}
		else {
			pastState = true;
			SetZoneObjColor(Color.gray);
		}
	}
	
	private void SetZoneObjColor (Color c){
		bar1.renderer.material.color = c;
		bar2.renderer.material.color = c;
		bar3.renderer.material.color = c;
		circle.renderer.material.color = c;
	}
}
