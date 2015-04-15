using UnityEngine;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;

public class EditorGUI : MonoBehaviour {
	
	//objNr used to make sure that all objects have different names;
	int objNr;
	
	public static ArrayList Obstacles = new ArrayList();
	public static bool animate;
	public static string SelectedObject;
	public bool levelSaveName;
	public bool levelLoadName;
	
	public static string objDepth = "depth";
	public static string objWidth = "width";
	public static string objHeight = "height";
	public static string objRotation = "rotation";
	public static string objRotationSpeed = "rotspeed";
	public static string objModveDistance = "movedist";
	public static string objMoveSpeed = "movespeed";	
	public static string levelName = "level";
	
	Rect windowStats = new Rect(10, Screen.height - 130, 200, 120);
	Rect enterNameWindow = new Rect ((Screen.width - 150) / 2, Screen.height / 2, 150, 85);
	
	// Use this for initialization
	void Start () {
		objNr = 0;
		animate = false;
		SelectedObject = "no object selected";
		levelSaveName = false;
		
		for(int i = 0; i<500; i+=25){
			int aux = Random.Range(1,4);
			if (aux == 1){
				Instantiate(Resources.Load("Prefabs/Background1"), new Vector3(i, 2.5f, -2), Quaternion.identity);
			}
			if (aux == 2){
				Instantiate(Resources.Load("Prefabs/Background2"), new Vector3(i, 2.5f, -2), Quaternion.identity);
			}
			if (aux == 3){
				Instantiate(Resources.Load("Prefabs/Background3"), new Vector3(i, 2.5f, -2), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Delete)){
			GameObject selectedObj = GameObject.Find(SelectedObject);
			Obstacles.Remove(selectedObj.name);
			Destroy(selectedObj);
			SelectedObject = "no object selected";
			objWidth = "width";
			objHeight = "height";
			objDepth = "depth";
			objRotation = "rotation";
		}
	}

	void statsWindow(int windowID) {
		
		GUI.Label(new Rect(10, 30, 50, 20), "Size");
		GUI.Label(new Rect(60, 30, 20, 20), "X:");
		objWidth = GUI.TextField(new Rect(80, 30, 45, 20), objWidth);
		GUI.Label(new Rect(130, 30, 20, 20), "Y:");
		objHeight = GUI.TextField(new Rect(150, 30, 45, 20), objHeight);
		
		GUI.Label(new Rect(10, 60, 50, 20), "Size");
		GUI.Label(new Rect(60, 60, 20, 20), "Z:");
		objDepth = GUI.TextField(new Rect(80, 60, 45, 20), objDepth);
		
		GUI.Label(new Rect(10, 90, 95, 20), "Rotation");
		objRotation = GUI.TextField(new Rect(80, 90, 50, 20), objRotation);
		
		if (GUI.Button(new Rect(155, 65, 30, 40), "Ok")){
			GameObject selectedObj = GameObject.Find(SelectedObject);
			selectedObj.transform.localScale = new Vector3 (float.Parse(objWidth), float.Parse(objHeight), float.Parse(objDepth));
			selectedObj.transform.eulerAngles = new Vector3 (selectedObj.transform.eulerAngles.x, selectedObj.transform.eulerAngles.y, float.Parse(objRotation));
			//selectedObj.transform.position = new Vector3 (selectedObj.transform.position.x, selectedObj.transform.position.y, float.Parse(objDepth));
			if(selectedObj.name.Contains("RotatePlatform")){
				selectedObj.GetComponent<MovePlatformRotate>().initialRotation = float.Parse(objRotation);
			}
			if(selectedObj.name.Contains("CrossPlatform")){
				selectedObj.GetComponent<CrossRotate>().initialRotation = float.Parse(objRotation);
			}
		}
    }
	
	void SaveWindow(int windowID){
		levelName = GUI.TextField(new Rect(10, 25, 130, 20), levelName);
		if (GUI.Button(new Rect(10, 55, 60, 20), "Ok")){
			SaveLevelToXML.Save(levelName);
			levelSaveName = false;
		}
		if (GUI.Button(new Rect(80, 55, 60, 20), "Cancel")){
			levelSaveName = false;
		}
	}
	
	void LoadWindow(int windowID){
		levelName = GUI.TextField(new Rect(10, 25, 130, 20), levelName);
		if (GUI.Button(new Rect(10, 55, 60, 20), "Ok")){
			loadLevel(levelName);												
			levelLoadName = false;
		}
		if (GUI.Button(new Rect(80, 55, 60, 20), "Cancel")){
			levelLoadName = false;
		}
	}

	void OnGUI() {
		
		windowStats = GUI.Window(0, windowStats, statsWindow, SelectedObject);
				
		//buttons for adding platforms
		int y = 10;
		for(int i = 1; i <= StaticData.platformTypes.Count; i++) {
			if(GUI.Button(new Rect(Screen.width - 250, y, 110, 40), StaticData.platformTypes[i]))
				AddObject (i);
			i++;
			if(StaticData.platformTypes.ContainsKey(i)){
				if(GUI.Button(new Rect(Screen.width - 130, y, 120, 40), StaticData.platformTypes[i]))
					AddObject (i);
				y +=50;
			}
		}
		
		//save button		
		if (GUI.Button(new Rect(10, 60, 100, 40), "Save")){
			levelSaveName = true;
		}
		
		if (GUI.Button(new Rect(10, 110, 100, 40), "Load")){
			levelLoadName = true;
		}
		
		if(levelSaveName){
			enterNameWindow = GUI.Window(1, enterNameWindow, SaveWindow, "Enter Level Name");
		}
		
		if(levelLoadName){
			enterNameWindow = GUI.Window(1, enterNameWindow, LoadWindow, "Enter Level Name");
		}
		
		//animate mode = on / off; when animate is on, the platforms will move but they will not be movable with the mouse.
		string AnimateButtonOnOffString;
		if(animate)
			AnimateButtonOnOffString = "on";
		else 
			AnimateButtonOnOffString = "off";
		if (GUI.Button(new Rect(10, 10, 100, 40), "Animate " + AnimateButtonOnOffString)){
			if(animate == true){
				animate = false;
			}
			else{
				animate = true;
			}
		}
		
		
		/*if( GUI.Button (new Rect (10, 300, 200, 80), "print") ){
			printObjectList();
		}*/
		
		
    }
	
	void printObjectList(){
		foreach ( string obj in Obstacles )
			print (obj);
	}
	
	//instantiate object; x -> type of object; the types are defined in StaticData.cs; the name of the instantiated object is always
	//unique; the name is saved in the ArrayList Obstacles for further use (deleting objects, saving the level to xml);
	
	void AddObject(int x) {
		Object currentObject = Instantiate(Resources.Load("Prefabs/" + StaticData.platformTypes[x]), new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
		currentObject.name = StaticData.platformTypes[x] + "_" + objNr;
	
		objNr ++;
		Obstacles.Add (currentObject.name);
	}
	
	void loadLevel (string levelName){
		
		//clear scene
		for(int i=0; i<EditorGUI.Obstacles.Count; i++){
			Destroy (GameObject.Find(Obstacles[i].ToString()));
		}		
		Obstacles.Clear();		
		levelName += ".txt";
		
		using (XmlReader reader = XmlReader.Create(levelName)){
			reader.MoveToContent();
		    while (reader.Read()){
				
				if(reader.Name == "platform" && reader.NodeType.Equals(XmlNodeType.Element)){
					reader.ReadAttributeValue();
					//print (reader.GetAttribute(0));
					
					Object obj = Instantiate(Resources.Load("Prefabs/" + reader.GetAttribute(0).ToString().Substring(0, reader.GetAttribute(0).ToString().IndexOf("_"))), new Vector3(0, 0, 0), Quaternion.identity);
					objNr = int.Parse (reader.GetAttribute(0).ToString().Substring(reader.GetAttribute(0).ToString().IndexOf("_") + 1, reader.GetAttribute(0).Length - reader.GetAttribute(0).ToString().IndexOf("_") - 1));
					obj.name = reader.GetAttribute(0);
					Obstacles.Add(obj.name);
					GameObject gObj = GameObject.Find(obj.name);
					
					reader.Read();
					reader.Read();
					
					reader.ReadAttributeValue();
					//print (reader.GetAttribute(0) + " " + reader.GetAttribute(1) + " " + reader.GetAttribute(2));
					
					gObj.transform.position = new Vector3 (float.Parse(reader.GetAttribute(0)), float.Parse(reader.GetAttribute(1)), float.Parse(reader.GetAttribute(2)));
					
					reader.Read();
					reader.Read();
					
					reader.ReadAttributeValue();
					//print (reader.GetAttribute(0) + " " + reader.GetAttribute(1) + " " + reader.GetAttribute(2));
					
					gObj.transform.localScale = new Vector3 (float.Parse(reader.GetAttribute(0)), float.Parse(reader.GetAttribute(1)), float.Parse(reader.GetAttribute(2)));;
					
					reader.Read();
					reader.Read();
					
					reader.ReadAttributeValue();
					//print (reader.GetAttribute(0) + " " + reader.GetAttribute(1) + " " + reader.GetAttribute(2));
					
					gObj.transform.eulerAngles = new Vector3 (float.Parse(reader.GetAttribute(0)), float.Parse(reader.GetAttribute(1)), float.Parse(reader.GetAttribute(2)));;
				}
			}
		}
		
		objNr++;
	}
	
	
}