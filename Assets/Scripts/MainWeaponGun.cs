using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]

public class MainWeaponGun : MainWeapon
{
	[SerializeField] private bool infiniteAmmo;
	[SerializeField] private bool reloadable;
	[SerializeField] private int ammunition;
	[SerializeField] private int bulletSet;
	[SerializeField] private float reloadTime;
	[SerializeField] private GameObject bullet;
	[SerializeField] private AudioClip reload;
	[SerializeField] private AudioClip emptyClip;
	[SerializeField] private Canvas ammuntionCanvas;
	
	private AudioSource audioSource;
	protected bool reloading;
	
	public void MainGunStart()
	{
		this.MainWeaponStart();
		reloading = false;
		audioSource = GetComponent<AudioSource>();
		
		if(!infiniteAmmo)
		{	
			Instantiate(ammuntionCanvas);
		}
		
	}
	
	public void MainGunUpdate()
	{
		this.MainWeaponUpdate();
		
	}
	
	
}
