using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_Inventory : MonoBehaviour
{
	[SerializeField]GameObject _inventorySlotPrefab;

	[SerializeField] Inventory _inventory;

	[SerializeField] List<UI_InventorySlot> _slots;
	/// <summary>
	/// UI格子信息
	/// </summary>
	public Inventory Inventory => _inventory;

	[ContextMenu("Initialize Inventory")]
	void InitializeInventoryUI()
	{
		if (_inventory == null || _inventorySlotPrefab == null) return;

		_slots = new List<UI_InventorySlot>(_inventory.Size);
		for (int i = 0; i < _inventory.Size; i++)
		{
			var uiSlot =UnityEditor.PrefabUtility.InstantiatePrefab(_inventorySlotPrefab) as GameObject;
			uiSlot.transform.SetParent(transform, false);
			var uiSlotScript = uiSlot.GetComponent<UI_InventorySlot>();
			uiSlotScript.AssignSlot(i);
			_slots.Add(uiSlotScript);
		}
		
	}
}

