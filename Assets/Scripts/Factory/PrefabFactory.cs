using UnityEngine;
using System.Collections;

public class PrefabFactory {
	private static GameObject lightBallPrefab;
	
	//Enemies
	private static GameObject magmaSlugPrefab;
	private static GameObject blueJellyPrefab;
	private static GameObject merupiPrefab;
	
	//Player
	private static GameObject suzePrefab;
	
	//Selector, Tower, and Zone
	private static GameObject selectorPrefab;
	private static GameObject towerPrefab;
	private static GameObject zonePrefab;
	
	
	public PrefabFactory () {
	}
	
	public static GameObject GetLightBallPrefab (){
		if (lightBallPrefab == null){
			lightBallPrefab = (GameObject)Resources.Load("Player/LightBall",typeof(GameObject));
		}
		return lightBallPrefab;
	}
	
	public static GameObject GetMagmaSlugPrefab (){
		if (magmaSlugPrefab == null){
			magmaSlugPrefab = (GameObject)Resources.Load("Enemy/Magma Slug/Slug",typeof(GameObject));
		}
		return magmaSlugPrefab;
	}
	
	public static GameObject GetBlueJellyPrefab (){
		if (blueJellyPrefab == null){
			blueJellyPrefab = (GameObject)Resources.Load("Enemy/Blue Jelly/BlueJellyPrefab",typeof(GameObject));
		}
		return blueJellyPrefab;
	}
	
	public static GameObject GetMerupiPrefab (){
		if (merupiPrefab == null){
			merupiPrefab = (GameObject)Resources.Load("Mascot/MerupiPrefab",typeof(GameObject));
		}
		return merupiPrefab;
	}
	
	public static GameObject GetSuzePrefab (){
		if (suzePrefab == null){
			suzePrefab = (GameObject)Resources.Load("Player/SusePrefab",typeof(GameObject));
		}
		return suzePrefab;
	}
	
	public static GameObject GetSelectorPrefab(){
		if (selectorPrefab == null){
			selectorPrefab = (GameObject)Resources.Load("Tower/SelectorPrefab",typeof(GameObject));
		}
		return selectorPrefab;
	}
	
	public static GameObject GetTowerPrefab(){
		if (towerPrefab == null){
			towerPrefab = (GameObject)Resources.Load("Tower/TowerPrefab",typeof(GameObject));
		}
		return towerPrefab;
	}
	
	public static GameObject GetZonePrefab(){
		if (zonePrefab == null){
			zonePrefab = (GameObject)Resources.Load("WallZone/ZonePrefab",typeof(GameObject));
		}
		return zonePrefab;
	}
	
	
}
