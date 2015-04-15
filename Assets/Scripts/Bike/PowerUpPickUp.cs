using UnityEngine;
using System.Collections;

public class PowerUpPickUp : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.name.Contains("PowerUp")){
			this.GetComponent<MoveBike>().activatePowerUp();
		}
    }
}
