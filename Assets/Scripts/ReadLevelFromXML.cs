using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class ReadLevelFromXML : MonoBehaviour {
	
	public List<GameObject> rootObjs = new List<GameObject>();

	// Use this for initialization
	void Awake () {
		// Create an XmlReader
		
		string fileName = StaticData.selectedLevelName;
		
		GameObject level = GameObject.Find("Level");
		if(StaticData.selectedLevelName == "testLevel"){
			level.transform.position = new Vector3 (level.transform.position.x, level.transform.position.y, -40);
		}
		else{
			Destroy(level);
			TextAsset textAsset = (TextAsset)Resources.Load("Levels/" + fileName, typeof(TextAsset));
			Stream str = new MemoryStream(textAsset.bytes);
			using (XmlReader reader = XmlReader.Create(str)){
				reader.MoveToContent();
			    while (reader.Read()){
					
					if(reader.Name == "platform" && reader.NodeType.Equals(XmlNodeType.Element)){
						reader.ReadAttributeValue();
						//print (reader.GetAttribute(0));
						
						Object obj = Instantiate(Resources.Load("Prefabs/" + reader.GetAttribute(0).ToString().Substring(0, reader.GetAttribute(0).ToString().IndexOf("_"))), new Vector3(0, 0, 0), Quaternion.identity);
						obj.name = reader.GetAttribute(0);
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
						
						rootObjs.Add(gObj);
						if(gObj.name.Contains("Ramp1")){							
							gObj.transform.GetChild(0).name += gObj.transform.name;
						}
						else if(gObj.name.Contains("Ramp2")){							
							gObj.transform.GetChild(0).name += gObj.transform.name;
						}
						else if(gObj.name.Contains("Wheel")){							
							gObj.transform.GetChild(0).name += gObj.transform.name;
						}
						else if(gObj.name.Contains("Cube1")){							
							gObj.transform.GetChild(0).name += gObj.transform.name;
						}
						else if(gObj.name.Contains("Cube2")){							
							gObj.transform.GetChild(0).name += gObj.transform.name;
						}
						else if(gObj.name.Contains("Cube3")){							
							gObj.transform.GetChild(0).name += gObj.transform.name;
						}
						else if(gObj.name.Contains("Cube4")){							
							gObj.transform.GetChild(0).name += gObj.transform.name;
						}
						else if(gObj.name.Contains("Tire")){							
							gObj.transform.GetChild(0).name += gObj.transform.name;
						}
						else if(gObj.name.Contains("CrossPlatform")){							
							gObj.transform.GetChild(0).name += gObj.transform.name;
							gObj.transform.GetChild(1).name += gObj.transform.name;
						}
						else if(gObj.name.Contains("Car")){							
							gObj.transform.GetChild(0).name += gObj.transform.name;
						}
						else if(gObj.name.Contains("CheckPoint")){							
							gObj.transform.Rotate(0,180,0);
						}
						else{
							if(!obj.name.Contains("PowerUp")){
								
							}
						}
					}
				}
				//Debug.Log(fileName);
			}
		}
		Debug.Log ("End ReadLevel");
	}


}