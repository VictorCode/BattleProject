using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Networking;

[RequireComponent(typeof (CharacterMovement))]

public class Character : NetworkBehaviour
{
	[SerializeField] private float healthMax;
	[SerializeField] private float powerMax;
	[SerializeField] private int ePower;
	[SerializeField] private int qPower;
	[SerializeField] private int fPower;
	[SerializeField] private float healthRegenTime;
	[SerializeField] private float healthRegenSpeed;
	[SerializeField] private float powerRegenTime;
	[SerializeField] private float powerRegenSpeed;
	[SerializeField] private int itemMax;
	[SerializeField] private AudioSource epowerSound;
	[SerializeField] private AudioSource qpowerSound;
	[SerializeField] private AudioSource fpowerSound;
	[SerializeField] private AudioSource healthRegenSound;
	[SerializeField] private AudioSource powerRegenSound;
	[SerializeField] private AudioSource lowHealthSound;
	[SerializeField] public MainWeapon[] mWeapons;
	[SerializeField] private Canvas hud;
	[SerializeField] private Canvas invHud;
	public Inventory inventory;
	
	[SyncVar] public float health;
	[SyncVar] public float power;
	public CharacterMovement charMovement;
	protected Animator anim;
	[SyncVar] public float timeSinceHit;
	[SyncVar] public float timeSincePow;
	[SyncVar] public bool hregSoundPlayed;
	[SyncVar] public bool pregSoundPlayed;
	private int hDangerThreshold;
	private bool lhSoundPlaying;
	private int walkingHash;
	private int idleHash;
	private int crouchingHash;
	private int backHash;
	private int rightHash;
	private int bSpeedHash;
	protected int qpowHash;
	protected int epowHash;
	protected int fpowHash;
	
	//Reminder: the MainCamera object for the character must be named exactly "MainCamera" for items and weapons to find it
	//must be used first in each character's Start function to initialize properly
	void Awake()
	{
		this.GetComponentInChildren<WIHolder>().character = this;
	}
	
	public void CharacterStart()
	{
		if(!isLocalPlayer)
		{
			this.gameObject.tag = "enemy"; //need to figure out
			inventory = new Inventory(itemMax);
			return;
		}
		
		this.gameObject.tag = "Player";
		
		Cursor.lockState = CursorLockMode.Locked;	//cursor handling could be moved
		Cursor.visible = false;
		
		health = healthMax;
		power = powerMax;
		charMovement = GetComponent<CharacterMovement>();
		anim = GameObject.Find("BodyModel").GetComponent<Animator>();
		walkingHash = Animator.StringToHash("walking");
		idleHash = Animator.StringToHash("idle");
		crouchingHash = Animator.StringToHash("crouching");
		backHash = Animator.StringToHash("backward");
		bSpeedHash = Animator.StringToHash("backSpeed");
		rightHash = Animator.StringToHash("right");
		qpowHash = Animator.StringToHash("qpower");
		epowHash = Animator.StringToHash("epower");
		fpowHash = Animator.StringToHash("fpower");
		anim.SetBool(idleHash,false);
		anim.SetBool(walkingHash,false);
		anim.SetBool(crouchingHash,false);
		anim.SetBool(backHash,false);
		anim.SetFloat(rightHash,.5f);
		anim.SetFloat(bSpeedHash,0.0f);
		timeSinceHit = 0.0f;
		timeSincePow = 0.0f;
		hregSoundPlayed = false;
		pregSoundPlayed = false;
		lhSoundPlaying = false;
		hDangerThreshold = (int) (healthMax * .2);
		Instantiate(hud);
		inventory = new Inventory(itemMax);
		inventory.invCanvas = Instantiate(invHud).GetComponent<InventoryCanvas>();
	}
	
	//must be used in each character's Update function to work
	public void CharacterUpdate()
	{
		if(!isLocalPlayer)
		{
			return;
		}
	
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
				}
			}
			
			if(health <= hDangerThreshold)
			{
				if(!lhSoundPlaying)
				{
					lowHealthSound.Play();
					lhSoundPlaying = true;
				}
			}
			else
			{
				lowHealthSound.Stop();
				lhSoundPlaying = false;
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
		
		//handle animation
		if(Input.GetKey("w")) //moving forward
		{
			anim.SetBool(idleHash,false);
			anim.SetFloat(bSpeedHash,0.0f);
			anim.SetBool(backHash,false);
			if(Input.GetKey("left shift")) // forward sprinting
			{
				anim.SetBool(walkingHash,false);
				if(Input.GetKey("a"))
				{
					anim.SetFloat(rightHash,.25f);
				}
				else if(Input.GetKey("d"))
				{
					anim.SetFloat(rightHash,.75f);
				}
				else
				{
					anim.SetFloat(rightHash,.5f);
				}
			}
			else
			{
				anim.SetBool(walkingHash,true);
				if(Input.GetKey("a"))
				{
					anim.SetFloat(rightHash,.25f);
				}
				else if(Input.GetKey("d"))
				{
					anim.SetFloat(rightHash,.75f);
				}
				else
				{
					anim.SetFloat(rightHash,.5f);
				}
			}
		}
		else if(Input.GetKey("s")) //moving backwards
		{
			anim.SetBool(backHash,true);
			anim.SetBool(idleHash,false);
			anim.SetBool(walkingHash,false);
			if(Input.GetKey("left shift"))
			{
				anim.SetFloat(bSpeedHash,1.0f);
			}
			else
			{
				anim.SetFloat(bSpeedHash,.5f);
			}
		}
		else if(Input.GetKey("a")) //moving just left
		{
			anim.SetFloat(rightHash,0.0f);
			anim.SetBool(idleHash,false);
			anim.SetFloat(bSpeedHash,0.0f);
			anim.SetBool(backHash,false);
			if(Input.GetKey("left shift"))
			{
				anim.SetBool(walkingHash,false);
			}
			else
			{
				anim.SetBool(walkingHash,true);
			}
		}
		else if(Input.GetKey("d")) //moving just right
		{
			anim.SetFloat(rightHash,1.0f);
			anim.SetBool(idleHash,false);
			anim.SetFloat(bSpeedHash,0.0f);
			anim.SetBool(backHash,false);
			if(Input.GetKey("left shift"))
			{
				anim.SetBool(walkingHash,false);
			}
			else
			{
				anim.SetBool(walkingHash,true);
			}
		}
		else //idle
		{
			anim.SetBool(idleHash,true);
			anim.SetBool(walkingHash,false);
			anim.SetFloat(bSpeedHash,0.0f);
			anim.SetBool(backHash,false);
		}
	}
	
	[Server]
	public void hurt(int damage)
	{
		timeSinceHit = Time.timeSinceLevelLoad;
		health -= damage;
		hregSoundPlayed = false;
		
		if(health <= 0)
		{
			die(); //still have to figure out
		}
	}
	
	[Command]
	public void CmdHurt(int damage)
	{
		timeSinceHit = Time.timeSinceLevelLoad;
		health -= damage;
		hregSoundPlayed = false;
		
		if(health <= 0)
		{
			die(); //still have to figure out
		}
	}
	
	private void die()
	{
		Debug.Log("Deattttthhhhh");	
	}
	
	[Command]
	public void CmdHeal(float moreHealth)
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
	
	[Command]
	public void CmdUsePower(float usedPower)
	{
		if(power >= usedPower)
		{
			power -= usedPower;
			timeSincePow = Time.timeSinceLevelLoad;
			pregSoundPlayed = false;
		}
	}
	
	[Command]
	public void CmdGainPower(float gainedPower)
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
	
	public Inventory getInventory()
	{
		return this.inventory;
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
