using UnityEngine;
using System.Collections;

public class GameManager {

	private enum GameState {Building = 1, Playing = 2, Paused, Stopped};
	private GameState gameState;
	private EnemyManager enemyManager;
	private GUIManager guiManager;
	private Map map;
	
	public GameManager (){
		map = new Map();
		map.GenerateLevel1();
		
		guiManager = new GUIManager ();
		enemyManager = new EnemyManager(map);
		
		// For Testing
		//gameState = GameState.Building;
		gameState = GameState.Playing;
		// gameState = GameState.Paused;
		// gameState = GameState.Stopped;
	}
	
	public void DrawScene (){
		guiManager.DrawGUI();
		if (gameState == GameState.Playing){
			enemyManager.DrawEnemy();
		}
		
		/*Effect eff = new Effect("fire");
		Zone zone = new Zone(eff, 3, 3, "present");
		Tower tower = new Tower(4, 3, zone);
		Tower tower2 = new Tower(4,6,zone);
		Wall wall = new Wall(tower, tower2);
		tower.createUpperZone(eff, 4, 3 , 2, 2);
		tower.createWall(wall);
		Debug.Log("Tower Created");*/
	}
}
