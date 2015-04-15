using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public GameObject BikeChasis;
	private float diffX;
	private float diffY;
	private float diffZ;
	//GameObject obj;
//	private int pozcovor1=0;
//	Object cov1,cov2,cov3,cov4;
//	GameObject c1,c2,c3,c4,aux;
	
	// Use this for initialization
	void Start () {
		Object obj;
		//GameObject gObj;
		//GameObject cam = this.gameObject;
		//ReadLevelFromXML readXML = cam.GetComponent<ReadLevelFromXML>();
		diffX = BikeChasis.transform.position.x - this.transform.position.x;
		diffY = BikeChasis.transform.position.y - this.transform.position.y;
		diffZ = BikeChasis.transform.position.z - this.transform.position.z;
		for(int i = 0; i<500; i+=25){
			/*int aux = Random.Range(1,4);
			if (aux == 1){
				Instantiate(Resources.Load("Prefabs/Background1"), new Vector3(i, 2.5f, -2), Quaternion.identity);
			}
			if (aux == 2){
				Instantiate(Resources.Load("Prefabs/Background2"), new Vector3(i, 2.5f, -2), Quaternion.identity);
			}
			if (aux == 3){
				Instantiate(Resources.Load("Prefabs/Background3"), new Vector3(i, 2.5f, -2), Quaternion.identity);
			}
		*/
			obj = Instantiate(Resources.Load("Prefabs/Background4"), new Vector3(i, 2.5f, -2), Quaternion.identity);
			/*obj.name = "tapet" + i;
			print("tapet" + i);
			gObj = GameObject.Find("tapet" + i);
			readXML.rootObjs.Add(gObj);*/
			
		}
		
		/*cov1 = Instantiate(Resources.Load("Prefabs/Background4"), new Vector3(pozcovor1, 2.5f, -2), Quaternion.identity);
		cov2 = Instantiate(Resources.Load("Prefabs/Background4"), new Vector3(pozcovor1 + 25, 2.5f, -2), Quaternion.identity);
		cov3 = Instantiate(Resources.Load("Prefabs/Background4"), new Vector3(pozcovor1 + 50, 2.5f, -2), Quaternion.identity);
		cov4 = Instantiate(Resources.Load("Prefabs/Background4"), new Vector3(pozcovor1 + 75, 2.5f, -2), Quaternion.identity);	
		
		cov1.name="covor1";
		cov2.name="covor2";
		cov3.name="covor3";
		cov4.name="covor4";
			
		*/}
	
	void Update () {
		this.transform.position = new Vector3 (BikeChasis.transform.position.x - diffX - 1, BikeChasis.transform.position.y - diffY + 2.5f, BikeChasis.transform.position.z - diffZ);
		
		/*
		if(BikeChasis.transform.position.x - pozcovor1 > 25){
			Destroy(cov1);
			pozcovor1 +=25; 
			cov1 = cov2;
			cov2 = cov3;
			cov3 = cov4;
			cov4 = Instantiate(Resources.Load("Prefabs/Background4"), new Vector3(pozcovor1 + 75, 2.5f, -2), Quaternion.identity);
			
		}
		else while (BikeChasis.transform.position.x - pozcovor1 < -25){
			Destroy(cov4);
			pozcovor1 -=25;
			cov4 = cov3;
			cov3 = cov2;
			cov2 = cov1;
			cov1 = Instantiate(Resources.Load("Prefabs/Background4"), new Vector3(pozcovor1, 2.5f, -2), Quaternion.identity);
		}*/
	}
}