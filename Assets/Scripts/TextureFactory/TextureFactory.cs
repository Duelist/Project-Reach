using UnityEngine;
using System.Collections;

public class TextureFactory{
	private static Texture fireTowerPast;
	private static Texture fireTowerFuture;
	private static Texture fireZonePast;
	private static Texture fireZoneFuture;
	
	public TextureFactory(){
	}
	
	public static Texture GetFireTowerPast(){
		if (fireTowerPast == null){
			fireTowerPast = Resources.Load ("Tower/FireTowerPast") as Texture;
		}
		return fireTowerPast;
	}
	
	public static Texture GetFireTowerFuture(){
		if (fireTowerFuture == null){
			fireTowerFuture = Resources.Load ("Tower/FireTowerFuture") as Texture;
		}
		return fireTowerFuture;
	}
	
	public static Texture GetFireZonePast(){
		if (fireZonePast == null){
			fireZonePast = Resources.Load ("WallZone/FireZonePast") as Texture;
		}
		return fireZonePast;
	}
	
	public static Texture GetFireZoneFuture(){
		if (fireZoneFuture == null){
			fireZoneFuture = Resources.Load ("WallZone/FireZoneFuture") as Texture;
		}
		return fireZoneFuture;
	}
}
