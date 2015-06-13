using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]

public class ItemCollision : MonoBehaviour 
{
	[SerializeField] private GameObject poof;
	[SerializeField] private AudioClip landingSound;

	private AudioSource a_source;
	private Item it;

	// Use this for initialization
	void Start () 
	{
		it = GetComponent<Item>();
		a_source = GetComponent<AudioSource>();
	}

	//Make landing sound unless all used up(ex. out of ammo) then poof and dissapear
	void OnCollisionEnter(Collision other)
	{
		if(it.usedUp)
		{
			Instantiate(poof);
			Destroy(this.gameObject);
		}
		else
		{
			it.rigBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
			a_source.clip = landingSound;
			a_source.Play();
		}
	}
	
	
	
}
