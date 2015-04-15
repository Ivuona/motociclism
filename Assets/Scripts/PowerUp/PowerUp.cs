using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	Vector3 initialPosition;

	// Use this for initialization
	void Start () {
		
		if(Application.loadedLevelName.Equals("levelEditor"))
			this.gameObject.layer=0;
		initialPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if(StaticData.resetPlayScene){
			resetFunc();
		}
	}
	
	void resetFunc (){
		this.transform.position = initialPosition;
	}
	
	void OnTriggerEnter(Collider other) {
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -100);
	}
}