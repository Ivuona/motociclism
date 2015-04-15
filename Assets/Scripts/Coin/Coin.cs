using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
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
		this.transform.Rotate(Vector3.down *50* Time.deltaTime);
		
	}
	
	void resetFunc (){
		this.transform.position = initialPosition;
	}
	
	void OnTriggerEnter(Collider other) {
		//if(other.name.Contains("Chasis"))
		//{
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -100);
			PlayerPrefs.SetInt("money",PlayerPrefs.GetInt("money",0)+1);
	//	}
	}
}
