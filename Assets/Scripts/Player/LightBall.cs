using UnityEngine;
using System.Collections;

public class LightBall{
	GameObject lightBall;
	static Vector3 endPos;
	static float lifeTime;
	
	public LightBall (string name, Vector3 sPos, Transform eObj, float lTime){
		
		endPos = eObj.position;
		lifeTime = lTime;
		
		GameObject lightPrefab = PrefabFactory.GetLightBallPrefab();
		GameManager.InstantiateModel(lightPrefab, sPos);
		lightBall = GameObject.Find("LightBall(Clone)");
		lightBall.name = name;
		lightBall.AddComponent("LightBallMove");
		lightBall.transform.LookAt(eObj);
	}
	
	public LightBall (string name, Vector3 sPos, Transform eObj, float lTime, Color glowColor){
		
		endPos = eObj.position;
		lifeTime = lTime;
		string prefabName = "LightBall(Clone)";
		GameObject lightPrefab = PrefabFactory.GetLightBallPrefab();
		if (glowColor.Equals(Color.red)){
			lightPrefab = PrefabFactory.GetLightBallRedPrefab();
			prefabName = "LightBallRed(Clone)";
		}
		else if (glowColor.Equals (Color.blue)){
			lightPrefab = PrefabFactory.GetLightBallBluePrefab();
			prefabName = "LightBallBlue(Clone)";
		}
		else if (glowColor.Equals (Color.black)){
			lightPrefab = PrefabFactory.GetLightBallDarkPrefab();
			prefabName = "LightBallDark(Clone)";
		}
		GameManager.InstantiateModel(lightPrefab, sPos);
		lightBall = GameObject.Find(prefabName);
		lightBall.name = name;
		lightBall.AddComponent("LightBallMove");
		lightBall.transform.LookAt(eObj);
		
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
