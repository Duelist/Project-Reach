using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(MeshFilter))]

public class Drawing : MonoBehaviour{
    public float height = 2.0f;
    public float time = 0.1f;
    public int sectionCount = 50;
    public bool alwaysUp = false;
    public float minDistance = 0.1f;
 
    public Color startColor = Color.white;
    public Color endColor = new Color(1.0f, 1.0f, 1.0f, 0);
 	int x =0;
		int y = 0;
    private int head = 0;
    private float ticker = 0;
 
    private class TronTrailSection{
        public Vector3 point;
        public Vector3 upDir;
    }
 
    private TronTrailSection[] sections;
    private Vector3[] vertices;
    private Color[] colors;
    private Vector2[] uv;
    private int[] triangles;
    private TronTrailSection currentSection;
	
	public bool candraw = false;
  
	public void DrawOrNot() {
		candraw = !candraw;
	}

	public void Start(){
        sections = new TronTrailSection[sectionCount];
        for (int i = 0; i < sectionCount; i++){
            sections[i] = new TronTrailSection();
        }
        vertices = new Vector3[sectionCount * 2];
        colors = new Color[sectionCount * 2];
        uv = new Vector2[sectionCount * 2];
        triangles = new int[(sectionCount - 1) * 2 * 3];
    }
	
	IEnumerator ScreenCoordinates () {
	    // fix world coordinate to the viewport coordinate
	    Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);
    	
	    while (Input.GetMouseButton(0))
	    {
		    Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20);
		    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace); 
		    transform.position = curPosition;
		    yield return 0;
	    }
    }
	
	public void LateUpdate(){
		if (candraw) {
			//transform.position = new Vector3(Input.mousePosition.x,Input.mousePosition.y,20);
			StartCoroutine(ScreenCoordinates());
		}
        ticker += Time.deltaTime;
    	if (ticker > time){
            ticker -= time;
            sections[head].point = transform.position;
            if (alwaysUp)
                sections[head].upDir = Vector3.left;
            else
                sections[head].upDir = transform.TransformDirection(Vector3.left);
 
            head++;
            if (head >= sectionCount){
                head = 0;
            }
        }
 
        // Rebuild the mesh
        Mesh mesh = (GetComponent(typeof(MeshFilter)) as MeshFilter).mesh;
        mesh.Clear();
 
        // Use matrix instead of transform.TransformPoint for performance reasons
        Matrix4x4 localSpaceTransform = transform.worldToLocalMatrix;
 
        int j = head-1;
        if (j < 0)
            j = sectionCount - 1;
 
        for(int i=0;i<sectionCount;i++){
        //foreach (TronTrailSection currentSection in sections){
            // Calculate u for texture uv and color interpolation
            currentSection = sections[j];
 
            float u = (float)i / (float)sectionCount;// 0.0f;
 
            // Calculate upwards direction
            Vector3 upDir = currentSection.upDir;
 
            // Generate vertices
            vertices[i * 2 + 0] = localSpaceTransform.MultiplyPoint(currentSection.point);
            vertices[i * 2 + 1] = localSpaceTransform.MultiplyPoint(currentSection.point + upDir * height);
 
            uv[i * 2 + 0].x = u;
            uv[i * 2 + 0].y = 0;
            uv[i * 2 + 1].x = u;
            uv[i * 2 + 1].y = 1.0f;
 
            // fade colors out over time
            Color interpolatedColor = Color.Lerp(startColor, endColor, u);
            colors[i * 2 + 0] = interpolatedColor;
            colors[i * 2 + 1] = interpolatedColor;
 
            j--;
            if (j < 0)
                j = sectionCount - 1;
        }
 
        // Generate triangles indices
        for (int i = 0; i < triangles.Length / 6; i++){
            triangles[i * 6 + 0] = i * 2;
            triangles[i * 6 + 1] = i * 2 + 1;
            triangles[i * 6 + 2] = i * 2 + 2;
 
            triangles[i * 6 + 3] = i * 2 + 2;
            triangles[i * 6 + 4] = i * 2 + 1;
            triangles[i * 6 + 5] = i * 2 + 3;
        }
 
        // Assign to mesh   
        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}