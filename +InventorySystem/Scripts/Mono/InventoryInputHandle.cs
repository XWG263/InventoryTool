using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryInputHandle : MonoBehaviour
{
	Inventory _inventory;

	private void Awake()
	{
		_inventory = GetComponent<Inventory>();
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q ))
		{
			_inventory.ActivateSlot(_inventory.ActiveSlotIndex + 1);
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			_inventory.ActivateSlot(_inventory.ActiveSlotIndex - 1);
		}
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (_inventory.GetActiveSlot().HasItem)
			{
				_inventory.RemoveItem(_inventory.ActiveSlotIndex, true);
			}

		}
	}

}

