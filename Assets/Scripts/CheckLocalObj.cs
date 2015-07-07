using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CheckLocalObj : NetworkBehaviour 
{
	//General disable local-only components script (only allowed one camera and audio listener etc...)
	[ClientCallback]
	void Awake() 
	{
		Debug.Log(isLocalPlayer);
		if (!isLocalPlayer)
		{
			if(this.GetComponent<Camera>())
			{
				this.GetComponent<Camera>().enabled = false;
			}
			if(this.GetComponent<AudioListener>())
			{
				this.GetComponent<AudioListener>().enabled = false;
			}
		}
	}
}
