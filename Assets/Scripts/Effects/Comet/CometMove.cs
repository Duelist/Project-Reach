using UnityEngine;
using System.Collections;

public class CometMove : MonoBehaviour {
	Vector3 startPos;
	Vector3 endPos;
	float lifeTime;
	float startTime;
	// Use this for initialization
	void Start () {
		startPos = this.transform.position;
		startPos.y += 0.1f;
		endPos = Comet.GetCurrentEndPos();
		endPos.y += 0.1f;
		lifeTime = Comet.GetCurrentLifeTime();
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float timeElapsed = Time.time - startTime;
		if (timeElapsed <= lifeTime){
			float x = ((endPos.x - startPos.x) / lifeTime) * timeElapsed;
			float y = ((endPos.y - startPos.y) / lifeTime) * timeElapsed;
			float z = ((endPos.z - startPos.z) / lifeTime) * timeElapsed;
			this.transform.position = new Vector3(startPos.x + x,startPos.y + y,startPos.z + z);
		}
		else{
			EnemyManager.DamageEnemyInTile(endPos, 10);
			GameManager.DestroyObject(this.gameObject);
		}
	}
}
