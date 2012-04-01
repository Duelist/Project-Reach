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
	private static GameState gameState;
	private EnemyManager enemyManager;
	private GUIManager guiManager;
	private Map map;
	private Camera camera;
	private float timeKeeper;
	private static int level;
	private static GameObject[] selectors;
	
	// Key: "T" + tower.getXPos() + "," + tower.getZPos()
	// Value: Tower object at that location
	private Hashtable towerList;
	
	void Start (){
		player1 = new Player ("Player 1", 14, 14);
		//map = new Map();
		//map.GenerateLevel1();
		guiManager = new GUIManager (map, player1);
		enemyManager = new EnemyManager(map);
		timeKeeper = Time.time;
		
		towerList = new Hashtable ();
		selectors = GameObject.FindGameObjectsWithTag ("selector");

		// For Testing
		gameState = GameState.Building;
		//gameState = GameState.Playing;
		// gameState = GameState.Paused;
		// gameState = GameState.Stopped;
		level = 0;
		
		//new LightBall ("LightTest", new Vector3 (0,0,0), player1.GetPlayerPos(), 5f);
	}
	
	public void HandleGameLogic (){
		// Increments player's mana pool and updates time
		if (timeKeeper + 1 < Time.time){
			//player1.IncMana(1);
			timeKeeper = Time.time;
		}
	}
	
	void OnGUI(){
		
		if (gameState == GameState.Building){
			guiManager.DrawBuildGUI(player1);
		}
		else if (gameState == GameState.Playing){
			HandleGameLogic();
			guiManager.DrawPlayGUI(player1);
			enemyManager.DrawEnemy(towerList, player1, level);
		}
		else if (gameState == GameState.Stopped){
			guiManager.DrawStoppedGUI(player1);
		}
	}
	
	public GUIManager GetGuiManager() {
		return guiManager;
	}
	
	public Player GetCurrentPlayer (){
		return player1;
	}
	
	public Hashtable GetTowerList(){
		return towerList;
	}
	
	public void AddTowerToList (Tower tower){
		towerList.Add("T" + tower.getXPos() + "," + tower.getZPos(),tower);
	}
	
	public static void DestroyObject (GameObject go) {
		Destroy(go);
	}
	
	
	public static int GetGameState (){
		return (int)gameState;
	}
	public static void SetGameState (int state){
		gameState = (GameState) state;
	}
	
	public static int GetLevel (){
		return level;
	}
	
	public static void SetLevel (int lv){
		level = lv;
	}
	
	public static void HideSelectors (){
		selectors = GameObject.FindGameObjectsWithTag ("selector");
		foreach (GameObject sObj in selectors){
			Debug.Log ("Hiding Selector");
			foreach (Transform child in sObj.transform){
				if (child.gameObject.renderer != null){
					child.gameObject.renderer.enabled = false;
				}
			}
		}
	}
	
	public static void ShowSelectors(){
		selectors = GameObject.FindGameObjectsWithTag ("selector");
		foreach (GameObject sObj in selectors){
			Debug.Log ("Showing Selector");
			foreach (Transform child in sObj.transform){
				if (child.gameObject.renderer != null){
					child.gameObject.renderer.enabled = true;
				}
			}
		}
	}
	
	public static void InstantiateModel(GameObject obj, Vector3 pos){
		Instantiate(obj, pos, Quaternion.identity);
	}
}
