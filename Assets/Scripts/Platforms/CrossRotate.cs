using UnityEngine;
using System.Collections;

public class CrossRotate : MonoBehaviour {
	
	//this is similar with the MovePlatformRotate script, with the following modification made for the CrossPlatform:
	//the cross has no collider or rigidbody; therefore the collisions are detected by the children who have colliders. 
	//we need to know then it collides with the bike, in order to rotate the bike around the cross center.
	//the nrChildCollisions variable is modified by the children when collisions occure.
	
	public GameObject bike;
	public bool bikeOnPlatform = false;
	public float rotateSpeed;
	public bool playScene;
	public int nrChildCollisions;
	public float initialRotation;
	bool reset = false;

	void Start () {
		nrChildCollisions = 0;
		rotateSpeed = 15;
		initialRotation = this.transform.eulerAngles.z;
		bike = GameObject.Find("bike");
		if (Application.loadedLevelName == "playScene" ||Application.loadedLevelName == "r2"){
			playScene = true;
		}
		else{
			playScene = false;
		}
	}
	
	void Update(){
		if(nrChildCollisions > 0)
			bikeOnPlatform = true;
		else 
			bikeOnPlatform = false;
	}
	
	void resetFunc (){
		this.transform.eulerAngles = new Vector3 (0, 0, -initialRotation);
		reset = false;
	}
	
	void FixedUpdate () {
		reset = true;
		if(playScene || EditorGUI.animate){
			transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
			if(bikeOnPlatform){
				bike.transform.RotateAround(this.transform.position, -Vector3.forward, rotateSpeed * Time.deltaTime);
			}
			
			if(StaticData.resetPlayScene){
				resetFunc();
			}
		}
		else if (reset){
			this.transform.eulerAngles = new Vector3 (0, 0, initialRotation);
		}
	}
	
}
