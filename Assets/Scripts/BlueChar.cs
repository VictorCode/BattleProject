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
	
		
		if (Input.GetKeyDown("h"))
		{
			this.hurt(10);
		}
		
		if (Input.GetKeyDown("g"))
		{
			this.usePower(10);
		}
	
	}
}
