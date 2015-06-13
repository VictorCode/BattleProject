using UnityEngine;
using System.Collections;

public class ExMainWeapon2 : MainWeaponGun
{
	void Start () 
	{
		this.MainWeaponGunStart();
	}
	
	void Update () 
	{
		this.MainWeaponGunUpdate();
		
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
