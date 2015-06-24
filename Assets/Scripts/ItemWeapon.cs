using UnityEngine;
using System.Collections;

public class ItemWeapon : Item
{
	[SerializeField] private Texture2D crosshair;
	[SerializeField] private Texture2D crosshairRed;
	[SerializeField] private int range;
	
	private bool enemyDetect;
	private Ray mouseRay;
	private RaycastHit hitInfo;
	private float x;
	private float y;

	public void ItemWeaponStart()
	{
		this.ItemStart();
	}
	
	public void ItemWeaponUpdate()
	{
		this.ItemUpdate();
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
	
	public int getRange()
	{
		return this.range;
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
}
