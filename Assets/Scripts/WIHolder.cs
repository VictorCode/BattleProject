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
	public int itemOffset;
	private bool weaponShow;
	private float mWheel;
	private float mWheelOld;
	private int wMax;
	private int iMax;
	private int oldItemIndex;
	private int oldWeaponIndex;
	public Transform hTransform;
	private bool oldWeaponShow;
	static public int changeId; //use to see if weapon/item has changed since last used
	private IKHands hands;

	void Start ()
	{
		hTransform = this.transform;
		character = (Character) GameObject.FindObjectOfType(typeof(Character));
		objects = new GameObject[character.getItemMax() + character.getWeaponMax() + 1];
		itemOffset = character.getWeaponMax();
		weaponIndex = 1;
		oldItemIndex = 1;
		oldWeaponIndex = 1;
		itemIndex = 1;
		weaponShow = true;
		oldWeaponShow = true;
		mWheel = 0.0f;
		mWheelOld = 0.0f;
		changeId = -100000;//increment up so more changes stored
		wMax = character.getWeaponMax();
		setupHolding(); // make sure occurs after appropriate initialization
		hands = GameObject.Find("BodyModel").GetComponent<IKHands>();
		hands.setHands(objects[1].name);
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
			objects[itemIndex + itemOffset].GetComponent<Item>().isThrown = true;
			GameObject temp = objects[itemIndex + itemOffset];
			temp.transform.SetParent(null);
			temp.GetComponent<Rigidbody>().isKinematic = false;
			temp.GetComponent<Collider>().isTrigger = false;
			temp.GetComponent<Rigidbody>().AddForce(forceThrow * 100 * transform.forward);
			objects[itemIndex + itemOffset] = new GameObject();
			character.inventory.removeItem(itemIndex);
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
			changeId++;
			if(weaponShow)
			{
				showWeapon(weaponIndex);
				hands.setHands(objects[weaponIndex].name);
			}
			else
			{
				showItem(itemIndex);
				hands.setHands(objects[itemIndex + itemOffset].name);
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
		
		for(int i = 1; i <= 5; i++)
		{
			objects[i + itemOffset] = new GameObject();
			objects[i + itemOffset].transform.SetParent(hTransform);
			objects[i + itemOffset].transform.localPosition = Vector3.zero;
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
