using UnityEngine;
using System.Collections;

public class SelectionManager : MonoBehaviour
{
	private RaycastHit hit;
	bool mouseDown = false;
	bool selectorHit = false;
	private Selector hitselector;
	Vector3 hitPos;
	Vector2 hitSize;
	private GameObject hitObject;
	private GameManager gmRef;
	private int lightBallLifeTime = 2;
	//private Ray ray;
	//ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	void Awake (){
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
				
				string infoMsg = "";
				infoMsg += hit.transform.gameObject.name;
				
				
				// Selector Events
				if (hit.transform.gameObject.tag == "selector") {
					if (GameStorage.gameState == 0){
						selectorHit = true;
						hitObject = hit.transform.gameObject;
						hitPos = ConvertObjectToScreenPos(hit.transform.position, hit.transform.localScale);
						hitSize = ConvertObjectToScreenSize(hit.transform.position, hit.transform.localScale);
						hitselector = hit.transform.gameObject.GetComponent<Selector>();
					}
				}
				
				// Enemy Selection
				if (hit.transform.gameObject.tag == "enemy"){
					
				}
				
				// Tower Selection
				if (hit.transform.gameObject.tag == "tower"){
					Tower towerSelected = (Tower)(GameStorage.towerList["T" + (int)hit.transform.position.x + "," + (int)hit.transform.position.z]);
					infoMsg += UpdateTowerInfo(towerSelected);
				}
				
				// Info Window Changes
				//gmRef.GetGuiManager().GetInfoWindow().SetTex(hit.transform.gameObject.renderer.material.mainTexture);
				gmRef.GetGuiManager().GetInfoWindow().SetDesc(infoMsg);
			}
		}
		// on mouse up for game objects
		if (Input.GetMouseButtonUp(0)){
			RaycastHit hit;
			
			if (Physics.Raycast (ray, out hit, 100000)) {
				string infoMsg = "";
				infoMsg += hit.transform.gameObject.name;
				// Tower Selection
				if (hit.transform.gameObject.tag == "tower"){
					Tower towerSelected = (Tower)(GameStorage.towerList["T" + (int)hit.transform.position.x + "," + (int)hit.transform.position.z]);
					towerSelected.FlipTime();
					infoMsg += UpdateTowerInfo(towerSelected);
				}
				// Info Window Changes
				//gmRef.GetGuiManager().GetInfoWindow().SetTex(hit.transform.gameObject.renderer.material.mainTexture);
				gmRef.GetGuiManager().GetInfoWindow().SetDesc(infoMsg);
			}
		}
	}
	
	void OnGUI (){
		if (selectorHit){
			RadialMenu.ShowRadialMenu(hitPos, hitSize);
			if (Input.GetMouseButtonUp(0)){
				Vector3 fireButtonPos = new Vector3 (hitPos.x, hitPos.y - hitSize.y, hitPos.z);
				Vector3 iceButtonPos = new Vector3 (hitPos.x, hitPos.y + hitSize.y, hitPos.z);
				Debug.Log(fireButtonPos.x + ":" + fireButtonPos.y + " | " + Input.mousePosition.x + ":" + Input.mousePosition.y + " | " + hitSize.x + ":" + hitSize.y);
				if (MouseUpAt (fireButtonPos,hitSize)){
					if (manaCheck(10)){
						GameStorage.player.DecMana (10);
						new LightBall ("PlayerManaBall", gmRef.GetCurrentPlayer().GetPlayerPos(), hitObject.transform, lightBallLifeTime, Color.red);
						CreateTower((int)hitObject.transform.position.x,(int)hitObject.transform.position.z, Effect.EffectType.Fire, hitselector.direction, hitselector.enemyEntry);
						GameStorage.player.GetPlayerObj().transform.LookAt(hitObject.transform);
						GameStorage.player.GetPlayerObj().animation.Play("Spin");
						GameStorage.player.GetAuraObj().Play();
						RemoveSelector (hitObject);
					}
				}
				else if (MouseUpAt (iceButtonPos,hitSize)){
					if (manaCheck(20)){
						GameStorage.player.DecMana (20);
						new LightBall ("PlayerManaBall", gmRef.GetCurrentPlayer().GetPlayerPos(), hitObject.transform, lightBallLifeTime, Color.blue);
						CreateTower((int)hitObject.transform.position.x,(int)hitObject.transform.position.z, Effect.EffectType.Ice, hitselector.direction, hitselector.enemyEntry);
						GameStorage.player.GetPlayerObj().transform.LookAt(hitObject.transform);
						GameStorage.player.GetPlayerObj().animation.Play("Jump");
						GameStorage.player.GetAuraObj().Play();
						RemoveSelector (hitObject);
					}
				}
				else {
					Debug.Log ("No Tower Type Selected");
				}
				selectorHit = false;
				mouseDown = false;
			}
		}
	}
					
	private bool MouseUpAt (Vector3 objPos, Vector2 objSize){
		if (Input.mousePosition.x > objPos.x 
			&& Input.mousePosition.x < objPos.x + objSize.x
			&& Screen.height - Input.mousePosition.y > objPos.y
			&& Screen.height - Input.mousePosition.y < objPos.y + objSize.y){
			return true;
		}
		return false;
	}
	
	private void CreateTower(int tilex, int tiley, Effect.EffectType effect, int dir, int eEntry){
		Tower tower = new Tower (tilex, tiley, effect, false, dir, eEntry);
		GameManager.AddTowerToList(tower);
		Debug.Log ("Tower Created");
	}
	
	private bool manaCheck(int manaCost) {
		if (GameStorage.player.GetMana() >= manaCost){
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
	
	private string UpdateTowerInfo (Tower towerSelected){
		string infoMsg = "";
		infoMsg += "\n State: " + towerSelected.GetFormattedState();
		infoMsg += "\n Type: " + towerSelected.GetEffect().GetFormattedEffectType();
		infoMsg += "\n Damage: " + towerSelected.GetZone().GetEffect().GetDamage();
		return infoMsg;
	}
	
	private void RemoveSelector (GameObject selector){
		selector.tag = "selector (used)";
		selector.collider.enabled = false;
		foreach (Transform child in selector.transform){
			if (child.gameObject.renderer != null){
				child.gameObject.renderer.enabled = false;
			}
		}
	}
}