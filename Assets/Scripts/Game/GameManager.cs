using UnityEngine;
using System.Collections;

public class GameManager {

	private enum GameState {Building, Playing, Paused, Stopped};
	private GameState gameState;
	private EnemyManager enemyManager;
	private GUIManager guiManager;
	private Map map;
	private Camera camera;
	private Effect fireEffect;
	private Zone fireZone;
	private Tower [] towerList;
	
	public GameManager (){
		map = new Map();
		map.GenerateLevel1();
		guiManager = new GUIManager (map);
		enemyManager = new EnemyManager(map);
		// Testing Tower stuff
		towerList = new Tower [map.selectorNum];
		Vector2 [] selectList = map.GetSelectorPositionList();
		for (int i = 0; i < map.selectorNum; i++){
			string dir = "";
			if (selectList[i].x == 0 || selectList[i].x == 5 || selectList[i].x == 10){
				dir = "right";
			}
			if (selectList[i].x == 4 || selectList[i].x == 9 || selectList[i].x == 14){
				dir = "left";
			}
			if (selectList[i].y == 0){
				dir = "up";
			}
			if (selectList[i].y == 14){
				dir = "down";
			}
			towerList[i] = new Tower ((int)selectList[i].x, (int)selectList[i].y, dir);
		}
		// For Testing
		//gameState = GameState.Building;
		gameState = GameState.Playing;
		// gameState = GameState.Paused;
		// gameState = GameState.Stopped;
	}
	
	public void DrawScene (){
		guiManager.DrawGUI(towerList);
		if (gameState == GameState.Playing){
			enemyManager.DrawEnemy(towerList);
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
