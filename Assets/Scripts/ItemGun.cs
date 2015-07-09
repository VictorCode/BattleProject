using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ItemGun : ItemWeapon 
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
	
	protected bool reloading;
	protected float rt;
	private int bulletSet;
	private int ammunition;
	private int need; //for reloading
	private int reloadDX;
	private Transform emitter;
	
	public void ItemGunStart()
	{
		this.ItemWeaponStart();
		emitter = GetComponentInChildren<Emitter>().gameObject.transform;
		
		if(!this.character.isLocalPlayer)
		{
			return;			//end here if not localPlayer
		}
		
		need = 0;
		
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
		
		reloadDX = WIHolder.changeId;
		reloading = false;
	}
	
	public void ItemGunUpdate() 
	{
		this.ItemWeaponUpdate();
		
		if(!this.character.isLocalPlayer)
		{
			return;			//end here if not localPlayer
		}
		
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
				this.audioSource.clip = shootSound;
				this.audioSource.Play();
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
					this.audioSource.PlayOneShot(emptyClip);
					return -1;
				}
			}
			else
			{
				this.audioSource.PlayOneShot(emptyClip);
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
	
	public int getAmmunition()
	{
		return this.ammunition;
	}
	
	public int getBulletSet()
	{
		return this.bulletSet;
	}
}
