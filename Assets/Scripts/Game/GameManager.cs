using UnityEngine;
using System.Collections;

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
	private Tower [] towerList;
	private float timeKeeper;
	
	public GameManager (){
		player1 = new Player ("Player 1");
		map = new Map();
		map.GenerateLevel1();
		guiManager = new GUIManager (map, player1);
		enemyManager = new EnemyManager(map);
		// Testing Tower stuff
		towerList = new Tower [map.selectorNum];
		Vector2 [] selectList = map.GetSelectorPositionList();
		for (int i = 0; i < map.selectorNum; i++){
			string dir = "";
			if (selectList[i].x + 1 < map.GetMapSizeX() && selectList[i].y + 1 < map.GetMapSizeY()) {
				if (!(map.GetTile((int) selectList[i].x + 1, (int) selectList[i].y).GetCollision())) {
					dir = "right";
					if (!(map.GetTile((int) selectList[i].x, (int) selectList[i].y + 1).GetCollision())) {
						dir = "topright";
					}
				} else if (map.GetTile((int) selectList[i].x + 1, (int) selectList[i].y).GetCollision() 
					&& map.GetTile((int) selectList[i].x, (int) selectList[i].y + 1).GetCollision()
					&& !map.GetTile((int) selectList[i].x + 1, (int) selectList[i].y + 1).GetCollision()) { 
						dir = "topright";
				}
			}
			if (selectList[i].x - 1  > 0 && selectList[i].y + 1 < map.GetMapSizeY()) {
				if (!(map.GetTile((int) selectList[i].x - 1, (int) selectList[i].y).GetCollision())) {
					dir = "left";
					if (!(map.GetTile((int) selectList[i].x, (int) selectList[i].y + 1).GetCollision())) {
						dir = "topleft";
					}
				} else if (map.GetTile((int) selectList[i].x - 1, (int) selectList[i].y).GetCollision() 
					&& map.GetTile((int) selectList[i].x, (int) selectList[i].y + 1).GetCollision()
					&& !map.GetTile((int) selectList[i].x - 1, (int) selectList[i].y + 1).GetCollision()) { 
						dir = "topleft";
				}
			}
			if (selectList[i].x - 1  > 0 && selectList[i].y - 1 > 0) {
				if (!(map.GetTile((int) selectList[i].x, (int) selectList[i].y - 1).GetCollision())) {
					dir = "bottom";
					if (!(map.GetTile((int) selectList[i].x - 1, (int) selectList[i].y).GetCollision())) {
						dir = "bottomleft";
					}
				} else if (map.GetTile((int) selectList[i].x - 1, (int) selectList[i].y).GetCollision() 
					&& map.GetTile((int) selectList[i].x, (int) selectList[i].y - 1).GetCollision()
					&& !map.GetTile((int) selectList[i].x - 1, (int) selectList[i].y - 1).GetCollision()) { 
						dir = "bottomleft";
				}
			}
			if (selectList[i].x + 1  < map.GetMapSizeX() && selectList[i].y - 1 > 0) {
				if (!(map.GetTile((int) selectList[i].x, (int) selectList[i].y - 1).GetCollision()) && !(map.GetTile((int) selectList[i].x + 1, (int) selectList[i].y).GetCollision())) {
					dir = "bottomright";
				} else if (map.GetTile((int) selectList[i].x + 1, (int) selectList[i].y).GetCollision() 
					&& map.GetTile((int) selectList[i].x, (int) selectList[i].y - 1).GetCollision()
					&& !map.GetTile((int) selectList[i].x + 1, (int) selectList[i].y - 1).GetCollision()) { 
						dir = "bottomright";
				}
			}
			if (selectList[i].y + 1 < map.GetMapSizeY() && selectList[i].x - 1  > 0 && selectList[i].x + 1  < map.GetMapSizeX()) {
				if (map.GetTile((int) selectList[i].x - 1, (int) selectList[i].y).GetCollision() && map.GetTile((int) selectList[i].x + 1, (int) selectList[i].y).GetCollision() && !map.GetTile((int) selectList[i].x, (int) selectList[i].y + 1).GetCollision()) {
						dir = "top";
				}
			}
			towerList[i] = new Tower ((int)selectList[i].x, (int)selectList[i].y, dir);
		}
		timeKeeper = Time.time;

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
