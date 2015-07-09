using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemAmmCanv : MonoBehaviour 
{
	private Component[] canvTexts;
	private Text bulletGui;
	private Text ammGui;
	private ItemGun weapon;
	
	void Start () 
	{
		ItemGun[] array = (ItemGun[]) GameObject.FindWithTag("Player").GetComponentsInChildren<ItemGun>();
		int i = 0;
		weapon = array[0];
		while(i < array.Length)
		{
			if(array[i].id < weapon.id)
			{
				weapon = array[i];
			}
			i++;
		}
		
		canvTexts = this.GetComponentsInChildren<Text>();
		bulletGui = (Text) canvTexts[0];
		ammGui = (Text) canvTexts[1];
		bulletGui.text = " ";
		ammGui.text = " ";
	}
	
	void Update () 
	{
		if((weapon.usedUp && weapon.isThrown) || weapon == null)
		{
			Destroy(this.gameObject);
		}
		
		if(weapon.isActiveAndEnabled && !weapon.isThrown)
		{
			bulletGui.text = weapon.getBulletSet().ToString();
			ammGui.text = weapon.getAmmunition().ToString();
		}
		else
		{
			bulletGui.text = " ";
			ammGui.text = " ";
		}
		
		
	}
}
