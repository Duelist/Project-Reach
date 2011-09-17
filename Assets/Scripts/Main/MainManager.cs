using UnityEngine;
using System.Collections;

public class MainManager : MonoBehaviour
{
	private enum MainState {Menu, InGame, Paused, GameOver};
	private MainState mainState;
	//private MenuManager menuManager;
	private EnemyManager enemyManager;
	private GUIManager guiManager;
	private Map map;
	private int enemyCount;

	void Start (){
		Effect eff = new Effect("fire");
		Zone zone = new Zone(eff, 3, 3, "present");
		Tower tower = new Tower(4, 3, zone);
		Tower tower2 = new Tower(4,7,zone);
		Wall wall = new Wall(tower, tower2);
		tower.createUpperZone(eff, 4, 3 , 3, 3);
		tower.createWall(wall);
		Debug.Log("Tower Created");

		
		guiManager = new GUIManager ();
		enemyManager = new EnemyManager();
		
		// The menu state of the game
		// This is set from the beginning of the game
		mainState = MainState.Menu;
		//menuManager.start()
		Debug.Log("Main Manager Start");
		//enemyManager.Sleep();
		
		// For testing
		// mainState = MainState.Menu;
		mainState = MainState.InGame;
		// mainState = MainState.Paused;
		// mainState = MainState.GameOver;
		
		
	}
	
	// When the menu calls the Game Manager to start:
	// set the level and the wave limit for the level.
	void startLevel(string levelName){
		//Application.LoadLevel(levelName);
		//TODO: make calls to enemy manager to set the level to lvl as well.
		mainState = MainState.InGame;
		//enemyManager.Awake();
	}
	
	void Update (){
		/*
			State code and state switching goes here
			Example:
			if (gameState == GameState.mainMenu)
			{
				Call Menu Stuff
			}
		*/
		
		// When the game is started:
		if (mainState == MainState.InGame){
			int gameState = enemyManager.getGameState();
			if (gameState == 1 || gameState == 2) { // Building or Playing
				enemyManager.DrawEnemy();
			}
			if (gameState == 3){ // paused
				PauseGame();
			}
			else if (gameState == 4){ // stopped
				EndGame();
			}
		}
	}
	
	void OnGUI(){
		if (mainState == MainState.InGame){
			int gameState = enemyManager.getGameState();
			if (gameState == 1 || gameState == 2) { // Building or Playing
				guiManager.DrawGUI();
				//enemyManager.DrawEnemy();
			}
		}
	}
	
	void PauseGame() {
		if (mainState == MainState.InGame)
		{
			// Pause code goes here
			mainState = MainState.Paused;
		}
		else
		{
			mainState = MainState.InGame;
		}
	}
	
	void EndGame() {
		//TODO: Game ending code goes here
		mainState = MainState.GameOver;
		// Show menu screen / Level selection screen (check level)
	}
}
