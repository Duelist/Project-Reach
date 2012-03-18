using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager{
	// wayPointArray object
	private ArrayList waypointArray;
	
	//Spawn timers and stuff
	float spawnTimer;
	float spawnInterval;
	int numEnemies;
	int enemiesOnDeck;
	int numWaves;
	int waveNum;
	Vector3 offset;

	//float animationSpeed;
	
	float moveHelper;
	int move;
	
	// Enemy 2: Merupi
	private Texture [] merupiTex;
	int maxTex2;
	
	// Stores a list of enemy references
	private List<Enemy> enemy;
	// Store a list of enemy paths
	ArrayList path;
	ArrayList path2;
	ArrayList path3;
	
	// Use this for initialization
	public EnemyManager (Map map) {		
		spawnTimer = 0.0f;
		spawnInterval = 5.0f;
		numEnemies = 10;
		enemiesOnDeck = 0;
		numWaves = 3;
		waveNum = 0;
		
		//animationSpeed = 0.2f; // change texture once per (animationSpeed) second;
		
		// Enemy 2: Merupi Initialization
		maxTex2 = 9;
		merupiTex = new Texture [maxTex2];
		for (int i = 0; i < maxTex2; i++){
			merupiTex[i] = Resources.Load ("Mascot/Merupi "+i) as Texture;
		}
		
                /*
		Debug.Log("Path 1");
		path = AStar.Search (map.tiles[1,0], map.tiles[11,14], map, 1.0f);
		Debug.Log("Path 2");
		path2 = AStar.Search (map.tiles[2,0], map.tiles[12,14], map, 1.0f);
		Debug.Log("Path 3");
		path3 = AStar.Search (map.tiles[3,0], map.tiles[13,14], map, 1.0f);
                */
		enemy = new List<Enemy> ();
		
		spawnTimer = Time.time;
	}
	
	// Update Method
	public void DrawEnemy (Hashtable towerList, Player player, int level) {
		Spawn ();
		MobMovement(towerList, player);
		MobDamage(towerList, player);
		LevelEndCheck (player);
	}
	
	public void Spawn (){
		if (spawnTimer <= Time.time){
			
			// Enemy Initialization parameters: string n, int x, int z, int hp, int ms, int arm, int dam, int bon. ArrayList imList, ArrayList tZone, Texture [] anim, int s, int maxT, string cpTag, int cpPoints		
			// Blue Jelly Init: string n, int x, int z, bool state, string cpTag, int cpPoints
			bool pastState = true;
			bool futureState = false;
			
			// Texture Settings
			int startX = 1;
			int startZ = 0;	
			
			// Level 0
			// Add if statement when we have more levels
			//if (GameManager.GetLevel() == 0){
				if (waveNum == 0){
					enemy.Add(new EnemyMagmaSlug ("Magma Slug 1", startX, startZ, futureState, "lp", 5));
					enemy.Add(new EnemyMagmaSlug ("Magma Slug 2", startX+1, startZ, pastState, "cp", 5));
					enemy.Add(new EnemyMagmaSlug ("Magma Slug 3", startX+2, startZ, futureState, "rp", 5));
					waveNum++;
				}
				else if (waveNum == 1){
					enemy.Add(new EnemyBlueJelly ("Blue Jelly 4", startX, startZ, pastState, "lp", 5));
					enemy.Add(new EnemyBlueJelly ("Blue Jelly 5", startX+1, startZ, futureState, "cp", 5));
					enemy.Add(new EnemyBlueJelly ("Blue Jelly 6", startX+2, startZ, pastState, "rp", 5));
					waveNum++;
				}
				else if (waveNum == 2){
					enemy.Add(new EnemyBlueJelly ("Blue Jelly 7", startX, startZ, pastState, "lp", 5));
					enemy.Add(new EnemyMerupi ("Merupi", startX+1, startZ, futureState, "cp", 5));
					enemy.Add(new EnemyBlueJelly ("Blue Jelly 9", startX+2, startZ, pastState, "rp", 5));
					waveNum++;
				}
			//}
			
			spawnTimer = Time.time + spawnInterval;
		}
	}
	
	// Waypoint array is a list of Waypoints that contain the position of waypoints and the direction to move.
	private void MobMovement (Hashtable towerList, Player player){
		float checkTime = 0.2f; // This is now the animation speed
		for (int i = 0; i < enemy.Count; i++){
			Enemy newEnemy = enemy[i];
			
			//newEnemy.GetGameObject().renderer.material.mainTexture = newEnemy.GetAnimate(newEnemy.GetCurTex());
			if (newEnemy.GetAnimHelper() + checkTime < Time.time){
				newEnemy.IncCurTex();
				newEnemy.SetAnimHelper(Time.time);
				
				float curX = newEnemy.GetPositionX();
				float curZ = newEnemy.GetPositionZ();
				
				GameObject checkpt = newEnemy.GetCpObj();
				if (checkpt != null){
					float cpX = checkpt.transform.position.x;
					float cpZ = checkpt.transform.position.z;
					newEnemy.AutoRotate(checkpt);
					
					if (curX > cpX){
						if (curX - newEnemy.GetMoveSpeed() < cpX){
							curX = cpX;
						}
						else {
							curX -= newEnemy.GetMoveSpeed();
						}						
					}
					else if (curX < cpX){
						if (curX + newEnemy.GetMoveSpeed() > cpX){
							curX = cpX;
						}
						else {
							curX += newEnemy.GetMoveSpeed();
						}
					}
					
					if (curZ > cpZ){
						if (curZ - newEnemy.GetMoveSpeed() < cpZ){
							curZ = cpZ;
						}
						else {
							curZ -= newEnemy.GetMoveSpeed();
						}						
					}
					else if (curZ < cpZ){
						if (curZ + newEnemy.GetMoveSpeed() > cpZ){
							curZ = cpZ;
						}
						else {
							curZ += newEnemy.GetMoveSpeed();
						}
					}
				
					newEnemy.SetPosition(curX, curZ);
					newEnemy.GetGameObject().transform.position = new Vector3(curX, 0, curZ);
					
					if (Math.Abs(curX - cpX) < newEnemy.GetMinDistance() && Math.Abs(curZ - cpZ) < newEnemy.GetMinDistance()){
						newEnemy.RemoveCp();
					}
				}
				else{ // Enemy Has reached the goal!
					PlayerDamage(player, newEnemy);
					newEnemy.Clean();
					enemy.Remove(newEnemy);
				}
			}
		}
	}
	
	// Zone detection
	private void MobDamage(Hashtable towerList, Player player){
		float atkSpeed = 0.2f;
		// Zone Dmg check
		foreach (Tower tower in towerList.Values){
			// Can be used for tower atk speed later
			bool areaAtk = false;
			if (tower.GetEffect().GetEffectType() == Effect.EffectType.Ice){
				areaAtk = true;
			}
			
			if (tower.GetAnimHelper() + atkSpeed < Time.time){
				tower.SetAnimHelper(Time.time);
				Zone newZone = tower.GetZone();
				Vector2 zonePos = newZone.GetPosition();
				for (int i = 0; i < enemy.Count; i++){
					Enemy newEnemy = enemy[i];
					Vector3 enemyPos = newEnemy.GetPosition();
					// if the enemy is within a zone
					if (zonePos.x - enemyPos.x <= 1 
						&& zonePos.x - enemyPos.x >= -1
						&& zonePos.y - enemyPos.z <= 1
						&& zonePos.y - enemyPos.z >= -1){
						
						// if the timezone is correct "past"/"future"
						if (newEnemy.GetPastState() == newZone.GetTime()){
							Effect newEff = newZone.GetEffect();
							newEnemy.SetCurHP((int)(newEnemy.GetCurHP() - newEff.GetDamage()));
							Debug.Log(newEnemy.GetName() + " DAMAGED for " + newEff.GetDamage());
							if (newEnemy.GetCurHP() <= 0){
								player.GetPlayerObj().animation.Play ("SmallHop");
								Debug.Log(newEnemy.GetName() + " has been Destroyed! You absorbed " + newEnemy.GetBonus() + " Mana!");
								player.IncMana(newEnemy.GetBonus());
								newEnemy.Clean();
								enemy.Remove(newEnemy);
							}
							if (!areaAtk){
								break;
							}
						}
					}
				}
			}
		}
	}
	
	private void PlayerDamage (Player player, Enemy newEnemy){
		player.DecHealth(newEnemy.GetDamage());
		Debug.Log(player.GetName() + " has been hit for " + newEnemy.GetDamage() + " damage");
	}
	
	private void LevelEndCheck (Player player){
		if (waveNum == numWaves && enemy.Count == 0){
			GameManager.SetGameState(0);
			waveNum = 0;
			GameManager.SetLevel(GameManager.GetLevel () + 1);
			GameManager.ShowSelectors();
			player.SetFaceTexture(TextureFactory.GetFaceTexture());
			player.GetPlayerObj().animation.Play ("FlipJump");
		}
	}
}
