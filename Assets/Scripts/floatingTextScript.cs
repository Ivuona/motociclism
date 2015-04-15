using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class floatingTextScript : MonoBehaviour {
	
	public string textToDisplay = "blabla";
	public float yOffset = 0;
	
	float startTime;
	float yPosition;
	
	float textAlpha;
	
	GUIStyle floatingTextStyle;
	
	Font chi640;
	
	// Use this for initialization
	void Start () {
		
		chi640 = (Font)Resources.Load("Fonts/chinonmn640", typeof(Font));
		
		textAlpha = 1;
		
		startTime = Time.time;
		yPosition = Screen.height/3 + yOffset;
		
		floatingTextStyle  = new GUIStyle();
		floatingTextStyle.font = chi640;
		floatingTextStyle.normal.textColor = new Color(1, 0, 0, textAlpha);
		floatingTextStyle.fontSize = Screen.height/24;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - startTime > 4){
			Destroy(gameObject);
		}
	}
	
	void FixedUpdate(){
		yPosition -= 1;
		textAlpha -= 0.007f;
		floatingTextStyle.normal.textColor = new Color(1, 0, 0, textAlpha);
	}
	
	void OnGUI(){
		GUI.Label (new Rect (3 * Screen.width / 9 , yPosition, 500, 50), textToDisplay, floatingTextStyle);
	}
}
