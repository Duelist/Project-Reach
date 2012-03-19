using UnityEngine;
using System.Collections;

public class LightBallMove : MonoBehaviour {
	Vector3 startPos;
	Vector3 endPos;
	float lifeTime;
	float startTime;
	// Use this for initialization
	void Start () {
		startPos = this.transform.position;
		startPos.y += 0.1f;
		endPos = LightBall.GetCurrentEndPos();
		endPos.y += 0.1f;
		lifeTime = LightBall.GetCurrentLifeTime();
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
			GameManager.DestroyObject(this.gameObject);
		}
	}
}
