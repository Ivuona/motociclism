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
		if(Input.GetKeyDown(KeyCode.A))
		{
			k=1;
		}
		
		if (Input.GetKeyUp(KeyCode.A))
		{
			k=2;
		}
		
		if(Input.GetKeyDown(KeyCode.D))
		{
			k=3;
		}
		
		if (Input.GetKeyUp(KeyCode.A))
		{
			k=4;
			print ("upDDD");
		}
		if (Input.GetKeyDown("HorizontalRight"))
			k=5;
		if (Input.GetKeyUp("HorizontalRight"))
			k=6;
		print(k+"---");
		
		if(k1!=k)
		{
			j=k;
		anim(j);
		}

	}
	
	
	void anim(int t)
	{
		
		switch (t)
		{
			case 0 :
		//	Idle();
			break;
			case 1:
			Lean_back();
			j=0;
			break;
			case 2:
			Lean_back_sit();
			
			break;
			case 3:
			Lean_front();
			j=0;
			break;
			case 4:
			Lean_front_sit();

			break;
			case 5:
			Push_front();
			j=0;
			break;
		case 6:
			Push_front_sit();

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
	void Idle()
	{
		animation.Play("idle");
	}
	void Push_front()
	{
		animation["Push_front"].speed = 1.0f;
		animation.Play("Push_front");
	}
	void Push_front_sit()
	{
		animation["Push_front"].time = animation["Push_front"].length;
		animation["Push_front"].speed = -1.0f;
		animation.Play("Push_front");
		
	}
}
