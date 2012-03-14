using UnityEngine;
using System.Collections;

public class Enemy{
	
	protected string ename;
	protected Vector3 position;
	protected int maxHP, curHP;
	protected float moveSpeed;
	protected int armour;
	protected int damage;
	protected int bonus;

	protected ArrayList debuffList;
	protected bool pastState;
	
	protected Texture[] animate;
	protected int eSize, maxTex, curTex;
	
	protected CheckpointList clist;
	
	protected float animHelper;
	
	// Object to be drawn on screen
	protected GameObject cubeObject;
	
	public Enemy (){
	}
	
	public Enemy (string n, float x, float z, int hp, float ms, int arm, int dam, int bon, bool tZone, Texture [] anim, int s, int maxT, string cpTag, int cpPoints){
		ename = n;
		position = new Vector3 (x,0,z);
		maxHP = hp;
		curHP = hp;
		moveSpeed = ms;
		
		if (arm > 100){
			armour = 100;
		}
		else {
			armour = arm;
		}
		
		damage = dam;
		bonus = bon;
		
		pastState = tZone;
		
		debuffList = new ArrayList ();

		animate = anim;
		if (animate == null){
			animate = new Texture [1];
		}
		eSize = s;
		maxTex = maxT;
		curTex = 0;
		
		//path = new ArrayList(pa);
		clist = new CheckpointList (cpTag, cpPoints);
		
		cubeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cubeObject.name = ename;
		cubeObject.transform.Rotate(0,0,180);
		cubeObject.transform.position = new Vector3(x, 0, z);
		cubeObject.transform.localScale = new Vector3(1f,1f,1f);
		cubeObject.tag = "enemy";
		
		animHelper = Time.time;
	}
	
	// Getters and Setters
	public string GetName(){
		return ename;
	}
	public void SetName(string n){
		ename = n;
	}
	public Vector3 GetPosition (){
		return position;
	}
	public float GetPositionX (){
		return (float) position.x;
	}
	public float GetPositionZ (){
		return (float) position.z;
	}
	public void SetPositionVec (Vector3 p){
		position = p;
	}
	public void SetPosition (float x, float z){
		position.x = x;
		position.z = z;
	}
	
	public int GetMaxHP (){
		return maxHP;
	}
	public void SetMaxHP(int mHP){
		maxHP = mHP;
	}
	
	public int GetCurHP (){
		return curHP;
	}
	public void SetCurHP (int cHP){
		curHP = cHP;
	}
	
	public float GetMoveSpeed (){
		return moveSpeed;
	}
	public void SetMoveSpeed (float ms){
		moveSpeed = ms;
	}
	
	public int GetArmour (){
		return armour;
	}
	public void SetArmour (int arm){
		if (arm > 100){
			armour = 100;
		}
		else {
			armour = arm;
		}
	}
	
	public int GetDamage(){
		return damage;
	}
	public void SetDamage(int dam){
		damage = dam;
	}
	public int GetBonus(){
		return bonus;
	}
	public void SetBonus(int bon){
		bonus = bon;
	}
	
	public void AutoRotate (GameObject cp){
		cubeObject.transform.LookAt(cp.transform);
	}
	
	// ArrayList getters and Modifiers
	
	// -- TimeZone -- //
	// Returns ArrayList <String>
	public bool GetPastState (){
		return pastState;
	}
	
	public void SetPastState (bool tzType){
		pastState = tzType;
	}
	
	// -- DebuffList -- //
	// Returns ArrayList <String>
	public ArrayList GetDebuffList (){
		return debuffList;
	}
	
	public bool ContainsDebuff (Effect.DebuffType dbType){
		if (debuffList.IndexOf(dbType) != -1){
			return true;
		}
		return false;
	}
	
	public bool AddDebuff (Effect.DebuffType dbType){
		if (ContainsDebuff(dbType)){
			return false;
		}
		debuffList.Add(dbType);
		return true;
	}
	
	public bool RemoveDebuff (Effect.DebuffType dbType){
		int temp = debuffList.Count;
		debuffList.Remove(dbType);
		if (debuffList.Count == temp){
			return false;
		}
		return true;
	}
	
	// Animation, eSize, MaxTex, and CurTex Getters/Setters
	public Texture[] GetAnimate(){
		return animate;
	}
	public Texture GetAnimate(int i){
		return animate[i];
	}
	public void SetAnimate(Texture [] anim){
		animate = anim;
	}
	
	public int GetESize(){
		return eSize;
	}
	public void SetESize(int s){
		eSize = s;
	}
	
	public int GetMaxTex(){
		return maxTex;
	}
	public void SetMaxTex(int maxT){
		maxTex = maxT;
	}
	
	public int GetCurTex (){
		return curTex;
	}
	public void SetCurTex (int curT){
		curTex = curT;
	}
	
	public GameObject GetGameObject(){
		return cubeObject;
	}
	public void SetGameObject (GameObject go){
		cubeObject = go;
	}
	
	public float GetAnimHelper(){
		return animHelper;
	}
	public void SetAnimHelper(float animH){
		animHelper = animH;
	}
	
	// Increment Current Texture by 1
	public void IncCurTex(){
		if (curTex == maxTex-1){
			curTex = 0;
		}
		else {
			curTex++;
		}
	}
	
	// Pop a vector in the current Path and return the vector popped
	public Vector3 PopPath() {
                /*
		if (path != null && path.Count > 0){
			Vector3 head = ((Tile)path[0]).tileObject.transform.position;
			path.RemoveAt(0);
			return head;
		}
                */
		return new Vector3 (-1,-1,-1);
	}
	
	// Grab the head of the current Path
	public Vector3 GetPathHead(){
                /*
		if (path != null && path.Count > 0){
			Vector3 head = ((Tile)path[0]).tileObject.transform.position;
			return head;
		}
                */
		return new Vector3 (-1, -1, -1);
	}
	
	// Check whether the next frame is animating or a Position Frame
	public bool IsAnimating(){
		return !(curTex == maxTex-1);
	}
	
	public GameObject GetCpObj(){
		return clist.GetCpHead();
	}
	
	public void RemoveCp(){
		clist.RemoveHead();
	}
	
	public float GetMinDistance() {
		return clist.GetMinDistance();
	}
	
	public void Clean (){
		GameManager.Destroy(cubeObject);
	}
}