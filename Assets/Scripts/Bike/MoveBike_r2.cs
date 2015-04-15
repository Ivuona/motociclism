using UnityEngine;
using System.Collections;

public class MoveBike_r2 : MonoBehaviour {
	
	public WheelCollider backWheel;
	public WheelCollider frontWheel;
	public float speed;
	public float move = 0;
	public GameObject centerMass;
	public int force;
	public GameObject forcePointFront;
	public GameObject forcePointBack;
	public float torque;
	public bool powerUpActive;
	public float powerupStartTime;
	public float powerUpDuration;
	public GUIStyle butonPauzaStyle, butonResumeStyle, butonBackStyle, butonExitStyle;
	GUIStyle myStyle;
	
	int pauza;
	
	float buttonSize,x,y;
	
	public Texture2D b1,b2,b3,b4, b5, bgPause;
	
	Font chi640, chi320;
	
	// Use this for initialization
	void Start () {
		
		chi640 = (Font)Resources.Load("Fonts/chinonmn640", typeof(Font));
		chi320 = (Font)Resources.Load("Fonts/chinonmn320", typeof(Font));
		
		x=(float)Screen.width/960;
		y=(float)Screen.height/640;
		Time.timeScale = 1.0F;
		 pauza = 0;
		torque = 200;
		Input.multiTouchEnabled = true;
		powerUpActive = false;
		powerupStartTime = -10;
		myStyle  = new GUIStyle();
		if(Screen.height == 640)
			myStyle.font = chi640;
		else
			myStyle.font = chi320;
		myStyle.normal.textColor = Color.yellow;
		myStyle.fontSize = 18;
		powerUpDuration = 5;
	}
	void OnGUI()
	{
		buttonSize = Screen.height / 8;
		if  (Time.timeScale == 1.0F && pauza==0)
		{
		if(GUI.Button (new Rect(10, Screen.height - buttonSize - 10, buttonSize, buttonSize),b1,"")){
			//GUI.Button(new Rect(120, Screen.height - buttonSize - 10, buttonSize, buttonSize),b1);
		}
		if(GUI.Button(new Rect(buttonSize + 20*x,Screen.height - buttonSize - 10*y, buttonSize, buttonSize),b2,"")){}
		if(GUI.Button(new Rect(Screen.width - buttonSize - buttonSize - 20*x,Screen.height - buttonSize - 10*y, buttonSize, buttonSize),b3,"")){}
		if(GUI.Button(new Rect(Screen.width - buttonSize - 10*x,Screen.height - buttonSize - 10*y, buttonSize, buttonSize),b4,"")){	}
		if(GUI.Button(new Rect(Screen.width - buttonSize- 20*x,Screen.height - buttonSize - 10*y, buttonSize, buttonSize),"",butonPauzaStyle))
				{
				print("asdadadadada");
				Time.timeScale = 0.0F;
				pauza = 1;
			}
		}
		else if  (Time.timeScale == 0.0F && pauza==1)
		{
			print("aaaaaaaaaaaaaaaaaaaaa");
				GUI.DrawTexture (new Rect(0,0, Screen.width, Screen.height), b5);
				GUI.DrawTexture (new Rect(Screen.height/2 - 150*x, 180*y, 600*x,320*y), bgPause);
				if(GUI.Button(new Rect(Screen.height/2 + 250*x,250*y, 140*x, 140*y),"",butonExitStyle))
					{
				Application.Quit();
			}
				if(GUI.Button(new Rect(Screen.height/2 - 90*x,250*y, 140*x, 140*y),"",butonBackStyle))
				{
				Application.LoadLevel("gameMenu");
			}
				if(GUI.Button(new Rect(Screen.height/2 + 80*x,250*y, 140*x, 140*y),"",butonResumeStyle))
				{
					Time.timeScale = 1.0F;
					pauza = 0;
				}
				
		}
		
		if(powerUpActive){
			GUI.Label (new Rect(10*x,50*y,100*x,50*y), "SpeedActive", myStyle);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		//powerUp lasts 5 seconds;
		//check if powerUp is still active;
		powerUpActive = true;
		if(Time.time > powerupStartTime + powerUpDuration){
			powerUpActive = false;
		}
			
		rigidbody.centerOfMass = centerMass.transform.localPosition;
		speed = Mathf.Sqrt ( (this.rigidbody.velocity.x * this.rigidbody.velocity.x) + (this.rigidbody.velocity.y * this.rigidbody.velocity.y) );
		
		if(powerUpActive){
			if(speed >15 || speed < -15){
				torque = 0;
			}
			else
				torque = 300;
		}
		else{
			if(speed >12 || speed < -12){
				torque = 0;
			}
			
			else{
				torque = 200;
			}
		}
		
		if (Input.GetButtonDown ("HorizontalLeft")){
			move = -1;
		}
		else if (Input.GetButtonDown("HorizontalRight")){
			move = 1;
		}
		else if (Input.GetButtonUp("HorizontalLeft") && move != 1){
			move = 0;
		}
		else if (Input.GetButtonUp("HorizontalRight") && move != -1){
			move = 0;
		}
		
		force = 0;
		if(Input.GetKey(KeyCode.A)){
			force = 1;
		}
		if(Input.GetKey(KeyCode.D)){
			force = -1;
		}
		
		/*force = 0;
		move=0;
		foreach(Touch touch in Input.touches){			
			if(new Rect(10,10, buttonSize, buttonSize).Contains(touch.position)){
					force=1;
				}
			if(new Rect(20 + buttonSize, 10, buttonSize, buttonSize).Contains(touch.position)){
					force=-1;
				}
			if(new Rect(Screen.width - buttonSize - buttonSize - 20, 10, buttonSize, buttonSize).Contains(touch.position)){
					move=-1;
				}
			if(new Rect(Screen.width - buttonSize - 10, 10, buttonSize, buttonSize).Contains(touch.position)){
					move=1;
				}
		}*/
	}
	
	public void activatePowerUp(){
		powerupStartTime = Time.time;
	}
	
	void FixedUpdate () {
		if(move == 1){
			backWheel.motorTorque = torque;
			frontWheel.motorTorque = 2*torque/3;
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
			backWheel.motorTorque = -2*torque/3;
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
			if(speed > 3){
				backWheel.brakeTorque = 20;
				frontWheel.brakeTorque = 20;
			}
			if(speed < 3){
				backWheel.brakeTorque = 10;
				frontWheel.brakeTorque = 10;
			}
			
			if(speed < 3 && speed >- 3){
				backWheel.brakeTorque = 0;
				frontWheel.brakeTorque = 0;
			}
		}
		
		//angular stuff
		if(force == 1 && rigidbody.angularVelocity.magnitude <3){
			rigidbody.AddForceAtPosition(transform.up * 1f, forcePointFront.transform.position, ForceMode.Impulse);
			rigidbody.AddForceAtPosition(-transform.up * 0.2f, forcePointBack.transform.position, ForceMode.Impulse);
		}
		if(force == -1 && rigidbody.angularVelocity.magnitude <3){
			rigidbody.AddForceAtPosition(transform.up * 1f, forcePointBack.transform.position, ForceMode.Impulse);
			rigidbody.AddForceAtPosition(-transform.up * 0.2f, forcePointFront.transform.position, ForceMode.Impulse);
		}
	}
}
