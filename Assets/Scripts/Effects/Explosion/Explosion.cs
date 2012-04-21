using UnityEngine;
using System.Collections;

public class Explosion {
	GameObject explosionObj;
	static Vector3 endPos;
	static float lifeTime;
	
	public Explosion (string name, Vector3 sPos, Transform eObj, float lTime){
		
		sPos.y += 1.5f;
		endPos = sPos;
		lifeTime = lTime;
		
		GameObject exploPrefab = PrefabFactory.GetExplosionPrefab();
		GameManager.InstantiateModel(exploPrefab, sPos);
		explosionObj = GameObject.Find("ExplosionPrefab(Clone)");
		explosionObj.name = name;
		explosionObj.AddComponent("ExplosionMove");
		explosionObj.transform.Rotate (new Vector3(-90,0,0));
	}
	
	public static Vector3 GetCurrentEndPos(){
		return endPos;
	}
	
	public static float GetCurrentLifeTime(){
		return lifeTime;
	}
}