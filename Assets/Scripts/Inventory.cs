using UnityEngine;
using System.Collections;

public class Inventory
{
	private int size;
	public Item[] items;
	private int itemNum;
	
	public Inventory(int num)
	{
		//max inventory size is 5
		if(num > 5)
		{
			num = 5;
		}
		
		itemNum = 0;
		size = num;
		items = new Item[size];
	}

	public int addItem(Item it)
	{
		if(itemNum < size)
		{
			int i = 0;
			while(i < size)
			{
				if(items[i] == null)
				{
					items[i] = it;
					itemNum++;
					return i;
				}
				i++;
			}
			return -1;
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
