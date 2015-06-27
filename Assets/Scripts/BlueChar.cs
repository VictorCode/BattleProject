using UnityEngine;
using System.Collections;

public class BlueChar : Character 
{
	// Use this for initialization
	// CharacterStart must be used first in each character's Start function
	void Start () 
	{
		this.CharacterStart();
	}
	
	// Update is called once per frame
	//CharacterUpdate must be used first in each character's Update function
	void Update () 
	{
		this.CharacterUpdate();
	
		if (Input.GetKeyDown("q"))
		{
			qpower();
		}
		
		if (Input.GetKeyDown("e"))
		{
			epower();
		}
		
		if (Input.GetKeyDown("f"))
		{
			fpower();
		}
		
		if (Input.GetKeyDown("h"))
		{
			this.hurt(10);
		}
		
		if (Input.GetKeyDown("g"))
		{
			this.usePower(10);
		}
	
	}
	
	void epower()
	{
		anim.SetTrigger(epowHash);
		usePower(20);
	}
	
	void qpower()
	{
		anim.SetTrigger(qpowHash);
		usePower(25);
	}
	
	void fpower()
	{
		anim.SetTrigger(fpowHash);
		usePower(10);
	}
}
