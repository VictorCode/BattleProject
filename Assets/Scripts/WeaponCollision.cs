using UnityEngine;
using System.Collections;

public class WeaponCollision : MonoBehaviour 
{
	public static bool colliding;
	
	void OnCollisionStay(Collision other)
	{
		if(other.gameObject.tag == "Surface")
		{
			colliding = true;
		}
	}
	
	void OnCollisionExit(Collision other)
	{
		if(other.gameObject.tag == "Surface")
		{
			colliding = false;
		}
	}
}
