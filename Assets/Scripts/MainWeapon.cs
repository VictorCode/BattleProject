using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (Camera))]
[RequireComponent(typeof (BoxCollider))]

public class MainWeapon : NetworkBehaviour
{
	[SerializeField] protected bool isZoomable;
	[SerializeField] private Texture2D crosshair;
	[SerializeField] private Texture2D crosshairRed;
	[SerializeField] private float zoomDist;
	[SerializeField] public Vector3 posOffset;
	[SerializeField] private int range;
	
	public Character character;
	protected bool zooming;
	protected Animator anim;
	private bool enemyDetect;
	private Ray mouseRay;
	private RaycastHit hitInfo;
	private float t;
	private Camera cam;
	private float normalFOV;
	private float x;
	private float y;
	
	//must be used first in each MainWeapon's Start function to initialize properly
	public void MainWeaponStart()
	{
		this.gameObject.tag = "MainWeapon";
		this.GetComponent<BoxCollider>().isTrigger = true; //Need for weapon collission
		character = this.GetComponentInParent<WIHolder>().character;
		anim = this.GetComponent<Animator>();
		
		if(!this.character.isLocalPlayer)
		{
			cam = null;
			return;			//end here if not localPlayer
		}
		
		cam = Camera.main;
		normalFOV = cam.fieldOfView;
		
		if(zoomDist < 20)
		{
			zoomDist = 20;
		}
	}
	
	//must be used first in each MainWeapon's Update function
	public void MainWeaponUpdate()
	{
		if(!this.character.isLocalPlayer)
		{
			return;			//end here if not localPlayer
		}
		
		mouseRay = Camera.main.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0));
		
		if(Physics.Raycast(mouseRay, out hitInfo))
		{
			if(hitInfo.transform.gameObject.tag == "enemy")
			{
				enemyDetect = true;
			}
			else
			{
				enemyDetect = false;
			}
		}
		else
		{
			enemyDetect = false;
		}
	}
	
	void OnGUI()
	{
		if(!this.character.isLocalPlayer)
		{
			return;			//end here if not localPlayer
		}
		
		if(enemyDetect)
		{
			x = (Screen.width - crosshair.width) / 2;
			y = (Screen.height - crosshair.height) / 2;
			GUI.DrawTexture (new Rect (x, y,crosshairRed.width,crosshairRed.height), crosshairRed);
		}
		else
		{
			x = (Screen.width - crosshair.width) / 2;
			y = (Screen.height - crosshair.height) / 2;
			GUI.DrawTexture (new Rect (x, y,crosshair.width,crosshair.height), crosshair);
		}
	}
	
	public int getRange()
	{
		return this.range;
	}
	
	public void zoomIn()
	{
		if(isZoomable)
		{
			t = Time.timeSinceLevelLoad - Time.time;
			zooming = true;
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
