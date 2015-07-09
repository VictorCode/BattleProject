using UnityEngine;
using System.Collections;

public class Emitter : MonoBehaviour 
{
	private Transform center;

	// Use this for initialization
	void Start () 
	{
		if(GetComponentInParent<MainWeapon>() != null)
		{
			center = GetComponentInParent<MainWeapon>().character.GetComponentInChildren<CenterPos>().gameObject.transform;
		}
		else if(GetComponentInParent<Item>() != null)
		{
			center = GetComponentInParent<Item>().character.GetComponentInChildren<CenterPos>().gameObject.transform;
		}
		else
		{
			center = null;
		}
		this.transform.LookAt(center);
	}
}
