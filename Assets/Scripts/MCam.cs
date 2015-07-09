using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MCam : MonoBehaviour 
{
	void Start()
	{
		if(!this.GetComponentInParent<Character>().isLocalPlayer)
		{
			this.gameObject.tag = "w";
			this.GetComponent<Camera>().enabled = false;
			this.GetComponent<AudioListener>().enabled = false;
		}
	}
	
}
