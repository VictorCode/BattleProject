using UnityEngine;
using System.Collections;

public class BulletHit : MonoBehaviour {

	[SerializeField] private AudioSource hitSound;
	
	void Start() 
	{
		hitSound.Play();
		Invoke("suicide", 1.0f);
	}
	
	void suicide()
	{
		Destroy (gameObject);
	}
}
