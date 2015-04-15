using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

public class MenuGame : MonoBehaviour {
	
	float buttonSize, buttonSizeW;
	static float speedStat, handlingStat, brakeStat;
	public Texture2D CshopSlot1,CshopSlot2, CshopSlot3,CshopSlot4,CshopSlot5,CshopSlot6, NshopSlot1,NshopSlot2, NshopSlot3,NshopSlot4, NshopSlot5,NshopSlot6 , LshopSlot1,LshopSlot2, LshopSlot3,LshopSlot4,LshopSlot5,LshopSlot6, CshopSlot7,CshopSlot8, CshopSlot9,CshopSlot10,CshopSlot11,CshopSlot12, NshopSlot7,NshopSlot8, NshopSlot9,NshopSlot10, NshopSlot11,NshopSlot12 , LshopSlot7,LshopSlot8, LshopSlot9,LshopSlot10,LshopSlot11,LshopSlot12;
	int  paginaShop, selectareItem;
	public GUIStyle SageataJos, SageataSus, playstyle, coinsShopStyle, HighScoreStyleText, CreditsStyleText, detaliiShopStyleText, shopSlot, butonBuy, butonEquip, shopSlot1,shopSlot2,shopSlot3,shopSlot4,shopSlot5,shopSlot6,shopSlot7,shopSlot8,shopSlot9,shopSlot10,shopSlot11,shopSlot12;
	public GUIStyle quitstyle,infoPozaShopStyle, butonDreaptaStyle, butonStangaStyle, butonBack, highscoreStyle,creditsStyle, replayStyle, selectareLevel, ShopStyle, selectareReplay, butonPlayReplay,butonDeleteReplay;
	public Texture2D bg_menu,Replay1,Replay2, bg_shop, speedStatScris, handlingStatScris, brakesStatScris, baraStats;
	public Texture2D bg_levels, bg_replays, Moneda, casutaShop , bgHighScore, bgCredits;
	public Texture2D _12, _18 , _22, _48, _98 , _160 , _82, _150, _290, _80, goala;
	float dreapta,stanga;
	GUIStyle MoneyStyle;
	int j=0, m=0;
	int[] replay_lvl=new int[20];
	int[] replay_lvl2=new int[20];
	
	public bool crawling;
	
	float x,y;
	int n=-1;
	public static string levelLoad;
	public static int selectLevel;
	
	String creds;
	float tempY = 320;
	float creditsHeight = 320;
	float speed = 67;
	
	Font chi640, mray640, grob640;
	
	void Start(){
		//PlayerPrefs.SetInt("money", 2000);
		creds = "Proiect de licenta\n\n Titlu lucrare de licenta:\n Programarea jocurilor\n pentru dispozitive mobile in Unity 3D\n\n Nume aplicatie:\n Motociclism\n\n Autor:\n Alexandru Petrescu\n\n";
		
		chi640 = (Font)Resources.Load("Fonts/chinonmn640", typeof(Font));
		mray640 = (Font)Resources.Load("Fonts/mailrays640", typeof(Font));
		grob640 = (Font)Resources.Load("Fonts/grobold640", typeof(Font));

		paginaShop = 1;
		speedStat = 40;
		brakeStat = 40;
		handlingStat = 40;
		selectareItem =-1;
		
		#region iteme
				for (int i=0;i<12;i++){
					if (PlayerPrefs.GetInt("shopItems["+i+"]", 0) == 2){
							if (i>=0 && i<=2){
								speedStat = 40 + i*40;
								PlayerPrefs.SetInt("AcceleratieStat", i);
							}
							if (i>=3 && i<=5){
								handlingStat = 40 + (i-3)*40;
								PlayerPrefs.SetInt("HandlingStat", (i-3));
							}
							if (i>=6 && i<=8){
								brakeStat = 40 + (i-6)*40;
								PlayerPrefs.SetInt("BrakeStat", (i-6));
							}
					}
				}
		print ("gooooing");
		if (PlayerPrefs.GetInt("shopItems["+0+"]", 2) != 1){
			PlayerPrefs.SetInt("shopItems["+0+"]", 2);
		}
		if (PlayerPrefs.GetInt("shopItems["+3+"]", 2) != 1){
			PlayerPrefs.SetInt("shopItems["+3+"]", 2);
		}
		if (PlayerPrefs.GetInt("shopItems["+6+"]", 2) != 1){
			PlayerPrefs.SetInt("shopItems["+6+"]", 2);
		}
		if (PlayerPrefs.GetInt("shopItems["+9+"]", 2) != 1){
			PlayerPrefs.SetInt("shopItems["+9+"]", 2);
		}
		

		#endregion
		x=(float)Screen.width/960;
		y=(float)Screen.height/640;
		
		tempY = 320*y;
		
		stanga=1;
		dreapta=1;
		ReplayLvl();
		StaticData.replaymax=PlayerPrefs.GetInt("Max",1);
		MoneyStyle = new GUIStyle();
		MoneyStyle.normal.textColor = Color.white;
		MoneyStyle.font = chi640;
		HighScoreStyleText.font = chi640;
		selectareLevel.font = mray640;
		selectareReplay.font = mray640;
		detaliiShopStyleText.font = grob640;

		MoneyStyle.fontSize = 20;
		detaliiShopStyleText.normal.textColor = Color.white;
	
		//Debug.Log(PlayerPrefs.GetInt("AcceleratieStat",0) + " " + PlayerPrefs.GetInt("HandlingStat",0) + " " + PlayerPrefs.GetInt("BrakeStat",0));
	}
	void OnGUI(){
		
		x=(float)Screen.width/960;//1.6   220 -> 352 150->224
		y=(float)Screen.height/640;//1.125   65 -> 73,125 45 -> 51
		buttonSize = Screen.height / 6;//213,(3)
		buttonSizeW = Screen.width / 9;//80
		
		HighScoreStyleText.font = chi640;
		selectareLevel.font = mray640;
		selectareReplay.font = mray640;
		detaliiShopStyleText.font = grob640;
		
		if (PlayerPrefs.GetInt("shopItems["+9+"]", 2) == 2)
			MoveBike.costumModel =1;
		else if (PlayerPrefs.GetInt("shopItems["+10+"]", 2) == 2)
			MoveBike.costumModel =3;
		else if (PlayerPrefs.GetInt("shopItems["+11+"]", 2) == 2)
			MoveBike.costumModel =2;
		
		
	switch (selectLevel){
		//highscore
		case 4:
			
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), bgHighScore);
			if(GUI.Button(new Rect(820*x,250*y, 100*x, 100*y),"", SageataSus)){
				if (m > 0)
					m--;
			}
			if(GUI.Button(new Rect(820*x,500*y, 100*x, 100*y),"", SageataJos)){
				if (m < StaticData.nrLevels-5)
					m++;
			}
			
			//LevelNames
			GUI.Label(new Rect(250*x,295*y,150*x,50*y), "Nivel "+ (1+m),HighScoreStyleText);
			GUI.Label(new Rect(250*x,350*y,150*x,50*y), "Nivel "+ (2+m),HighScoreStyleText); 
			GUI.Label(new Rect(250*x,400*y,150*x,50*y), "Nivel "+ (3+m),HighScoreStyleText);
			GUI.Label(new Rect(250*x,452*y,150*x,50*y), "Nivel "+ (4+m),HighScoreStyleText);
			GUI.Label(new Rect(250*x,505*y,150*x,50*y), "Nivel "+ (5+m),HighScoreStyleText);
			GUI.Label(new Rect(250*x,555*y,150*x,50*y), "Nivel "+ (6+m),HighScoreStyleText);
			//ScoreLevels
			GUI.Label(new Rect(680*x,295*y,150*x,50*y), PlayerPrefs.GetInt("Level"+(m+0)+"Points",0).ToString(),HighScoreStyleText);
			GUI.Label(new Rect(680*x,350*y,150*x,50*y), PlayerPrefs.GetInt("Level"+(m+1)+"Points",0).ToString(),HighScoreStyleText); 
			GUI.Label(new Rect(680*x,400*y,150*x,50*y), PlayerPrefs.GetInt("Level"+(m+2)+"Points",0).ToString(),HighScoreStyleText);
			GUI.Label(new Rect(680*x,452*y,150*x,50*y), PlayerPrefs.GetInt("Level"+(m+3)+"Points",0).ToString(),HighScoreStyleText);
			GUI.Label(new Rect(680*x,505*y,150*x,50*y), PlayerPrefs.GetInt("Level"+(m+4)+"Points",0).ToString(),HighScoreStyleText);
			GUI.Label(new Rect(680*x,555*y,150*x,50*y), PlayerPrefs.GetInt("Level"+(m+5)+"Points",0).ToString(),HighScoreStyleText);
			//TimeLevels
			GUI.Label(new Rect(500*x,295*y,150*x,50*y), PlayerPrefs.GetFloat("Level"+(m+0)+"Time",0).ToString("#.##"),HighScoreStyleText);
			GUI.Label(new Rect(500*x,350*y,150*x,50*y), PlayerPrefs.GetFloat("Level"+(m+1)+"Time",0).ToString("#.##"),HighScoreStyleText); 
			GUI.Label(new Rect(500*x,400*y,150*x,50*y), PlayerPrefs.GetFloat("Level"+(m+2)+"Time",0).ToString("#.##"),HighScoreStyleText);
			GUI.Label(new Rect(500*x,452*y,150*x,50*y), PlayerPrefs.GetFloat("Level"+(m+3)+"Time",0).ToString("#.##"),HighScoreStyleText);
			GUI.Label(new Rect(500*x,505*y,150*x,50*y), PlayerPrefs.GetFloat("Level"+(m+4)+"Time",0).ToString("#.##"),HighScoreStyleText);
			GUI.Label(new Rect(500*x,555*y,150*x,50*y), PlayerPrefs.GetFloat("Level"+(m+5)+"Time",0).ToString("#.##"),HighScoreStyleText);
					
			
			if(GUI.Button(new Rect(10*x, 4*buttonSize + 100*y, 100*x, 100*y),"", butonBack)){
				selectLevel = 0;
			}
			break;
		//main menu	
		case 0:
				GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), bg_menu);
			if(GUI.Button(new Rect(Screen.width - buttonSize - 180*x, Screen.height/2 -50*y , 220*x, 65*y),"", playstyle)){
				selectLevel = 1 ;
				j=0;
			}
			
			if(GUI.Button(new Rect(10*x, Screen.height/2 + 230*y , 220*x, 65*y),"", ShopStyle)){
				selectLevel = 3;
			}
			if(GUI.Button(new Rect(Screen.width - buttonSize - 180*x, Screen.height/2 + 230*y, 220*x , 65*y),"",quitstyle)){
				Application.Quit();
			}
			if(GUI.Button(new Rect(Screen.width - buttonSize - 180*x, Screen.height/2 + 90*y, 220*x , 65*y),"",highscoreStyle))
			{
				selectLevel = 4;
			}
			if(GUI.Button(new Rect(Screen.width - buttonSize - 180*x, Screen.height/2 + 160*y, 220*x , 65*y),"",creditsStyle)){
				//Application.LoadLevel("levelEditor");
				tempY = 300;
				Time.timeScale = 1;
				selectLevel = 5;
			}
			if(GUI.Button(new Rect(Screen.width - buttonSize - 180*x, Screen.height/2 + 20*y, 220*x , 65*y),"",replayStyle)){
				selectLevel = 2;
				n=-1;
				j=0;
			}
			break;
			
		//credits
		case 5:
			//tempY=0;
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), bgCredits);
			if(GUI.Button(new Rect(10*x, 4*buttonSize + 100*y, 100*x, 100*y),"", butonBack)){
				selectLevel = 0;
				crawling = false;
			}
			
			GUI.BeginGroup(new Rect(160*x,258*y,640*x,320*y));
			//crawling = true;
			//print ("print (tempY);" + tempY);
			GUI.Label(new Rect(0,tempY,640*x,creditsHeight),creds,CreditsStyleText);
			
			GUI.EndGroup();
			
			break;
		
		//play	
		case 1:
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), bg_levels);	

			if ( GUI.Button(new Rect(10*x,Screen.height/2 + 60*y,100*x,100*y),"", butonStangaStyle)){
				if(j>0)
					j--;
			}

			if ( GUI.Button (new Rect(Screen.width - 110*x,Screen.height/2 + 60*y,100*x,100*y),"", butonDreaptaStyle)){
				if(j<StaticData.nrLevels-9)
					j++;
			}

			for(int i=0;i<StaticData.nrLevels;i++){		
				if (i<5){
					if(GUI.Button(new Rect(138*x + i*(140*x) + dreapta*x-stanga*x, Screen.height/2 - 10*y, 117*x, 117*y),""+(i+j+1), selectareLevel)){
						StaticData.points = 0;
						StaticData.selectedLevelName = "level"+(i+j);
						Application.LoadLevel("loadingPlay");
						levelLoad = "Play";
					}
				}
			
				if (i<10 && i>4){
					if(GUI.Button(new Rect(138*x + (i-5)*(140*x) + dreapta*x-stanga*x, Screen.height/2 + 110*y, 117*x, 117*y),""+(i+j+1),selectareLevel)){
						StaticData.points = 0;
						StaticData.selectedLevelName = "level"+(i+j);
						Application.LoadLevel("loadingPlay");
						levelLoad = "Play";
					}
				}
			}
			
			if(GUI.Button(new Rect(10*x, 4*buttonSize + 100*y, 100*x, 100*y),"", butonBack))
				selectLevel = 0;
			
			break;
		//replay
		case 2:
			#region case2
			int t=0;
			print ("Application.persistentDataPath " + Application.persistentDataPath);
				GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), bg_replays);	
			
		
			if(GUI.Button(new Rect(10*x, 4*buttonSize + 100*y, 100*x, 100*y),"", butonBack)){
				selectLevel = 0;
			}
			for(int i=0;i<20;i++){	
					if( i == n){
                        selectareReplay.normal.background=Replay2;
                    }
                    else{
                        selectareReplay.normal.background=Replay1;
                    }
					if(replay_lvl2[i] != -1){
						if (t<5){
								if(GUI.Button(new Rect(138*x + t*(140*x) + dreapta*x-stanga*x, Screen.height/2 - 10*y, 117*x, 117*y),""+ (t+j+1), selectareReplay)){
									n=i;
									print("n=" + n);
								}
						}
						if (t<10 && t>4){
							if(GUI.Button(new Rect(138*x + (t-5)*(140*x) + dreapta*x-stanga*x, Screen.height/2 + 110*y, 117*x, 117*y),""+ (t+j+1), selectareReplay)){
								n=i;
								print("n=" + n);
							}	
						}
						t++;
					}
					if (n != -1){
						print(replay_lvl2[n+j]);
						if(GUI.Button(new Rect(Screen.width/2 - 180*x , Screen.height - 80*y, buttonSizeW+70*x, buttonSize-40*y),"", butonPlayReplay)){
							int p=0;
							while (replay_lvl2[n+j] != replay_lvl[p]){
								p++;	
							}
							using (StreamReader sr = new StreamReader(Application.persistentDataPath + "/ghostPlayer_level" + replay_lvl[p] + ".txt")){
								StaticData.points = 0;
								StaticData.selectedLevelName =sr.ReadLine();
								StaticData.replayName = replay_lvl[p];			
							}		
							Application.LoadLevel("loadingPlay");
							levelLoad = "Replay";
						}	
						if(GUI.Button(new Rect(Screen.width/2 + 20*x , Screen.height - 80*y, buttonSizeW+70*x, buttonSize-40*y),"", butonDeleteReplay)){
							int p=0;
							while (replay_lvl2[n+j] != replay_lvl[p]){
								p++;	
							}		
							File.Delete(Application.persistentDataPath + "/ghostPlayer_level" + replay_lvl[(p)] + ".txt");
							PlayerPrefs.SetInt("R"+(p),-1);
							ReplayLvl();
								n = -1;
							if(j>0)
								j -= 1;
						}
					}	
			}	
			#endregion case2
			break;
		//shop
		case 3:
			
				GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), bg_shop);	
			#region infoSlots
				GUI.Button(new Rect(Screen.width/2 + 230*x, 150*y,60*x,60*y),"", coinsShopStyle);
				GUI.Button(new Rect(Screen.width/2 + 290*x, 220*y,120*x,120*y),"", infoPozaShopStyle);
			print ("selectareItem " + selectareItem);
			/*if(StaticData.firstStart){
				for (int i=0; i<12; i++){
					if(i==0 || i==3 || i==6 || i==9)
						PlayerPrefs.SetInt("shopItems["+i+"]", 2);
					else
						PlayerPrefs.SetInt("shopItems["+i+"]", 0);
				}
				StaticData.firstStart = false;
			}*/
			
				if (selectareItem == 0)
			{
				coinsShopStyle.normal.background = goala;
				infoPozaShopStyle.normal.background = NshopSlot1;
				GUI.Button(new Rect(Screen.width/2 + 300*x, 150*y,200*x,80*y),"Motor", detaliiShopStyleText);
				GUI.Button(new Rect(Screen.width/2 + 290*x, 190*y,200*x,80*y),"(Nivel 1)", detaliiShopStyleText);
			}
			if (selectareItem == 1)
			{
				infoPozaShopStyle.normal.background = NshopSlot2;
				coinsShopStyle.normal.background = _150;
				GUI.Button(new Rect(Screen.width/2 + 300*x, 150*y,200*x,80*y),"Motor", detaliiShopStyleText);
				GUI.Button(new Rect(Screen.width/2 + 290*x, 190*y,200*x,80*y),"(Nivel 2)", detaliiShopStyleText);
			}
			if (selectareItem == 2)
			{
				infoPozaShopStyle.normal.background = NshopSlot3;
				coinsShopStyle.normal.background = _290;
				GUI.Button(new Rect(Screen.width/2 + 300*x, 150*y,200*x,80*y),"Motor", detaliiShopStyleText);
				GUI.Button(new Rect(Screen.width/2 + 290*x, 190*y,200*x,80*y),"(Nivel 3)", detaliiShopStyleText);
			}
			if (selectareItem == 3)
			{
				coinsShopStyle.normal.background = goala;
				infoPozaShopStyle.normal.background = NshopSlot4;
				GUI.Button(new Rect(Screen.width/2 + 300*x, 150*y,200*x,80*y),"Suspensie", detaliiShopStyleText);
				GUI.Button(new Rect(Screen.width/2 + 290*x, 190*y,200*x,80*y),"(Nivel 1)", detaliiShopStyleText);
			}
			if (selectareItem == 4)
			{
				infoPozaShopStyle.normal.background = NshopSlot5;
				coinsShopStyle.normal.background = _98;
				GUI.Button(new Rect(Screen.width/2 + 300*x, 150*y,200*x,80*y),"Suspensie", detaliiShopStyleText);
				GUI.Button(new Rect(Screen.width/2 + 290*x, 190*y,200*x,80*y),"(Nivel 2)", detaliiShopStyleText);
			}
			if (selectareItem == 5)
			{
				infoPozaShopStyle.normal.background = NshopSlot6;
				coinsShopStyle.normal.background = _160;
				GUI.Button(new Rect(Screen.width/2 + 300*x, 150*y,200*x,80*y),"Suspensie", detaliiShopStyleText);
				GUI.Button(new Rect(Screen.width/2 + 290*x, 190*y,200*x,80*y),"(Nivel 3)", detaliiShopStyleText);
			}
			if (selectareItem == 6)
			{
			coinsShopStyle.normal.background = goala;
				infoPozaShopStyle.normal.background = NshopSlot7;
				GUI.Button(new Rect(Screen.width/2 + 300*x, 150*y,200*x,80*y),"Frane", detaliiShopStyleText);
				GUI.Button(new Rect(Screen.width/2 + 290*x, 190*y,200*x,80*y),"(Nivel 1)", detaliiShopStyleText);
			}
			if (selectareItem == 7)
			{
				infoPozaShopStyle.normal.background = NshopSlot8;
				coinsShopStyle.normal.background = _150;
				GUI.Button(new Rect(Screen.width/2 + 300*x, 150*y,200*x,80*y),"Frane", detaliiShopStyleText);
				GUI.Button(new Rect(Screen.width/2 + 290*x, 190*y,200*x,80*y),"(Level 2)", detaliiShopStyleText);
			}
			if (selectareItem == 8)
			{
				infoPozaShopStyle.normal.background = NshopSlot9;
				coinsShopStyle.normal.background = _290;
				GUI.Button(new Rect(Screen.width/2 + 300*x, 150*y,200*x,80*y),"Frane", detaliiShopStyleText);
				GUI.Button(new Rect(Screen.width/2 + 290*x, 190*y,200*x,80*y),"(Nivel 3)", detaliiShopStyleText);
			}
			if (selectareItem == 9)
			{
				infoPozaShopStyle.normal.background = NshopSlot10;
				coinsShopStyle.normal.background = goala;
				GUI.Button(new Rect(Screen.width/2 + 300*x, 150*y,200*x,80*y),"Costum", detaliiShopStyleText);
				GUI.Button(new Rect(Screen.width/2 + 290*x, 190*y,200*x,80*y),"(Rosu)", detaliiShopStyleText);
			}
			if (selectareItem == 10)
			{
				infoPozaShopStyle.normal.background = NshopSlot11;
				coinsShopStyle.normal.background = _18;
				GUI.Button(new Rect(Screen.width/2 + 300*x, 150*y,200*x,80*y),"Costum", detaliiShopStyleText);
				GUI.Button(new Rect(Screen.width/2 + 290*x, 190*y,200*x,80*y),"(Violet)", detaliiShopStyleText);
			}
			if (selectareItem == 11)
			{
				infoPozaShopStyle.normal.background = NshopSlot12;
				coinsShopStyle.normal.background = _22;
				GUI.Button(new Rect(Screen.width/2 + 300*x, 150*y,200*x,80*y),"Costum", detaliiShopStyleText);
				GUI.Button(new Rect(Screen.width/2 + 290*x, 190*y,200*x,80*y),"(Verde)", detaliiShopStyleText);
			}
			#endregion
			#region Slots
			//incarcare texturi butoane
			//prima pagina default
			if (PlayerPrefs.GetInt("shopItems["+0+"]", -2) == 2)
								shopSlot1.normal.background = CshopSlot1;
			//if (PlayerPrefs.GetInt("shopItems["+0+"]", 2) == 1)
			else
								shopSlot1.normal.background = NshopSlot1;
			
			if (PlayerPrefs.GetInt("shopItems["+3+"]", -2) == 2)
								shopSlot4.normal.background = CshopSlot4;
			//if (PlayerPrefs.GetInt("shopItems["+3+"]", 2) == 1)
			else
								shopSlot4.normal.background = NshopSlot4;
			
			//restu butoanelor

			if (PlayerPrefs.GetInt("shopItems["+1+"]", -2) == 2)
								shopSlot2.normal.background = CshopSlot2;
			if (PlayerPrefs.GetInt("shopItems["+1+"]", -2) != 2 && PlayerPrefs.GetInt("shopItems["+1+"]", 2) != 1)
								shopSlot2.normal.background = LshopSlot2;
			if (PlayerPrefs.GetInt("shopItems["+1+"]", -2) == 1)
								shopSlot2.normal.background = NshopSlot2;
			
			if (PlayerPrefs.GetInt("shopItems["+2+"]", -2) == 2)
								shopSlot3.normal.background = CshopSlot3;
			if (PlayerPrefs.GetInt("shopItems["+2+"]", -2) != 2 && PlayerPrefs.GetInt("shopItems["+2+"]", 2) != 1)
								shopSlot3.normal.background = LshopSlot3;
			if (PlayerPrefs.GetInt("shopItems["+2+"]", -2) == 1)
								shopSlot3.normal.background = NshopSlot3;
			
			if (PlayerPrefs.GetInt("shopItems["+4+"]", -2) == 2)
								shopSlot5.normal.background = CshopSlot5;
			if (PlayerPrefs.GetInt("shopItems["+4+"]", -2) != 2 && PlayerPrefs.GetInt("shopItems["+4+"]", 2) != 1)
								shopSlot5.normal.background = LshopSlot5;
			if (PlayerPrefs.GetInt("shopItems["+4+"]", -2) == 1)
								shopSlot5.normal.background = NshopSlot5;
			
			if (PlayerPrefs.GetInt("shopItems["+5+"]", -2) == 2)
								shopSlot6.normal.background = CshopSlot6;
			if (PlayerPrefs.GetInt("shopItems["+5+"]", -2) != 2 && PlayerPrefs.GetInt("shopItems["+5+"]", 2) != 1)
								shopSlot6.normal.background = LshopSlot6;
			if (PlayerPrefs.GetInt("shopItems["+5+"]", -2) == 1)
								shopSlot6.normal.background = NshopSlot6;
			
			if (PlayerPrefs.GetInt("shopItems["+6+"]", -2) == 2)
								shopSlot7.normal.background = CshopSlot7;
			/*if (PlayerPrefs.GetInt("shopItems["+6+"]", 2) != 2 && PlayerPrefs.GetInt("shopItems["+6+"]", 2) != 1)
								shopSlot7.normal.background = LshopSlot7;
			if (PlayerPrefs.GetInt("shopItems["+6+"]", 2) == 1)*/
			else
								shopSlot7.normal.background = NshopSlot7;
			
			if (PlayerPrefs.GetInt("shopItems["+7+"]", -2) == 2)
								shopSlot8.normal.background = CshopSlot8;
			if (PlayerPrefs.GetInt("shopItems["+7+"]", -2) != 2 && PlayerPrefs.GetInt("shopItems["+7+"]", 2) != 1)
								shopSlot8.normal.background = LshopSlot8;
			if (PlayerPrefs.GetInt("shopItems["+7+"]", -2) == 1)
								shopSlot8.normal.background = NshopSlot8;
			
			if (PlayerPrefs.GetInt("shopItems["+8+"]", -2) == 2)
								shopSlot9.normal.background = CshopSlot9;
			if (PlayerPrefs.GetInt("shopItems["+8+"]", -2) != 2 && PlayerPrefs.GetInt("shopItems["+8+"]", 2) != 1)
								shopSlot9.normal.background = LshopSlot9;
			if (PlayerPrefs.GetInt("shopItems["+8+"]", -2) == 1)
								shopSlot9.normal.background = NshopSlot9;
			
			if (PlayerPrefs.GetInt("shopItems["+9+"]", -2) == 2)
								shopSlot10.normal.background = CshopSlot10;
			/*if (PlayerPrefs.GetInt("shopItems["+9+"]", 2) != 2 && PlayerPrefs.GetInt("shopItems["+9+"]", 2) != 1)
								shopSlot10.normal.background = LshopSlot10;
			if (PlayerPrefs.GetInt("shopItems["+9+"]", 2) == 1)*/
			else
								shopSlot10.normal.background = NshopSlot10;
			
			if (PlayerPrefs.GetInt("shopItems["+10+"]", -2) == 2)
								shopSlot11.normal.background = CshopSlot11;
			if (PlayerPrefs.GetInt("shopItems["+10+"]", -2) != 2 && PlayerPrefs.GetInt("shopItems["+10+"]", 2) != 1)
								shopSlot11.normal.background = LshopSlot11;
			if (PlayerPrefs.GetInt("shopItems["+10+"]", -2) == 1)
								shopSlot11.normal.background = NshopSlot11;
			
//			print ("12" + Payer);
			if (PlayerPrefs.GetInt("shopItems["+11+"]", -2) == 2)
								shopSlot12.normal.background = CshopSlot12;
			if (PlayerPrefs.GetInt("shopItems["+11+"]", -2) != 2 && PlayerPrefs.GetInt("shopItems["+11+"]", 2) != 1)
								shopSlot12.normal.background = LshopSlot12;
			if (PlayerPrefs.GetInt("shopItems["+11+"]", -2) == 1)
								shopSlot12.normal.background = NshopSlot12;
			#endregion 
			for(int i=0;i<12;i++)
			{			
							if (i==0){
						if(GUI.Button(new Rect(255*x + 0*(140*x) + dreapta*x-stanga*x, Screen.height/2 - 185*y, buttonSizeW+23*x, buttonSize+10*y),"", shopSlot1))
						{
							selectareItem = i;
							speedStat = 40 + 0*30;
				GUI.Button(new Rect(Screen.width/2 + 300*x, 150*y,200*x,80*y),"Engine", detaliiShopStyleText);
				GUI.Button(new Rect(Screen.width/2 + 290*x, 190*y,200*x,80*y),"(Level 1)", detaliiShopStyleText);
				GUI.DrawTexture(new Rect(Screen.width/2 + 290*x, 220*y,120*x,120*y), NshopSlot1);
			
						}}
							if (i==1){
						if(GUI.Button(new Rect(255*x + 1*(140*x) + dreapta*x-stanga*x, Screen.height/2 - 185*y, buttonSizeW+23*x, buttonSize+10*y),"", shopSlot2))
						{
							selectareItem = i;
							speedStat = 40 + 1*30;
						}}
							if (i==2){
						if(GUI.Button(new Rect(255*x + 2*(140*x) + dreapta*x-stanga*x, Screen.height/2 - 185*y, buttonSizeW+23*x, buttonSize+10*y),"", shopSlot3))
						{
							selectareItem = i;
							speedStat = 40 + 2*30;
						}}
				
							if (i==6){
						if(GUI.Button(new Rect(255*x + 0*(140*x) + dreapta*x-stanga*x, Screen.height/2 - 65*y, buttonSizeW+23*x, buttonSize+10*y),"", shopSlot7))
						{
							selectareItem = i;
							brakeStat = 40 + 0*30;
						}}
							if (i==7){
						if(GUI.Button(new Rect(255*x + 1*(140*x) + dreapta*x-stanga*x, Screen.height/2 - 65*y, buttonSizeW+23*x, buttonSize+10*y),"", shopSlot8))
						{
							selectareItem = i;
							brakeStat = 40 + 1*30;
						}}
							if (i==8){
						if(GUI.Button(new Rect(255*x + 2*(140*x) + dreapta*x-stanga*x, Screen.height/2 - 65*y, buttonSizeW+23*x, buttonSize+10*y),"", shopSlot9))
						{
							selectareItem = i;
							brakeStat = 40 + 2*30;
						}}

						if (i==3){
						if(GUI.Button(new Rect(255*x + 0*(140*x) + dreapta*x-stanga*x, Screen.height/2 + 45*y, buttonSizeW+23*x, buttonSize+10*y),"", shopSlot4))
						{
							selectareItem = i;
							handlingStat = 40 + 0*30;
						}}
							if (i==4){
						if(GUI.Button(new Rect(255*x + 1*(140*x) + dreapta*x-stanga*x, Screen.height/2 + 45*y, buttonSizeW+23*x, buttonSize+10*y),"", shopSlot5))
						{
							selectareItem = i;
							handlingStat = 40 + 1*30;
						}}
							if (i==5){
						if(GUI.Button(new Rect(255*x + 2*(140*x) + dreapta*x-stanga*x, Screen.height/2 + 45*y, buttonSizeW+23*x, buttonSize+10*y),"", shopSlot6))
						{
							selectareItem = i;
							handlingStat = 40 + 2*30;
						}}

							if (i==9){
						if(GUI.Button(new Rect(255*x + 0*(140*x) + dreapta*x-stanga*x, Screen.height/2 + 160*y, buttonSizeW+23*x, buttonSize+10*y),"", shopSlot10))
						{
							selectareItem = i;
						}}
							if (i==10){
						if(GUI.Button(new Rect(255*x + 1*(140*x) + dreapta*x-stanga*x, Screen.height/2 + 160*y, buttonSizeW+23*x, buttonSize+10*y),"", shopSlot11))
						{
							selectareItem = i;
						}}
							if (i==11){
						if(GUI.Button(new Rect(255*x + 2*(140*x) + dreapta*x-stanga*x, Screen.height/2 + 160*y, buttonSizeW+23*x, buttonSize+10*y),"", shopSlot12))
						{
							selectareItem = i;
						}}
				}

			
			//resetare Buy pe toate obiectele
			/*if(GUI.Button(new Rect(Screen.width/2 + 280*x, Screen.height/2 + 280*y, 100*x , 45*y),"", butonBuy))
			{
				
				PlayerPrefs.SetInt("money", 2000);
			} //cheats
			*/
			//tot ce tine de shop
			for (int i=0; i<12; i++)
			{
				if ( selectareItem == i)
				{
					if ( PlayerPrefs.GetInt("shopItems["+i+"]", 0) != 1 && PlayerPrefs.GetInt("shopItems["+i+"]", 0) != 2 )
							if(GUI.Button(new Rect(Screen.width/2 + 280*x, 510*y, 150*x , 45*y),"", butonBuy))
								{
						if (i==1 || i ==7)
						{
							if (PlayerPrefs.GetInt("money",0)>=150)
															{
															PlayerPrefs.SetInt("shopItems["+i+"]", 1);
															PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money",0)-150));
															}
						}
						if (i==2 || i ==8)
						{
							if (PlayerPrefs.GetInt("money",0)>=290)
															{
															PlayerPrefs.SetInt("shopItems["+i+"]", 1);
															PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money",0)-290));
															}
						}
						if (i==5)
						{
							if (PlayerPrefs.GetInt("money",0)>=160)
															{
															PlayerPrefs.SetInt("shopItems["+i+"]", 1);
															PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money",0)-160));
															}
						}
						if (i==4)
						{
							if (PlayerPrefs.GetInt("money",0)>=98)
															{
															PlayerPrefs.SetInt("shopItems["+i+"]", 1);
															PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money",0)-98));
															}
						}
							if (i==10)
						{
							if (PlayerPrefs.GetInt("money",0)>=18)
															{
															PlayerPrefs.SetInt("shopItems["+i+"]", 1);
															PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money",0)-18));
															}
						}
							if (i==11)
						{
							if (PlayerPrefs.GetInt("money",0)>=22)
															{
															PlayerPrefs.SetInt("shopItems["+i+"]", 1);
															PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money",0)-22));
															}
						}
						
						
					
								}
					if (PlayerPrefs.GetInt("shopItems["+i+"]", 0) == 1)
					{
							if(GUI.Button(new Rect(Screen.width/2 + 280*x, 510*y, 150*x , 45*y),"", butonEquip))
								{
										if (i>=0 && i<=2)
												{
													for (int j=0; j<3;j++)
															{
													if (PlayerPrefs.GetInt("shopItems["+j+"]", 0) == 2)	
																PlayerPrefs.SetInt("shopItems["+j+"]", 1);
															}
											PlayerPrefs.SetInt("shopItems["+i+"]", 2);
											PlayerPrefs.SetInt("AcceleratieStat", i);
												}	
									if (i>=3 && i<=5)
												{
													for (int j=0; j<3;j++)
															{
													if (PlayerPrefs.GetInt("shopItems["+(j+3)+"]", 0) == 2)	
																PlayerPrefs.SetInt("shopItems["+(j+3)+"]", 1);
															}
											PlayerPrefs.SetInt("shopItems["+i+"]", 2);
											PlayerPrefs.SetInt("HandlingStat", (i-3));
												}	
									if (i>=6 && i<=8)
												{
													for (int j=0; j<3;j++)
															{
													if (PlayerPrefs.GetInt("shopItems["+(j+6)+"]", 0) == 2)	
																PlayerPrefs.SetInt("shopItems["+(j+6)+"]", 1);
															}
											PlayerPrefs.SetInt("shopItems["+i+"]", 2);
											PlayerPrefs.SetInt("BrakeStat", (i-6));
												}	
									if (i>=9 && i<=11)
												{
													for (int j=0; j<3;j++)
															{
													if (PlayerPrefs.GetInt("shopItems["+(j+9)+"]", 0) == 2)	
																PlayerPrefs.SetInt("shopItems["+(j+9)+"]", 1);
															}
											PlayerPrefs.SetInt("shopItems["+i+"]", 2);
												}	
								}
					}
					if (PlayerPrefs.GetInt("shopItems["+i+"]", 0) ==2)
								{}
				}
						
			}
			
			if(GUI.Button(new Rect(10*x, 4*buttonSize + 100*y, 100*x, 100*y),"", butonBack)){
				selectLevel = 0;	
			}
			
			GUI.DrawTexture(new Rect(Screen.width/2 + 305*x, 460*y, 35*x , 35*y), Moneda);	
			GUI.Button(new Rect(Screen.width/2 + 350*x, 465*y,100*x,40*y),(PlayerPrefs.GetInt("money",0)).ToString(), MoneyStyle);
			
			// Stats panel
			
			GUI.DrawTexture(new Rect(Screen.width/2 + 230*x,375*y,120*x,20*y), speedStatScris);	
			GUI.DrawTexture(new Rect(Screen.width/2 + 230*x,400*y,120*x,20*y), handlingStatScris);	
			GUI.DrawTexture(new Rect(Screen.width/2 + 230*x,425*y,120*x,20*y), brakesStatScris);	
			GUI.DrawTexture(new Rect(Screen.width/2 + 355*x,377*y,speedStat,15*y), baraStats);	
			GUI.DrawTexture(new Rect(Screen.width/2 + 355*x,402*y,handlingStat,15*y), baraStats);	
			GUI.DrawTexture(new Rect(Screen.width/2 + 355*x,427*y,brakeStat,15*y), baraStats);
			// End Stats panel
			
			break;

			}
}

			 
	void ReplayLvl()
	{
		//PlayerPrefs.DeleteAll();
		for(int i=0;i<20;i++)
		{
			replay_lvl[i]=PlayerPrefs.GetInt("R"+i,-1);
			replay_lvl2[i]=replay_lvl[i];
//			print ("replay_lvl2[" + i + "]= " + replay_lvl2[i]);
		}
		Array.Sort(replay_lvl2);
	}
	void Update () {
		//print(tempY);
		//print ("Time.deltaTime" + Time.deltaTime );
		tempY -= Time.deltaTime * speed;
		if(tempY<-creditsHeight)
			tempY = 320*y;
	}
}
