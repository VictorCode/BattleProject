using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainAmmCanv : MonoBehaviour 
{
	private Component[] canvTexts;
	private Text bulletGui;
	private Text ammGui;
	MainWeaponGun weapon;
	
	void Start () 
	{
		weapon = (MainWeaponGun) GameObject.FindWithTag("MainWeapon").GetComponent<MainWeaponGun>();
		canvTexts = this.GetComponentsInChildren<Text>();
		bulletGui = (Text) canvTexts[0];
		ammGui = (Text) canvTexts[1];
		bulletGui.text = " ";
		ammGui.text = " ";
	}
	
	void Update () 
	{
		if(weapon.isActiveAndEnabled)
		{
			bulletGui.text = weapon.getBulletSet().ToString();
			ammGui.text = weapon.getAmmuntion().ToString();
		}
		else
		{
			bulletGui.text = " ";
			ammGui.text = " ";
		}
	}
}
