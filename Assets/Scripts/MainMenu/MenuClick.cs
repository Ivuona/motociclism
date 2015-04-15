using UnityEngine;
using System.Collections;

public class MenuClick : MonoBehaviour {
	
	float buttonSize;
	public Texture2D playButton, quitButton;
	bool selectLevel;
	
	void Start(){
		selectLevel = false;
	}
	
	//use gui to create the click-able buttons;
	void OnGUI(){
		buttonSize = Screen.height / 5;
		if(!selectLevel){
			if(GUI.Button(new Rect(Screen.width/2 - buttonSize/2, Screen.height/2 - buttonSize - 10, buttonSize, buttonSize),playButton)){
				selectLevel = true;
			}
			if(GUI.Button(new Rect(Screen.width/2 - buttonSize/2, Screen.height/2 + 10, buttonSize, buttonSize),quitButton)){
				Application.Quit();
			}
		}
		else{
			if(GUI.Button(new Rect(10, 10, buttonSize, buttonSize),"testLevel")){
				StaticData.points = 0;
				StaticData.selectedLevelName = "testLevel";
				Application.LoadLevel("playScene");
			}
			if(GUI.Button(new Rect(10, buttonSize + 20, buttonSize, buttonSize),"level1")){
				StaticData.points = 0;
				StaticData.selectedLevelName = "level1";
				Application.LoadLevel("playScene");
			}
			if(GUI.Button(new Rect(10, 2*buttonSize + 30, buttonSize, buttonSize),"level2")){
				StaticData.points = 0;
				StaticData.selectedLevelName = "level2";
				Application.LoadLevel("playScene");
			}
			if(GUI.Button(new Rect(10, 3*buttonSize + 40, buttonSize, buttonSize),"level3")){
				StaticData.points = 0;
				StaticData.selectedLevelName = "level3";
				Application.LoadLevel("playScene");
			}
			if(GUI.Button(new Rect(200, buttonSize + 20, buttonSize, buttonSize),"yahoo")){
				StaticData.points = 0;
				StaticData.selectedLevelName = "yahoo";
				Application.LoadLevel("playScene");
			}
			if(GUI.Button(new Rect(200, 2*buttonSize + 30, buttonSize, buttonSize),"levelGodLike")){
				StaticData.points = 0;
				StaticData.selectedLevelName = "levelGodLike";
				Application.LoadLevel("playScene");
			}
			if(GUI.Button(new Rect(10, 4*buttonSize + 50, buttonSize, buttonSize),"back")){
				selectLevel = false;
			}
		}
	}
	
	//in update we check for touches inside the button rects;
	void Update () {

	}
	
	
	
}
