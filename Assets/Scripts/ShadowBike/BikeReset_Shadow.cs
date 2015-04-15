using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class BikeReset_Shadow : MonoBehaviour {
	
	private float diffX;
	private float diffY;
	private float diffZ;
	public GameObject Cam;
	public GameObject bike;
	public GameObject ShadowBike;
	int i;
	float [,] saveGhost;
	float [,] playGhost;
	bool ghostPlayerOn;
	Vector3 initialPosition;
	Vector3 initialRotation;
	GUIStyle timerStyle;

	float startTime;
	float elapsedTime;
	
	float currentBestTime;
	float buttonSize;
	
	public Texture2D b6,b7,b8;
	
	Font chi640, chi320;

	void Start () {
		
		chi640 = (Font)Resources.Load("Fonts/chinonmn640", typeof(Font));
		chi320 = (Font)Resources.Load("Fonts/chinonmn320", typeof(Font));
	
		/*GameObject CACAT = GameObject.Find ("CACAT");
		CACAT.renderer.material.mainTexture = texturaTest;
		CACAT.renderer.material.mainTextureScale = new Vector2 (1.5f, 1);*/
		Time.timeScale = 1.0F;
		saveGhost = new float[15,15000];
		playGhost = new float[15,15000];
		
		//initialPosition = new Vector3 (0f, 8.41f, 0);
		//initialRotation = new Vector3 (0, 0, 17.3f);
		initialPosition = new Vector3 (0, 4, 0);
		initialRotation = new Vector3 (0, 0, 0);
		//bike.transform.position = initialPosition;
		//bike.transform.eulerAngles = initialRotation;
		
		UnityEngine.Object obj = Instantiate(Resources.Load("Prefabs/ShadowBike"));
		obj.name = "ShadowBike";
		ShadowBike = GameObject.Find ("ShadowBike");
		ShadowBike.transform.position = new Vector3 (0, 0, -100);
		//Debug.Log(ShadowBike.ToString());
		readGhostPlayer();
		
		ghostPlayerOn = true;
		ShadowBike.transform.position = new Vector3 (playGhost[0,0], playGhost[0,1], 0.5f);
		ShadowBike.transform.eulerAngles = new Vector3 (0, 0, playGhost[0,2]);
		i = 0;
		
		diffX = ShadowBike.transform.position.x - Cam.transform.position.x;
		diffY = ShadowBike.transform.position.y - Cam.transform.position.y;
		diffZ = ShadowBike.transform.position.z - Cam.transform.position.z;
		
		startTime = Time.time;
		currentBestTime = PlayerPrefs.GetFloat(StaticData.selectedLevelName + "_bestTime");
		
		timerStyle  = new GUIStyle();
		//if(Screen.height == 640)
			timerStyle.font = chi640;
		//else
		//	timerStyle.font = chi320;
		timerStyle.normal.textColor = Color.white;
		timerStyle.fontSize = Screen.height/24;
	}
	
	void OnGUI(){
		
		GUI.Label (new Rect (Screen.width - 110, 10, 100, 50), elapsedTime.ToString("#.##"), timerStyle);
		GUI.Label (new Rect (10, 10, 120, 50), StaticData.points.ToString(), timerStyle);
		if (Time.timeScale == 0.0F)
		{
				GUI.Box(new Rect(Screen.height/2 - 150, 150, 600,300), "Game Finished!");
				if(GUI.Button(new Rect(Screen.height/2 + 250,250, buttonSize +30, buttonSize+30),b6,""))
					{
				Application.Quit();
			}
				if(GUI.Button(new Rect(Screen.height/2 - 50,250, buttonSize+30, buttonSize+30),b7,""))
				{
				Application.LoadLevel("gameMenu");
			}
				if(GUI.Button(new Rect(Screen.height/2 + 100,250, buttonSize+30, buttonSize+30),b8,""))
				{}
		}
	}

	void FixedUpdate (){
		if(i == 1){
			StaticData.resetPlayScene = false;
		}
		/*
		//save ghost player
		saveGhost[0,i] = bike.transform.position.x;
		saveGhost[1,i] = bike.transform.position.y;
		saveGhost[2,i] = bike.transform.eulerAngles.z;
		saveGhost[3,i] = bike.transform.Find("WheelBack").transform.position.x;
		saveGhost[4,i] = bike.transform.Find("WheelBack").transform.position.y;
		saveGhost[5,i] = bike.transform.Find("WheelBack").transform.eulerAngles.z;
		saveGhost[6,i] = bike.transform.Find("WheelFront").transform.position.x;
		saveGhost[7,i] = bike.transform.Find("WheelFront").transform.position.y;
		saveGhost[8,i] = bike.transform.Find("WheelFront").transform.eulerAngles.z;
		saveGhost[9,i] = bike.transform.Find("backPieceTextureContainer").transform.position.x;
		saveGhost[10,i] = bike.transform.Find("backPieceTextureContainer").transform.position.y;
		saveGhost[11,i] = bike.transform.Find("backPieceTextureContainer").transform.eulerAngles.z;
		saveGhost[12,i] = bike.transform.Find("SuspensionTextureContainer").transform.position.x;
		saveGhost[13,i] = bike.transform.Find("SuspensionTextureContainer").transform.position.y;
		saveGhost[14,i] = bike.transform.Find("SuspensionTextureContainer").transform.eulerAngles.z;
		*/
		i++;
		
		//play ghost player
		
		Debug.Log(ghostPlayerOn.ToString());
		if (ghostPlayerOn){
			ShadowBike.transform.position = new Vector3 (playGhost[0,i], playGhost[1,i], ShadowBike.transform.position.z);
			ShadowBike.transform.eulerAngles = new Vector3 (ShadowBike.transform.eulerAngles.x, ShadowBike.transform.eulerAngles.y, playGhost[2,i]);
			ShadowBike.transform.Find("WheelBack").transform.position = new Vector3 (playGhost[3,i], playGhost [4,i], ShadowBike.transform.Find("WheelBack").transform.position.z);
			ShadowBike.transform.Find("WheelBack").transform.eulerAngles = new Vector3 (ShadowBike.transform.Find("WheelBack").transform.eulerAngles.x, ShadowBike.transform.Find("WheelBack").transform.eulerAngles.y, playGhost [5,i]);
			ShadowBike.transform.Find("WheelFront").transform.position = new Vector3 (playGhost[6,i], playGhost [7,i], ShadowBike.transform.Find("WheelFront").transform.position.z);
			ShadowBike.transform.Find("WheelFront").transform.eulerAngles = new Vector3 (ShadowBike.transform.Find("WheelFront").transform.eulerAngles.x, ShadowBike.transform.Find("WheelFront").transform.eulerAngles.y, playGhost [8,i]);
			ShadowBike.transform.Find("backPieceTextureContainer").transform.position = new Vector3 (playGhost[9,i], playGhost [10,i], ShadowBike.transform.Find("backPieceTextureContainer").transform.position.z);
			ShadowBike.transform.Find("backPieceTextureContainer").transform.eulerAngles = new Vector3 (ShadowBike.transform.Find("backPieceTextureContainer").transform.eulerAngles.x, ShadowBike.transform.Find("backPieceTextureContainer").transform.eulerAngles.y, playGhost [11,i]);
			ShadowBike.transform.Find("SuspensionTextureContainer").transform.position = new Vector3 (playGhost[12,i], playGhost [13,i], ShadowBike.transform.Find("SuspensionTextureContainer").transform.position.z);
			ShadowBike.transform.Find("SuspensionTextureContainer").transform.eulerAngles = new Vector3 (ShadowBike.transform.Find("SuspensionTextureContainer").transform.eulerAngles.x, ShadowBike.transform.Find("SuspensionTextureContainer").transform.eulerAngles.y, playGhost [14,i]);
		}
		
	}

	// Update is called once per frame
	void Update () {
		
		Cam.transform.position = new Vector3 (ShadowBike.transform.position.x - diffX - 1, ShadowBike.transform.position.y - diffY, ShadowBike.transform.position.z  - diffZ);

		
		buttonSize = Screen.height / 8;
		
		elapsedTime = Time.time - startTime;
		/*
		if(bike.transform.position.y < -30)
			resetBike();	
	*/
	}

	void OnTriggerEnter(Collider other) {
	/*	if(!other.name.Contains("FinishLine"))
        	resetBike();
    */
    }
	
	void OnTriggerExit (Collider other){
		if(other.name.Contains("FinishLine")){
			finishLevel();
		}
	}
	
	void finishLevel(){
		if(elapsedTime < currentBestTime || currentBestTime == 0){
			PlayerPrefs.SetFloat(StaticData.selectedLevelName + "_bestTime", elapsedTime);
			saveGhostPlayer();
			Time.timeScale = 0.0F;
	
		}
	}

	void resetBike(){
	/*
		bike.transform.position = initialPosition;
		bike.transform.eulerAngles = initialRotation;
		bike.rigidbody.velocity = Vector3.zero;
		bike.rigidbody.angularVelocity = Vector3.zero;
		bike.GetComponent<MoveBike>().powerupStartTime -=10;
	*/	
		saveGhost = new float[15,15000];
	
		
		playGhost = new float[15,15000];
		
		//read ghost player from file
		readGhostPlayer();
		
		//set the ghost player initial position
		
		i=0;
		
		ghostPlayerOn = true;
		ShadowBike.transform.position = new Vector3 (playGhost[0,0], playGhost[0,1], 0.5f);
		ShadowBike.transform.eulerAngles = new Vector3 (0, 0, playGhost[0,2]);
		
		StaticData.resetPlayScene = true;
		startTime = Time.time;
		
		StaticData.points = 0;
		/*
		bike.GetComponent<WheelColliderOrientation>().flipRotation = 0;
		bike.GetComponent<WheelColliderOrientation>().timeSpendInAir = 0;
		bike.GetComponent<WheelColliderOrientation>().backWheelieTime = 0;
		bike.GetComponent<WheelColliderOrientation>().frontWheelieTime = 0;
		*/	
	}
	
	//save ghost player to file
	void saveGhostPlayer(){
		using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/ghostPlayer_" + StaticData.selectedLevelName + ".txt")){
        	for (int j=0; j<i; j++){
				file.WriteLine(saveGhost[0,j] + " " + saveGhost[1,j] + " " + saveGhost[2,j] 
					+ " " + saveGhost[3,j] + " " + saveGhost[4,j] + " " + saveGhost[5,j] 
					+ " " + saveGhost[6,j] + " " + saveGhost[7,j] + " " + saveGhost[8,j]
					+ " " + saveGhost[9,j] + " " + saveGhost[10,j] + " " + saveGhost[11,j] 
					+ " " + saveGhost[12,j] + " " + saveGhost[13,j] + " " + saveGhost[14,j]);
			}
		}
	}
	
	void readGhostPlayer(){
		i = 0;
		
		Debug.Log("PlayGhost");
		try{
			using (StreamReader sr = new StreamReader(Application.persistentDataPath + "/ghostPlayer_" + StaticData.selectedLevelName + ".txt")){
				string line;
				while ((line = sr.ReadLine()) != null){
					string[] comp = line.Split(' ');
				//	Debug.Log(float.Parse(comp[0]).ToString());
					playGhost[0,i] = float.Parse(comp[0]);
					playGhost[1,i] = float.Parse(comp[1]);
					playGhost[2,i] = float.Parse(comp[2]);
					playGhost[3,i] = float.Parse(comp[3]);
					playGhost[4,i] = float.Parse(comp[4]);
					playGhost[5,i] = float.Parse(comp[5]);
					playGhost[6,i] = float.Parse(comp[6]);
					playGhost[7,i] = float.Parse(comp[7]);
					playGhost[8,i] = float.Parse(comp[8]);
					playGhost[9,i] = float.Parse(comp[9]);
					playGhost[10,i] = float.Parse(comp[10]);
					playGhost[11,i] = float.Parse(comp[11]);
					playGhost[12,i] = float.Parse(comp[12]);
					playGhost[13,i] = float.Parse(comp[13]);
					playGhost[14,i] = float.Parse(comp[14]);
					i++;
				}
			}
		}
		
		catch (Exception e)
		{
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
	}
}