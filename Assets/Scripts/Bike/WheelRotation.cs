using UnityEngine;
using System.Collections;

public class WheelRotation : MonoBehaviour {

	public WheelCollider backWheel;
	public WheelCollider frontWheel;
	public GameObject bike;
	public AudioSource sourceBG, sourcePlatforms, sourceBox, sourceCar;
	float degreesBack;
	float degreesFront;
	
	// Use this for initialization
	void Start () {		
		degreesBack = 0;
		degreesFront = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//rotating the wheel
		
		if(this.name.Contains ("Back")){
			if(bike.GetComponent<WheelColliderOrientation>().raycastHit1){
				degreesBack = backWheel.rpm * 6 * Time.deltaTime;
				transform.Rotate (-Vector3.forward * degreesBack);
			} else if ( degreesBack > 0 ){
				degreesBack -= 0.0005f;
				transform.Rotate (-Vector3.forward * degreesBack);
			} else if ( degreesBack < 0 ){
				degreesBack += 0.0005f;
				transform.Rotate (-Vector3.forward * degreesBack);
			}
		}
		if(this.name.Contains ("Front")){
			if(bike.GetComponent<WheelColliderOrientation>().raycastHit2){
				degreesFront = frontWheel.rpm * 6 * Time.deltaTime;
				transform.Rotate (-Vector3.forward * degreesFront);
			} else if ( degreesFront > 0 ) {
				degreesFront -= 0.0005f;
				transform.Rotate (-Vector3.forward * degreesFront);
			} else if ( degreesFront < 0) {
				degreesFront += 0.0005f;
				transform.Rotate (-Vector3.forward * degreesFront);
			}
		}
		
	}
	   void OnTriggerEnter(Collider obj){
        if(obj.gameObject.tag == "Background")
            sourceBG.Play();
        if(obj.gameObject.tag == "Platform")
            sourcePlatforms.Play();
        if(obj.gameObject.tag == "Box")
            sourceBox.Play();
        if(obj.gameObject.tag == "Car")
            sourceCar.Play();
    }
}