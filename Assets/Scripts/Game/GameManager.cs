using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private enum GameState {Menu, Playing, Paused, GameOver};
	private GameState gameState;

	void Start ()
	{
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
