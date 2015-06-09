using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof (CharacterMovement))]
[RequireComponent(typeof (Animator))]

public class Character : MonoBehaviour 
{
	[SerializeField] private float healthMax;
	[SerializeField] private float powerMax;
	[SerializeField] private int itemMax;
	[SerializeField] private int ePower;
	[SerializeField] private int qPower;
	[SerializeField] private int fPower;
	[SerializeField] private float healthRegenTime;
	[SerializeField] private float healthRegenSpeed;
	[SerializeField] private float powerRegenTime;
	[SerializeField] private float powerRegenSpeed;
	[SerializeField] private AudioSource throwSound;
	[SerializeField] private AudioSource epowerSound;
	[SerializeField] private AudioSource qpowerSound;
	[SerializeField] private AudioSource fpowerSound;
	[SerializeField] private AudioSource healthRegenSound;
	[SerializeField] private AudioSource powerRegenSound;
	[SerializeField] private AudioSource lowHealthSound;
	[SerializeField] private MainWeapon[] mWeapons;
	[SerializeField] private Canvas hud;
	
	private float health;
	private float power;
	protected CharacterMovement charMovement;
	protected Animator anim;
	private float timeSinceHit;
	private float timeSincePow;
	private bool hregSoundPlayed;
	private bool pregSoundPlayed;
	private bool lhSoundPlayed;
	private int hDangerThreshold;
	private Inventory inventory;
	
	//Reminder: the MainCamera object for the character must be named exactly "MainCamera" for items and weapons to find it
	//must be used first in each character's Start function to initialize properly
	public void CharacterStart()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		
		this.gameObject.tag = "Player";
		health = healthMax;
		power = powerMax;
		charMovement = GetComponent<CharacterMovement>();
		anim = GetComponent<Animator>();
		timeSinceHit = 0.0f;
		timeSincePow = 0.0f;
		hregSoundPlayed = false;
		pregSoundPlayed = false;
		lhSoundPlayed = false;
		hDangerThreshold = (int) (healthMax * .2);
		inventory = new Inventory(itemMax);
		// need to make it first...Instantiate(hud);
	}
	
	//must be used in each character's Update function to work
	public void CharacterUpdate()
	{
		//health regeneration functionality
		if(health < healthMax)
		{
			if((Time.timeSinceLevelLoad - timeSinceHit) >= healthRegenTime)
			{
				health += 1 * healthRegenSpeed * Time.deltaTime;
				
				if(!hregSoundPlayed)
				{
					healthRegenSound.Play();
					hregSoundPlayed = true;	
				}
				
				if(health >= healthMax)
				{
					health = healthMax;
					hregSoundPlayed = false;
					lhSoundPlayed = false;
				}
			}
			
			if(health <= hDangerThreshold)
			{
				if(!lhSoundPlayed)
				{
					lowHealthSound.Play();
					lhSoundPlayed = true;
				}
			}
			else
			{
				lowHealthSound.Stop();
			}
		}
		
		//power regeneration functionality
		if(power < powerMax)
		{
			if((Time.timeSinceLevelLoad - timeSincePow) >= powerRegenTime)
			{
				power += 1 * powerRegenSpeed * Time.deltaTime;
				
				if(!pregSoundPlayed)
				{
					powerRegenSound.Play();
					pregSoundPlayed = true;	
				}
				
				if(power >= powerMax)
				{
					power = powerMax;
					pregSoundPlayed = false;
				}
			}
		}
		
	}
	
	public void hurt(int damage)
	{
		timeSinceHit = Time.timeSinceLevelLoad;
		health -= damage;
		hregSoundPlayed = false;
		
		if(health <= 0)
		{
			die();
		}
	}
	
	private void die()
	{
		Debug.Log("Deattttthhhhh");	
	}
	
	public void heal(float moreHealth)
	{
		float need = healthMax - health;
		
		if(need >= moreHealth)
		{
			health += moreHealth;	
		}
		else
		{
			health += need;
		}
	}
	
	public void usePower(float usedPower)
	{
		if(power >= usedPower)
		{
			power -= usedPower;
			timeSincePow = Time.timeSinceLevelLoad;
			pregSoundPlayed = false;
		}
	}
	
	public void gainPower(float gainedPower)
	{
		float needP = powerMax - power;
		
		if(needP >= gainedPower)
		{
			power += gainedPower;
		}
		else
		{
			power += needP;
		}
	}
	
	public float getHealthMax()
	{
		return this.healthMax;
	}
	
	public float getHealth()
	{
		return this.health;
	}
	
	public float getPower()
	{
		return this.power;
	}
	
	public float getHealthRegenSpeed()
	{
		return this.healthRegenSpeed;
	}
	
	public float getPowerRegenSpeed()
	{
		return this.powerRegenSpeed;
	}
	
	public float getPowerMax()
	{
		return this.powerMax;
	}
	
	public int getWeaponMax()
	{
		return this.mWeapons.Length;
	}
	
	public int getItemMax()
	{
		return this.itemMax;
	}
	
	public int getEPower()
	{
		return this.ePower;
	}
	
	public int getQPower()
	{
		return this.qPower;
	}
	
	public int getFPower()
	{
		return this.fPower;
	}
	
	public float getHealthRegenTime()
	{
		return this.healthRegenTime;
	}
	
	public float getPowerRegenTime()
	{
		return this.powerRegenTime;
	}
	
	public float setHealthMax(float hm)
	{
		float hmold = this.healthMax;
		this.healthMax = hm;
		hDangerThreshold = (int) (healthMax * .2);
		return hmold;
	}
	
	public float setPowerMax(float pm)
	{
		float pmold = this.powerMax;
		this.powerMax = pm;
		return pmold;
	}
	
	public int setItemMax(int im)
	{
		int imold = this.itemMax;
		this.itemMax = im;
		return imold;
	}
	
	public int setEPower(int ep)
	{
		int epold = this.ePower;
		this.ePower = ep;
		return epold;
	}
	
	public int setQPower(int qp)
	{
		int qpold = this.qPower;
		this.qPower = qp;
		return qpold;
	}
	
	public int setFPower(int fp)
	{
		int fpold = this.ePower;
		this.ePower = fp;
		return fpold;
	}
	
	public float setHealthRegenTime(float hrt)
	{
		float hrtold = this.healthRegenTime;
		this.healthRegenTime = hrt;
		return hrtold;
	}
	
	public float setPowerRegenTime(float prt)
	{
		float prtold = this.powerRegenTime;
		this.powerRegenTime = prt;
		return prtold;
	}
	
	public float setHealthRegenSpeed(float hrs)
	{
		float hrsold = this.healthRegenSpeed;
		this.healthRegenSpeed = hrs;
		return hrsold;
	}
	
	public float setPowerRegenSpeed(float prs)
	{
		float prsold = this.powerRegenSpeed;
		this.powerRegenSpeed = prs;
		return prsold;
	}
}
