using UnityEngine;
using System.Collections;

public class SelectionManager : MonoBehaviour
{
	private RaycastHit hit;
	bool mouseDown = false;
	bool selectorHit = false;
	private RadialMenu radial;
	private Selector hitselector;
	//private Ray ray;
	//ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	void Awake (){
		radial = new RadialMenu ();
		hitselector = new Selector();
	}
	
	void Update () {
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		// Input.GetToouch for iPhone, may translate to this after port.
		if (Input.GetMouseButtonDown(0)) {
			mouseDown = true;
			Debug.Log("mouseDown");
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100000)) {
				//float distanceToGround = hit.distance;
				//Debug.DrawLine(ray.origin, hit.point);
				//Debug.DrawRay(ray.origin, hit.point, Color.green, 2);
				//if (hit.transform.tag == "tile") {
					//Tile tile = hit.transform.gameObject.GetComponent("Tile");
					//Debug.Log(tile.GetSelector());
				//} else 
				if (hit.transform.gameObject.name == "selector") {
					Debug.Log("Selector Hit!");
					selectorHit = true;
					hitselector = hit.transform.gameObject.GetComponent<Selector>();
					Debug.Log(hitselector.direction);
					CreateRadial((int)hit.transform.position.x,(int)hit.transform.position.z, radial);
				}
			}
		}
		if (Input.GetMouseButtonUp(0) && selectorHit){
			mouseDown = false;
			selectorHit = false;
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100000)) {
				if (hit.transform.gameObject.name == "createSingle") {
					CreateTower(radial.selectorX,radial.selectorY, Effect.EffectType.Fire, hitselector.direction);
				}
				else if (hit.transform.gameObject.name == "createMulti"){
					CreateTower(radial.selectorX,radial.selectorY, Effect.EffectType.Ice, hitselector.direction);
				}
				else {
					Debug.Log ("No Tower Type Selected");
				}
			}
			radial.HideRadial();
		}
	}
	
	private void CreateTower(int tilex, int tiley, Effect.EffectType effect, int dir){
		Tower tower = new Tower (tilex, tiley, effect, false, dir);
		
		Debug.Log ("Tower Created");
	}
	
	private void CreateRadial(int tilex, int tiley, RadialMenu rad){
		rad.SetPosition(tilex, tiley);
		rad.ShowRadial();
	}
}