using UnityEngine;
using System.Collections;

public class SelectionManager : MonoBehaviour
{
	private RaycastHit hit;
	bool mouseDown = false;
	bool selectorHit = false;
	private RadialMenu radial;
	private Selector hitselector;
	private GameObject hitObject;
	private GameManager gmRef;
	//private Ray ray;
	//ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	void Awake (){
		radial = new RadialMenu ();
		hitselector = new Selector();
		gmRef = ((GameManager)(GameObject.Find("GameManager").GetComponent("GameManager")));
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
					selectorHit = true;
					hitObject = hit.transform.gameObject;
					hitselector = hit.transform.gameObject.GetComponent<Selector>();
					CreateRadial((int)hit.transform.position.x,(int)hit.transform.position.z, radial);
				}
			}
		}
		if (Input.GetMouseButtonUp(0)){
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100000)) {
				if (hit.transform.gameObject.tag == "tower") {
					Debug.Log ("Tower hit");
					Tower towerSelected = (Tower)(gmRef.GetTowerList()["T" + (int)hit.transform.position.x + "," + (int)hit.transform.position.z]);
					towerSelected.FlipTime();
				}
				if (selectorHit){
					
					if (hit.transform.gameObject.name == "createSingle") {
						if (manaCheck(10)){
							CreateTower(radial.selectorX,radial.selectorY, Effect.EffectType.Fire, hitselector.direction);
						}	
					}
					else if (hit.transform.gameObject.name == "createMulti"){
						if (manaCheck(20)){
							CreateTower(radial.selectorX,radial.selectorY, Effect.EffectType.Ice, hitselector.direction);
						}
					}
					else {
						Debug.Log ("No Tower Type Selected");
					}
				}
			}
			selectorHit = false;
			mouseDown = false;
			radial.HideRadial();
		}
	}
	
	private void CreateTower(int tilex, int tiley, Effect.EffectType effect, int dir){
		Tower tower = new Tower (tilex, tiley, effect, false, dir);
		gmRef.AddTowerToList(tower);
		Debug.Log ("Tower Created");
	}
	
	private void CreateRadial(int tilex, int tiley, RadialMenu rad){
		rad.SetPosition(tilex, tiley);
		rad.ShowRadial();
	}
	
	private bool manaCheck(int manaCost) {
		Player p1 = gmRef.GetCurrentPlayer();
		if (p1.GetMana() > manaCost){
			p1.DecMana (manaCost);
			hitObject.name = "selector (used)";
			return true;
		}
		else {
			Debug.Log ("Insufficient Mana");
		}
		return false;
	}
}