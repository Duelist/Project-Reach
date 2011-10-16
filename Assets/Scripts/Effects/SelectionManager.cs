using UnityEngine;
using System.Collections;


public class SelectionManager : MonoBehaviour
{
	private RaycastHit hit;
	//private Ray ray;
	//ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	
	void Update () {
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		// Input.GetToouch for iPhone, may translate to this after port.
		if (Input.GetMouseButtonUp(0)) {
			Debug.Log("x=" + Input.mousePosition.x+"  y="+ Input.mousePosition.y+"   z=" + Input.mousePosition.z);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100000)) {
				//float distanceToGround = hit.distance;
				//Debug.DrawLine(ray.origin, hit.point);
				//Debug.DrawRay(ray.origin, hit.point, Color.green, 2);
				//if (hit.transform.tag == "tile") {
					//Tile tile = hit.transform.gameObject.GetComponent("Tile");
					//Debug.Log(tile.GetSelector());
				//} else 
				if (hit.transform.tag== "tower") {
					Debug.Log("Tower Hit!");
					//Tower tw = hit.transform.gameObject.GetComponent("Tower");
					//tw.selected = true;
				}
			}
			
		}
	}
}