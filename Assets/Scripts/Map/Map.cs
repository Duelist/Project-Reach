using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
	public int width;
	public int height;
	public bool pastState;
		
	public Texture2D tilesetPast;
	public Texture2D tilesetFuture;
	
	public ArrayList startZones;
	public ArrayList endZones;
}
