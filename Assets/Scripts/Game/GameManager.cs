using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private enum GameState {Menu, Building, Playing, Paused, GameOver};
	private GameState gameState;
	private EnemyManager enemyManager;
	private int level;
	private int waveNum, waveLimit;
	private int enemyCount;

	void Start ()
	{
		// The menu state of the game
		this.gameState = GameState.Menu;
	}
	
	void Update ()
	{
		/*
			State code and state switching goes here
			Example:
			if (gameState == GameState.mainMenu)
			{
				Call Menu Stuff
			}
		*/
		
		// When the game is started:
		if (gameState == GameState.Playing){
			// Call enemy manager to do an enemy count. 
			// enemyCount = enemyManager.countEnemy();
			// if there are no enemies, start the next building phase.
			if (enemyCount == 0){
				gameState = GameState.Building;
				// if this is the last wave, end the level.
				if (level == waveLimit){
					gameState = GameState.GameOver;
				}
			}
			
		}
	}
	
	// When the menu calls the Game Manager to start:
	// set the level and the wave limit for the level.
	void startLevel(int lvl, int limit){
		level = lvl;
		waveLimit = limit;
		//TODO: make calls to enemy manager to set the level to lvl as well.
		startBuilding ();
	}
	
	void startBuilding (){
		// The game goes into building phase
		gameState = GameState.Building;
		enemyManager.setSleep(true);
	}
	
	// When the wave begins, set the wave number, spawn the monsters, 
	// and change the gamestate to Playing.
	void startWave (int wave){
		waveNum = wave;
		enemyManager.setSleep(false);
		enemyManager.spawn();
		gameState = GameState.Playing;
	}
	
	void endGame(int lvl) {
		enemyManager.setSleep(true);
		//TODO: Game ending code goes here
		gameState = GameState.GameOver;
		// Show menu screen / Level selection screen (check level)
	}
	
	void PauseGame()
	{
		if (this.gameState == GameState.Playing)
		{
			// Pause code goes here
			this.gameState = GameState.Paused;
		}
		else
		{
			this.gameState = GameState.Playing;
		}
	}
}
