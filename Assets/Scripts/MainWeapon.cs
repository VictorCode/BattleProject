using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Character))]
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (Camera))]

public class MainWeapon : MonoBehaviour 
{
	[SerializeField] protected bool isZoomable;
	[SerializeField] private Texture2D crosshair;
	[SerializeField] private Texture2D crosshairRed;
	[SerializeField] private float zoomDist;
	
	protected Character character;
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
		character = GetComponent<Character>();
		anim = GetComponent<Animator>();
		cam = GameObject.Find("MainCamera").GetComponent<Camera>();
		normalFOV = cam.fieldOfView;
	}
	
	//must be used first in each MainWeapon's Update function
	public void MainWeaponUpdate()
	{
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
	
	public void zoomIn()
	{
		if(isZoomable)
		{
			t = Time.timeSinceLevelLoad - Time.time;
			zooming = true;
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
	
}
