﻿using UnityEngine;
using System.Collections;

public class ExMainWeapon2 : MainWeaponGun
{
	private float temprt;
	void Start () 
	{
		this.MainWeaponGunStart();
	}
	
	void Update () 
	{
		this.MainWeaponGunUpdate();
		
		
		if (Input.GetKeyDown("r") && !this.reloading && ((temprt = reload()) > this.rt))
		{
			//reload animation
			this.rt = temprt;
		}

		if (Input.GetMouseButtonDown(0) && !this.reloading && (character.charMovement.isWalking() || !character.charMovement.isGrounded()))
		{
			if(shoot() == -1)
			{
				this.rt = reload();
				//reload animation
			}
		}
		
		if(Input.GetMouseButton(1) && !this.reloading)
		{
			zoomIn();
		}
		else
		{
			zoomOut();
		}
	}
	
}
