using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Rigidbody))]

public class Bullet : MonoBehaviour
{
	[SerializeField] private int speed;
	[SerializeField] private GameObject hitSoundObj;

	private Transform emitter;
	
	void Start () 
	{
		this.gameObject.tag = "bullet"; // needed for weapon gui to function
		emitter = GameObject.Find("Emitter").transform;
		this.transform.position = emitter.position;
		this.transform.rotation = emitter.rotation;
		
		GetComponent<Rigidbody>().velocity = emitter.transform.forward * speed * 50 * Time.deltaTime;
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
