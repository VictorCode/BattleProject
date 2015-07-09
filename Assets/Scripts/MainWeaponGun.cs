using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof (AudioSource))]

public class MainWeaponGun : MainWeapon
{
	[SerializeField] private bool infiniteAmmo;
	[SerializeField] private bool reloadable;
	[SerializeField] private int ammunitionMax;
	[SerializeField] private int bulletSetMax;
	[SerializeField] private float reloadTime;
	[SerializeField] private GameObject bullet;
	[SerializeField] private int bulletSpeed;
	[SerializeField] private AudioClip reloadSound;
	[SerializeField] private AudioClip emptyClip;
	[SerializeField] private AudioClip shootSound;
	[SerializeField] private Canvas ammuntionCanvas;
	
	private AudioSource audioSource;
	protected bool reloading;
	protected float rt;
	private int bulletSet;
	private int ammunition;
	private int need; //for reloading
	private int reloadDX;
	private Transform emitter;
	
	public void MainWeaponGunStart()
	{
		this.MainWeaponStart();
		audioSource = GetComponent<AudioSource>();
		emitter = GetComponentInChildren<Emitter>().gameObject.transform;
		
		if(!this.character.isLocalPlayer)
		{
			return;			//end here if not localPlayer
		}
		
		reloading = false;
		rt = 0.0f;
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
	
	public void MainWeaponGunUpdate()
	{
		this.MainWeaponUpdate();
		
		if(!this.character.isLocalPlayer)
		{
			return;
		}
		
		//universal reload input
		
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
	}
	
	public int shoot()
	{
		if(!reloading)
		{
			if(bulletSet > 0)
			{
				audioSource.clip = shootSound;
				audioSource.Play();
				GameObject bTemp = (GameObject) Instantiate(bullet,emitter.position,emitter.rotation);
				bTemp.GetComponent<Rigidbody>().velocity = emitter.transform.forward * bulletSpeed * 100 * Time.deltaTime;
				
				if(!infiniteAmmo || reloadable)
				{
					bulletSet--;
					return 1;
				}
			}
			else if(ammunition > 0)
			{
				if(reloadable)
				{
					audioSource.PlayOneShot(emptyClip);
					return -1;
				}
			}
			else
			{
				audioSource.PlayOneShot(emptyClip);
				return 1;
			}
		}
		return 0;
	}
	
	//returns the time when finished reloading. Failure to reload returns -1
	public float reload()
	{
		if(reloadable && !reloading && (ammunition != 0) && (bulletSet != bulletSetMax))
		{
			need = bulletSetMax - bulletSet;
			
			if(need != 0)
			{
				reloading = true;
				if(!infiniteAmmo)
				{
					reloadDX = WIHolder.changeId;
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
			return this.rt;
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
	
	public int getBulletSet()
	{
		return this.bulletSet;
	}
	
	public int getAmmuntion()
	{
		return this.ammunition;
	}
}
