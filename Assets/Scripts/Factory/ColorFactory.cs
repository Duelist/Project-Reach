using UnityEngine;
using System.Collections;

public class ColorFactory {
	private static Color lightRed;
	private static Color lightBlue;
	private static Color darkGray;
	
	public static Color GetLightRed (){
		if (lightRed.r == 0f){
			lightRed = new Color (1f, 0.5f, 0.5f, 0.5f);
		}
		return lightRed;
	}
	
	public static Color GetLightBlue (){
		if (lightBlue.b == 0f){
			lightBlue = new Color (0.5f, 0.5f, 1f, 0.5f);
		}
		return lightBlue;
	}
	
	public static Color GetDarkGray (){
		if (darkGray.r == 0f){
			darkGray = new Color (0.1f, 0.1f, 0.1f, 0.5f);
		}
		return darkGray;
	}
}
