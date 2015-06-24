using UnityEngine;
using System.Collections;

public class Emitter : MonoBehaviour 
{
	Component[] array;
	private Item it;
	private Transform center;

	// Use this for initialization
	void Start () 
	{
		array = GetComponentsInParent<Item>();
		it = (Item) array[0];
		center = GameObject.Find("MainCamera/Center").transform;
		this.transform.LookAt(center);
	}
	
	//it's less expensive than having the item update every frame
	void Update ()
	{
		if(it.emitterCheck)
		{
			this.gameObject.name = "w";
		}
		else
		{
			this.gameObject.name = "Emitter";
		}
	}
}
