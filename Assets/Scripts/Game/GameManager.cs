using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private EnemyManager enemyManager;
	private GUIManager guiManager;
	private Camera camera;
	private float timeKeeper;
	private static GameObject[] selectors;
	
	void Start()
	{
		
		GameStorage.player = new Player ("Player", 14, 14);
		guiManager = new GUIManager ();
		enemyManager = new EnemyManager();
		timeKeeper = Time.time;
		
		GameStorage.towerList = new Hashtable ();
		selectors = GameObject.FindGameObjectsWithTag ("selector");

		GameStorage.gameState = GameStorage.GameState.Building;
		GameStorage.level = 0;
		GameStorage.currentWave = 0;
		GameStorage.waveTotal = 3;
		
		GameStorage.enemies = new System.Collections.Generic.List<Enemy> ();
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
			enemyManager.DrawEnemy();
		}
		else if (GameStorage.gameState == GameStorage.GameState.Stopped){
			guiManager.DrawStoppedGUI();
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
	
	public static void AddTowerToList(Tower tower)
	{
		GameStorage.towerList.Add("T" + tower.getXPos() + "," + tower.getZPos(),tower);
	}
	
	public static void DestroyObject(GameObject go)
	{
		Destroy(go);
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
	
	public static void ShowSelectors()
	{
		selectors = GameObject.FindGameObjectsWithTag ("selector");
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
	
	public static void ResetGame (){
		GameStorage.gameState = GameStorage.GameState.Building;
		GameStorage.level = 0;
		GameStorage.currentWave = 0;
		GameStorage.waveTotal = 3;
		
		GameStorage.player.SetHealth(100);
		GameStorage.player.GetPlayerObj().animation.Play("WakeUp");
		
		while (GameStorage.enemies.Count > 0){
			Enemy remainingEnemy = GameStorage.enemies[0];
			remainingEnemy.Clean();
			GameStorage.enemies.Remove(remainingEnemy);
		}
		
		foreach (Tower t in GameStorage.towerList.Values){
			DestroyObject(t.GetZone().GetZoneObj()); // Removing zones from map
			DestroyObject (t.GetTowerObj()); // Removing towers from map
		}
		GameStorage.towerList = new Hashtable ();
		
		// Need to recreate all selectors destroyed.
		ResetSelectors ();
		ShowSelectors ();
	}
	
	private static void ResetSelectors(){
		GameObject[] usedSelectors = GameObject.FindGameObjectsWithTag ("selector (used)");
		foreach (GameObject sObj in usedSelectors)
		{
			Debug.Log ("Enabling Selector");
			sObj.tag = ("selector");
			sObj.collider.enabled = true;
		}
	}
	
	public static void InstantiateModel(GameObject obj, Vector3 pos)
	{
		Instantiate(obj, pos, Quaternion.identity);
	}
}
