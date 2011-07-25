#pragma strict
class GameManager extends MonoBehaviour
{
	private enum GameState {Menu, Playing, Paused, GameOver};
	private var gameState : GameState;
	
	function Awake()
	{
	}
	
	function Start()
	{
		this.gameState = GameState.Menu;
	}
	
	function Update()
	{
		/*
			State code and state switching goes here
			Example:
			if (gameState == GameState.mainMenu)
			{
				Call Menu Stuff
			}
		*/
	}
	
	function PauseGame()
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