using UnityEngine;
using System.Collections;

public class TrailSection {
	public Vector3 point;
	public Vector3 upDir;
	public float time;
}

public class CastSpell {

	GestureTemplates templates;
	ArrayList points;
	
	public bool mousedown = false;
	public Rect textwin =  new Rect (10,10,Screen.width - 20,Screen.height - 20);
	private bool castwindow = false;
	public int sizeDivisor = 6;
	
	private int brushheight = 2;
	private int brushwidth = 2;
	
	private int texheight = Screen.height;
	private int texwidth = Screen.width;
	private Texture2D drawtex;
	private Color[] brush;
	int oldx;
	int oldy;
	
	Drawing dw;	
	GameObject go;
	
	public CastSpell () {
		
		templates = new GestureTemplates();
		
		points = new ArrayList();
		go = GameObject.Find("Draw");
		dw = go.GetComponent<Drawing>();
	}
	
	public void DrawGUI () {
		if (GUI.Button (new Rect(Screen.width - Screen.width/sizeDivisor, Screen.height-Screen.height/sizeDivisor,Screen.width/sizeDivisor,Screen.height/sizeDivisor),"Cast Spell")){
			castwindow = true;
		}
		
		if (castwindow) {
			//GUI.color = Color.white;
			//textwin = GUI.Window(0,textwin,DoMyWindow,"Spell Window");
			DrawSpell();
			Time.timeScale = 1;
  		    AudioListener.pause = true;
		}
	}
	
	public void DrawSpell() {
		Event e = Event.current;
		if (e.type == EventType.MouseDown) {
			mousedown = true;

			dw.DrawOrNot();
		}
		
		if (mousedown) {
			Vector2 curr = new Vector2(Input.mousePosition.x , Input.mousePosition.y);
		    points.Add(curr);
			//ScreenCoordinates ();		
		}
		
		if (e.type == EventType.MouseUp) {
			mousedown = false;
			dw.DrawOrNot();
			Debug.Log (points.Count);
			GestureRecognizer.startRecognizer(points);
			points.Clear();
		}
	}
	
	IEnumerator ScreenCoordinates () {
	    // fix world coordinate to the viewport coordinate
	    Vector3 screenSpace = Camera.main.WorldToScreenPoint(go.transform.position);
    	
	    //while (Input.GetMouseButton(0))
	    //{
		    Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
		    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace); 
		    go.transform.position = curPosition;
		    //yield return 0;
	    //}
		yield return 0;
    }
	
	public void DoMyWindow(int windowID) {
		//if (GUI.Button (new Rect (10,10,10,10),"Hello")) {
			
		//}
	}
}
