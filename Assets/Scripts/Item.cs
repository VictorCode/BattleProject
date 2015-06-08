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
	
	protected Animator anim;
	protected Rigidbody rigBody;
	protected bool zooming;
	private Camera cam;
	private float normalFOV;
	private float t;
	private AudioSource audioSource;
	protected bool usedUp;
	
	//must be used first in each Item's Update function
	public void ItemStart()
	{
		this.gameObject.tag = "Item";
		this.GetComponent<Collider>().isTrigger = true;
		anim = GetComponent<Animator>();
		rigBody = GetComponent<Rigidbody>();
		cam = GameObject.Find("MainCamera").GetComponent<Camera>();
		normalFOV = cam.fieldOfView;
		zooming = false;
		audioSource = GetComponent<AudioSource>();
		usedUp = false;
		rigBody.isKinematic = true;
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
			audioSource.Play();
		}
	}
	
}
