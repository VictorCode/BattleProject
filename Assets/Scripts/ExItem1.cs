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
		
		if(!character.charMovement.isWalking() || this.reloading)
		{
			zoomOut();
		}
		
		if (Input.GetMouseButtonDown(0) && !this.reloading && (character.charMovement.isWalking() || !character.charMovement.isGrounded()))
		{
			shoot();
		}
		
		if(Input.GetMouseButton(1))
		{
			if(character.charMovement.isWalking() || !character.charMovement.isGrounded())
			{
				zoomIn();
			}
		}
		else
		{
			zoomOut();
		}
		
		if((this.getAmmunition()) == 0 && (this.getBulletSet() == 0))
		{
			this.usedUp = true;
		}
		else
		{
			this.usedUp = false;
		}
	}
}
