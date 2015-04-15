using UnityEngine;
using System.Collections;

public class AnimationBiker : MonoBehaviour {
	int k=0,k1=0,j=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		k1=k;
		if(Input.GetKeyDown("a"))
		{
			k=1;
		}
		
		if (Input.GetKeyUp("a"))
		{
			k=2;
		}
		
		if(Input.GetKeyDown("d"))
		{
			k=3;
		}
		
		if (Input.GetKeyUp("d"))
		{
			k=4;
		}
		//print (k+"________________"+j);
		//if(Input.ge)
		if(k1!=k)
		{
			j=k;
		//print (k+"_______________________");
		anim(j);
		}
	//	print (k+"________________"+j);
	}
	
	
	void anim(int t)
	{
		
		switch (t)
		{
		case 0 :
			//nimik
			break;
			case 1:
			Lean_back();
			j=0;
			break;
			case 2:
			Lean_back_sit();
			j=0;
			break;
			case 3:
			Lean_front();
			j=0;
			break;
			case 4:
			Lean_front_sit();
			j=0;
			break;
		}
	}
	
	
	void Lean_front()
	{
		animation["Lean_front"].speed = 1.0f;
		animation.Play("Lean_front");
	}
	void Lean_front_sit()
	{
		animation["Lean_front"].time = animation["Lean_front"].length;
		animation["Lean_front"].speed = -1.0f;
  	    animation.Play("Lean_front");	
	}
	void Lean_back()
	{
		animation["Lean_back"].speed = 1.0f;
		animation.Play("Lean_back");
	}
	void Lean_back_sit()
	{
		animation["Lean_back_sit"].speed = 1.0f;
		animation.Play("Lean_back_sit");
	}
}
