using UnityEngine;
using System.Collections;

public class deactivator : MonoBehaviour {
	
	public GameObject bike;
	int d1 = 20;
	int d2 = 40;
	int gap = 5;
	float lastChecked = 100;
	GameObject cam;
	ReadLevelFromXML readXML;

	// Use this for initialization
	void Start () {
		cam = this.gameObject;
		readXML = cam.GetComponent<ReadLevelFromXML>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(bike.transform.position.x > lastChecked + gap || bike.transform.position.x < lastChecked - gap)	
			foreach (GameObject obj in readXML.rootObjs){
				lastChecked = bike.transform.position.x;
				/*if(obj.name.Contains("tapet"))
					d2 = 70;
				else
					d2 = 40;*/
				if(obj.transform.position.x < bike.transform.position.x + d2 && obj.transform.position.x > bike.transform.position.x - d1){
					if(!obj.active)
						obj.SetActiveRecursively(true);
				}
				else {
					if(obj.active)				
						obj.SetActiveRecursively(false);
				}
			}

	}
}
