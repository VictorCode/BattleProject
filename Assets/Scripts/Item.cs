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
	[SerializeField] private AudioClip landingSound;
	[SerializeField] private GameObject poof;
	
	protected bool isThrown;
	protected Character character;
	protected Animator anim;
	public Rigidbody rigBody;
	protected bool zooming;
	private Camera cam;
	private float normalFOV;
	private float t;
	protected AudioSource audioSource;
	protected bool usedUp;
	private bool lMousePressed;
	private bool rMousePressed;
	private bool lMouse;
	private bool rMouse;
	private bool lMouseUp;
	private bool rMouseUp;
	
	//must be used first in each Item's Update function
	public void ItemStart()
	{
		this.gameObject.tag = "Item"; //if not tagged "Item" the WIHolder and picking up items won't work
		this.GetComponent<Collider>().isTrigger = true;
		anim = GetComponent<Animator>();
		rigBody = GetComponent<Rigidbody>();
		cam = GameObject.Find("MainCamera").GetComponent<Camera>();
		normalFOV = cam.fieldOfView;
		zooming = false;
		audioSource = GetComponent<AudioSource>();
		usedUp = false;
		rigBody.isKinematic = true;
		isThrown = false;
	}
	
	public void ItemUpdate()
	{
		this.ItemInput();
	}
	
	//Item's universal getinput function. Helps items not have to worry about if thrown
	private void ItemInput()
	{
		if(!isThrown)
		{
			if(Input.GetMouseButtonDown(0))
			{
				this.lMousePressed = true;
			}
			else
			{
				this.lMousePressed = false;
			}
			
			if(Input.GetMouseButtonDown(1))
			{
				this.rMousePressed = true;
			}
			else
			{
				this.rMousePressed = false;
			}
			
			if(Input.GetMouseButton(0))
			{
				this.lMouse = true;
			}
			else
			{
				this.lMouse = false;
			}
			
			if(Input.GetMouseButton(1))
			{
				this.rMouse = true;
			}
			else
			{
				this.rMouse = false;
			}
			
			if(Input.GetMouseButtonUp(0))
			{
				this.lMouseUp = true;
			}
			else
			{
				this.lMouseUp = false;
			}
			
			if(Input.GetMouseButtonUp(1))
			{
				this.rMouseUp = true;
			}
			else
			{
				this.rMouseUp = false;
			}
		}
		else
		{
			this.lMousePressed = false;
			this.rMousePressed = false;
			this.lMouse = false;
			this.rMouse = false;
			this.lMouseUp = false;
			this.rMouseUp = false;
		}
	}
	
	public void zoomIn()
	{
		if(isZoomable)
		{
			zooming = true;
			t = Time.timeSinceLevelLoad - Time.time;
			cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,zoomDist, (t + .01f) * Time.deltaTime * 1000);
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
	
	//Make landing sound unless all used up(ex. out of ammo) then poof and dissapear
	void OnCollisionEnter(Collision other)
	{
		if(usedUp)
		{
			Instantiate(poof);
			Destroy(this.gameObject);
		}
		else
		{
			rigBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
			audioSource.clip = landingSound;
			audioSource.Play();
		}
	}
	
	public void setThrown(bool tossed)
	{
		isThrown = tossed;
	}
	
	//Item Input Interface
	public bool lClickPressed()
	{
		return lMousePressed;
	}
	
	public bool rClickPressed()
	{
		return rMousePressed;
	}
	
	public bool lClick()
	{
		return lMouse;
	}
	
	public bool rClick()
	{
		return rMouse;
	}
	
	public bool lClickUp()
	{
		return lMouseUp;
	}
	
	public bool rClickUp()
	{
		return rMouseUp;
	}
}
