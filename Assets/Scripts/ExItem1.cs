using UnityEngine;
using System.Collections;

public class ExItem1 : ItemGun 
{
	// Use this for initialization
	void Start () 
	{
		this.ItemGunStart();
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.ItemGunUpdate();
		
		if(!character.charMovement.isWalking())
		{
			zoomOut();
		}
		
		if (Input.GetMouseButtonDown(0) && !this.reloading && (character.charMovement.isWalking() || !character.charMovement.isGrounded()))
		{
			shoot();
		}
		
		if(Input.GetMouseButton(1))
		{
			if(!this.reloading && character.charMovement.isWalking())
			{
				zoomIn();
			}
		}
		else
		{
			zoomOut();
		}
	}
}
