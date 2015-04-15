using UnityEngine;
using System.Collections;

public class EditorCameraMovement : MonoBehaviour {
	
	public bool left;
	public bool right;
	public bool up;
	public bool down;
	float cameraSpeed;
	public Camera mainCamera;
	
	
	// Use this for initialization
	void Start () {
		left = false;
		right = false;
		up = false;
		down = false;
		cameraSpeed = 0.5f;	
		mainCamera.orthographicSize = 10;
	}
	
	void OnGUI() {
        Event e = Event.current;
		if(e.mousePosition.x <= 1)
			left = true;
		else
			left = false;
		
		if(e.mousePosition.x >= Screen.width-1)
			right = true;
		else
			right = false;
		
		if(e.mousePosition.y <= 1)
			up = true;
		else
			up = false;
		
		if(e.mousePosition.y >= Screen.height-1)
			down = true;
		else
			down = false;
    }
	
	// Update is called once per frame
	void Update () {
		
		float asd = Input.GetAxis("Mouse ScrollWheel");
		mainCamera.orthographicSize -= asd * 10;
		//this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z + asd*10);
		
		if (Input.GetKeyDown("1")){
			this.transform.eulerAngles = new Vector3 (20, 20, 0);
		}
		if (Input.GetKeyDown("2")){
			this.transform.eulerAngles = new Vector3 (20, 0, 0);
		}
		if (Input.GetKeyDown("3")){
			this.transform.eulerAngles = new Vector3 (20, -20, 0);
		}
	}
	
	void FixedUpdate () {
		if (Input.GetKey("up")){
			this.transform.position += new Vector3 (0, cameraSpeed, 0);
		}
		if (Input.GetKey("down")){
			this.transform.position += new Vector3 (0, -cameraSpeed, 0);
		}
		if (Input.GetKey("left")){
			this.transform.position += new Vector3 (-cameraSpeed, 0, 0);
		}
		if (Input.GetKey("right")){
			this.transform.position += new Vector3 (cameraSpeed, 0, 0);
		}
		
		
	}
}
