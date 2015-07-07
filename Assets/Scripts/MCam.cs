using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MCam : MonoBehaviour 
{
	void Start()
	{
		if(!GetComponentInParent<Character>().isLocalPlayer)
		{
			this.GetComponent<Camera>().enabled = false;
			this.GetComponent<AudioListener>().enabled = false;
		}
	}
	
}
