using UnityEngine;
using System.Collections;

//manages the poof effect when an item gets destroyed
public class Poof : MonoBehaviour 
{
	public AudioSource poof;
	
	void Start () 
	{
		poof.Play ();
		Invoke ("suicide", 1.0f);
	}
	
	public void suicide()
	{
		Destroy (gameObject);
	}
}
