using UnityEngine;
using System.Collections;

public class RadialMenu {
	public int selectorX;
	public int selectorY;
	private GameObject singleFireTower;
	private GameObject multiFireTower;
	private GameObject radialSelector;
	
	public RadialMenu (){
		radialSelector = GameObject.CreatePrimitive(PrimitiveType.Cube);
		radialSelector.transform.position = new Vector3(-1, 0, -1);
		radialSelector.transform.localScale = new Vector3(1f,0.2f,1f);
		radialSelector.transform.Rotate(0,0,180);
		radialSelector.name = "radialSelector";
		radialSelector.renderer.material.mainTexture = TextureFactory.GetTileSelector();
		radialSelector.renderer.enabled = false;
		
		singleFireTower = GameObject.CreatePrimitive(PrimitiveType.Cube);
		singleFireTower.transform.position = new Vector3(-1, 0, -1);
		singleFireTower.transform.localScale = new Vector3(1f,0.2f,1f);
		singleFireTower.transform.Rotate(0,0,180);
		singleFireTower.name = "createSingle";
		singleFireTower.renderer.material.mainTexture = TextureFactory.GetFireSelectorButton();
		singleFireTower.renderer.enabled = false;
		
		multiFireTower = GameObject.CreatePrimitive(PrimitiveType.Cube);
		multiFireTower.transform.position = new Vector3(-1, 0, -1);
		multiFireTower.transform.localScale = new Vector3(1f,0.2f,1f);
		multiFireTower.transform.Rotate(0,0,180);
		multiFireTower.name = "createMulti";
		multiFireTower.renderer.material.mainTexture = TextureFactory.GetIceSelectorButton();
		multiFireTower.renderer.enabled = false;
	}
	
	public void SetPosition (int tilex, int tiley){
		selectorX = tilex;
		selectorY = tiley;
		
		radialSelector.transform.position = new Vector3(selectorX, 0, selectorY);
		singleFireTower.transform.position = new Vector3(selectorX, 0, selectorY + 1);
		multiFireTower.transform.position = new Vector3(selectorX, 0, selectorY - 1);
	}
	
	public void ShowRadial (){
		radialSelector.renderer.enabled = true;
		singleFireTower.renderer.enabled = true;
		multiFireTower.renderer.enabled = true;
	}
	
	public void HideRadial (){
		radialSelector.renderer.enabled = false;
		singleFireTower.renderer.enabled = false;
		multiFireTower.renderer.enabled = false;
	}

}