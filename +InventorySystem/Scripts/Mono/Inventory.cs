using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/// <summary>
/// 背包功能
/// 存储背包list信息 ，list<InventorySlot>
/// </summary>
public class Inventory : MonoBehaviour
{
	[SerializeField] int _size = 8;

	[SerializeField] List<InventorySlot> _slots;

	int _activeSlotIndex;

	public int Size => _size;

	public List<InventorySlot> Slots => _slots;

	public int ActiveSlotIndex
	{
		get => _activeSlotIndex;
		private set
		{
			//激活当前插槽
			_slots[_activeSlotIndex].Active = false;
			_activeSlotIndex = value < 0 ? _size - 1 : value % Size;
			_slots[_activeSlotIndex].Active = true;
		}
	}

	private void Awake()
	{
		if (_size > 0)
		{
			_slots[0].Active = true;
		}
	}


	private void OnValidate()
	{
		Adjustsize();
	}
	void Adjustsize()
	{
		if (_slots == null) _slots = new List<InventorySlot>();

		if (_slots.Count > _size) _slots.RemoveRange(_size, _slots.Count - _size);

		if (_slots.Count < _size) _slots.AddRange(new InventorySlot[_size - _slots.Count]);
	}

	public bool IsFull()
	{
		return _slots.Count(slot => slot.HasItem) >= _size; 
	}

	public bool CanAcceptItem(ItemStack itemStack)
	{
		var slotWithStackableitem = FindSlots(itemStack.Item, true);
		return !IsFull() || slotWithStackableitem != null;
	}

	InventorySlot FindSlots(ItemDefinitely item,bool onlyStackable = false)
	{
		return _slots.FirstOrDefault(slot => slot.Item == item &&
																	item.IsStackable ||
																	!onlyStackable);
	}

	public bool HasItem(ItemStack itemStack, bool checkNumberOfItems = false)
	{
		var itemSlot = FindSlots(itemStack.Item);
		if (itemSlot == null) return false;
		if (!checkNumberOfItems) return true;

		if (itemStack.Item.IsStackable)
		{
			return itemSlot.NumberOfItems >= itemStack.NumOfItems;
		}
		return _slots.Count(slot => slot.Item == itemStack.Item) >= itemStack.NumOfItems;
	}

	public ItemStack AddItem(ItemStack itemStack)
	{
		var relevantSlot = FindSlots(itemStack.Item, true);
		if (IsFull() && relevantSlot == null)
		{
			throw new InventoryException(InventoryOperation.Add, "Inventory is full");
		}

		if (relevantSlot != null)
		{
			relevantSlot.NumberOfItems += itemStack.NumOfItems;
		}
		else
		{
			relevantSlot = _slots.FirstOrDefault(slot => !slot.HasItem);
			relevantSlot.State = itemStack;
		}

		return relevantSlot.State;
	}

	public ItemStack RemoveItem(int atIndex , bool spawn = false)
	{
		if (!_slots[atIndex].HasItem)
		{
			throw new InventoryException(InventoryOperation.Remove, "slot is empty");
		}
		if (spawn && TryGetComponent<GameItemSpawner>(out var itemSpawner))
		{
			
			itemSpawner.SpawnItem(_slots[atIndex].State);
		}
		ClearSlot(atIndex);
		return new ItemStack();
	}

	public ItemStack RemoveItem(ItemStack itemStack)
	{
		var itemSlot = FindSlots(itemStack.Item);
		if (itemSlot == null)
			throw new InventoryException(InventoryOperation.Remove, "no item in inventory");
		if (itemSlot.Item.IsStackable && itemSlot.NumberOfItems < itemStack.NumOfItems)
			throw new InventoryException(InventoryOperation.Remove, "no enough items");

		itemSlot.NumberOfItems -= itemStack.NumOfItems;
		if (itemSlot.Item.IsStackable &&itemSlot.NumberOfItems > 0)
		{
			return itemSlot.State;
		}

		itemSlot.Clear();
		return new ItemStack();
	}

	public void ClearSlot(int atIndex)
	{
		_slots[atIndex].Clear();
	}

	public void ActivateSlot(int atIndex)
	{
		ActiveSlotIndex = atIndex;
	}

	public InventorySlot GetActiveSlot()
	{
		return _slots[ActiveSlotIndex];
	} 
}

