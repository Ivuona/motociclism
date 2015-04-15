using UnityEngine;
using System.Collections;

public class CrossChildCollision : MonoBehaviour {
	
	//this script is on the CrossPlatform children; it increments and decrements the nrChildCollisions variable on the parrent.
	
	public Transform test;
	
	// Use this for initialization
	void Start () {
		test = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
			
	}
	
	void OnCollisionEnter(Collision collision) {
		(test.GetComponent ("CrossRotate") as CrossRotate).nrChildCollisions ++;
    }
	
	void OnCollisionExit(Collision collisionInfo) {
		(test.GetComponent ("CrossRotate") as CrossRotate).nrChildCollisions --;
	}
}
