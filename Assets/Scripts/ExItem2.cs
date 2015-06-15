using UnityEngine;
using System.Collections;

public class ExItem2 : Item {

	[SerializeField] private float health;
	[SerializeField] private int useNum;
	[SerializeField] private AudioClip healSound;

	private AudioSource aSource;
	
	// Use this for initialization
	void Start () 
	{
		this.ItemStart();
		aSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.ItemUpdate();
		
		if (Input.GetMouseButtonDown(0) && (useNum != 0))
		{
			heal();
		}
		
		if(useNum == 0)
		{
			this.usedUp = true;
		}
	}
	
	void heal()
	{
		this.character.heal(health);
		aSource.clip = healSound;
		aSource.Play();
		useNum--;
		
	}
	
}
