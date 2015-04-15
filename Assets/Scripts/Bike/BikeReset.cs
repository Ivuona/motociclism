using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class BikeReset : MonoBehaviour {
	GameObject finish;
	public GameObject bike;
	int i,fin, ceva, limitaSalvare, lastI;
	int saveI = 0;
	int CheckpointPoints = 0;
	public Texture Costum2, Costum3, Costum1, Casca1,Casca2,Casca3;
	public GameObject Casca, Costum;
	public static bool resetare = false;
	float [,] saveGhost;
	float [,] playGhost;
	bool ghostPlayerOn;
	Vector3 initialPosition;
	Vector3 initialRotation;
	GUIStyle timerStyle;
	bool check=false;
	bool didDie = false;
	bool didFin = false;
	float startTime;
	float elapsedTime;
	
	float currentBestTime;
	float buttonSize,x,y;
	public GUIStyle butonSalvareStyle, butonBackStyle, butonExitStyle, butonNextStyle, ShopStyle;
	public Texture2D bgPause, butonSalvareGri, bgEnd;
	
	Font chi640, chi320;

	void Start () {
		
		chi640 = (Font)Resources.Load("Fonts/chinonmn640", typeof(Font));
		chi320 = (Font)Resources.Load("Fonts/chinonmn320", typeof(Font));
				
		if (MoveBike.costumModel == 1){
			Casca.renderer.material.mainTexture = Casca1;
			Costum.renderer.material.mainTexture = Costum1;
		}
		else if (MoveBike.costumModel == 2){
			Casca.renderer.material.mainTexture = Casca2;
			Costum.renderer.material.mainTexture = Costum2;
		}
		else if (MoveBike.costumModel == 3){
			Casca.renderer.material.mainTexture = Casca3;
			Costum.renderer.material.mainTexture = Costum3;
		}
	
		ceva = 0;
		limitaSalvare=0;
		fin=1;

		x=(float)Screen.width/960;
		y=(float)Screen.height/640;
		buttonSize = Screen.height / 8;

		Time.timeScale = 1.0F;
		saveGhost = new float[17,15000];
		playGhost = new float[17,15000];

		initialPosition = new Vector3 (0, 6, 0);
		initialRotation = new Vector3 (0, 0, 0);
		bike.transform.position = initialPosition;
		bike.transform.eulerAngles = initialRotation;
		
		i = 0;
		
		startTime = Time.time;
		currentBestTime = PlayerPrefs.GetFloat(StaticData.selectedLevelName + "_bestTime");
		
		timerStyle  = new GUIStyle();
		//print(Screen.height + " Screen.height ");
		//if(Screen.height == 640)
			timerStyle.font = chi640;
		//else
		//	timerStyle.font = chi320;
		timerStyle.normal.textColor = Color.white;
	}
	
	void OnGUI(){
		
		GUI.Label (new Rect (Screen.width - 110*x, 17*y, 100*x, 50*y), elapsedTime.ToString("#.##"), timerStyle);
		GUI.Label (new Rect (10*x, 17*y, 120*x, 50*y), StaticData.points.ToString(), timerStyle);
		
		if (Time.timeScale == 0.0F && fin == 0){
			GUI.DrawTexture (new Rect(0,0, Screen.width, Screen.height), bgEnd);
			GUI.DrawTexture (new Rect(Screen.height/2 - 150*x, 140*y, 600*x,380*y), bgPause);
			if(GUI.Button(new Rect(Screen.height/2 + 250*x,220*y, 160*x, 210*y),"",butonNextStyle)){
				for (int i=0; i<StaticData.nrLevels;i++){
					if (StaticData.selectedLevelName == "level"+ i){
						//StaticData.points = 0;
						//StaticData.selectedLevelName = "level"+(i+1);
						//Application.LoadLevel("playScene");
						MenuGame.levelLoad = "level"+(i+1);
						Application.LoadLevel("LoadingPlay");
						Debug.Log("level"+(i+1).ToString());
						break;
					}	
				}
			}
			if(GUI.Button(new Rect(10*x, Screen.height/2 + 230*y , 180*x, 50*y),"", ShopStyle)){
				MenuGame.selectLevel = 3;
				MenuGame.levelLoad = "Menu";
				Application.LoadLevel("LoadingPlay");	
			}
			if(GUI.Button(new Rect(Screen.height/2 - 95*x,220*y, 160*x, 210*y),"", butonBackStyle)){
				Application.LoadLevel("loadingPlay");
				MenuGame.levelLoad = "Menu";
			}
			GUI.DrawTexture(new Rect(Screen.height/2 + 80*x,220*y, 160*x, 160*y),butonSalvareGri);
			if (limitaSalvare==0){
				if(GUI.Button(new Rect(Screen.height/2 + 80*x,220*y, 160*x, 250*y),"",butonSalvareStyle)){
					for(int w=0;w<20;w++){
						print ("w= " + w + "PlayerPrefs.GetIn " + PlayerPrefs.GetInt("R"+w,-1));
						if(PlayerPrefs.GetInt("R"+w,-1) == -1){
							print ("okw");
							PlayerPrefs.SetInt("R"+w,StaticData.replaymax);
							print ("w= " + w);
							break;
						}
					}
					PlayerPrefs.Save();
					saveGhostPlayer();
					StaticData.replaymax++;
					print ("StaticData.replaymax " + StaticData.replaymax);
					limitaSalvare=1;
					print ("button pressed");
				}
			}    
		}
	}

	void FixedUpdate (){
		if(i == 1 || i == saveI + 1) {
			StaticData.resetPlayScene = false;
		}
		if (resetare == true)
			resetare = false;
		//save ghost player
		if(i<=14900){
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
			saveGhost[15,i] = MoveBike.animatiek;
			saveGhost[16,i] = MoveBike.costumModel;
			lastI = i;
		}
		i++;
		
	}

	// Update is called once per frame
	void Update () {
//		print(StaticData.resetPlayScene + " reset");
		checkPointTrigger();
		if(!didFin)
			finishTrigger();
		elapsedTime = Time.time - startTime;
		if(bike.transform.position.y < -30)
			resetBike();	
	}

	void OnTriggerEnter(Collider other) {
		print("collider");
		if(other.name.Contains("CheckPoint"))
		{
			CheckPointDone();
			print("checkpoint");
		}
		else
			if (other.name.Contains("Coin"))
		{
			print("coin");
		}
		
		else
			if(!other.name.Contains("FinishLine"))
        	resetBike();
    }
	
	/*void OnTriggerExit (Collider other){
		if(other.name.Contains("FinishLine")){
			StaticData.Final = true;
			finishLevel();
		}
	}*/
	
	void finishTrigger(){
		if(GameObject.FindGameObjectWithTag("Finish")){
		finish = GameObject.FindGameObjectWithTag("Finish");
		if (bike.transform.position.x >= finish.transform.position.x){
			StaticData.Final = true;
			finishLevel();
			didFin = true;
		}
		}
	}
	
	void checkPointTrigger(){
		if(StaticData.hitCheckPoint){
			CheckPointDone();
			StaticData.hitCheckPoint = false;
		}
	}
	
	void finishLevel(){
		for (int i=0;i<StaticData.nrLevels+1;i++){
			if (StaticData.selectedLevelName == "level"+ i.ToString()){
				if(StaticData.points >= PlayerPrefs.GetInt("Level"+i+"Points",0)){
					PlayerPrefs.SetInt("Level"+i+"Points", StaticData.points);
					PlayerPrefs.SetFloat("Level"+i+"Time", elapsedTime);
					//FacebookBinding.postMessage("My new Highscore at Trial Bike 2 is: " + elapsedTime);
					//saveGhostPlayer();
							Debug.Log(PlayerPrefs.GetInt("Level"+i+"Points",0) + "   "+ PlayerPrefs.GetFloat("Level"+i+"Time",0));
				}
			}
		}
			fin=0;
			Time.timeScale = 0.0F;	
	}

	void resetBike(){
		didDie = true;
		bike.transform.position = initialPosition;
		bike.transform.eulerAngles = initialRotation;
		bike.rigidbody.velocity = Vector3.zero;
		bike.rigidbody.angularVelocity = Vector3.zero;
		bike.GetComponent<MoveBike>().powerupStartTime -=10;
		
		StaticData.points = CheckpointPoints;
		bike.GetComponent<WheelColliderOrientation>().flipRotation = 0;
		bike.GetComponent<WheelColliderOrientation>().lastRotation = 0;
		bike.GetComponent<WheelColliderOrientation>().timeSpendInAir = 0;
		bike.GetComponent<WheelColliderOrientation>().backWheelieTime = 0;
		bike.GetComponent<WheelColliderOrientation>().frontWheelieTime = 0;
		
		if(!check)
		{
		saveGhost = new float[17,15000];
		playGhost = new float[17,15000];
		
		//read ghost player from file
		//readGhostPlayer();
		//set the ghost player initial position
		
		i=0;
		
		StaticData.resetPlayScene = true;
		startTime = Time.time;
		
		StaticData.points = 0;
		}
	}
	
	//save ghost player to file
	void saveGhostPlayer(){
		print ("saveghostpleyrr");
		using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/ghostPlayer_level" + (StaticData.replaymax).ToString() + ".txt")){
        	file.WriteLine(StaticData.selectedLevelName);
			for (int j=0; j<lastI; j++){
				file.WriteLine(saveGhost[0,j] + " " + saveGhost[1,j] + " " + saveGhost[2,j] 
					+ " " + saveGhost[3,j] + " " + saveGhost[4,j] + " " + saveGhost[5,j] 
					+ " " + saveGhost[6,j] + " " + saveGhost[7,j] + " " + saveGhost[8,j]
					+ " " + saveGhost[9,j] + " " + saveGhost[10,j] + " " + saveGhost[11,j] 
					+ " " + saveGhost[12,j] + " " + saveGhost[13,j] + " " + saveGhost[14,j]+ " " + saveGhost[15,j] + " "+ saveGhost[16,j]);
			}
		}
		PlayerPrefs.SetInt("Max",StaticData.replaymax+1);
	}
	
	public void  CheckPointDone(){
		initialPosition=bike.transform.position;
		//initialRotation=bike.transform.eulerAngles;
		check=true;
		CheckpointPoints = StaticData.points;
		saveI = i;
	}
}