using UnityEngine;
using System.Collections;

public class EnemyBlueJelly : Enemy {
	// Enemy 2: Blue Jelly
	
	public EnemyBlueJelly (string n, int x, int z, bool state, string cpTag, int cpPoints){
		ename = n;
		position = new Vector3 (x,0,z);
		maxHP = 20;
		curHP = 20;
		moveSpeed = 0.2f;
		armour = 0;
		damage = 1;
		bonus = 10;
		pastState = state;
		debuffList = new ArrayList ();
		
		// Enemy 2: Blue Jelly Initialization
		clist = new CheckpointList (cpTag, cpPoints);
		GameObject bluePrefab = (GameObject)Resources.Load("Enemy/Blue Jelly/BlueJellyPrefab",typeof(GameObject));
		GameManager.InstantiateModel(bluePrefab, new Vector3(x,0,z));
		cubeObject = GameObject.Find("BlueJellyPrefab(Clone)");
		cubeObject.name = ename;
		
		if (!pastState){
			GameObject cubeBody = cubeObject.transform.Find("Body").gameObject;
			cubeBody.renderer.material.color = Color.white;
			cubeBody.renderer.material.mainTexture = TextureFactory.GetJellyFutureTexture();
		}
		
		animHelper = Time.time;
	}
}
