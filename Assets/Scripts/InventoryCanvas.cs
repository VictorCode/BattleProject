using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryCanvas : MonoBehaviour 
{
	[SerializeField] private Sprite emptyBox;
	
	private Image[] InvImages;
	private Color boxColor = Color.white;
	private Color boxColorHidden = Color.white;
	
	void Start()
	{
		this.gameObject.tag = "InvCanvas"; //so WIHolder can find it
		InvImages = (Image[]) GetComponentsInChildren<Image>();
		boxColor.a = .75f;
		boxColorHidden.a = .25f;
		InvImages[0].color = boxColorHidden;
		InvImages[1].color = boxColorHidden;
		InvImages[2].color = boxColorHidden;
		InvImages[3].color = boxColorHidden;
		InvImages[4].color = boxColorHidden;
	}
	
	public void canvShow(int index, Sprite spr)
	{
		InvImages[index - 1].sprite = spr;
		InvImages[index - 1].color = boxColor;
	}
	
	public void canvHide(int index)
	{
		InvImages[index - 1].sprite = emptyBox;
		InvImages[index - 1].color = boxColorHidden;
	}
	
}
