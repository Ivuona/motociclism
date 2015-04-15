using UnityEngine;
using System.Collections;

public class loadingScreen_Play : MonoBehaviour {

	void Start () {
	
	}

	void Update () {
		if ( MenuGame.levelLoad == "Play"){
			if(Application.GetStreamProgressForLevel("playScene") ==1)
   				 Application.LoadLevel("playScene");
			}
		
		if ( MenuGame.levelLoad == "Replay"){
			if(Application.GetStreamProgressForLevel("r2") ==1)
   				 Application.LoadLevel("r2");
		}
		
		if ( MenuGame.levelLoad == "Menu"){
			if(Application.GetStreamProgressForLevel("gameMenu") ==1)
   				 Application.LoadLevel("gameMenu");
		}
		else {
			for (int i=0; i<StaticData.nrLevels+1;i++){
				if (MenuGame.levelLoad == "level"+ i){
					StaticData.points = 0;
					StaticData.selectedLevelName = "level"+ i;
					if(Application.GetStreamProgressForLevel("playScene") ==1)
   						 Application.LoadLevel("playScene");
					break;
					}
					
			}
		}
		
	}

}