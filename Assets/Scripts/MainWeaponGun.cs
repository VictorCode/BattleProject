using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]

public class MainWeaponGun : MainWeapon
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
	
	private AudioSource audioSource;
	protected bool reloading;
	private float rt;
	private int bulletSet;
	private int ammunition;
	
	public void MainWeaponGunStart()
	{
		this.MainWeaponStart();
		reloading = false;
		audioSource = GetComponent<AudioSource>();
		rt = 0.0f;
		
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
	
	public void MainWeaponGunUpdate()
	{
		this.MainWeaponUpdate();
		
		//universal reload input
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
				audioSource.clip = shootSound;
				audioSource.Play();
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
					audioSource.PlayOneShot(emptyClip);
					rt = reload();
				}
			}
			else
			{
				audioSource.PlayOneShot(emptyClip);
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
				
				audioSource.PlayOneShot(reloadSound);
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
