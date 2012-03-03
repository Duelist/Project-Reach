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
	
	// Enemy 1: Blue Jelly
	private Texture [] pBlueJellyTex;
	private Texture [] fBlueJellyTex;
	int maxTex;
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
		
		// Enemy 1: Blue Jelly Initialization
		maxTex = 5;
		pBlueJellyTex = new Texture [maxTex];
		for (int i = 0; i < maxTex; i++){
			pBlueJellyTex[i] = Resources.Load ("Enemy/Blue Jelly/Past Blue Jelly "+i) as Texture;
		}

		maxTex = 5;
		fBlueJellyTex = new Texture [maxTex];
		for (int i = 0; i < maxTex; i++){
			fBlueJellyTex[i] = Resources.Load ("Enemy/Blue Jelly/Future Blue Jelly "+i) as Texture;
		}
		
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
	public void DrawEnemy (Hashtable towerList, Player player) {
		Spawn ();
		MobMovement(towerList, player);
		LevelEndCheck ();
	}
	
	public void Spawn (){
		if (spawnTimer <= Time.time){
			
			// Initialization parameters:
			// string n, int x, int z, int hp, int ms, int arm, int dam. ArrayList imList, ArrayList tZone, Texture [] anim, int s, int maxT, ArrayList path
			bool pastState = true;
			bool futureState = false;
			
			// Texture Settings
			int startX = 1;
			int startZ = 0;
			
			
			// Level 1
			if (waveNum == 0){
				enemy.Add(new Enemy ("Blue Jelly 1", startX, startZ, 20, 0.2f, 0, 1, pastState, pBlueJellyTex, 50, maxTex, "lp", 5));
				enemy.Add(new Enemy ("Blue Jelly 2", startX+1, startZ, 20, 0.2f, 0, 1, pastState, pBlueJellyTex, 50, maxTex, "cp", 5));
				enemy.Add(new Enemy ("Merupi", startX+2, startZ, 50, 0.5f, 0, 5, futureState, merupiTex, 50, maxTex2, "rp", 5));
				waveNum++;
			}
			else if (waveNum == 1){
				enemy.Add(new Enemy ("Blue Jelly 4", startX, startZ, 20, 0.2f, 0, 1, futureState, fBlueJellyTex, 50, maxTex, "lp", 5));
				enemy.Add(new Enemy ("Blue Jelly 5", startX+1, startZ, 20, 0.2f, 0, 1, futureState, fBlueJellyTex, 50, maxTex, "cp", 5));
				enemy.Add(new Enemy ("Blue Jelly 6", startX+2, startZ, 20, 0.2f, 0, 1, futureState, fBlueJellyTex, 50, maxTex, "rp", 5));
				waveNum++;
			}
			else if (waveNum == 2){
				enemy.Add(new Enemy ("Blue Jelly 7", startX, startZ, 20, 0.2f, 0, 1, futureState, fBlueJellyTex, 50, maxTex, "lp", 5));
				enemy.Add(new Enemy ("Blue Jelly 8", startX+1, startZ, 20, 0.2f, 0, 1, pastState, pBlueJellyTex, 50, maxTex, "cp", 5));
				enemy.Add(new Enemy ("Blue Jelly 9", startX+2, startZ, 20, 0.2f, 0, 1, futureState, fBlueJellyTex, 50, maxTex, "rp", 5));
				waveNum++;
			}
			
			spawnTimer = Time.time + spawnInterval;
		}
	}
	
	// Waypoint array is a list of Waypoints that contain the position of waypoints and the direction to move.
	private void MobMovement (Hashtable towerList, Player player){
		for (int i = 0; i < enemy.Count; i++){
			Enemy newEnemy = enemy[i];
			
			newEnemy.GetGameObject().renderer.material.mainTexture = newEnemy.GetAnimate(newEnemy.GetCurTex());
			//if (newEnemy.GetAnimHelper() + animationSpeed < Time.time){ no longer animation speed
			//float checkTime = (float)newEnemy.GetMoveSpeed() / (float)newEnemy.GetMaxTex();
			float checkTime = 0.2f; // This is now the animation speed
			if (newEnemy.GetAnimHelper() + checkTime < Time.time){
				// Zone Dmg check
				foreach (Tower tower in towerList.Values){
					// Can be used for tower atk speed later
					float atkSpeed = 0.2f;
					//Debug.Log(newEnemy.GetName());
					//if (tower.GetAnimHelper() + atkSpeed < Time.time){
					//	tower.SetAnimHelper(Time.time);
						MobDamage(tower, newEnemy);
					//}
				}
				
				newEnemy.IncCurTex();
				newEnemy.SetAnimHelper(Time.time);
				
				float curX = newEnemy.GetPositionX();
				float curZ = newEnemy.GetPositionZ();
				
				GameObject checkpt = newEnemy.GetCpObj();
				if (checkpt != null){
					float cpX = checkpt.transform.position.x;
					float cpZ = checkpt.transform.position.z;
					
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
	private void MobDamage(Tower tower, Enemy newEnemy){
		Vector3 enemyPos = newEnemy.GetPosition();
		Zone newZone = tower.GetZone();
		Vector2 zonePos = newZone.GetPosition();
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
					Debug.Log(newEnemy.GetName() + " has been Destroyed!");
					newEnemy.Clean();
					enemy.Remove(newEnemy);
				}
			}
		}
	}
	
	private void PlayerDamage (Player player, Enemy newEnemy){
		player.DecHealth(newEnemy.GetDamage());
		Debug.Log(player.GetName() + " has been hit for " + newEnemy.GetDamage() + " damage");
	}
	
	private void LevelEndCheck (){
		if (waveNum == numWaves && enemy.Count == 0){
			GameManager.SetGameState(0);
			waveNum = 0;
		}
	}
}
