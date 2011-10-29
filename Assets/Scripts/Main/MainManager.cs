using UnityEngine;
using System.Collections;

public class MainManager : MonoBehaviour
{
	private enum MainState {Menu, InGame, GameOver};
	private MainState mainState;
	private MenuManager menuManager;
	private GameManager gameManager;
	private int enemyCount;

	void Start (){
		/*guiManager = new GUIManager ();
		enemyManager = new EnemyManager();
		menuManager = new MenuManager();*/
		
		// The menu state of the game
		// This is set from the beginning of the game
		mainState = MainState.Menu;
		//menuManager.start()
		Debug.Log("Main Manager Start");
		//enemyManager.Sleep();
		
		// ---------------------- For Testing Purposes ------------------------//
		//mainState = MainState.Menu;
		mainState = MainState.InGame;
		//mainState = MainState.Paused;
		//mainState = MainState.GameOver;
		
		gameManager = new GameManager();
		menuManager = new MenuManager();
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
		
		//	State code and state switching goes here
			//Example:
			/*if (mainState == MainState.Menu)
			{
				menuManager.DrawMain();
			}*/
		
		
		// When the game is started:
		/*if (mainState == MainState.InGame){
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
		}*/
	}
	
	void OnGUI(){
		if (mainState == MainState.Menu){
			menuManager.DrawMain();
		}
		if (mainState == MainState.InGame){
			gameManager.DrawScene();
		}
	}
	
	void EndGame() {
		//TODO: Game ending code goes here
		mainState = MainState.GameOver;
		// Show menu screen / Level selection screen (check level)
	}
}
