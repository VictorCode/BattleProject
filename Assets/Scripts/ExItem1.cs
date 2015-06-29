using UnityEngine;
using System.Collections;

public class ExItem1 : ItemGun 
{
	private int relHash;
	private float temprt;

	// Use this for initialization
	void Start () 
	{
		this.ItemGunStart();
		relHash = Animator.StringToHash("expistolreload");
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.ItemGunUpdate();
		
		if (Input.GetKeyDown("r") && !this.reloading && ((temprt = reload()) > this.rt))
		{
			bodyAnim.SetTrigger(relHash);
			this.rt = temprt;
		}
		
		if (Input.GetMouseButtonDown(0) && !this.reloading && (character.charMovement.isWalking() || !character.charMovement.isGrounded()))
		{
			if(shoot() == -1)
			{
				this.rt = reload();
				bodyAnim.SetTrigger(relHash);
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
