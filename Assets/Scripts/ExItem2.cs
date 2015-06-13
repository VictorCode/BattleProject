using UnityEngine;
using System.Collections;

public class ExItem2 : Item {

	[SerializeField] private float health;

	// Use this for initialization
	void Start () 
	{
		this.ItemStart();
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.ItemUpdate();
		
		if (Input.GetMouseButtonDown(0))
		{
			heal();
		}
	}
	
	void heal()
	{
		this.character.heal(health);
	}
	
}
