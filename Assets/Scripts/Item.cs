using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (Camera))]
[RequireComponent(typeof (BoxCollider))]
[RequireComponent(typeof (AudioSource))]

public class Item : MonoBehaviour 
{
	[SerializeField] protected bool isZoomable;
	[SerializeField] private float zoomDist;
	[SerializeField] public Sprite symbol;
	
	public bool isThrown;
	public bool emitterCheck;
	protected Character character;
	protected Animator anim;
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
		this.GetComponent<Collider>().isTrigger = true;
		
		if(zoomDist < 20)
		{
			zoomDist = 20;
		}
		
		character = GameObject.FindWithTag("Player").GetComponent<Character>();
		anim = GetComponent<Animator>();
		rigBody = GetComponent<Rigidbody>();
		cam = GameObject.Find("MainCamera").GetComponent<Camera>();
		normalFOV = cam.fieldOfView;
		zooming = false;
		audioSource = GetComponent<AudioSource>();
		usedUp = false;
		rigBody.isKinematic = true;
		isThrown = false;
		emitterCheck = false;
	}
	
	public void ItemUpdate()
	{
		if(this.isThrown)
		{
			emitterCheck = true;
			this.enabled = false;
		}
	}
	
	public void zoomIn()
	{
		if(isZoomable)
		{
			zooming = true;
			t = Time.timeSinceLevelLoad - Time.time;
			cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,1000/zoomDist, (t + .01f) * Time.deltaTime * 1500);
		}
	}
	
	public void zoomOut()
	{
		if(isZoomable)
		{
			zooming = false;
			cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,normalFOV,(t + .01f) * Time.deltaTime * 1500);
		}
	}
	
}
