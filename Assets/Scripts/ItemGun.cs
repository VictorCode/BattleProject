using UnityEngine;
using System.Collections;

public class ItemGun : ItemWeapon 
{
	[SerializeField] private bool infiniteAmmo;
	[SerializeField] private bool reloadable;
	[SerializeField] private int ammunitionMax;
	[SerializeField] private int bulletSetMax;
	[SerializeField] private float reloadTime;
	[SerializeField] private GameObject bullet;
	[SerializeField] private AudioClip reloadSound;
	[SerializeField] private AudioClip emptyClip;
	[SerializeField] private AudioClip shootSound;
	[SerializeField] private Canvas ammuntionCanvas;
	
	protected bool reloading;
	private float rt;
	private int bulletSet;
	private int ammunition;
	private int need; //for reloading
	private int reloadDX;
	
	public void ItemGunStart()
	{
		this.ItemWeaponStart();
		reloading = false;
		need = 0;
		reloadDX = WIHolder.changeId;
		
		if(!infiniteAmmo)
		{	
			bulletSet = bulletSetMax;
			ammunition = ammunitionMax;
			Instantiate(ammuntionCanvas);
		}
		else
		{
			bulletSet = 1000; //arbitrary non-zero or negative number so shoot function always satisfies first if-condition(shoot function will make sure it doesn't decrement)
		}
	}
	
	public void ItemGunUpdate() 
	{
		this.ItemWeaponUpdate();
		
		if(reloading && (reloadDX != WIHolder.changeId))
		{
			reloading = false;
		}
			
		if(rt <= Time.time)
		{
			if((reloadDX == WIHolder.changeId) && reloading)
			{
				finReload();
			}
			reloading = false;
		}
		
		if (Input.GetKeyDown("r") && (ammunition != 0) && (bulletSet != bulletSetMax) && !reloading)
		{
			rt = reload();
		}
		
	}
	
	public void shoot()
	{
		if(!reloading)
		{
			if(bulletSet > 0)
			{
				this.audioSource.clip = shootSound;
				this.audioSource.Play();
				Instantiate(bullet);
				
				if(!infiniteAmmo || reloadable)
				{
					bulletSet--;
				}
			}
			else if(ammunition > 0)
			{
				if(reloadable)
				{
					this.audioSource.PlayOneShot(emptyClip);
					rt = reload();
				}
			}
			else
			{
				this.audioSource.PlayOneShot(emptyClip);
			}	
		}	
	}
	
	//returns the time when finished reloading. Failure to reload returns -1
	public float reload()
	{
		if(reloadable && !reloading)
		{
			need = bulletSetMax - bulletSet;
			
			if(need != 0)
			{
				reloading = true;
				if(!infiniteAmmo)
				{
					this.reloadDX = WIHolder.changeId;
				}
				this.audioSource.PlayOneShot(reloadSound);
				return (Time.time + reloadTime);
			}
			else
			{
				return -1.0f;
			}
		}
		else
		{
			return -1.0f;
		}
	}
	
	private void finReload()
	{
		if(reloadDX == WIHolder.changeId)
		{
			if(ammunition >= need)
			{
				ammunition -= need;
				bulletSet += need;
			}
			else
			{
				bulletSet += ammunition;
				ammunition = 0;
			}
		}
	}
	
	public void addAmmo(int num)
	{
		if(num <= (ammunitionMax - ammunition))
		{
			ammunition += num;
		}
		else
		{
			ammunition += (ammunitionMax - ammunition);
		}
	}
	
	public int getAmmunition()
	{
		return this.ammunition;
	}
	
	public int getBulletSet()
	{
		return this.bulletSet;
	}
}
