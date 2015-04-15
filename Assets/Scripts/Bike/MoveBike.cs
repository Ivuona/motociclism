using UnityEngine;
using System.Collections;
using System.Threading;

public class MoveBike : MonoBehaviour {
	
	
	public GameObject Maimutoi;
	public static int animatiek, animatiek1,animatiej, costumModel;

	
	public WheelCollider backWheel;
	public WheelCollider frontWheel;
	public float speed;
	public float move = 0;
	public GameObject centerMass;
	public int force, bani, ceva,ceva2,ceva3;
	public GameObject forcePointFront;
	public GameObject forcePointBack;
	public float torque;
	public bool powerUpActive;
	public float powerupStartTime;
	public float powerUpDuration;
	public GUIStyle butonPauzaStyle, butonResumeStyle, butonBackStyle, butonExitStyle;
	GUIStyle myStyle, MoneyStyle;
	public Texture2D baraSusStanga, baraSusDreapta, Moneys;
	
	private Rect topBarLeftRect;
	private Rect topBarRightRect;
	private Rect leanBackRect;
	private Rect leanFrontRect;
	private Rect backRect;
	private Rect frontRect;
	private Rect pauseRect;
	private Rect monezi ;
	private Rect moneziStyle;
	
	private int Acceleratie, Handling, Brake;
	
	int pauza;
	float buttonSize,x,y;
	
	public Texture2D b1,b2,b3,b4, b5, bgPause;
	
	public AudioSource sourceAcceleratie, sourceIdle, sourcePornire, sourceOprire;
	
	bool frontPlayed = false;
    bool backPlayed = false;
	
	Font chi640, chi320;
	
	// Use this for initialization
	void Start () {
		
		chi640 = (Font)Resources.Load("Fonts/chinonmn640", typeof(Font));
		chi320 = (Font)Resources.Load("Fonts/chinonmn320", typeof(Font));
		
		sourcePornire.Play();
        sourceIdle.Play(43000);
		
		if (PlayerPrefs.GetInt("AcceleratieStat", 0) == 0)
				Acceleratie = 3;
		else if (PlayerPrefs.GetInt("AcceleratieStat", 0) == 1)
				Acceleratie = 9;
		else if (PlayerPrefs.GetInt("AcceleratieStat", 0) == 2)
				Acceleratie = 13;
		
		if (PlayerPrefs.GetInt("BrakeStat", 0) == 0)
				Brake = 3;
		else if (PlayerPrefs.GetInt("BrakeStat", 0) == 1)
				Brake = 9;
		else if (PlayerPrefs.GetInt("BrakeStat", 0) == 2)
				Brake = 13;
		
		if (PlayerPrefs.GetInt("HandlingStat", 0) == 0)
				Handling = 3;
		else if (PlayerPrefs.GetInt("HandlingStat", 0) == 1)
				Handling = 7;
		else if (PlayerPrefs.GetInt("HandlingStat", 0) == 2)
				Handling = 11;
		
	//		Debug.Log(PlayerPrefs.GetInt("AcceleratieStat",0) + " " + PlayerPrefs.GetInt("HandlingStat",0) + " " + PlayerPrefs.GetInt("BrakeStat",0));
	
		buttonSize = Screen.height / 6;
		animatiej = 0;
		animatiek=0;
		animatiek1=0;
		x=(float)Screen.width/960;
		y=(float)Screen.height/640;
		Time.timeScale = 1.0F;
		 pauza = 0;
		torque = 200;
		Input.multiTouchEnabled = true;
		powerUpActive = false;
		powerupStartTime = -10;
		myStyle  = new GUIStyle();
		myStyle.font = chi640;
		myStyle.normal.textColor = Color.yellow;
		MoneyStyle = new GUIStyle();
		MoneyStyle.normal.textColor = Color.white;
		MoneyStyle.font = chi640;
		powerUpDuration = 5;
		
		CalculateRectangles();
	}
	void OnGUI(){
		GUI.DrawTexture(topBarLeftRect, baraSusStanga);
		GUI.DrawTexture(topBarRightRect, baraSusDreapta);

		for (int i=0; i<StaticData.nrLevels+1 ; i++){
				if (StaticData.selectedLevelName == "level" + i)
				GUI.Label(new Rect(12*x,56*y, 300*x, 100*y), "Level "+ (i+1), MoneyStyle);
		}

		if  (Time.timeScale != 0f && pauza==0){
			if(GUI.Button (leanBackRect,b1,"")){
			
			}
			if(GUI.Button (leanFrontRect,b2,"")){
					
			}
			if(GUI.Button(backRect,b3,"")){
							
			}
			if(GUI.Button(frontRect,b4,"")){	
					
			}
			if(GUI.Button(new Rect(Screen.width/2 - buttonSize/2,Screen.height - buttonSize - 10*y, buttonSize,buttonSize),"",butonPauzaStyle)){
	
			}
			
		}
		else if  (Time.timeScale == 0.0F && pauza==1){
                frontPlayed = false;
                backPlayed = false;
                sourceIdle.Stop();
                sourceAcceleratie.Stop();
			
				GUI.DrawTexture (new Rect(0,0, Screen.width, Screen.height), b5);
				GUI.DrawTexture (new Rect(Screen.height/2 - 150*x, 140*y, 600*x,380*y), bgPause);
				if(GUI.Button(new Rect(Screen.height/2 + 250*x,220*y, 160*x, 210*y),"",butonExitStyle)){
					Application.LoadLevel("loadingPlay");
					MenuGame.levelLoad = "Menu";
					MenuGame.selectLevel = 0;
				}
				if(GUI.Button(new Rect(Screen.height/2 - 95*x,220*y, 160*x, 210*y),"",butonBackStyle)){
					Application.LoadLevel("loadingPlay");
					MenuGame.levelLoad = "Menu";
				}
				if(GUI.Button(new Rect(Screen.height/2 + 80*x,220*y, 160*x, 210*y),"",butonResumeStyle)){
					Time.timeScale = 1.0F;
					pauza = 0;
					sourceIdle.Play();
				}	
		}
		GUI.DrawTexture(monezi, Moneys);
		GUI.Button(moneziStyle,(PlayerPrefs.GetInt("money",0)).ToString(), MoneyStyle);
		if(powerUpActive){
			GUI.Label (new Rect(12*x,120*y,300*x,60*y), "SpeedActive", myStyle);
		}
	}
	
	void CalculateRectangles(){
		topBarLeftRect = new Rect(0,0,Screen.width/3-(20*x),Screen.height/6);
		topBarRightRect = new Rect(Screen.width- Screen.width/7,0,Screen.width/7,Screen.height/10);
		leanBackRect = new Rect(10*x, Screen.height - buttonSize - 10*y, buttonSize, buttonSize);
		leanFrontRect = new Rect(buttonSize + 50*x,Screen.height - buttonSize - 10*y, buttonSize, buttonSize);
		backRect = new Rect(Screen.width - buttonSize - buttonSize - 50*x,Screen.height - buttonSize - 10*y, buttonSize, buttonSize);
		frontRect = new Rect(Screen.width - buttonSize - 10*x,Screen.height - buttonSize - 10*y, buttonSize, buttonSize);
		//pauseRect = new Rect(Screen.width/2 - buttonSize/2,Screen.height - buttonSize - 10*y, buttonSize, buttonSize);
		monezi = new Rect(140*x,8*y,40*x,40*y);
		moneziStyle = new Rect(185*x,17*y,100*x,40*y);

	}
	
	// Update is called once per frame
	void Update () {	
		
		 if ( StaticData.Final ){
             frontPlayed = true;
             backPlayed = false;
             sourceAcceleratie.Stop();
             sourceIdle.Stop();
             sourceOprire.Play();
             StaticData.Final = false;
        }
		
		if (StaticData.resetPlayScene){
			 frontPlayed = false;
             backPlayed = false;
             sourceIdle.Stop();
             sourceAcceleratie.Stop();
             sourcePornire.Play();
             sourceIdle.Play(30000);
		}
		
		
		powerUpActive = true;
		if(Time.time > powerupStartTime + powerUpDuration){
			powerUpActive = false;
		}
		if(Time.time > powerupStartTime + powerUpDuration-1 && powerUpActive){
			anim(6);
		}
		
		rigidbody.centerOfMass = centerMass.transform.localPosition;
		speed = Mathf.Sqrt ( (this.rigidbody.velocity.x * this.rigidbody.velocity.x) + (this.rigidbody.velocity.y * this.rigidbody.velocity.y) );
		
		if ( (int) speed == 2)
            sourceAcceleratie.pitch = 0.57f;
        else if ( (int)speed == 3)
            sourceAcceleratie.pitch = 0.59f;
        else if ( (int)speed == 4)
            sourceAcceleratie.pitch = 0.61f;
        else if ( (int)speed == 5)
            sourceAcceleratie.pitch = 0.63f;
        else if ( (int)speed == 6)
            sourceAcceleratie.pitch = 0.65f;
        else if ( (int)speed == 7)
            sourceAcceleratie.pitch = 0.67f;
        else if ((int) speed == 8)
            sourceAcceleratie.pitch = 0.69f;
        else if ( (int)speed == 9)
            sourceAcceleratie.pitch = 0.71f;
        else if ( (int)speed == 10)
            sourceAcceleratie.pitch = 0.73f;
        else if ( (int)speed == 11)
            sourceAcceleratie.pitch = 0.75f;
        else if ( (int)speed == 12)
            sourceAcceleratie.pitch = 0.77f;
        else if ( (int)speed >= 13)
            sourceAcceleratie.pitch = 0.79f;
        else
            sourceAcceleratie.pitch = 0.55f;
		
		if(powerUpActive){
			if(speed >15 || speed < -15){
				torque = 0;
			}
			else
				torque = 300;
		}
		else{
			if(speed >12 + (Acceleratie /2) || speed < -12){
				torque = 0;
			}
			else{
				torque = 200;
			}
		}
		force = 0;
		move=0;
        
		if (Input.GetButton("HorizontalLeft")){
			move = -1;
			//print ("leftttftf");
		}
		else if (Input.GetButton("HorizontalRight")){
			move = 1;
		}
		else if (Input.GetButtonUp("HorizontalLeft") && move != 1){
			move = 0;
		}
		else if (Input.GetButtonUp("HorizontalRight") && move != -1){
			move = 0;
		}
		
		if(Input.GetKey(KeyCode.A)){
			force = 1;
		}
		if(Input.GetKey(KeyCode.D)){
			force = -1;
		}
		
		if(Input.GetKey(KeyCode.P)){
			Time.timeScale = 0.0F;
			pauza = 1;
		}
		
	
		animatiek1=animatiek;
		if(!Maimutoi.animation.isPlaying)
		animatiek=9;
		foreach(Touch touch in Input.touches){		
			if(new Rect(Screen.width/2 - buttonSize/2, 10, buttonSize, buttonSize).Contains(touch.position)){
						Time.timeScale = 0.0F;
						pauza = 1;
					}
		}
		
		
		if(!Application.loadedLevelName.Equals("r2")){
			foreach(Touch touch in Input.touches){			
				if(new Rect(0,10, buttonSize, buttonSize).Contains(touch.position)){
					force=1;
					animatiek = 1;
				}
				else 
					ceva3 =0;
				if(new Rect(20 + buttonSize, 10, buttonSize, buttonSize).Contains(touch.position)){
					force=-1;
					animatiek = 3;
				}
				else 
					ceva2=0;
				if(new Rect(Screen.width - buttonSize - buttonSize - 20, 10, buttonSize, buttonSize).Contains(touch.position)){
					move=-1;
					if(frontPlayed == false){
		            	sourceAcceleratie.Play();
		                frontPlayed = true;
		            }
				}
				if(new Rect(Screen.width - buttonSize - 10, 10, buttonSize, buttonSize).Contains(touch.position)){
					move=1;
					if(frontPlayed == false){
		            	sourceAcceleratie.Play();
		                frontPlayed = true;
		            }
				}
			}
		}
		else
			animatiek=r2.animk1;
		
		if((animatiek1== 1 || animatiek1 == 3) && animatiek == 9){
			switch (animatiek1){
				case 1:
				animatiek =2;
				break;
				
				case 3:
				animatiek =4;
				break;
			}
		}
		
		if (WheelColliderOrientation.Air_jump ==1  && animatiek == 9)
			animatiek = 7;
		else if (WheelColliderOrientation.Air_jump == 0 && animatiek1 == 7)
			animatiek = 8;

		
		if(animatiek1!=animatiek){
			animatiej=animatiek;
			anim(animatiej);
		}
	}
	
	void anim(int t){
		switch (t){
		case 0 :
			//Idle();
		break;
			
		case 1:
			Lean_back();
			animatiej=0;
			break;
			
		case 2:
			Lean_back_sit();
			animatiej=0;
			break;
			
		case 3:
			Lean_front();
			animatiej=0;
			break;
			
		case 4:
			Lean_front_sit();
			animatiej=0;
			break;
			
		case 5:
			Push_front();
			animatiej=0;
			break;
			
		case 6:
			Push_front_sit();
			animatiej=0;
			break;
		case 7:
			Air();
			animatiej=0;
			break;
		case 8:
			Air_sit();
			animatiej=0;
			break;
		case 9:
			Idle();
			animatiej=0;
			break;
			
		}
	}
	
	#region anim
	void Lean_front(){
		Maimutoi.animation["Lean_front"].speed = 1.0f;
		Maimutoi.animation.Play("Lean_front");
	}
	void Lean_front_sit(){
		Maimutoi.animation["Lean_front"].time =Maimutoi.animation["Lean_front"].length;
		Maimutoi.animation["Lean_front"].speed = -1.0f;
  	    Maimutoi.animation.Play("Lean_front");		
	}
	void Lean_back(){
		Maimutoi.animation["Lean_back"].speed = 1.0f;
		Maimutoi.animation.Play("Lean_back");
	}
	void Lean_back_sit(){
		Maimutoi.animation["Lean_back_sit"].speed = 1.0f;
		Maimutoi.animation.Play("Lean_back_sit");
		
	}
	void Idle(){
		Maimutoi.animation.Play("idle");
	}
	void Push_front(){
		Maimutoi.animation["Push_front"].speed = 1.0f;
		Maimutoi.animation.Play("Push_front");
	}
	void Push_front_sit(){
		Maimutoi.animation["Push_front"].time = Maimutoi.animation["Push_front"].length;
		Maimutoi.animation["Push_front"].speed = -1.0f;
		Maimutoi.animation.Play("Push_front");	
	}
	void Air(){
		Maimutoi.animation["Air"].speed = 1.0f;
		Maimutoi.animation.Play("Air");
	}
		void Air_sit(){
		Maimutoi.animation["Air"].time = Maimutoi.animation["Air"].length;
		Maimutoi.animation["Air"].speed = -1.0f;
		Maimutoi.animation.Play("Air");
	}
	
	#endregion anim

	public void activatePowerUp(){
		powerupStartTime = Time.time;
		anim(5);
	}

	void FixedUpdate () {
		if(move == 1){
			backWheel.motorTorque = torque;
			frontWheel.motorTorque = Acceleratie * torque/3;
			if(speed < -3){
				backWheel.brakeTorque = 30;
				frontWheel.brakeTorque = 30;
			}
			else{
				backWheel.brakeTorque = 0;
				frontWheel.brakeTorque = 0;
			}
		}
		else if (move == -1){
			backWheel.motorTorque = - Brake * torque/3;
			frontWheel.motorTorque = -4*torque/9;
			if(speed > 3){
				backWheel.brakeTorque = 20;
				frontWheel.brakeTorque = 20;
			}
			else{
				backWheel.brakeTorque = 0;
				frontWheel.brakeTorque = 0;
			}
		}
		else if (move == 0){
			backWheel.motorTorque = 0;
			frontWheel.motorTorque = 0;
			if(speed < 3 && speed >- 3){
				backWheel.brakeTorque = 0;
				frontWheel.brakeTorque = 0;
			}
			else if(speed > 3){
				backWheel.brakeTorque = 20;
				frontWheel.brakeTorque = 20;
			}
			else if(speed < 3){
				backWheel.brakeTorque = 10;
				frontWheel.brakeTorque = 10;
			}
		}
		
		//angular stuff
		if(force == 1 && rigidbody.angularVelocity.magnitude <3){
			rigidbody.AddForceAtPosition(transform.up * 1f, forcePointFront.transform.position, ForceMode.Impulse);
			rigidbody.AddForceAtPosition(-transform.up * 0.2f, forcePointBack.transform.position, ForceMode.Impulse);
		}
		else if(force == -1 && rigidbody.angularVelocity.magnitude <3){
			rigidbody.AddForceAtPosition(transform.up * 1f, forcePointBack.transform.position, ForceMode.Impulse);
			rigidbody.AddForceAtPosition(-transform.up * 0.2f, forcePointFront.transform.position, ForceMode.Impulse);
		}
		
		
	
	}
}
