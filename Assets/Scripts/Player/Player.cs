using UnityEngine;
using System.Collections;

public class Player{
	private string name;
	private int health;
	private int mana;
	private int maxMana;
	private GameObject player;
	
	public Player (string n, int x, int z){
		name = n;
		health = 100;
		mana = 100;
		maxMana = 100;
		GameObject playerPrefab = PrefabFactory.GetSuzePrefab();
		GameManager.InstantiateModel(playerPrefab, new Vector3(x,0,z));
		player = GameObject.Find("SusePrefab(Clone)");
		player.name = name;
		player.transform.Rotate(0,180,0);
	}
	
	public string GetName (){
		return name;
	}
	public void SetName (string n){
		name = n;
	}
	
	public int GetHealth (){
		return health;
	}
	public void SetHealth (int hp){
		health = hp;
	}
	
	public int GetMana (){
		return mana;
	}
	public void SetMana (int m){
		mana = m;
	}
	
	public int GetMaxMana(){
		return maxMana;
	}
	public void SetMaxMana(int mm){
		maxMana = mm;
	}
	
	public void DecHealth (int damage){
		health = health - damage;
		SetFaceTexture(TextureFactory.GetFace2Texture());
		player.animation.Play ("FallOver");
	}
	
	public void DecMana (int cost){
		mana = mana - cost;
	}
	
	public void IncMana (int regen){
		if (mana + regen <= maxMana){
			mana = mana + regen;
		}
		else {
			mana = maxMana;
		}
	}
	
	public void SetFaceTexture (Texture ft){
		GameObject pface = player.transform.Find("Face").gameObject;
		pface.renderer.material.mainTexture = ft;
	}
	
	public GameObject GetPlayerObj (){
		return player;
	}
	
	public Vector3 GetPlayerPos (){
		return player.transform.position;
	}
}
