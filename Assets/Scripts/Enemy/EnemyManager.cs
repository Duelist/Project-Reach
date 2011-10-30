using UnityEngine;
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

	float animationSpeed;
	
	float moveHelper;
	int move;
	
	// Enemy 1: Blue Jelly
	private Texture [] blueJellyTex;
	// Stores a list of enemy references
	private List<Enemy> enemy;
	//GameObject blueJellyCube;
	
	// Use this for initialization
	public EnemyManager (Map map) {		
		spawnTimer = 0.0f;
		spawnInterval = 5.0f;
		numEnemies = 10;
		enemiesOnDeck = 0;
		numWaves = 3;
		
		animationSpeed = 0.2f; // change texture once per (animationSpeed) second;
		
		// Enemy 1: Blue Jelly Initialization
		ArrayList imList = new ArrayList ();
		ArrayList tZone = new ArrayList ();
		tZone.Add("past");
		// Texture Settings
		int maxTex = 5;
		blueJellyTex = new Texture [maxTex];
		for (int i = 0; i < maxTex; i++){
			blueJellyTex[i] = Resources.Load ("Enemy/Blue Jelly/Blue Jelly "+i) as Texture;
		}
		
		enemy = new List<Enemy> ();
		// Initialization parameters:
		// string n, int x, int z, int hp, int ms, int arm, ArrayList imList, ArrayList tZone, Texture [] anim, int s, int maxT, ArrayList path
		int startX = 1;
		int startZ = 0;
		Debug.Log("Path 1");
		ArrayList path = AStar.Search (map.tiles[1,0], map.tiles[11,14], map, 1.0f);
		Debug.Log("Path 2");
		ArrayList path2 = AStar.Search (map.tiles[2,0], map.tiles[12,14], map, 1.0f);
		Debug.Log("Path 3");
		ArrayList path3 = AStar.Search (map.tiles[3,0], map.tiles[13,14], map, 1.0f);
		
		enemy.Add(new Enemy ("Blue Jelly 1", startX, startZ, 20, 1, 0, imList, tZone, blueJellyTex, 50, maxTex, path));
		enemy.Add(new Enemy ("Blue Jelly 2", startX+1, startZ, 20, 1, 0, imList, tZone, blueJellyTex, 50, maxTex, path2));
		enemy.Add(new Enemy ("Merupi", startX+2, startZ, 20, 1, 0, imList, tZone, blueJellyTex, 50, maxTex, path3));
	
		// Using gameobjects for now, gonna have to discuss wtf is going on here.
		/*blueJellyCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		blueJellyCube.transform.Rotate(0,0,180);
		blueJellyCube.transform.position = new Vector3(blueJelly.GetPositionX(), 0, blueJelly.GetPositionZ());
		blueJellyCube.transform.localScale = new Vector3(1f,0.01f,1f);*/
	}
	
	// Update Method
	public void DrawEnemy (Tower[] towerList) {
		MobMovement(towerList);
	}
	
	public void spawn (){
		/*if (Time.time > spawnTimer && enemiesOnDeck < numEnemies) {
			spawnTimer = Time.time + spawnInterval;
			//Instantiate(enemy,transform.position + offset,Quaternion.identity);
			enemiesOnDeck++;
		}*/
	}
	
	// Waypoint array is a list of Waypoints that contain the position of waypoints and the direction to move.
	private void MobMovement (Tower[] towerList){
		for (int i = 0; i < enemy.Count; i++){
			Enemy newEnemy = enemy[i];
			newEnemy.GetGameObject().renderer.material.mainTexture = newEnemy.GetAnimate(newEnemy.GetCurTex());
			if (newEnemy.GetAnimHelper() + animationSpeed < Time.time){
				newEnemy.IncCurTex();
				newEnemy.SetAnimHelper(Time.time);

				if (newEnemy.IsAnimating()){
					// Implimenting smooth movement
					Vector3 nextPosition = newEnemy.GetPathHead();
					float maxFrame = newEnemy.GetMaxTex();
					float curFrame = newEnemy.GetCurTex();
					float nextX = (float) nextPosition.x;
					float nextZ = (float) nextPosition.z;
					float curX = newEnemy.GetPositionX();
					float curZ = newEnemy.GetPositionZ();
					// If not at the end...
					if (nextX != -1 && nextZ != -1){
						// For smooth x
						float switcher = 1.0f;
						
						if (curX == nextX){
							switcher = 0.0f;
						}
						else if (curX > nextX){
							switcher = -1.0f;
						}// else switcher = 1;
						curX += ((curFrame+1)/maxFrame) * switcher;
						
						// For smooth z
						switcher = 1.0f;
						if (curZ == nextZ){
							switcher = 0.0f;
						}
						else if (curZ > nextZ){
							switcher = -1.0f;
						}// else switcher = 1;
						curZ += ((curFrame+1)/maxFrame) * switcher;

						newEnemy.GetGameObject().transform.position = new Vector3(curX, 0, curZ);
					}
				}
				//if (moveHelper + (1/blueJelly.GetMoveSpeed()) < Time.time){
				else{
					Vector3 newPosition = newEnemy.PopPath();
					int x = (int)newPosition.x;
					int z = (int)newPosition.z;
					if (x != -1 || z != -1){
						newEnemy.SetPosition(x, z);
						newEnemy.GetGameObject().transform.position = new Vector3(x, 0, z);
						MobDamage(towerList, newEnemy);
					}
					/*if (blueJelly.GetPositionX() == 6){
						move *= -1;
					}
					else if (blueJelly.GetPositionX() == 1){
						move *= -1;
					}*/
				}
				
			}
		}
	}
	
	// Zone detection
	private void MobDamage(Tower[] towerList, Enemy newEnemy){
		Vector3 enemyPos = newEnemy.GetPosition();
		for (int j = 0; j < towerList.Length; j++){
			if (towerList[j].GetActive()){
				Zone newZone = towerList[j].GetZone();
				Vector2 zonePos = newZone.GetPosition();
				if (zonePos.x - enemyPos.x <= 1 
					&& zonePos.x - enemyPos.x >= -1
					&& zonePos.y - enemyPos.z <= 1
					&& zonePos.y - enemyPos.z >= -1){
					
					Effect newEff = newZone.GetEffect();
					newEnemy.SetCurHP(newEnemy.GetCurHP() - newEff.GetDamage());
					Debug.Log(newEnemy.GetName() + " DAMAGED for " + newEff.GetDamage());
					if (newEnemy.GetCurHP() <= 0){
						Debug.Log(newEnemy.GetName() + " has been Destroyed!");
						enemy.Remove(newEnemy);
					}
				}
			}
		}
	}
}
