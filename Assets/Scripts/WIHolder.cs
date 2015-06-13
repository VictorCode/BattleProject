using UnityEngine;
using System.Collections;

public class WIHolder : MonoBehaviour 
{
	[SerializeField] private AudioSource throwSound;
	[SerializeField] private int forceThrow;

	public GameObject[] objects;
	private Character character;
	private int itemIndex;
	private int weaponIndex;
	private int itemOffset;
	private bool weaponShow;
	private Inventory inventory;
	private float mWheel;
	private float mWheelOld;
	private int wMax;
	private int iMax;
	private int oldItemIndex;
	private int oldWeaponIndex;
	public Transform hTransform;
	private bool oldWeaponShow;

	void Start ()
	{
		hTransform = this.transform;
		character = (Character) GameObject.FindObjectOfType(typeof(Character));
		objects = new GameObject[character.getItemMax() + character.getWeaponMax() + 1];
		inventory = character.getInventory();
		itemOffset = character.getWeaponMax();
		weaponIndex = 1;
		oldItemIndex = 1;
		oldWeaponIndex = 1;
		itemIndex = 1;
		weaponShow = true;
		oldWeaponShow = true;
		mWheel = 0.0f;
		mWheelOld = 0.0f;
		wMax = character.getWeaponMax();
		setupHolding(); // must be last thing to occur in Start
	}
	
	void Update ()
	{
		//get input and decide weapon and item indexes
		mWheel += Input.GetAxis("Mouse ScrollWheel");
		
		if(mWheel != mWheelOld)
		{
			weaponShow = true;
			if(mWheel > mWheelOld)
			{
				if(weaponIndex < wMax)
				{
					weaponIndex++;
				}
			}
			else if(mWheel < mWheelOld)
			{
				if(weaponIndex > 1)
				{
					weaponIndex--;
				}
			}	
			mWheelOld = mWheel;
		}
		
		//throw item functionality
		if((Input.GetMouseButtonDown(2) || Input.GetKeyDown("t")) && !weaponShow)
		{
			throwSound.Play();
			inventory.items[itemIndex].setThrown(true);
			GameObject temp = objects[itemIndex];
			temp.transform.SetParent(temp.transform);
			temp.GetComponent<Rigidbody>().isKinematic = false;
			temp.GetComponent<Collider>().isTrigger = false;
			temp.GetComponent<Rigidbody>().AddForce(forceThrow * 100 * transform.forward);
			inventory.removeItem(itemIndex);
			objects[itemIndex] = null;
			weaponShow = true;
		}
		
		if (Input.GetKeyDown("1"))
		{
			if(itemIndex == 1 && !weaponShow)
			{
				weaponShow = true;
			}
			else
			{
				if(objects[1 + itemOffset].tag == "Item")
				{
					itemIndex = 1;
					weaponShow = false;
				}
			}
		}
		else if (Input.GetKeyDown("2"))
		{
			if(itemIndex == 2 && !weaponShow)
			{
				weaponShow = true;
			}
			else
			{
				if(objects[2 + itemOffset].tag == "Item")
				{
					itemIndex = 2;
					weaponShow = false;
				}
			}
		}
		else if (Input.GetKeyDown("3"))
		{
			if(itemIndex == 3 && !weaponShow)
			{
				weaponShow = true;
			}
			else
			{
				if(objects[3 + itemOffset].tag == "Item")
				{
					itemIndex = 3;
					weaponShow = false;
				}
			}
		}
		else if (Input.GetKeyDown("4"))
		{
			if(itemIndex == 4 && !weaponShow)
			{
				weaponShow = true;
			}
			else
			{
				if(objects[4 + itemOffset].tag == "Item")
				{
					itemIndex = 4;
					weaponShow = false;
				}
			}
		}
		else if (Input.GetKeyDown("5"))
		{
			if(itemIndex == 5 && !weaponShow)
			{
				weaponShow = true;
			}
			else
			{
				if(objects[5 + itemOffset].tag == "Item")
				{
					itemIndex = 5;
					weaponShow = false;
				}
			}
		}
		
		if((itemIndex != oldItemIndex) || (weaponIndex != oldWeaponIndex) || (weaponShow != oldWeaponShow))
		{
			if(weaponShow)
			{
				showWeapon(weaponIndex);
			}
			else
			{
				showItem(itemIndex);
			}
		}
		
		oldItemIndex = itemIndex;
		oldWeaponIndex = weaponIndex;
		oldWeaponShow = weaponShow;
	}
	
	private void setupHolding()
	{
		objects[0] = new GameObject();
		objects[0].transform.SetParent(hTransform);
		objects[0].transform.localPosition = Vector3.zero;
		
		for(int i = 0; i < wMax; i++)
		{
			objects[i + 1] = Instantiate(character.mWeapons[i].gameObject);
			objects[i + 1].transform.SetParent(hTransform);
			objects[i + 1].transform.localPosition = Vector3.zero;
		}
		
		for(int i = 0; i < 5;i++ )
		{
			objects[i + itemOffset + 1] = new GameObject();
			objects[i + itemOffset + 1].transform.SetParent(hTransform);
			objects[i + itemOffset + 1].transform.localPosition = Vector3.zero;
		}
		
		showWeapon(weaponIndex);
	}
	
	private void showWeapon(int index)
	{
		hideAll();
		objects[index].SetActive(true);
	}
	
	private void showItem(int index)
	{
		index += itemOffset;
		hideAll();
		objects[index].SetActive(true);
	}
	
	private void hideAll()
	{
		for(int i = 0; i < objects.Length; i++)
		{	
			objects[i].SetActive(false);
		}
	}
}
