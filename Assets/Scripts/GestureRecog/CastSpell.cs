using UnityEngine;
using System.Collections;

public class TrailSection {
	public Vector3 point;
	public Vector3 upDir;
	public float time;
}

public class CastSpell {

	static GameObject mainscene;
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
		mainscene = GameObject.Find ("Scenes/MainScene");
		
		points = new ArrayList();
		go = GameObject.Find("Draw");
		dw = go.GetComponent<Drawing>();
	}
	
	public void DrawGUI () {
		if (GUI.Button (new Rect(Screen.width - Screen.width/sizeDivisor, Screen.height-Screen.height/sizeDivisor,Screen.width/sizeDivisor,Screen.height/sizeDivisor),"Cast Spell")){
			//Debug.Log ("spell cast");
			castwindow = true;
			//Vector3 screenSpace = Camera.main.WorldToScreenPoint(mainscene.transform.position);
		    //Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
		    //Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace); 
		    //mainscene.transform.position = curPosition;
			
			//GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);			
			//drawtex = new Texture2D(texwidth, texheight);
			//plane.renderer.material.mainTexture = drawtex;
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
			//EraseDrawing(drawtex);
			//brush = new Color[brushwidth * brushheight];
			//for (int i = 0; i < brush.Length; i++) {
			//	brush[i] = Color.black;
   			//}
			dw.DrawOrNot();
		}
		
		if (mousedown) {
			Vector2 curr = new Vector2(Input.mousePosition.x , Input.mousePosition.z);
		    points.Add(curr);
		    //StartCoroutine("ScreenCoordinates");
			//ScreenCoordinates();	
			//Vector3 screenSpace = Camera.main.WorldToScreenPoint(mainscene.transform.position);
		   	//Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
		   	//Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace); 
		    //mainscene.transform.position = curPosition;
			//RaycastHit hit; 
   
  			//Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition); 
    
  			//if (Physics.Raycast(camRay, hit) && Input.GetMouseButton(0)) { 
		    // 	int x = hit.textureCoord.x * texwidth; 
	      	//	int y = hit.textureCoord.y * texheight; 
       
	      	//	if ((x != oldX) || (y != oldY)) { 
		    //    	transTex.SetPixels(x, y, brushwidth, brushheight, brush); 
		    //     	transTex.Apply(); 
		    //     	oldx = x; 
		    //     	oldy = y; 
	        //	} 
   			//}
		
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
