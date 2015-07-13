using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (BoxCollider))]
[RequireComponent(typeof (AudioSource))]

public class Item : NetworkBehaviour 
{
	[SerializeField] private float zoomDist;
	[SerializeField] public Sprite symbol;
	[SerializeField] public Vector3 posOffset;
	
	public bool isThrown;
	public int id;
	public Character character;
	protected Animator anim;
	protected Animator bodyAnim;
	public Rigidbody rigBody;
	private float t;
	protected AudioSource audioSource;
	public bool usedUp;
	
	//must be used first in each Item's Update function
	public void ItemStart()
	{
		this.gameObject.tag = "Item"; //if not tagged "Item" the WIHolder and picking up etc... items won't work
		this.GetComponent<BoxCollider>().isTrigger = true; //Need for weapon collission
		character = this.GetComponentInParent<WIHolder>().character;
		anim = this.GetComponent<Animator>();
		bodyAnim = character.GetComponentInChildren<IKHands>().gameObject.GetComponent<Animator>(); //use IK hands to get body-model-object
		rigBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		usedUp = false;
		rigBody.isKinematic = true;
		isThrown = false;
		this.id = (int) Time.time;
		
		if(!this.character.isLocalPlayer)
		{
			return;			//end here if not localPlayer
		}
	}
	
	public void ItemUpdate()
	{
		if(this.isThrown)
		{
			this.enabled = false;
		}
	}
}
