using UnityEngine;
using System.Collections;

public class CameraMovement_r2 : MonoBehaviour {
	
	public GameObject BikeChasis;
	private float diffX;
	private float diffY;
	private float diffZ;
	
	// Use this for initialization
	void Start () {
		diffX = BikeChasis.transform.position.x - this.transform.position.x;
		diffY = BikeChasis.transform.position.y - this.transform.position.y;
		diffZ = BikeChasis.transform.position.z - this.transform.position.z;
		
		for(int i = 0; i<500; i+=25){/*
			int aux = Random.Range(1,4);
			if (aux == 1){
				Instantiate(Resources.Load("Prefabs/Background1"), new Vector3(i, 2.5f, -2), Quaternion.identity);
			}
			if (aux == 2){
				Instantiate(Resources.Load("Prefabs/Background2"), new Vector3(i, 2.5f, -2), Quaternion.identity);
			}
			if (aux == 3){
				Instantiate(Resources.Load("Prefabs/Background3"), new Vector3(i, 2.5f, -2), Quaternion.identity);
			}*/
			Instantiate(Resources.Load("Prefabs/Background4"), new Vector3(i, 2.5f, -2), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (BikeChasis.transform.position.x - diffX - 1, BikeChasis.transform.position.y - diffY - 1.5f, BikeChasis.transform.position.z - diffZ);
	}
}