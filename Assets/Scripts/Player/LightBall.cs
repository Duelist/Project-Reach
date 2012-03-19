using UnityEngine;
using System.Collections;

public class LightBall{
	GameObject lightBall;
	static Vector3 endPos;
	static float lifeTime;
	
	public LightBall (string name, Vector3 sPos, Vector3 ePos, float lTime){
		endPos = ePos;
		lifeTime = lTime;
		
		GameObject lightPrefab = (GameObject)Resources.Load("Player/LightBall",typeof(GameObject));
		GameManager.InstantiateModel(lightPrefab, sPos);
		lightBall = GameObject.Find("LightBall(Clone)");
		lightBall.name = name;
		lightBall.AddComponent("LightBallMove");
	}
	
	public static Vector3 GetCurrentEndPos(){
		return endPos;
	}
	
	public static float GetCurrentLifeTime(){
		return lifeTime;
	}
}
