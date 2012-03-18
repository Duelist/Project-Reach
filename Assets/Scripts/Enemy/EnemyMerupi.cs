using UnityEngine;
using System.Collections;

public class EnemyMerupi : Enemy {
	// Enemy 1: Merupi
	
	public EnemyMerupi (string n, int x, int z, bool state, string cpTag, int cpPoints){
		ename = n;
		position = new Vector3 (x,0,z);
		maxHP = 50;
		curHP = 50;
		moveSpeed = 0.5f;
		armour = 0;
		damage = 5;
		bonus = 50;
		pastState = state;
		debuffList = new ArrayList ();
		
		// Original: enemy.Add(new EnemyMerupi ("Merupi", startX+1, startZ, 50, 0.5f, 0, 5, 50, futureState, merupiTex, 50, maxTex2, "cp", 5));
		// Enemy 1: Merupi Initialization
		clist = new CheckpointList (cpTag, cpPoints);
		GameObject merupiPrefab = (GameObject)Resources.Load("Mascot/MerupiPrefab",typeof(GameObject));
		GameManager.InstantiateModel(merupiPrefab, new Vector3(x,0,z));
		cubeObject = GameObject.Find("MerupiPrefab(Clone)");
		cubeObject.name = ename;
		
		animHelper = Time.time;
	}
}