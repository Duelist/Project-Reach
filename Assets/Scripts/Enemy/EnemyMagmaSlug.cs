using UnityEngine;
using System.Collections;

public class EnemyMagmaSlug : Enemy {
	// Enemy 3: MagmaSlug
	
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
		GameObject magmaPrefab = PrefabFactory.GetMagmaSlugPrefab();
		GameManager.InstantiateModel(magmaPrefab, new Vector3(x,0,z));
		cubeObject = GameObject.Find("Slug(Clone)");
		cubeObject.name = ename;
		
		if (!pastState){
			GameObject cubeBody = cubeObject.transform.Find("Body").gameObject;
			cubeBody.renderer.material.color = Color.white;
			cubeBody.renderer.material.mainTexture = TextureFactory.GetMagmaFutureTexture();
		}
		
		animHelper = Time.time;
	}
}
