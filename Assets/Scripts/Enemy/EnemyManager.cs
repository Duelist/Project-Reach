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
	private enum GameState {Building = 1, Playing = 2, Paused, Stopped};
	private GameState gameState;
	
	float animHelper;
	float animationSpeed;
	
	float moveHelper;
	int move;
	
	// Enemy 1: Blue Jelly
	private Texture [] blueJellyTex;
	private Enemy blueJelly;
	GameObject blueJellyCube;
	
	// Use this for initialization
	public EnemyManager (Map map) {
		enemyArray = new ArrayList ();
		
		spawnTimer = 0.0f;
		spawnInterval = 5.0f;
		numEnemies = 10;
		enemiesOnDeck = 0;
		numWaves = 3;
		
		animHelper = Time.time;
		animationSpeed = 0.20f; // change texture once per (animationSpeed) second;
		
		moveHelper = Time.time;
		move = 1;
		
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
		
		// Initialization parameters:
		// string n, int x, int z, int hp, int ms, int arm, ArrayList imList, ArrayList tZone, Texture [] anim, int s, int maxT
		int startX = 1;
		int startZ = 0;
		ArrayList path = AStar.Search (map.tiles[startX + (startZ*(int)map.mapSize.x)], map.tiles[98], map, 1.0f);
		blueJelly = new Enemy ("Blue Jelly", startX, startZ, 10, 1, 0, imList, tZone, blueJellyTex, 50, maxTex, path);
		// Using gameobjects for now, gonna have to discuss wtf is going on here.
		blueJellyCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		blueJellyCube.transform.Rotate(0,0,180);
		blueJellyCube.transform.position = new Vector3(blueJelly.GetPositionX(), 0, blueJelly.GetPositionZ());
		blueJellyCube.transform.localScale = new Vector3(1f,0.01f,1f);
		
		
		// For Testing
		//gameState = GameState.Building;
		gameState = GameState.Playing;
		// gameState = GameState.Paused;
		// gameState = GameState.Stopped;
	}
	
	// Update is called once per frame
	public void DrawEnemy () {
		if (gameState == GameState.Playing){
			mobMovement();
		}
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
		blueJellyCube.renderer.material.mainTexture = blueJelly.GetAnimate(blueJelly.GetCurTex());
		blueJellyCube.transform.position = new Vector3(blueJelly.GetPositionX(), 0, blueJelly.GetPositionZ());
		if (animHelper + animationSpeed < Time.time){
			blueJelly.IncCurTex();
			animHelper = Time.time;
		}
		if (moveHelper + (1/blueJelly.GetMoveSpeed()) < Time.time){
			Vector3 newPosition = blueJelly.PopPath();
			int x = (int)newPosition.x;
			int z = (int)newPosition.z;
			if (x != -1 && z != -1){
				blueJelly.SetPosition(x, z);
			}
			else{
			}
			moveHelper = Time.time;
			/*if (blueJelly.GetPositionX() == 6){
				move *= -1;
			}
			else if (blueJelly.GetPositionX() == 1){
				move *= -1;
			}*/
		}
		
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
