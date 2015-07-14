using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ExItem1 : ItemGun 
{
	private int relHash;
	private float temprt;

	// Use this for initialization
	void Start ()
	{
		this.ItemGunStart();
		
		if(!this.character.isLocalPlayer)
		{
			return;			//end here if not localPlayer
		}
		
		relHash = Animator.StringToHash("expistolreload");
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.ItemGunUpdate();
		
		if(!this.character.isLocalPlayer)
		{
			return;			//end here if not localPlayer
		}
		
		if (Input.GetKeyDown("r") && !this.reloading && ((temprt = reload()) > this.rt))
		{
			bodyAnim.SetTrigger(relHash);
			this.rt = temprt;
		}
		
		if (Input.GetMouseButtonDown(0) && !this.reloading)
		{
			if(shoot() == -1)
			{
				this.rt = reload();
				bodyAnim.SetTrigger(relHash);
			}
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
