using UnityEngine;
using System.Collections;

public class Player{
	private string name;
	private int health;
	private int mana;
	private int maxMana;
	
	public Player (string n){
		name = n;
		health = 100;
		mana = 100;
		maxMana = 100;
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
	}
	
	public void DecMana (int cost){
		mana = mana - cost;
	}
	
	public void IncMana (int regen){
		if (mana + regen < maxMana){
			mana = mana + regen;
		}
	}
}
