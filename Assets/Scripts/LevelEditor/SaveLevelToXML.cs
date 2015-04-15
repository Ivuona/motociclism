using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class SaveLevelToXML : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static void Save(string fileName){
		
		fileName += ".txt";
		
		 //Creating XmlWriter Settings
		XmlWriterSettings settings = new XmlWriterSettings();
		settings.Indent = true;
		settings.IndentChars = "  ";
		settings.NewLineOnAttributes = true;
		
		//Using XmlWriter to create xml file.
		using (XmlWriter writer = XmlWriter.Create(fileName ,settings)){
			
			//print (obj.name.Substring(0, obj.name.IndexOf("_")));
			
			writer.WriteStartElement("platforms");
			GameObject obj;
			
			for(int i=0; i<EditorGUI.Obstacles.Count; i++){
				obj = GameObject.Find(EditorGUI.Obstacles[i].ToString());
				
				writer.WriteStartElement("platform");
				writer.WriteAttributeString("name", obj.name);
				
				writer.WriteStartElement("coordonates");
				writer.WriteAttributeString("x", obj.transform.position.x.ToString());
				writer.WriteAttributeString("y", obj.transform.position.y.ToString());
				writer.WriteAttributeString("z", obj.transform.position.z.ToString());
				writer.WriteEndElement();
				
				writer.WriteStartElement("scale");
				writer.WriteAttributeString("x", obj.transform.localScale.x.ToString());
				writer.WriteAttributeString("y", obj.transform.localScale.y.ToString());
				writer.WriteAttributeString("z", obj.transform.localScale.z.ToString());
				writer.WriteEndElement();
				
				writer.WriteStartElement("rotation");
				writer.WriteAttributeString("x", obj.transform.eulerAngles.x.ToString());
				writer.WriteAttributeString("y", obj.transform.eulerAngles.y.ToString());
				writer.WriteAttributeString("z", obj.transform.eulerAngles.z.ToString());
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			writer.Flush();
		}
	}
}