using UnityEngine;
using System.Collections;

public class ExMainWeapon1 : MainWeaponGun
{
	void Start () 
	{
		this.MainWeaponGunStart();
	}
	
	void Update () 
	{
		this.MainWeaponGunUpdate();
		
		if(!this.character.isLocalPlayer)
		{
			return;
		}
		
		if (Input.GetMouseButtonDown(0) && !this.reloading)
		{
			shoot();
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
