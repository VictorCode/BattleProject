using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Rigidbody))]

public class Bullet : MonoBehaviour
{
	[SerializeField] private GameObject hitSoundObj;
	
	void Start ()
	{
		this.gameObject.tag = "bullet"; // needed for weapon gui to function
	}
	
	void OnCollisionEnter(Collision other)
	{	
		if((other.gameObject.tag != "MainWeapon") && (other.gameObject.tag != "Item"))
		{
			Instantiate(hitSoundObj);
			Destroy(this.gameObject);
		}
	}
}
