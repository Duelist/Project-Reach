using UnityEngine;

public static class GameStorage
{
	public static Player player;
	public static Enemy[] enemies;
	
	public enum GameState {Building, Playing, Paused, Stopped};
	public static GameState gameState;
	
	public static int level;
	public static int currentWave;
	public static int waveTotal;
	
	public static Map map;
}