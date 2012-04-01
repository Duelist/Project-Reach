using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private EnemyManager enemyManager;
	private GUIManager guiManager;
	private Camera camera;
	private float timeKeeper;
	private static GameObject[] selectors;
	
	private Hashtable towerList;
	
	void Start()
	{
		
		GameStorage.player = new Player ("Player", 14, 14);
		guiManager = new GUIManager ();
		enemyManager = new EnemyManager();
		timeKeeper = Time.time;
		
		towerList = new Hashtable ();
		selectors = GameObject.FindGameObjectsWithTag ("selector");

		GameStorage.gameState = GameStorage.GameState.Building;
		GameStorage.level = 0;
	}
	
	public void HandleGameLogic()
	{
		// Increments player's mana pool and updates time
		if (timeKeeper + 1 < Time.time)
		{
			timeKeeper = Time.time;
		}
	}
	
	void OnGUI()
	{
		if (GameStorage.gameState == GameStorage.GameState.Building)
		{
			guiManager.DrawBuildGUI();
		}
		else if (GameStorage.gameState == GameStorage.GameState.Playing)
		{
			HandleGameLogic();
			guiManager.DrawPlayGUI();
			enemyManager.DrawEnemy(towerList);
		}
		else if (gameState == GameState.Stopped){
			guiManager.DrawStoppedGUI(player1);
		}
	}
	
	public GUIManager GetGuiManager()
	{
		return guiManager;
	}
	
	public Player GetCurrentPlayer()
	{
		return GameStorage.player;
	}
	
	public Hashtable GetTowerList()
	{
		return towerList;
	}
	
	public void AddTowerToList(Tower tower)
	{
		towerList.Add("T" + tower.getXPos() + "," + tower.getZPos(),tower);
	}
	
	public static void DestroyObject(GameObject go)
	{
		Destroy(go);
	}
	
	public static void HideSelectors (){
		foreach (GameObject sObj in selectors){
			Debug.Log ("Hiding Selector");
			foreach (Transform child in sObj.transform){
				if (child.gameObject.renderer != null){
					child.gameObject.renderer.enabled = false;
				}
			}
		}
	}
	
	public static void ShowSelectors()
	{
		foreach (GameObject sObj in selectors)
		{
			Debug.Log ("Showing Selector");
			foreach (Transform child in sObj.transform){
				if (child.gameObject.renderer != null){
					child.gameObject.renderer.enabled = true;
				}
			}
		}
	}
	
	public static void InstantiateModel(GameObject obj, Vector3 pos)
	{
		Instantiate(obj, pos, Quaternion.identity);
	}
}
