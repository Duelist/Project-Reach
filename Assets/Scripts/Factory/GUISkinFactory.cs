using UnityEngine;
using System.Collections;

public class GUISkinFactory{
	private static GUISkin fireButtonSkin;
	private static GUISkin iceButtonSkin;
	private static GUISkin earthButtonSkin;
	private static GUISkin windButtonSkin;
	public GUISkinFactory(){
	}
	
	public static GUISkin GetFireButtonSkin(){
		if (fireButtonSkin == null){
			fireButtonSkin = Resources.Load("GUI/Rolling Menu Textures/FireButtonSkin") as GUISkin;
		}
		return fireButtonSkin;
	}
	
	public static GUISkin GetIceButtonSkin(){
		if (iceButtonSkin == null){
			iceButtonSkin = Resources.Load("GUI/Rolling Menu Textures/IceButtonSkin") as GUISkin;
		}
		return iceButtonSkin;
	}
	
	public static GUISkin GetEarthButtonSkin(){
		if (earthButtonSkin == null){
			earthButtonSkin = Resources.Load("GUI/Rolling Menu Textures/EarthButtonSkin") as GUISkin;
		}
		return earthButtonSkin;
	}
	
	public static GUISkin GetWindButtonSkin(){
		if (windButtonSkin == null){
			windButtonSkin = Resources.Load("GUI/Rolling Menu Textures/WindButtonSkin") as GUISkin;
		}
		return windButtonSkin;
	}
}
