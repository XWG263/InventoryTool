using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 背包具体信息
/// 物品数量
/// 物品基类
/// </summary>
[System.Serializable]
public class ItemStack
{
	[SerializeField] ItemDefinitely _item;
	[SerializeField] int _numberOfItems;

	public bool IsStackable => _item != null && _item.IsStackable;

	public ItemDefinitely Item => _item;

	public int NumOfItems
	{
		get => _numberOfItems;
		set
		{
			value = value < 0 ? 0 : value;
			_numberOfItems = IsStackable ? value : 1;
		}
	}

	public ItemStack(ItemDefinitely item , int numberOfItems)
	{
		_item = item;
		NumOfItems = numberOfItems;
	}
	public ItemStack() { }
}

