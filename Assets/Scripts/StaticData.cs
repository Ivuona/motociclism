using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StaticData : MonoBehaviour {
	
	public static Dictionary<int, string> platformTypes = new Dictionary<int, string>();
	public static string selectedLevelName;
	public static bool resetPlayScene;
	public static bool Final = false;
	static bool started = false;
	public static int points = 0;
	public static int replayName = -1,replaymax = -1;
	public static int nrLevels = 17;
	public static bool hitCheckPoint = false;
	public static bool firstStart = true;
	// manualy add to dictionary every platform type, and a coresponding key. And add buttons for adding each of them. Rest is done automaticaly
	
	// Use this for initialization
	void Awake () {
		if(!started){
			platformTypes.Add (1, "StaticPlatform");
			platformTypes.Add (2, "HorizontalPlatform");
			platformTypes.Add (3, "VerticalPlatform");
			platformTypes.Add (4, "RotatePlatform");
			platformTypes.Add (5, "CrossPlatform");
			platformTypes.Add (6, "Ramp1");
			platformTypes.Add (7, "Ramp2");
			platformTypes.Add (8, "Cube1");
			platformTypes.Add (9, "Cube2");
			platformTypes.Add (10, "Cube3");
			platformTypes.Add (11, "Cube4");
			platformTypes.Add (12, "Tire");
			platformTypes.Add (13, "Car");
			platformTypes.Add (14, "Supports");
			platformTypes.Add (15, "FlipPlatform2");
			platformTypes.Add (16, "Wave");
			platformTypes.Add (17, "Wheel");
			platformTypes.Add (18, "PowerUp");
			platformTypes.Add (19, "FinishLine");
			platformTypes.Add (20, "CheckPoint");
			platformTypes.Add (21, "Coin");
			platformTypes.Add (22, "Golf");
			platformTypes.Add (23, "Aston");
		}
		
		selectedLevelName = "testLevel";
		
		resetPlayScene = false;
		
		started = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}