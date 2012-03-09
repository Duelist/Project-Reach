using UnityEngine;
using System.Collections;

public class SelectionManager : MonoBehaviour
{
	private RaycastHit hit;
	bool mouseDown = false;
	bool selectorHit = false;
	private RadialMenu radial;
	private Selector hitselector;
	Vector3 hitPos;
	Vector2 hitSize;
	private GameObject hitObject;
	private GameManager gmRef;
	//private Ray ray;
	//ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	void Awake (){
		radial = new RadialMenu ();
		gmRef = ((GameManager)(GameObject.Find("GameManager").GetComponent("GameManager")));
		hitPos =  new Vector3 (-1, 0, -1);
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
				if (hit.transform.gameObject.name == "selector" && GameManager.GetGameState() == 0) {
					selectorHit = true;
					hitObject = hit.transform.gameObject;
					hitPos = ConvertObjectToScreenPos(hit.transform.position, hit.transform.localScale);
					hitSize = ConvertObjectToScreenSize(hit.transform.position, hit.transform.localScale);
					hitselector = hit.transform.gameObject.GetComponent<Selector>();
					CreateRadial((int)hit.transform.position.x,(int)hit.transform.position.z, radial);
				}
				gmRef.GetGuiManager().GetInfoWindow().SetTex(hit.transform.gameObject.renderer.material.mainTexture);
				gmRef.GetGuiManager().GetInfoWindow().SetDesc(hit.transform.gameObject.name);
			}
		}
		if (Input.GetMouseButtonUp(0)){
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100000)) {
				if (hit.transform.gameObject.tag == "tower") {
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
	
	void OnGUI (){
		if (selectorHit){
			radial.ShowRadialMenu(hitPos, hitSize);
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
		if (p1.GetMana() >= manaCost){
			p1.DecMana (manaCost);
			hitObject.name = "selector (used)";
			return true;
		}
		else {
			Debug.Log ("Insufficient Mana");
		}
		return false;
	}
	
	private Vector3 ConvertObjectToScreenPos (Vector3 v3, Vector3 sv3){
		Vector3 conV = camera.WorldToScreenPoint(new Vector3 (v3.x - (sv3.x/2), v3.y, v3.z + (sv3.z/2)));
		Debug.Log ("x:" + conV.x + " y:" + conV.y + " z:" + conV.z);
		conV.y = Screen.height - conV.y;
		return conV;
	}
	
	private Vector2 ConvertObjectToScreenSize (Vector3 v3, Vector3 sv3){
		Vector3 conV1 = camera.WorldToScreenPoint(new Vector3 (v3.x, v3.y, v3.z));
		Vector3 conV2 = camera.WorldToScreenPoint(new Vector3 (v3.x + sv3.x, v3.y, v3.z + sv3.z));
		Vector2 conSize = new Vector2 (Mathf.Abs(conV1.x - conV2.x), Mathf.Abs(conV1.y - conV2.y));
		return conSize;
	}
}