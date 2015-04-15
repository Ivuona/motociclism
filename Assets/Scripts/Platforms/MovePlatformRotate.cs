using UnityEngine;
using System.Collections;

public class MovePlatformRotate : MonoBehaviour {
	
	//script for rotating a platform around its z axis. Also rotates the bike when it is colliding with the platform, to avoid strange behaviours.
	
	public GameObject bike;
	public bool bikeOnPlatform = false;
	public float rotateSpeed;
	public bool playScene;
	public float initialRotation;
	bool reset = false;
	
	void Start () {
		rotateSpeed = 25;
		initialRotation = this.transform.eulerAngles.z;
		bike = GameObject.Find("bike");
		if (Application.loadedLevelName == "playScene"||Application.loadedLevelName == "r2"){
			playScene = true;
		}
		else{
			playScene = false;
		}
	}
	
	void FixedUpdate () {
		reset = true;
		if(playScene || EditorGUI.animate){
			transform.Rotate(-Vector3.forward * rotateSpeed * Time.deltaTime);
			if(bikeOnPlatform){
				bike.transform.RotateAround(this.transform.position, -Vector3.forward, rotateSpeed * Time.deltaTime);
			}
			
			if(StaticData.resetPlayScene){
				resetFunc();
				//resetPlayScene = false;
			}
		}
		else if (reset){
			this.transform.eulerAngles = new Vector3 (0, 0, initialRotation);
		}
	}
	
	void resetFunc (){
		this.transform.eulerAngles = new Vector3 (0, 0, initialRotation);
		reset = false;
	}
	
	void OnCollisionEnter(Collision collision) {
		bikeOnPlatform = true;
    }
	
    void OnCollisionExit(Collision collisionInfo) {
		bikeOnPlatform = false;
    }
	
}
