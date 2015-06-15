using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharCanvas : MonoBehaviour 
{
	private Component[] images;
	private Image healthBar;
	private Image healthBorder;
	private Image powerBar;
	private Color healthColor = Color.green;
	private Color borderColor = Color.black;
	private Color powerColor = Color.blue;
	private Character character;
	private float colordx;
	private float rdx;
	
	void Start () 
	{
		character = (Character) GameObject.FindWithTag("Player").GetComponent("Character");
		images = this.GetComponentsInChildren<Image>();
		healthBar = (Image) images[0];
		healthBorder = (Image) images[1];
		powerBar = (Image) images[2];
		healthColor.a = .75f;
		healthBar.color = healthColor;
		borderColor.a = .75f;
		healthBorder.color = borderColor;
		powerColor.a = .75f;
		powerBar.color = powerColor;
		colordx = 0.0f;
	}
	
	void Update () 
	{
		colordx = character.getHealth()/character.getHealthMax();
		rdx = 1.0f - colordx;
		healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount,colordx,Time.deltaTime * 15);
		powerBar.fillAmount = Mathf.Lerp(powerBar.fillAmount,character.getPower()/character.getPowerMax(),Time.deltaTime * 15);
		healthColor.g = colordx;
		healthColor.r = colordx - rdx;
		healthBar.color = healthColor;
	}
}
