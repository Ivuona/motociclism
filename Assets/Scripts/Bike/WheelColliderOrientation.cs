using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WheelColliderOrientation : MonoBehaviour {
	public GameObject backWheelColliderDummy;
	public GameObject frontWheelColliderDummy;
	public bool collidedBack = false;
	public bool collidedFront = false;
	public WheelCollider backWheel;
	public WheelCollider frontWheel;
	public GameObject backWheelObject;
	public GameObject frontWheelObject;
	public static int Air_jump;
	
	public GameObject SuspensionTextureContainer;
	public GameObject SuspensionOrientationDummy;
	public GameObject SuspensionTexture;
	
	public GameObject backPieceTextureContainer;
	public GameObject backPieceOrientationDummy;
	public GameObject backPieceTexture;
	
	public Vector3 normalFront;
	public Vector3 normalBack;
	
	public bool raycastHit1 = false;
	public bool raycastHit2 = false;
	
	float distance1, distance2;
	float xxx, yyy;
	
	int RayDegree = 45;
	int RayGap = 5;
	
	RaycastHit hit;
	
	float minFrontDistance, minBackDistance;
	float translateBack;
	float translateFront;
	
	Vector3 cb, cf;
	
	/*float flyAngle = 0;
	Quaternion flyQ;
	Quaternion newQ;
	bool simpleSwitch = false;*/
	private int rayGapTime = 1;
	
	//flip variables
	public float flipRotation = 0;
	public float lastRotation = 0;
	string jumpQuality = "";
	
	//big air variables
	public float timeSpendInAir = 0;
	bool BackwheelieJump = false;
	string bigAirQuality = "";
	
	//weelie variables
	bool backWheelContact;
	bool frontWheelContact;
	public float backWheelieTime = 0;
	public float frontWheelieTime = 0;
	string wheelieQuality = "";
	
	float wheelColliderRadius;
	Dictionary<string, int> dictionary = new Dictionary<string, int>();
	
	// Use this for initialization
	void Start () {
		wheelColliderRadius = 0.5f;
		translateBack = 0;
		translateFront = 0;
		Air_jump = 0;
	}
	
	/*float AngleAboutZ(Quaternion q){
	  float ang = 2*Mathf.Rad2Deg*Mathf.Acos(q.w*Mathf.Sign(q.z));
	  if (ang > 180) ang -= 360;
	  return ang;
	}*/
	
	// Update is called once per frame
	void Update () {
	//	print("ZEuler: " + this.transform.eulerAngles.z);
		if(dictionary.Count == 0){
			float lastFrameRotation;
			lastFrameRotation = this.transform.eulerAngles.z - lastRotation;
			if (lastFrameRotation > 20){
				lastFrameRotation -= 360;
			} else if (lastFrameRotation < -20){
				lastFrameRotation += 360;
			}
			flipRotation += lastFrameRotation;
			lastRotation = this.transform.eulerAngles.z;
			//print("Last Rotation: " + lastRotation);
			//print("QQQ: " + AngleAboutZ(this.transform.rotation));
			timeSpendInAir += Time.deltaTime;
		/*	newQ.w = this.transform.rotation.w - flyQ.w;
			newQ.z = this.transform.rotation.z - flyQ.z;
			lastFrameRotation = AngleAboutZ(newQ);
			flipRotation += lastFrameRotation;
			flyQ = this.transform.rotation;
			lastRotation = AngleAboutZ(this.transform.rotation);
			//print("Last Rotation: " + lastRotation);
			print("QQQ: " + AngleAboutZ(newQ));
			timeSpendInAir += Time.deltaTime;
		}
		else {
			flyAngle = AngleAboutZ(this.transform.rotation);
			flyQ = this.transform.rotation;
			simpleSwitch = false;*/
		}
		
		
		//in case of reset, the bike will be brought in the original position, with the original rotation (0, 0 ,0). 
		//this means a big angle in one frame, so we do this to avoid a "fake" flip being detected.
		if (StaticData.resetPlayScene){
			flipRotation = 0;
		}
		
		//Orientation of the front suspension texture;
		SuspensionTextureContainer.transform.position = frontWheelObject.transform.position + new Vector3 (0, 0, -0.1f);
		SuspensionTextureContainer.transform.position += (SuspensionTextureContainer.transform.up * 0.3f );
		SuspensionTextureContainer.transform.eulerAngles = new Vector3 (0, 0, -(float)((Math.Atan2(SuspensionOrientationDummy.transform.position.x - frontWheelObject.transform.position.x, SuspensionOrientationDummy.transform.position.y - frontWheelObject.transform.position.y)) * 180 / Math.PI));
		
		//orientation of the back piece texture;
		backPieceTextureContainer.transform.position = backWheelObject.transform.position;// + new Vector3 (0,0,0.15f);
		backPieceTextureContainer.transform.position -= (backPieceTextureContainer.transform.up * 0.5f );
		backPieceTextureContainer.transform.eulerAngles = new Vector3 (0, 0, -(float)((Math.Atan2(backWheelObject.transform.position.x - backPieceOrientationDummy.transform.position.x, backWheelObject.transform.position.y - backPieceOrientationDummy.transform.position.y)) * 180 / Math.PI));
		
		if(dictionary.Count == 0){
			translateBack =  Vector3.Distance(backWheel.transform.position, backWheelObject.transform.position) + Time.fixedDeltaTime;
			translateFront =  Vector3.Distance(frontWheel.transform.position, frontWheelObject.transform.position) + Time.fixedDeltaTime;
			RaycastHit hit;
			
			bool raycastHitBack = false;
			bool raycastHitFront = false;
			int xxx = 0;
			int yyy = 0;
			
			//back wheel
			for(int i = -RayDegree; i <= RayDegree; i += RayGap){
				if (Physics.Raycast( backWheel.transform.position, (-this.transform.up * Mathf.Cos(i*Mathf.PI/180) + this.transform.right * Mathf.Sin(i*Mathf.PI/180)), out hit, backWheel.suspensionDistance + 0.3f )) {
					raycastHitBack = true;
					xxx = i;
				}
			}
			
			if(raycastHitBack){
				backWheelColliderDummy.transform.eulerAngles = new Vector3(-xxx, 90, 0);
			} else {
				if(!collidedBack && translateBack <= 0.4f){
					backWheelObject.transform.position = backWheel.transform.position - this.transform.up * translateBack;
				}
			}
			
			//front wheel
			for(int i = -RayDegree; i <= RayDegree; i += RayGap){
				if (Physics.Raycast( frontWheel.transform.position, (-this.transform.up * Mathf.Cos(i*Mathf.PI/180) + this.transform.right * Mathf.Sin(i*Mathf.PI/180)), out hit, frontWheel.suspensionDistance + 0.3f )) {
					raycastHitFront = true;
					yyy = i;
				}
			}
			
			if(raycastHitFront) {
				frontWheelColliderDummy.transform.eulerAngles = new Vector3(-yyy, 90, 0);
			} else {
				if(!collidedFront && translateFront <= 0.4f)
					frontWheelObject.transform.position = frontWheel.transform.position - this.transform.up * translateFront;
			}
		}
		
		
		//wheelie points:
		if(backWheelContact && !frontWheelContact){
			backWheelieTime += Time.deltaTime;
		} else {
			if(!backWheelContact)
				backWheelieTime -= 0.8f;
			else 
			if(backWheelieTime > 1.5){
				int wheeliePoints = (int) (30 * backWheelieTime);
				if(backWheelieTime < 2.5){
					wheelieQuality = "Decent ";
				} else if(backWheelieTime < 3.5){
					wheelieQuality = "Good ";
					wheeliePoints = (int) (wheeliePoints * 1.3f);
				} else if(backWheelieTime < 4.5){
					wheelieQuality = "Great ";
					wheeliePoints = (int) (wheeliePoints * 1.6f);
				} else if(backWheelieTime >= 4.5){
					wheelieQuality = "Epic ";
					wheeliePoints = (int) (wheeliePoints * 2f);
				}
				StaticData.points += wheeliePoints;
				GameObject text =  (GameObject)Instantiate(Resources.Load("Prefabs/floatingText" ), new Vector3(0, 0, 0), Quaternion.identity);
				text.GetComponent<floatingTextScript>().textToDisplay = wheelieQuality + "Back Wheelie + " + wheeliePoints;
				text.GetComponent<floatingTextScript>().yOffset = -70;
			}
			backWheelieTime = 0;
		}
		
		
		if(!backWheelContact && frontWheelContact){
			frontWheelieTime += Time.deltaTime;
		} else {
			if(!frontWheelContact)
				frontWheelieTime -= 0.8f;
			else if(frontWheelieTime > 1){
				int wheeliePoints = (int) (30 * frontWheelieTime);
				if(frontWheelieTime < 1.5){
					wheelieQuality = "Decent ";
				} else if(frontWheelieTime < 2){
					wheelieQuality = "Good ";
					wheeliePoints = (int) (wheeliePoints * 2f);
				} else if(frontWheelieTime < 2.5){
					wheelieQuality = "Great ";
					wheeliePoints = (int) (wheeliePoints * 3f);
				} else if(frontWheelieTime >= 2.5){
					wheelieQuality = "Epic ";
					wheeliePoints = (int) (wheeliePoints * 4f);
				}
				StaticData.points += wheeliePoints;
				GameObject text =  (GameObject)Instantiate(Resources.Load("Prefabs/floatingText" ), new Vector3(0, 0, 0), Quaternion.identity);
				text.GetComponent<floatingTextScript>().textToDisplay = wheelieQuality + "Nose Wheelie + " + wheeliePoints;
				text.GetComponent<floatingTextScript>().yOffset = -70;
			}
			frontWheelieTime = 0;
		}
	}

	//the dictionary is used to keep track of the current collisions
	void OnCollisionEnter(Collision collision) {
//		Debug.Log("FLIP ROTATION: " + flipRotation);
		Air_jump = 0;
		
		if(dictionary.Count == 0){
			//after the land, we check if a flip has been made;
			if (StaticData.resetPlayScene)
				{
					GameObject text =  (GameObject)Instantiate(Resources.Load("Prefabs/floatingText" ), new Vector3(0, 0, 0), Quaternion.identity);
					text.GetComponent<floatingTextScript>().textToDisplay = "Flip Failed";
				}
			else
			{

			if(flipRotation > 150 )
			{
				int flipPoints = (int) flipRotation;
				if (flipRotation <250) {
					jumpQuality = "Decent ";
				} else if (flipRotation <300) {
					jumpQuality = "Good ";
					flipPoints = (int) (flipPoints * 1.3);
				} else if (flipRotation < 350) {
					jumpQuality = "Great ";
					flipPoints = (int) (flipPoints * 1.6);
				} else {
					jumpQuality = "Epic ";
					flipPoints = (int) (flipPoints * 2);
				};
				StaticData.points += flipPoints;
				GameObject text =  (GameObject)Instantiate(Resources.Load("Prefabs/floatingText" ), new Vector3(0, 0, 0), Quaternion.identity);
				text.GetComponent<floatingTextScript>().textToDisplay = jumpQuality + "Back Flip + " + flipPoints;	
			}

			if(flipRotation < -150 )
			{
					print("minus rot: " + flipRotation);
				int flipPoints = (int) -flipRotation;
				if (flipRotation >-250) {
					jumpQuality = "Decent ";
				} else if (flipRotation >-300) {
					jumpQuality = "Good ";
					flipPoints = (int) (flipPoints * 1.3);
				} else if (flipRotation > -350) {
					jumpQuality = "Great ";
					flipPoints = (int) (flipPoints * 1.6);
				} else {
					jumpQuality = "Epic ";
					flipPoints = (int) (flipPoints * 2);
				}
				StaticData.points += flipPoints;
				GameObject text =  (GameObject)Instantiate(Resources.Load("Prefabs/floatingText" ), new Vector3(0, 0, 0), Quaternion.identity);
				text.GetComponent<floatingTextScript>().textToDisplay = jumpQuality + "Front Flip + " + flipPoints;	
			}
			}
			
			//if time spend in air is > 1, bonus points are awarded;
			if(timeSpendInAir > 1.5){
				int airPoints = ((int) (50 * timeSpendInAir));
				if(timeSpendInAir < 2){
					bigAirQuality = "Decent ";
				} else if(timeSpendInAir < 2.5){
					bigAirQuality = "Good ";
					airPoints = (int) (airPoints * 1.3);
				} else if (timeSpendInAir < 3.5){
					bigAirQuality = "Great ";
					airPoints = (int) (airPoints * 1.6);
				} else if (timeSpendInAir >= 3.5){
					bigAirQuality = "Epic ";
					airPoints = (int) (airPoints * 2);
				}
				
				GameObject text =  (GameObject)Instantiate(Resources.Load("Prefabs/floatingText" ), new Vector3(0, 0, 0), Quaternion.identity);
				StaticData.points += airPoints;
				text.GetComponent<floatingTextScript>().textToDisplay = bigAirQuality + "Air Time + " + airPoints.ToString();
				text.GetComponent<floatingTextScript>().yOffset = 70;
			}
			
		}
		dictionary.Add ( collision.transform.name, 0 );
		timeSpendInAir = 0;
    }
	
    void OnCollisionExit(Collision collisionInfo) {
		dictionary.Remove ( collisionInfo.transform.name );
		if(dictionary.Count == 0){
			//start incrementing the rotation, to detect flips
			lastRotation = this.transform.eulerAngles.z;
			flipRotation = 0;
			//the frame when the jump is started
		}
    }
	
	/*void backPhysics(int i, float dprev, float dist, float dnext){
		//verific distanta pt i
		if(dist==0)
			if (Physics.Raycast( backWheel.transform.position, (-this.transform.up * Mathf.Cos(i*Mathf.PI/180) + this.transform.right * Mathf.Sin(i*Mathf.PI/180)), out hit, backWheel.suspensionDistance + wheelColliderRadius - 0.2f ) && !hit.collider.name.Contains("PowerUp") && !hit.collider.name.Contains("FinishLine")) {
				dist = Vector3.Distance(backWheel.transform.position, hit.point);
				raycastHit1 = true;
				if(distance1==100){
					distance1 = dist;
					xxx = i;
				}
			}
		if(dprev==0 && i-RayGap>-RayDegree)
			if (Physics.Raycast( backWheel.transform.position, (-this.transform.up * Mathf.Cos((i-RayGap)*Mathf.PI/180) + this.transform.right * Mathf.Sin((i-RayGap)*Mathf.PI/180)), out hit, backWheel.suspensionDistance + wheelColliderRadius - 0.2f ) && !hit.collider.name.Contains("PowerUp") && !hit.collider.name.Contains("FinishLine")) {
				dprev = Vector3.Distance(backWheel.transform.position, hit.point);
				raycastHit1 = true;
				if(distance1==100){
					distance1 = dprev;
					xxx = i-RayGap;
				}
			}
		if(dnext==0 && i+RayGap<RayDegree)
			if (Physics.Raycast( backWheel.transform.position, (-this.transform.up * Mathf.Cos((i+RayGap)*Mathf.PI/180) + this.transform.right * Mathf.Sin((i+RayGap)*Mathf.PI/180)), out hit, backWheel.suspensionDistance + wheelColliderRadius - 0.2f ) && !hit.collider.name.Contains("PowerUp") && !hit.collider.name.Contains("FinishLine")) {
				dnext = Vector3.Distance(backWheel.transform.position, hit.point);
				raycastHit1 = true;
				if(distance1==100){
					distance1 = dnext;
					xxx = i+RayGap;
				}
			}
		if((dist<dprev && dist<dnext) || (dist<dprev && i==RayDegree) || (dist<dnext && i==-RayDegree)){
			distance1 = dist;
			xxx = i;
		}
		else if(dist>dprev && i-RayGap>-RayDegree)
			backPhysics(i-RayGap,0,dprev,dist);
		else if(dist>dnext && i+RayGap<RayDegree)
			backPhysics(i+RayGap,dist,dnext,0);
		
	}
	
	void frontPhysics(int i, float dprev, float dist, float dnext){
		//verific distanta pt i
		if(dist==0)
			if (Physics.Raycast( frontWheel.transform.position, (-this.transform.up * Mathf.Cos(i*Mathf.PI/180) + this.transform.right * Mathf.Sin(i*Mathf.PI/180)), out hit, frontWheel.suspensionDistance + wheelColliderRadius - 0.2f ) && !hit.collider.name.Contains("PowerUp") && !hit.collider.name.Contains("FinishLine")) {
				dist = Vector3.Distance(frontWheel.transform.position, hit.point);
				raycastHit2 = true;
				if(distance2==100){
					distance2 = dist;
					yyy = i;
				}
			}
		if(dprev==0 && i-RayGap>-RayDegree)
			if (Physics.Raycast( frontWheel.transform.position, (-this.transform.up * Mathf.Cos((i-RayGap)*Mathf.PI/180) + this.transform.right * Mathf.Sin((i-RayGap)*Mathf.PI/180)), out hit, frontWheel.suspensionDistance + wheelColliderRadius - 0.2f ) && !hit.collider.name.Contains("PowerUp") && !hit.collider.name.Contains("FinishLine")) {
				dprev = Vector3.Distance(frontWheel.transform.position, hit.point);
				raycastHit2 = true;
				if(distance2==100){
					distance2 = dprev;
					yyy = i-RayGap;
				}
			}
		if(dnext==0 && i+RayGap<RayDegree)
			if (Physics.Raycast( frontWheel.transform.position, (-this.transform.up * Mathf.Cos((i+RayGap)*Mathf.PI/180) + this.transform.right * Mathf.Sin((i+RayGap)*Mathf.PI/180)), out hit, frontWheel.suspensionDistance + wheelColliderRadius - 0.2f ) && !hit.collider.name.Contains("PowerUp") && !hit.collider.name.Contains("FinishLine")) {
				dnext = Vector3.Distance(frontWheel.transform.position, hit.point);
				raycastHit2 = true;
				if(distance2==100){
					distance2 = dnext;
					yyy = i+RayGap;
				}
			}
		if((dist<dprev && dist<dnext) || (dist<dprev && i==RayDegree) || (dist<dnext && i==-RayDegree)){
			distance2 = dist;
			yyy = i;
		}
		else if(dist>dprev && i-RayGap>-RayDegree)
			frontPhysics(i-RayGap,0,dprev,dist);
		else if(dist>dnext && i+RayGap<RayDegree)
			frontPhysics(i+RayGap,dist,dnext,0);
		
	}*/
	
	void OnCollisionStay(Collision collision) {
		//print("START");
		
		
		
		
        foreach (ContactPoint contact in collision.contacts) {
			if(contact.thisCollider.name == "WheelBack"){
				//print ("back contct: " + Vector3.Distance(backWheel.transform.position, contact.point));
	        	Debug.DrawRay(contact.point, contact.normal, Color.blue);
				
				//distance1 = Vector3.Distance(backWheel.transform.position, contact.point);
				//xxx = Vector3.Angle(backWheel.transform.position, contact.point);
				collidedBack = true;
			}
			if(contact.thisCollider.name == "WheelFront"){
				Debug.DrawRay(contact.point, contact.normal, Color.blue);
				//distance2 = Vector3.Distance(frontWheel.transform.position, contact.point);
				//print("distance 2 " + Vector3.Distance(frontWheel.transform.position, contact.point));
				collidedFront = true;
				//cf = contact.point;
			}
			if(collidedBack && collidedFront)
				break;
        }
		
		dictionary[collision.transform.name] = 1;
		
		//On collision stay is called for every collision. Each collision processed, will be tagged with "1" in the dictionary.
		//This is needed to know when all the collisions have been processed. This happens when there is nothing in the 
		//dictionary tagget with "0", and then the following code is executed.
		
		if(!dictionary.ContainsValue(0)){
			
			translateBack =  Vector3.Distance(backWheel.transform.position, backWheelObject.transform.position) + Time.fixedDeltaTime;
			translateFront =  Vector3.Distance(frontWheel.transform.position, frontWheelObject.transform.position) + Time.fixedDeltaTime;
			
			distance1 = 100;
			distance2 = 100;
			
			xxx = 0;
			yyy = 0;
			
			raycastHit1 = false;
			raycastHit2 = false;
			
			
			
		/*	
			if(collidedBack){
				raycastHit1 = true;
			}
			if(collidedFront){
				raycastHit2 = true;
			}*/
			//raycasts are sent downwards, to see in what direction is the closest collision. The direction and distance
			//for each wheel are retained for the closest collision, and used to orientate the wheelCollider, and to set
			//the wheel position, to mimic the suspension movement.
			/*
			foreach (ContactPoint contact in collision.contacts) {
//		        print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
		        // Visualize the contact point
		        Debug.DrawRay(contact.point, contact.normal, Color.white);
			}*/
			if(collidedBack)
			for(int i = -RayDegree; i <= RayDegree; i += RayGap){
				if (Physics.Raycast( backWheel.transform.position, (-this.transform.up * Mathf.Cos(i*Mathf.PI/180) + this.transform.right * Mathf.Sin(i*Mathf.PI/180)), out hit, backWheel.suspensionDistance + wheelColliderRadius - 0.2f ) && !hit.collider.name.Contains("PowerUp") && !hit.collider.name.Contains("FinishLine")) {
					if(Vector3.Distance(backWheel.transform.position, hit.point) < distance1){
						distance1 = Vector3.Distance(backWheel.transform.position, hit.point);
						xxx = i;
					}
					raycastHit1 = true;
				}
			}
			
			//backPhysics(0,0,0,0);
			
			//print("distance1: " + distance1 + " i: " + xxx);
			
			//print("START");
			if(collidedFront)
			for(int i = -RayDegree; i <= RayDegree; i += RayGap){
			//	print(i);
				//if(distance2 > 0.65){
				if (Physics.Raycast( frontWheel.transform.position, (-this.transform.up * Mathf.Cos(i*Mathf.PI/180) + this.transform.right * Mathf.Sin(i*Mathf.PI/180)), out hit, frontWheel.suspensionDistance + wheelColliderRadius - 0.2f ) && !hit.collider.name.Contains("PowerUp") && !hit.collider.name.Contains("FinishLine")) {
					if(Vector3.Distance(frontWheel.transform.position, hit.point) < distance2){
						distance2 = Vector3.Distance(frontWheel.transform.position, hit.point);
						yyy = i;
						
					}
					
					raycastHit2 = true;
				}
				//}
			}
			//frontPhysics(0,0,0,0);
			if(raycastHit2 && distance2==100)
			print("distance2: " + distance2 + " i: " + yyy + "raycast: " + raycastHit2);
			
			//print("STOP");
				
			
			//this is where the orientation of the wheel colliders are set.
			backWheelColliderDummy.transform.localEulerAngles = new Vector3 ( -xxx, 90, 0);
			frontWheelColliderDummy.transform.localEulerAngles = new Vector3 ( -yyy, 90, 0);			
			
			//this is where the wheel textures positions are set.
			if(raycastHit1){
//				print ("DDDD1: " + distance1);
				backWheelContact = true;
				backWheelObject.transform.position = backWheel.transform.position - (this.transform.up * distance1) + (this.transform.up * wheelColliderRadius);
			}else{
				backWheelContact = false;
				if(!collidedBack && translateBack <= 0.4f){
					backWheelObject.transform.position = backWheel.transform.position - this.transform.up * translateBack;
				}
			}			
			
			if(raycastHit2){
				//print ("DDDD2: " + distance2);
				frontWheelContact = true;
				frontWheelObject.transform.position = frontWheel.transform.position - (this.transform.up * distance2) + (this.transform.up * wheelColliderRadius);
			}else{
				frontWheelContact = false;
				if(!collidedFront && translateFront <= 0.4f){
					frontWheelObject.transform.position = frontWheel.transform.position - this.transform.up * translateFront;
				}
			}

			collidedBack = false;
			collidedFront = false;
			
			
			//we reset the keys in the dictionary
			List<string> keysToNuke = new List<string>();
			foreach( String k in dictionary.Keys ){
				keysToNuke.Add(k);
			}
			foreach (string k in keysToNuke){
    			dictionary[k] = 0;
			}
		}
		//print("STOP");
    }
	
	void FixedUpdate(){
		if(timeSpendInAir > 0.5){
					Air_jump = 1;
			}
		
	}
}
