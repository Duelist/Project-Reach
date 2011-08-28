using UnityEngine;
using System.Collections;

public class EnemyManager{
	// Stores a list of enemy references
	private ArrayList enemyArray;
	// wayPointArray object
	private ArrayList waypointArray;
	
	//Spawn timers and stuff
	float spawnTimer;
	float spawnInterval;
	int numEnemies;
	int enemiesOnDeck;
	int numWaves;
	Vector3 offset;
	
	GameObject enemy;
	private enum GameState {Building = 1, Playing, Paused, Stopped};
	private GameState gameState;
	
	// Enables everything in update when this variable is set to true
	private bool sleep;
	
	// Use this for initialization
	public EnemyManager () {
		sleep = true;
		enemyArray = new ArrayList ();
		
		spawnTimer = 0.0f;
		spawnInterval = 5.0f;
		numEnemies = 10;
		enemiesOnDeck = 0;
		numWaves = 3;
		
		// For Testing
		gameState = GameState.Building;
	}
	
	// Update is called once per frame
	public void DrawEnemy () {
		if (!(sleep)){
			mobMovement();
		}
	}
	
	public void Sleep(){
		sleep = true;
	}

	public void Awake(){
		sleep = false;
	}
	
	public int getGameState (){
		return (int) gameState;
	}
	
	public void spawn (){
		if (Time.time > spawnTimer && enemiesOnDeck < numEnemies) {
			spawnTimer = Time.time + spawnInterval;
			//Instantiate(enemy,transform.position + offset,Quaternion.identity);
			enemiesOnDeck++;
		}
	}
	
	// Waypoint array is a list of Waypoints that contain the position of waypoints and the direction to move.
	public void mobMovement (){
		// each enemy's position is compared to their next waypoint's position.
		// if they are at the last way point, player loses life.
		// if they are at the waypoint, increase the waypoint counter array at i.
		// if they are not at the waypoint, move towards the waypoint at its speed.
		/*for (var i = 0; i < waypointCounterArray.length; i++){
			// i know ian said the waypoints are an area, but this will have to do for now...
			// TODO: will have to write a comparison operation
			if (enemyArray[i].getPosition() == waypointArray[waypointCounterArray[i]].getPosition())
			if (true){
				//waypointCounterArray[i] = waypointCounterArray[i] + 1;
				if (waypointCounterArray[i] >= waypointArray.length){
					playerDamage();
				}
			}
			else {
				var xAdd = 0;
				var yAdd = 0;
				/*if (waypointArray[waypointCounterArray[i]].getDirection().equals("up")){
					yAdd = -(enemyArray[i].getMoveSpeed());
				}
				else if (waypointArray[waypointCounterArray[i]].getDirection().equals("down")){
					yAdd = enemyArray[i].getMoveSpeed();
				}
				else if (waypointArray[waypointCounterArray[i]].getDirection().equals("left")){
					xAdd = -(enemyArray[i].getMoveSpeed());
				}
				else if (waypointArray[waypointCounterArray[i]].getDirection().equals("right")){
					xAdd = enemyArray[i].getMoveSpeed();
				}
				else{
				}
				enemyArray[i].setPosition(enemyArray[i].getPositionX + xAdd, enemyArray[i].getPositionY + yAdd);
				
			}
		}*/
	}
	
	public void playerDamage(){
	}
}
