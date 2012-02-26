using UnityEngine;
using System.Collections;

// Graphical Layers in order:
// 1. Tiles (lowest)
// 2. Towers/Zones/Walls
// 3. Enemies
// 4. GUI (using Unity GUI)

public class GameManager : MonoBehaviour{

	private Player player1;
	private enum GameState {Building, Playing, Paused, Stopped};
	private GameState gameState;
	private EnemyManager enemyManager;
	private GUIManager guiManager;
	private Map map;
	private Camera camera;
	private float timeKeeper;
	
	// Radical Changes, changing towerList to Hashtable for optimization
	// Key: tower.x + "," + tower.y + "Tower"
	// Value: Tower (at that position)
	private Hashtable towerList;
	
	void Start (){
		player1 = new Player ("Player 1");
		//map = new Map();
		//map.GenerateLevel1();
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
	
	public void HandleGameLogic (){
		// Increments player's mana pool and updates time
		if (timeKeeper + 1 < Time.time){
			player1.IncMana(1);
			timeKeeper = Time.time;
		}
	}
	
	void OnGUI(){
		guiManager.DrawGUI(towerList, player1);
		if (gameState == GameState.Playing){
			HandleGameLogic();
			enemyManager.DrawEnemy(towerList, player1);
		}
	}
	
	public Player GetCurrentPlayer (){
		return player1;
	}
	
	public static void DestroyObject (GameObject go) {
		Destroy(go);
	}
}
