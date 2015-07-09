using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour 
{
	[SerializeField] private AudioSource pickupSound;
	
	//use box collider to pickup or catch loose items
	public void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Item" && other.gameObject.GetComponent<Item>().isThrown == true)
		{
			other.gameObject.GetComponent<Item>().enabled = true;
			other.gameObject.GetComponent<Item>().isThrown = false;
			
			int theIndex = this.gameObject.GetComponentInParent<Character>().inventory.addItem(other.GetComponent<Item>());
			
			if(theIndex != -1)
			{
				pickupSound.Play();
				WIHolder temp = this.gameObject.GetComponentInParent<Character>().GetComponentInChildren<WIHolder>();
				other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
				other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				temp.objects[theIndex + temp.itemOffset] = other.gameObject;
				temp.objects[theIndex + temp.itemOffset].transform.rotation = temp.transform.rotation;
	 			temp.objects[theIndex + temp.itemOffset].transform.SetParent(temp.hTransform);
				other.gameObject.transform.localPosition = Vector3.zero;
				temp.objects[theIndex + temp.itemOffset].SetActive(false);
			}
			else
			{
				other.gameObject.GetComponent<Item>().enabled = false;
				other.gameObject.GetComponent<Item>().isThrown = true;
			}
		}
	}
	
	public void pickUpItem(Item it)
	{
		it.GetComponent<Item>().enabled = true;
		it.gameObject.GetComponent<Item>().isThrown = false;
		int theIndex = this.gameObject.GetComponentInParent<Character>().inventory.addItem(it);
		
		if(theIndex != -1)
		{
			pickupSound.Play();
			it.gameObject.GetComponent<Rigidbody>().isKinematic = true;
			it.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			WIHolder temp = this.gameObject.GetComponentInParent<Character>().GetComponentInChildren<WIHolder>();
			temp.objects[theIndex + temp.itemOffset] = it.gameObject;
			temp.objects[theIndex + temp.itemOffset].transform.rotation = temp.transform.rotation;
			temp.objects[theIndex + temp.itemOffset].transform.SetParent(temp.hTransform);
			it.gameObject.transform.localPosition = Vector3.zero;
			temp.objects[theIndex + temp.itemOffset].SetActive(false);
		}
		else
		{
			it.gameObject.GetComponent<Item>().enabled = false;
			it.gameObject.GetComponent<Item>().isThrown = true;
		}
	}
}
