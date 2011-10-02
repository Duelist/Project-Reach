using UnityEngine;
using System.Collections;

public class Enemy{
	
	private string ename;
	private Vector3 position;
	private int maxHP, curHP;
	private int moveSpeed;
	private int armour;
	// Typing:
	// immunityList : ArrayList <String>
	// timeZone : ArrayList <String>
	// debuffList : ArrayList <String>
	private ArrayList immunityList;
	private ArrayList timeZoneList;
	private ArrayList debuffList;
	
	private Texture [] animate;
	private int eSize, maxTex, curTex;
	
	private ArrayList path;
	
	public Enemy (string n, int x, int z, int hp, int ms, int arm, ArrayList imList, ArrayList tZone, Texture [] anim, int s, int maxT, ArrayList pa){
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
		
		immunityList = new ArrayList ();
		if (imList != null){
			immunityList = imList;
		}
		
		timeZoneList = new ArrayList ();
		if (tZone != null){
			timeZoneList = tZone;
		}
		
		debuffList = new ArrayList ();

		animate = anim;
		if (animate == null){
			animate = new Texture [1];
		}
		eSize = s;
		maxTex = maxT;
		curTex = 0;
		
		path = pa;
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
	public int GetPositionX (){
		return (int) position.x;
	}
	public int GetPositionZ (){
		return (int) position.z;
	}
	public void SetPositionVec (Vector3 p){
		position = p;
	}
	public void SetPosition (int x, int z){
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
	
	public int GetMoveSpeed (){
		return moveSpeed;
	}
	public void SetMoveSpeed (int ms){
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
	
	// ArrayList getters and Modifiers
	// -- ImmunityList -- //
	// Returns ArrayList <String>
	public ArrayList GetImmunityList (){
		return immunityList;
	}
	// Returns true if the immunity exists in the list, false ow
	public bool ContainsImmunity (string imType){
		if (immunityList.IndexOf(imType) != -1){
			return true;
		}
		return false;
	}
	// Returns true if an Immunity is added, false ow
	public bool AddImmunity (string imType){
		if (ContainsImmunity(imType)){
			return false;
		}
		immunityList.Add(imType);
		return true;
	}
	// Returns true if an Immunity was removed, false ow
	public bool RemoveImmunity (string imType){
		int temp = immunityList.Count;
		immunityList.Remove(imType);
		if (immunityList.Count == temp){
			return false;
		}
		return true;
	}
	
	// -- TimeZone -- //
	// Returns ArrayList <String>
	public ArrayList GetTimeZoneList (){
		return timeZoneList;
	}
	
	public bool ContainsTimeZone (string tzType){
		if (timeZoneList.IndexOf(tzType) != -1){
			return true;
		}
		return false;
	}
	
	public bool AddTimeZone (string tzType){
		if (ContainsTimeZone(tzType)){
			return false;
		}
		timeZoneList.Add(tzType);
		return true;
	}
	
	public bool RemoveTimeZone (string tzType){
		int temp = timeZoneList.Count;
		timeZoneList.Remove(tzType);
		if (timeZoneList.Count == temp){
			return false;
		}
		return true;
	}
	
	// -- DebuffList -- //
	// Returns ArrayList <String>
	public ArrayList GetDebuffList (){
		return debuffList;
	}
	
	public bool ContainsDebuff (string dbType){
		if (debuffList.IndexOf(dbType) != -1){
			return true;
		}
		return false;
	}
	
	public bool AddDebuff (string dbType){
		if (ContainsDebuff(dbType)){
			return false;
		}
		debuffList.Add(dbType);
		return true;
	}
	
	public bool RemoveDebuff (string dbType){
		int temp = debuffList.Count;
		debuffList.Remove(dbType);
		if (debuffList.Count == temp){
			return false;
		}
		return true;
	}
	
	// Animation, eSize, MaxTex, and CurTex get/set
	public Texture [] GetAnimate(){
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
	
	public void setCurTex (int curT){
		curTex = curT;
	}
	
	public void IncCurTex(){
		curTex++;
		if (curTex == maxTex){
			curTex = 0;
		}
	}
	
	public Vector3 PopPath() {
		if (path != null && path.Count > 0){
			Vector3 head = ((Tile)path[0]).tileObject.transform.position;
			path.RemoveAt(0);
			return head;
		}
		return new Vector3 (-1,-1,-1);
	}
}