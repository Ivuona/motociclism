using UnityEngine;
using System.Collections;

public class LoadingScreenScriptChangeBG : MonoBehaviour {
	public GUIStyle BgStyle;
	public Texture2D Textura3;
	
	void Start () {
	}
	

	void Update () {		
			BgStyle.normal.background = Textura3;
	}
	
	void OnGUI(){
		GUI.Button(new Rect(0, 0, Screen.width, Screen.height),"", BgStyle);

	}
}
