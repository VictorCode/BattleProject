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
		if(!isLocalPlayer)
		{
			return;
		}
	
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
			this.CmdHurt(10);
		}
		
		if (Input.GetKeyDown("g"))
		{
			this.CmdUsePower(10);
		}
	}
	
	//need to figure out animator sync
	void epower()
	{
		anim.SetTrigger(epowHash);
		CmdUsePower(20);
	}
	
	void qpower()
	{
		anim.SetTrigger(qpowHash);
		CmdUsePower(25);
	}
	
	void fpower()
	{
		anim.SetTrigger(fpowHash);
		CmdUsePower(10);
	}
}
