using UnityEngine;
using System.Collections;

// Graphical Layers in order:
// 1. Tiles (lowest)
// 2. Towers/Zones/Walls
// 3. Enemies
// 4. GUI (using Unity GUI)

public class GameManager {

	private Player player1;
	private enum GameState {Building, Playing, Paused, Stopped};
	private GameState gameState;
	private EnemyManager enemyManager;
	private GUIManager guiManager;
	private Map map;
	private Camera camera;
	private Effect fireEffect;
	private Zone fireZone;
	private float timeKeeper;
	
	// Radical Changes, changing towerList to Hashtable for optimization
	// Key: tower.x + "," + tower.y + "Tower"
	// Value: Tower (at that position)
	private Hashtable towerList;
	
	public GameManager (){
		player1 = new Player ("Player 1");
		map = new Map();
		map.GenerateLevel1();
		guiManager = new GUIManager (map, player1);
		enemyManager = new EnemyManager(map);
		timeKeeper = Time.time;
		
		towerList = new Hashtable ();

		// For Testing
		//gameState = GameState.Building;
		gameState = GameState.Playing;
		// gameState = GameState.Paused;
		// gameState = GameState.Stopped;
	}
	
	public void DrawScene (){
		guiManager.DrawGUI(towerList, player1);
		if (gameState == GameState.Playing){
			enemyManager.DrawEnemy(towerList, player1);
		}
		if (timeKeeper + 1 < Time.time){
			player1.IncMana(1);
			timeKeeper = Time.time;
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
