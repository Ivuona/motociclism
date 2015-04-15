using UnityEngine;
using System.Collections;

public class MovePlatformSimple : MonoBehaviour {
	
	//this is used for dragging / deleting platforms; 
	//active on simple platforms;
	
	public float deltaX;
	public float deltaY;
	bool active;

	// Use this for initialization
	void Start () {
		if (Application.loadedLevelName == "levelEditor"){
			active = true;
		}
		else{
			active = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown(){
		if(active){
			EditorGUI.SelectedObject = this.name;
			EditorGUI.objWidth = this.transform.localScale.x.ToString();
			EditorGUI.objHeight = this.transform.localScale.y.ToString();
			EditorGUI.objRotation = this.transform.eulerAngles.z.ToString();
			EditorGUI.objDepth = this.transform.localScale.z.ToString();
			
			deltaX = this.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
			deltaY = this.transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

		}
	}
	
	void OnMouseDrag() {
		if(active){
			if(!EditorGUI.animate){
				this.transform.position = new Vector3 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x + deltaX, Camera.main.ScreenToWorldPoint(Input.mousePosition).y + deltaY, this.transform.position.z);
			}
		}
	}
}
