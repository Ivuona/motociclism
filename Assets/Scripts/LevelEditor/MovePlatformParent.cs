using UnityEngine;
using System.Collections;

public class MovePlatformParent : MonoBehaviour {
	
	//this is used for dragging / deleting platforms; 
	//active on children of complex platforms;
	
	float diffX;
	float diffY;
	Vector2 initialPos;
	
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
			EditorGUI.SelectedObject = this.transform.parent.name;
			EditorGUI.objWidth = this.transform.parent.transform.localScale.x.ToString();
			EditorGUI.objHeight = this.transform.parent.transform.localScale.y.ToString();
			EditorGUI.objRotation = this.transform.parent.transform.eulerAngles.z.ToString();
			EditorGUI.objDepth = this.transform.parent.transform.localScale.z.ToString();
			
			diffX = this.transform.parent.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
			diffY = this.transform.parent.transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
			
			initialPos = new Vector2 (this.transform.parent.transform.position.x, this.transform.parent.transform.position.y);
		}
	}
	
	void OnMouseDrag() {
		if(active){
			if(!EditorGUI.animate){
				float angleX = Camera.main.transform.eulerAngles.x;
				float angleY = Camera.main.transform.eulerAngles.y;
				if (angleX < 0.1 && angleX >= 0)
					angleX = 0.1f;
				if (angleX >-0.1 && angleX <= 0)
					angleX = -0.1f;
				if (angleY < 0.1 && angleY >= 0)
					angleY = 0.1f;
				if (angleY >-0.1 && angleY <= 0)
					angleY = -0.1f;
				
				float deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x + diffX - initialPos.x;
				float xxx = deltaX / Mathf.Sin ((90 - angleY) * Mathf.PI / 180) - deltaX;
				
				float deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y + diffY - initialPos.y;
				float yyy = deltaY / Mathf.Sin ((90 - angleX) * Mathf.PI / 180) - deltaY;
				
				this.transform.parent.transform.position = new Vector3 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x + diffX + xxx - yyy / Mathf.Sin (angleX * Mathf.PI / 180), Camera.main.ScreenToWorldPoint(Input.mousePosition).y + diffY + yyy - xxx / Mathf.Sin (angleY * Mathf.PI / 180), this.transform.parent.transform.position.z);
			}
		}
	}
}