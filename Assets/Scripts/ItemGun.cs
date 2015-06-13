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
	
	public void ItemGunStart()
	{
		this.ItemWeaponStart();
		reloading = false;
		
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
		
		if (Input.GetKeyDown("r") && (ammunition != 0) && (bulletSet != bulletSetMax) && !reloading)
		{
			rt = reload();
		}
		
		if(rt <= Time.time)
		{
			reloading = false;
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
			int need = bulletSetMax - bulletSet;
			
			if(need != 0)
			{
				reloading = true;
				if(!infiniteAmmo)
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
}
