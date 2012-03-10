	using UnityEngine;
using System.Collections;

public class CastSpell {

	public Rect textwin =  new Rect (10,10,Screen.width - 20,Screen.height - 20);
	private bool castwindow = false;
	public int sizeDivisor = 6;
	public Material mat;
	public Mesh aMesh;
	
	public CastSpell () {
		assignMesh();
	}
	
	public void DrawGUI () {
		if (GUI.Button (new Rect(Screen.width - Screen.width/sizeDivisor, Screen.height-Screen.height/sizeDivisor,Screen.width/sizeDivisor,Screen.height/sizeDivisor),"Cast Spell")){
			Debug.Log ("spell cast");
			castwindow = true;
		}
		if (castwindow) {
			GUI.color = Color.white;
			textwin = GUI.Window(0,textwin,DoMyWindow,"Spell Window");
			DrawSpell();
			Time.timeScale = 1;
  		    AudioListener.pause = true;
		}
	}
	
	public Vector3[] newVertices;
	public Vector2[] newUV;
	public int[] newTriangles;
	
	void assignMesh() {
		//aMesh = new Mesh();
		//GetComponent<MeshFilter>.aMesh = aMesh;
		//aMesh.vertices = newVertices;
		//aMesh.uv = newUV;
		//aMesh.triangles = newTriangles;
	}
	
	public void DrawSpell() {
		Event e = Event.current;
		if (e.type == EventType.MouseDown) {
			//GL.PushMatrix();
			//mat.SetPass(0);
			//GL.LoadOrtho();
			//GL.Begin(GL.LINES);
			//GL.Color(new Color(1,1,1,1));
			//GL.Vertex3 (Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z);
			//Graphics.DrawMesh(aMesh,Input.mousePosition,Quaternion.identity,mat,0);			
		}
		if (e.type == EventType.MouseUp) {
			//GL.End ();
			//GL.PopMatrix();
		}
	}
	
	public void DoMyWindow(int windowID) {
		//if (GUI.Button (new Rect (10,10,10,10),"Hello")) {
			
		//}
	}
	
}
