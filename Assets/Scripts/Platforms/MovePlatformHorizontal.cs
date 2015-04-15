using UnityEngine;
using System.Collections;

public class MovePlatformHorizontal : MonoBehaviour {

	public float initialX;
	public bool moveLeft;
	public GameObject bike;
	public Vector3 speed;
	public bool bikeOnPlatform = false;
	public bool playScene;
	bool reset = false;
	
	// Use this for initialization
	void Start () {
		initialX = this.transform.position.x;
		moveLeft = false;
		speed = new Vector3 (0.1f, 0, 0);
		bike = GameObject.Find("bike");
		if (Application.loadedLevelName == "playScene"||Application.loadedLevelName == "r2"){
			playScene = true;
		}
		else{
			playScene = false;
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		bikeOnPlatform = true;
    }
	
    void OnCollisionExit(Collision collisionInfo) {
		bikeOnPlatform = false;
    }
	
	// Update is called once per frame
	void Update () {
		//set the direction of the movement
		if(this.transform.position.x > initialX + 10){
			moveLeft = false;
		}
		if(this.transform.position.x < initialX - 10){
			moveLeft = true;
		}
		
	}
	
	void OnMouseDrag() {
		//set the default position of the platform. Used to reset the position after deactivating the animation mode in the level editor.
		initialX = this.transform.position.x;
	}
	
	void resetFunc (){
		this.transform.position = new Vector3 (initialX, this.transform.position.y, this.transform.position.z);
		moveLeft = false;
		reset = false;
	}
	
	void FixedUpdate() {
		//moving the platform, and the bike if it is on the platform. This is done in the playScene, and in the editor, if animation mode is active.
		if(playScene || EditorGUI.animate){
			reset = true;
			if(moveLeft){
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
			this.transform.position = new Vector3 (initialX, this.transform.position.y, this.transform.position.z);
			moveLeft = false;
			reset = false;
		}
	}
}