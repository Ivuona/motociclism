using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {
	Vector3 initialPosition;
	
	// Use this for initialization
	void Start () {
		this.transform.Rotate(0,0,0);
		
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
		if(other.name==("Chasis")){
			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -100);
			StaticData.hitCheckPoint = true;
			//this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
			print("check coll");
		}
	}
}
