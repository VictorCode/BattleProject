using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ExItem2 : Item 
{
	[SerializeField] private float health;
	[SerializeField] private int useNum;
	[SerializeField] private AudioClip healSound;

	private AudioSource aSource;
	private int drinkHash;
	
	// Use this for initialization
	void Start () 
	{
		this.ItemStart();
		aSource = GetComponent<AudioSource>();
		
		if(!this.character.isLocalPlayer)
		{
			return;			//end here if not localPlayer
		}
		
		drinkHash = Animator.StringToHash("corona");
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.ItemUpdate();
		
		if(!this.character.isLocalPlayer)
		{
			return;			//end here if not localPlayer
		}
		
		if (Input.GetMouseButtonDown(0) && (useNum != 0))
		{
			heal();
			bodyAnim.SetTrigger(drinkHash);
		}
		
		if(useNum == 0)
		{
			this.usedUp = true;
		}
	}
	
	void heal()
	{
		this.character.heal(health);
		aSource.clip = healSound;
		aSource.Play();
		useNum--;
	}
}
