using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private enum MainState {Menu, InGame, Paused, GameOver};
	private MainState mainState;
	//private MenuManager menuManager;
	private EnemyManager enemyManager;
	private Map map;
	private int enemyCount;

	void Start (){
		// The menu state of the game
		// This is set from the beginning of the game
		mainState = MainState.Menu;
		//menuManager.start()
		enemyManager.Sleep();
	}
	
	// When the menu calls the Game Manager to start:
	// set the level and the wave limit for the level.
	void startLevel(string levelName){
		//Application.LoadLevel(levelName);
		//TODO: make calls to enemy manager to set the level to lvl as well.
		mainState = MainState.InGame;
		enemyManager.Awake();
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
			if (gameState == 3){ // paused
				PauseGame();
			}
			if (gameState == 4){ // stopped
				EndGame();
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
		enemyManager.Sleep();
		//TODO: Game ending code goes here
		mainState = MainState.GameOver;
		// Show menu screen / Level selection screen (check level)
	}
}
