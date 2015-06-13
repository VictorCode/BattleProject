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
		this.ItemUpdate();
		
		if(!character.charMovement.isWalking())
		{
			zoomOut();
		}
		
		if (Input.GetMouseButtonDown(0) && (character.charMovement.isWalking() || !character.charMovement.isGrounded()))
		{
			shoot();
		}
		
		if(Input.GetMouseButton(1))
		{
			if(character.charMovement.isWalking())
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
