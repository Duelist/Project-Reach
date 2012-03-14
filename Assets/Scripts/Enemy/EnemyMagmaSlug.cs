using UnityEngine;
using System.Collections;

public class EnemyMagmaSlug : Enemy {
	// Enemy 2: MagmaSlug
	private Texture [] pBlueJellyTex;
	private Texture [] fBlueJellyTex;
	
	public EnemyMagmaSlug (string n, int x, int z, bool state, string cpTag, int cpPoints){
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
		
		// Enemy 3: Magma Slug Initialization
		clist = new CheckpointList (cpTag, cpPoints);
		GameObject magmaPrefab = (GameObject)Resources.Load("Enemy/Magma Slug/Slug",typeof(GameObject));
		GameManager.InstantiateModel(magmaPrefab, new Vector3(x,0,z));
		cubeObject = GameObject.Find("Slug(Clone)");
		cubeObject.name = ename;
		
		animHelper = Time.time;
	}
}
