using UnityEngine;
using System.Collections;

public class Comet {
	GameObject cometObj;
	static Vector3 endPos;
	static float lifeTime;
	
	public Comet (string name, Vector3 sPos, Transform eObj, float lTime){
		
		endPos = eObj.position;
		endPos.y -= 0.5f;
		lifeTime = lTime;
		
		GameObject cometPrefab = PrefabFactory.GetCometPrefab();
		GameManager.InstantiateModel(cometPrefab, sPos);
		cometObj = GameObject.Find("MerupiCometPrefab(Clone)");
		cometObj.name = name;
		cometObj.AddComponent("CometMove");
		cometObj.transform.LookAt(eObj);
	}
	
	public static Vector3 GetCurrentEndPos(){
		return endPos;
	}
	
	public static float GetCurrentLifeTime(){
		return lifeTime;
	}
}
