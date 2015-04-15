using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class r2 : MonoBehaviour {
	//public GameObject Maimutoi;
	public static int animk1, costum;
	public GameObject bike;
	public GameObject ShadowBike;
	public Texture Costum2, Costum3, Costum1, Casca1,Casca2,Casca3;
	public GameObject Casca, Costum;
	
	int i, end, lastI;
	float [,] saveGhost;
	float [,] playGhost;
	bool ghostPlayerOn;
	Vector3 initialPosition;
	Vector3 initialRotation;
	GUIStyle timerStyle;

	float startTime;
	float elapsedTime;
	
	float currentBestTime;
	float buttonSize,x,y;
	public Texture2D bgPause;
	public GUIStyle butonSalvareStyle, butonBackStyle, butonExitStyle, buttonFastForwarding, buttonSlowForwarding;
	
	Font chi640, chi320;

	void Start () {
		
		chi640 = (Font)Resources.Load("Fonts/chinonmn640", typeof(Font));
		chi320 = (Font)Resources.Load("Fonts/chinonmn320", typeof(Font));
		
		buttonSize = Screen.height / 6;
		
		
		end=0;
			x=(float)Screen.width/960;
			y=(float)Screen.height/640;
		Time.timeScale = 1.0F;
		//saveGhost = new float[15,15000];
		playGhost = new float[17,15000];
		

		initialPosition = new Vector3 (0, 6, 0);
		initialRotation = new Vector3 (0, 0, 0);
		bike.transform.position = initialPosition;
		bike.transform.eulerAngles = initialRotation;

		readGhostPlayer();
		
		costum = (int)playGhost[16,0];
		
			if (costum == 1)
		{
			Casca.renderer.material.mainTexture = Casca1;
			Costum.renderer.material.mainTexture = Costum1;
		}
			if (costum == 2)
		{
			Casca.renderer.material.mainTexture = Casca2;
			Costum.renderer.material.mainTexture = Costum2;
		}
			if (costum == 3)
		{
			Casca.renderer.material.mainTexture = Casca3;
			Costum.renderer.material.mainTexture = Costum3;
		}
		
		ghostPlayerOn = true;
		bike.transform.position = new Vector3 (playGhost[0,0], playGhost[0,1], 0.1f);
		bike.transform.eulerAngles = new Vector3 (0, 0, playGhost[0,2]);
		i = 0;
		
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
		
		//GUI.Label (new Rect (Screen.width - 110*x, 17*y, 100*x, 50*y), elapsedTime.ToString("#.##"), timerStyle);
		GUI.Label (new Rect (10*x, 17*y, 120*x, 50*y), StaticData.points.ToString(), timerStyle);
		if (Time.deltaTime != 0f && end != 1)
		{
			if(GUI.Button(new Rect(Screen.width - 150*x,Screen.height - buttonSize - 10*y,buttonSize, buttonSize),"",buttonFastForwarding))
				{
					if ( Time.timeScale < 3f)
					Time.timeScale += 0.2f;
				}
			if(GUI.Button(new Rect(50*x,Screen.height - buttonSize - 10*y, buttonSize, buttonSize),"",buttonSlowForwarding))
				{
					if ( Time.timeScale > 0.2f)
					Time.timeScale -= 0.2f;
				}
		}
		if (end==1 )
		{
				GUI.DrawTexture (new Rect(Screen.height/2 - 150*x, 140*y, 600*x,380*y), bgPause);
				if(GUI.Button(new Rect(Screen.height/2 + 250*x,220*y, 150*x, 189*y),"",butonExitStyle))
			{
				Application.LoadLevel("playScene");
			}
				if(GUI.Button(new Rect(Screen.height/2 - 90*x,220*y, 150*x, 189*y),"",butonBackStyle))
				{
				Application.LoadLevel("loadingPlay");	
				MenuGame.levelLoad = "Menu";
			}
				if(GUI.Button(new Rect(Screen.height/2 + 80*x,220*y, 150*x, 189*y),"",butonSalvareStyle))
			{
				Application.LoadLevel("r2");
			}
		}
		
	}

	void FixedUpdate (){
		if(i == 1){
			StaticData.resetPlayScene = false;
		}
		//play ghost player
		//if (/ghostPlayerOn){
		if(i<lastI){
			bike.transform.position = new Vector3 (playGhost[0,i], playGhost[1,i], bike.transform.position.z);
			bike.transform.eulerAngles = new Vector3 (bike.transform.eulerAngles.x, bike.transform.eulerAngles.y, playGhost[2,i]);
			bike.transform.Find("WheelBack").transform.position = new Vector3 (playGhost[3,i], playGhost [4,i], bike.transform.Find("WheelBack").transform.position.z);
			bike.transform.Find("WheelBack").transform.eulerAngles = new Vector3 (bike.transform.Find("WheelBack").transform.eulerAngles.x, bike.transform.Find("WheelBack").transform.eulerAngles.y, playGhost [5,i]);
			bike.transform.Find("WheelFront").transform.position = new Vector3 (playGhost[6,i], playGhost [7,i], bike.transform.Find("WheelFront").transform.position.z);
			bike.transform.Find("WheelFront").transform.eulerAngles = new Vector3 (bike.transform.Find("WheelFront").transform.eulerAngles.x, bike.transform.Find("WheelFront").transform.eulerAngles.y, playGhost [8,i]);
			bike.transform.Find("backPieceTextureContainer").transform.position = new Vector3 (playGhost[9,i], playGhost [10,i], bike.transform.Find("backPieceTextureContainer").transform.position.z);
			bike.transform.Find("backPieceTextureContainer").transform.eulerAngles = new Vector3 (bike.transform.Find("backPieceTextureContainer").transform.eulerAngles.x, bike.transform.Find("backPieceTextureContainer").transform.eulerAngles.y, playGhost [11,i]);
			bike.transform.Find("SuspensionTextureContainer").transform.position = new Vector3 (playGhost[12,i], playGhost [13,i], bike.transform.Find("SuspensionTextureContainer").transform.position.z);
			bike.transform.Find("SuspensionTextureContainer").transform.eulerAngles = new Vector3 (bike.transform.Find("SuspensionTextureContainer").transform.eulerAngles.x, bike.transform.Find("SuspensionTextureContainer").transform.eulerAngles.y, playGhost [14,i]);
			animk1=(int)playGhost[15,i];
			costum = (int)playGhost[16,i];
		i++;
		}
		else{
			end=1;
			Time.timeScale = 0.0F;
			StaticData.Final = true;
			finishLevel();	
		}
		
	}

	// Update is called once per frame
	void Update () {

		elapsedTime = Time.time - startTime;
		
		if(bike.transform.position.y < -30)
			resetBike();	
	}

	void OnTriggerEnter(Collider other) {
		//if(!other.name.Contains("FinishLine"))
        //	resetBike();
    }
	
	void OnTriggerExit (Collider other){
		if(other.name.Contains("FinishLine")){
			end=1;
			Time.timeScale = 0.0F;
			finishLevel();
		}
	}
	
	void finishLevel(){
		if(elapsedTime < currentBestTime || currentBestTime == 0){
			PlayerPrefs.SetFloat(StaticData.selectedLevelName + "_bestTime", elapsedTime);
			//saveGhostPlayer();
			
			//fin=0;
		}
	}

	void resetBike(){

		bike.rigidbody.velocity = Vector3.zero;
		bike.rigidbody.angularVelocity = Vector3.zero;
		bike.GetComponent<MoveBike>().powerupStartTime -=10;
		
		//saveGhost = new float[15,15000];
		playGhost = new float[17,15000];
		
		//read ghost player from file
		readGhostPlayer();
		
		//set the ghost player initial position
		
		i=0;
		
		ghostPlayerOn = true;
		bike.transform.position = new Vector3 (playGhost[0,0], playGhost[0,1], 0.5f);
		bike.transform.eulerAngles = new Vector3 (0, 0, playGhost[0,2]);
		
		StaticData.resetPlayScene = true;
		startTime = Time.time;
		
		StaticData.points = 0;
		bike.GetComponent<WheelColliderOrientation>().flipRotation = 0;
		bike.GetComponent<WheelColliderOrientation>().timeSpendInAir = 0;
		bike.GetComponent<WheelColliderOrientation>().backWheelieTime = 0;
		bike.GetComponent<WheelColliderOrientation>().frontWheelieTime = 0;
	}
	
	
	void readGhostPlayer(){
		i = 0;
		
		try{
			using (StreamReader sr = new StreamReader(Application.persistentDataPath + "/ghostPlayer_level" + StaticData.replayName + ".txt")){
				string line;
				sr.ReadLine();
				while ((line = sr.ReadLine()) != null){
					
					string[] comp = line.Split(' ');
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
					playGhost[15,i] = int.Parse(comp[15]);
					playGhost[16,i] = int.Parse(comp[16]);
					i++;
					lastI = i;
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