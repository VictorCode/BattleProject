using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (BoxCollider))]
[RequireComponent(typeof (AudioSource))]

public class Item : NetworkBehaviour 
{
	[SerializeField] protected bool isZoomable;
	[SerializeField] private float zoomDist;
	[SerializeField] public Sprite symbol;
	[SerializeField] public Vector3 posOffset;
	
	public bool isThrown;
	public int id;
	public Character character;
	protected Animator anim;
	protected Animator bodyAnim;
	public Rigidbody rigBody;
	protected bool zooming;
	private Camera cam;
	private float normalFOV;
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
		zooming = false;
		audioSource = GetComponent<AudioSource>();
		usedUp = false;
		rigBody.isKinematic = true;
		isThrown = false;
		//emitterCheck = false;
		this.id = (int) Time.time;
		
		if(!this.character.isLocalPlayer)
		{
			cam = null;
			return;			//end here if not localPlayer
		}
		
		if(zoomDist < 20)
		{
			zoomDist = 20;
		}
		
		cam = Camera.main;
		normalFOV = cam.fieldOfView;
	}
	
	public void ItemUpdate()
	{
		if(this.isThrown)
		{
			this.enabled = false;
		}
	}
	
	public void zoomIn()
	{
		if(this.isZoomable)
		{
			zooming = true;
			t = Time.timeSinceLevelLoad - Time.time;
			cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,1000/zoomDist, (t + .01f) * Time.deltaTime * 1500);
		}
	}
	
	public void zoomOut()
	{
		if(this.isZoomable)
		{
			zooming = false;
			cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,normalFOV,(t + .01f) * Time.deltaTime * 1500);
		}
	}
	
}
