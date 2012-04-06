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
	Vector3 offset;

	//float animationSpeed;
	
	float moveHelper;
	int move;

	// Store a list of enemy paths
	ArrayList path;
	ArrayList path2;
	ArrayList path3;
	
	// Use this for initialization
	public EnemyManager() {		
		spawnTimer = 0.0f;
		spawnInterval = 5.0f;
		numEnemies = 10;
		enemiesOnDeck = 0;
		spawnTimer = Time.time;
	}
	
	// Update Method
	public void DrawEnemy() {
		Spawn();
		MobMovement(GameStorage.towerList, GameStorage.player);
		MobDamage(GameStorage.towerList, GameStorage.player);
	}
	
	public void Spawn(){
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
				if (GameStorage.currentWave == 0){
					GameStorage.enemies.Add(new EnemyMagmaSlug ("Magma Slug 1", startX, startZ, futureState, "lp", 5));
					GameStorage.enemies.Add(new EnemyMagmaSlug ("Magma Slug 2", startX+1, startZ, pastState, "cp", 5));
					GameStorage.enemies.Add(new EnemyMagmaSlug ("Magma Slug 3", startX+2, startZ, futureState, "rp", 5));
					GameStorage.currentWave++;
				}
				else if (GameStorage.currentWave == 1){
					GameStorage.enemies.Add(new EnemyBlueJelly ("Blue Jelly 4", startX, startZ, pastState, "lp", 5));
					GameStorage.enemies.Add(new EnemyBlueJelly ("Blue Jelly 5", startX+1, startZ, futureState, "cp", 5));
					GameStorage.enemies.Add(new EnemyBlueJelly ("Blue Jelly 6", startX+2, startZ, pastState, "rp", 5));
					GameStorage.currentWave++;
				}
				else if (GameStorage.currentWave == 2){
					GameStorage.enemies.Add(new EnemyBlueJelly ("Blue Jelly 7", startX, startZ, pastState, "lp", 5));
					GameStorage.enemies.Add(new EnemyMerupi ("Merupi", startX+1, startZ, futureState, "cp", 5));
					GameStorage.enemies.Add(new EnemyBlueJelly ("Blue Jelly 9", startX+2, startZ, pastState, "rp", 5));
					GameStorage.currentWave++;
				}
			//}
			
			spawnTimer = Time.time + spawnInterval;
		}
	}
	
	// Waypoint array is a list of Waypoints that contain the position of waypoints and the direction to move.
	private void MobMovement (Hashtable towerList, Player player){
		float checkTime = 0.2f; // This is now the animation speed
		for (int i = 0; i < GameStorage.enemies.Count; i++){
			Enemy newEnemy = GameStorage.enemies[i];
			
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
					GameStorage.enemies.Remove(newEnemy);
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
				bool broken = false;
				for (int j = 0; !broken && j < newZone.getZoneWidth(); j++){
					for (int i = 0; i < GameStorage.enemies.Count; i++){
						Enemy newEnemy = GameStorage.enemies[i];
						Vector3 enemyPos = newEnemy.GetPosition();
						
						// if the enemy is within a zone
						if (EnemyIsInZone(zonePos, enemyPos, tower.GetEnemyEntry(), newZone.getZoneWidth(), j)){
							// if the timezone is correct "past"/"future"
							if (newEnemy.GetPastState() == newZone.GetTime()){
								Effect newEff = newZone.GetEffect();
								newEnemy.SetCurHP((int)(newEnemy.GetCurHP() - newEff.GetDamage()));
								Debug.Log(newEnemy.GetName() + " DAMAGED for " + newEff.GetDamage());
								if (newEnemy.GetCurHP() <= 0){
									new LightBall ("EnemyManaBall", newEnemy.GetGameObject().transform.position, player.GetPlayerObj().transform, 2);
									player.GetPlayerObj().animation.Play ("SmallHop");
									Debug.Log(newEnemy.GetName() + " has been Destroyed! You absorbed " + newEnemy.GetBonus() + " Mana!");
									player.IncMana(newEnemy.GetBonus());
									newEnemy.Clean();
									GameStorage.enemies.Remove(newEnemy);
								}
								if (!areaAtk){
									broken = true;
									break;
								}
							}
						}
					}
				}
			}
		}
	}
	
	private bool EnemyIsInZone (Vector2 zonePos, Vector3 enemyPos, int enemyEntry, int zoneWidth, int iter){
		bool enemyInZone = false;
		if (enemyEntry == 2){
			if (zonePos.x - enemyPos.x <= ((int)(zoneWidth/2)+0.5)
					&& zonePos.x - enemyPos.x >= -((int)(zoneWidth/2)+0.5)
					&& zonePos.y - enemyPos.z <= iter-0.5
					&& zonePos.y - enemyPos.z >= iter-1.5){
				enemyInZone = true;
			}
		}
		else if (enemyEntry == 4){
			if (zonePos.x - enemyPos.x <= iter-0.5
					&& zonePos.x - enemyPos.x >= iter-1.5
					&& zonePos.y - enemyPos.z <= ((int)(zoneWidth/2)+0.5)
					&& zonePos.y - enemyPos.z >= -((int)(zoneWidth/2)+0.5)){
				enemyInZone = true;
			}
		}
		else if (enemyEntry == 6){
			if (zonePos.x - enemyPos.x <= ((int)(zoneWidth/2)+0.5) - iter 
					&& zonePos.x - enemyPos.x >= ((int)(zoneWidth/2)+0.5) - (iter+1)
					&& zonePos.y - enemyPos.z <= ((int)(zoneWidth/2)+0.5)
					&& zonePos.y - enemyPos.z >= -((int)(zoneWidth/2)+0.5)){
				enemyInZone = true;
			}
		}
		else if (enemyEntry == 8){
			if (zonePos.x - enemyPos.x <= ((int)(zoneWidth/2)+0.5)
					&& zonePos.x - enemyPos.x >= -((int)(zoneWidth/2)+0.5)
					&& zonePos.y - enemyPos.z <= ((int)(zoneWidth/2)+0.5) - iter
					&& zonePos.y - enemyPos.z >= ((int)(zoneWidth/2)+0.5) - (iter+1)){
				enemyInZone = true;
			}
		}
		return enemyInZone;
	}
	
	private void PlayerDamage (Player player, Enemy newEnemy){
		player.DecHealth(newEnemy.GetDamage());
		Debug.Log(player.GetName() + " has been hit for " + newEnemy.GetDamage() + " damage");
		if (player.IsDead()){
			GameStorage.gameState = GameStorage.GameState.Stopped; // Game stopped
			player.GetPlayerObj().animation.Play ("Dead");
		}
	}
	
	// Comes with the comet commit
	public static void DamageEnemyInTile (Vector3 tilePos, int damage){
		for (int i = 0; i < GameStorage.enemies.Count; i++){
			Enemy e = GameStorage.enemies[i];
			if (tilePos.x - e.GetPositionX() < 0.5
				&& tilePos.x - e.GetPositionX() >= -0.5
				&& tilePos.z - e.GetPositionZ() < 0.5
				&& tilePos.z - e.GetPositionZ() >= -0.5){
				e.SetCurHP((int)(e.GetCurHP() - damage));
				Debug.Log(e.GetName() + " DAMAGED for " + damage);
				if (e.GetCurHP() <= 0){
					new LightBall ("EnemyManaBall", e.GetGameObject().transform.position, GameStorage.player.GetPlayerObj().transform, 2);
					GameStorage.player.GetPlayerObj().animation.Play ("SmallHop");
					Debug.Log(e.GetName() + " has been Destroyed! You absorbed " + e.GetBonus() + " Mana!");
					GameStorage.player.IncMana(e.GetBonus());
					e.Clean();
					GameStorage.enemies.Remove(e);
				}
			}
		}
	}
}
