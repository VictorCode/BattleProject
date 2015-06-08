using UnityEngine;
using System.Collections;

public class Inventory
{
	private int size;
	private Item[] items;
	private int itemNum;
	
	public Inventory(int num)
	{
		itemNum = 0;
		size = num;
		items = new Item[size];
	}

	public int addItem(Item it)
	{
		if(itemNum < size)
		{
			for(int i = 0; i < size; i++)
			{
				if(items[i] != null)
				{
					items[i] = it;
					itemNum++;
				}
			}
			return 1;
		}
		else
		{
			return -1;
		}
	}
	
	public void removeItem(int index)
	{
		if(items[index] != null)
		{
			items[index] = null;
			itemNum--;
		}
	}
}
