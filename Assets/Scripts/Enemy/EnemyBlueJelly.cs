using UnityEngine;
using System.Collections;

public class EnemyBlueJelly : Enemy {
	// Enemy 1: Blue Jelly
	private Texture [] pBlueJellyTex;
	private Texture [] fBlueJellyTex;
	
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
		
		// Enemy 1: Blue Jelly Initialization
		maxTex = 5;
		pBlueJellyTex = new Texture [maxTex];
		for (int i = 0; i < maxTex; i++){
			pBlueJellyTex[i] = Resources.Load ("Enemy/Blue Jelly/Past Blue Jelly "+i) as Texture;
		}
		
		fBlueJellyTex = new Texture [maxTex];
		for (int i = 0; i < maxTex; i++){
			fBlueJellyTex[i] = Resources.Load ("Enemy/Blue Jelly/Future Blue Jelly "+i) as Texture;
		}
		
		if (pastState){
			animate = pBlueJellyTex;
		} else {
			animate = fBlueJellyTex;
		}
		
		eSize = 50;
		curTex = 0;
		
		//path = new ArrayList(pa);
		clist = new CheckpointList (cpTag, cpPoints);
		
		cubeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cubeObject.name = ename;
		cubeObject.transform.Rotate(0,0,180);
		cubeObject.transform.position = new Vector3(x, 0, z);
		cubeObject.transform.localScale = new Vector3(1f,1f,1f);
		
		animHelper = Time.time;
	}
}
