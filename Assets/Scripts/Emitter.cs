using UnityEngine;
using System.Collections;

public class Emitter : MonoBehaviour 
{
	Component[] array;
	private Item it;


	// Use this for initialization
	void Start () 
	{
		array = GetComponentsInParent<Item>();
		it = (Item) array[0];
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
