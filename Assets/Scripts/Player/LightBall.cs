using UnityEngine;
using System.Collections;

public class LightBall{
	GameObject lightBall;
	static Vector3 endPos;
	static float lifeTime;
	
	public LightBall (string name, Vector3 sPos, Vector3 ePos, float lTime){
		endPos = ePos;
		lifeTime = lTime;
		
		GameObject lightPrefab = PrefabFactory.GetLightBallPrefab();
		GameManager.InstantiateModel(lightPrefab, sPos);
		lightBall = GameObject.Find("LightBall(Clone)");
		lightBall.name = name;
		lightBall.AddComponent("LightBallMove");
	}
	
	public LightBall (string name, Vector3 sPos, Vector3 ePos, float lTime, Color glowColor){
		endPos = ePos;
		lifeTime = lTime;
		
		GameObject lightPrefab = PrefabFactory.GetLightBallPrefab();
		GameManager.InstantiateModel(lightPrefab, sPos);
		lightBall = GameObject.Find("LightBall(Clone)");
		lightBall.name = name;
		lightBall.AddComponent("LightBallMove");
		
		GameObject glow = lightBall.transform.Find("Glow").gameObject;
		glow.light.color = glowColor;
	}
	
	public static Vector3 GetCurrentEndPos(){
		return endPos;
	}
	
	public static float GetCurrentLifeTime(){
		return lifeTime;
	}
}
