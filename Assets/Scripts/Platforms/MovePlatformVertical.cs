using UnityEngine;
using System.Collections;

public class MovePlatformVertical : MonoBehaviour {
	
	public float initialY;
	public bool moveUP;
	public bool bikeOnPlatform;
	public GameObject bike;
	public Vector3 speed;
	public bool playScene;
	bool reset = false;

	// Use this for initialization
	void Start () {
		initialY = this.transform.position.y;
		moveUP = false;
		bikeOnPlatform = false;
		speed = new Vector3 (0,0.05f,0);
		bike = GameObject.Find("bike");
		if (Application.loadedLevelName == "playScene"||Application.loadedLevelName == "r2"){
			playScene = true;
		}
		else{
			playScene = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//set the direction of the movement
		if(this.transform.position.y > initialY + 5){
			moveUP = false;
		}
		if(this.transform.position.y < initialY - 5){
			moveUP = true;
		}
	}	
	
	void OnCollisionEnter(Collision collision) {
		bikeOnPlatform = true;
    }
	
    void OnCollisionExit(Collision collisionInfo) {
		bikeOnPlatform = false;
    }
	
	void OnMouseDrag() {
		//set the default position of the platform. Used to reset the position after deactivating the animation mode in the level editor.
		initialY = this.transform.position.y;
	}
	
	void resetFunc (){
		this.transform.position = new Vector3 (this.transform.position.x, initialY, this.transform.position.z);
		moveUP = false;
		reset = false;
	}
	
	void FixedUpdate() {
		//moving the platform, and the bike if it is on the platform. This is done in the playScene, and in the editor, if animation mode is active.
		if(playScene || EditorGUI.animate){
			reset = true;
			if(moveUP){
				this.transform.position += speed;
				if(bikeOnPlatform){
					bike.transform.position += speed;
				}
			}
			else {
				this.transform.position -= speed;
				if(bikeOnPlatform){
					bike.transform.position -= speed;
				}
			}
			
			if(StaticData.resetPlayScene){
				resetFunc();
			}
		}
		//if in editor and animation mode is off, reset the platform to its default position
		else if (reset){
			this.transform.position = new Vector3 (this.transform.position.x, initialY, this.transform.position.z);
			moveUP = false;
			reset = false;
		}
	}
}
