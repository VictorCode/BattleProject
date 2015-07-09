using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ItemBox : NetworkBehaviour
{
	[SerializeField] private Item[] items;
	[SerializeField] private int speed;
	private int iNum;
	
	//make server only and sync iNum
	void Start()
	{
		ItemNumGen();
	}
	
	void Update () 
	{
		transform.Rotate(Vector3.up, speed * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Character>() != null)
		{
			Character otherChar = other.GetComponent<Character>();
			if(otherChar.inventory.getItemNum() < 5)
			{
				GameObject temp = Instantiate(items[iNum].gameObject);
				WIHolder whtemp = otherChar.GetComponentInChildren<WIHolder>();
				temp.transform.SetParent(whtemp.gameObject.transform);
				temp.transform.localPosition = Vector3.zero;
				temp.transform.rotation = whtemp.gameObject.transform.rotation;
				whtemp.objects[otherChar.inventory.addItem(temp.GetComponent<Item>()) + whtemp.itemOffset] = temp;
				temp.gameObject.tag = "Item";
				temp.SetActive(false);
			}
			ItemNumGen(); //Make "respawn" after used and then use server only function ItemNumGen() to sync with all clients
		}
		
	}
	
	private void ItemNumGen()
	{
		iNum = (int) Mathf.Round(Random.Range(0.0f,items.Length - 1));
	}
}
