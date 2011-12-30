using UnityEngine;
using System.Collections;

public class TextureFactory{
	private static Texture fireTowerPast;
	private static Texture fireTowerFuture;
	private static Texture fireZonePast;
	private static Texture fireZoneFuture;
	private static Texture fireButtonDecal;
	
	private static Texture iceTowerPast;
	private static Texture iceTowerFuture;
	private static Texture iceButtonDecal;
	
	private static Texture earthTowerPast;
	private static Texture earthTowerFuture;
	private static Texture earthButtonDecal;
	
	private static Texture windTowerPast;
	private static Texture windTowerFuture;
	private static Texture windButtonDecal;
	
	public TextureFactory(){
	}
	
	// Fire Textures
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
	
	public static Texture GetFireButtonDecal(){
		if (fireButtonDecal == null){
			fireButtonDecal = Resources.Load("GUI/Decals/Fire-Decal") as Texture;
		}
		return fireButtonDecal;
	}
	
	// Ice Textures
	public static Texture GetIceTowerPast(){
		if (iceTowerPast == null){
			iceTowerPast = Resources.Load ("Tower/IceTowerPast") as Texture;
		}
		return iceTowerPast;
	}
	
	public static Texture GetIceTowerFuture(){
		if (iceTowerFuture == null){
			iceTowerFuture = Resources.Load ("Tower/IceTowerFuture") as Texture;
		}
		return iceTowerFuture;
	}
	
	public static Texture GetIceButtonDecal(){
		if (iceButtonDecal == null){
			iceButtonDecal = Resources.Load("GUI/Decals/Ice-Decal") as Texture;
		}
		return iceButtonDecal;
	}
	
	
	// Earth Textures
	public static Texture GetEarthTowerPast(){
		if (earthTowerPast == null){
			earthTowerPast = Resources.Load ("Tower/EarthTowerPast") as Texture;
		}
		return earthTowerPast;
	}
	
	public static Texture GetEarthTowerFuture(){
		if (earthTowerFuture == null){
			earthTowerFuture = Resources.Load ("Tower/EarthTowerFuture") as Texture;
		}
		return earthTowerFuture;
	}
	
	public static Texture GetEarthButtonDecal(){
		if (earthButtonDecal == null){
			earthButtonDecal = Resources.Load("GUI/Decals/Earth-Decal") as Texture;
		}
		return earthButtonDecal;
	}
	
	
	// Wind Textures
	public static Texture GetWindTowerPast(){
		if (windTowerPast == null){
			windTowerPast = Resources.Load ("Tower/WindTowerPast") as Texture;
		}
		return windTowerPast;
	}
	
	public static Texture GetWindTowerFuture(){
		if (windTowerFuture == null){
			windTowerFuture = Resources.Load ("Tower/WindTowerFuture") as Texture;
		}
		return windTowerFuture;
	}
	
	public static Texture GetWindButtonDecal(){
		if (windButtonDecal == null){
			windButtonDecal = Resources.Load("GUI/Decals/Wind-Decal") as Texture;
		}
		return windButtonDecal;
	}
}
