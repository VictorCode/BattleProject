using UnityEngine;
using System.Collections;

public class ItemBox : MonoBehaviour 
{
	[SerializeField] private Item[] items;
	[SerializeField] private int speed;
	
	void Update () 
	{
		transform.Rotate(Vector3.up, speed * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			GameObject temp = Instantiate(items[((int)Mathf.Round(Random.Range(0.0f,items.Length - 1)))].gameObject);
			temp.transform.SetParent(GameObject.Find("WIHolder").transform);
			temp.transform.localPosition = Vector3.zero;
			temp.transform.rotation = GameObject.Find("WIHolder").transform.rotation;
			GameObject.Find("WIHolder").GetComponent<WIHolder>().objects[other.GetComponent<Character>().inventory.addItem(temp.GetComponent<Item>()) + GameObject.Find("WIHolder").GetComponent<WIHolder>().itemOffset] = temp;
			temp.gameObject.tag = "Item";
			temp.SetActive(false);
		}
	}
}
