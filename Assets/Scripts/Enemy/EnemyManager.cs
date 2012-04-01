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
				for (int i = 0; i < GameStorage.enemies.Count; i++){
					Enemy newEnemy = GameStorage.enemies[i];
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
								new LightBall ("EnemyManaBall", newEnemy.GetGameObject().transform.position, player.GetPlayerObj().transform, 2);
								player.GetPlayerObj().animation.Play ("SmallHop");
								Debug.Log(newEnemy.GetName() + " has been Destroyed! You absorbed " + newEnemy.GetBonus() + " Mana!");
								player.IncMana(newEnemy.GetBonus());
								newEnemy.Clean();
								GameStorage.enemies.Remove(newEnemy);
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
		if (player.IsDead()){
			GameStorage.gameState = GameStorage.GameState.Stopped; // Game stopped
			player.GetPlayerObj().animation.Play ("Dead");
		}
	}
	
	private void LevelEndCheck (Player player){
		if (GameStorage.currentWave == GameStorage.waveTotal && GameStorage.enemies.Count == 0){
			GameStorage.gameState = 0;
			GameStorage.currentWave = 0;
			GameStorage.level = GameStorage.level + 1;
			GameManager.ShowSelectors();
			player.SetFaceTexture(TextureFactory.GetFaceTexture());
			player.GetPlayerObj().animation.Play ("FlipJump");
		}
	}
}
