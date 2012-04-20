using UnityEngine;
using System.Collections;

public class ExplosionMove : MonoBehaviour {
	Vector3 startPos;
	Vector3 endPos;
	float lifeTime;
	float startTime;
	// Use this for initialization
	void Start () {
		startPos = this.transform.position;
		endPos = startPos;
		lifeTime = Explosion.GetCurrentLifeTime();
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float timeElapsed = Time.time - startTime;
		if (timeElapsed <= lifeTime){
		}
		else{
			GameManager.DestroyObject(this.gameObject);
		}
	}
}
