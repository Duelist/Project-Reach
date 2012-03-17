using UnityEngine;
using System.Collections;

public class CastSpell {
	
	static GameObject mainscene;
	GestureTemplates templates;
	ArrayList points;
	
	private bool mousedown = false;
	public Rect textwin =  new Rect (10,10,Screen.width - 20,Screen.height - 20);
	private bool castwindow = false;
	public int sizeDivisor = 6;
	
	public CastSpell () {
		
		templates = new GestureTemplates();
		mainscene = GameObject.Find ("Scenes/MainScene");
		
		points = new ArrayList();
	}
	
	public void DrawGUI () {
		if (GUI.Button (new Rect(Screen.width - Screen.width/sizeDivisor, Screen.height-Screen.height/sizeDivisor,Screen.width/sizeDivisor,Screen.height/sizeDivisor),"Cast Spell")){
			//Debug.Log ("spell cast");
			castwindow = true;
			//Vector3 screenSpace = Camera.main.WorldToScreenPoint(mainscene.transform.position);
		    //Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
		    //Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace); 
		    //mainscene.transform.position = curPosition;
		}
		if (castwindow) {
			GUI.color = Color.white;
			textwin = GUI.Window(0,textwin,DoMyWindow,"Spell Window");
			DrawSpell();
			Time.timeScale = 1;
  		    AudioListener.pause = true;
		}
	}
	
	public void DrawSpell() {
		Event e = Event.current;
		if (e.type == EventType.MouseDown) {
			mousedown = true;
		}
		
		if (mousedown) {
			Vector2 curr = new Vector2(Input.mousePosition.x , Input.mousePosition.z);
		    points.Add(curr);
		    //StartCoroutine("ScreenCoordinates");
			//ScreenCoordinates();	
			//Vector3 screenSpace = Camera.main.WorldToScreenPoint(mainscene.transform.position);
		   // Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
		   // Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace); 
		    //mainscene.transform.position = curPosition;
		}
		
		if (e.type == EventType.MouseUp) {
			mousedown = false;
			Debug.Log (points.Count);
			GestureRecognizer.startRecognizer(points);
			points.Clear();
		}
	}
	
	IEnumerator ScreenCoordinates () {
	    // fix world coordinate to the viewport coordinate
	    Vector3 screenSpace = Camera.main.WorldToScreenPoint(mainscene.transform.position);
    	
	    while (Input.GetMouseButton(1))
	    {
		    Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
		    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace); 
		    mainscene.transform.position = curPosition;
		    yield return 0;
	    }
    }
	
	public void DoMyWindow(int windowID) {
		//if (GUI.Button (new Rect (10,10,10,10),"Hello")) {
			
		//}
	}
	
}
