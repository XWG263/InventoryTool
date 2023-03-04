using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventorySlot : MonoBehaviour
{
	[SerializeField] Inventory _inventory;

	[SerializeField] int _inventorySlotIndex;

	[SerializeField] Image _itemIcon;

	[SerializeField] Image _activeIndicator;

	[SerializeField] TMP_Text _numbeiOfItems;

	private InventorySlot _slot;

	private void Start()
	{
		AssignSlot(_inventorySlotIndex);
	}

	public void AssignSlot(int slotIndex)
	{
		if (_slot != null) _slot.StateChanged -= OnStateChanged;
		_inventorySlotIndex = slotIndex;
		if (_inventory == null) _inventory = GetComponentInParent<UI_Inventory>().Inventory;
		_slot = _inventory.Slots[_inventorySlotIndex];
		_slot.StateChanged += OnStateChanged;

		UpdateViewState(_slot.State, _slot.Active);
	}

	void UpdateViewState(ItemStack state, bool active)
	{
		_activeIndicator.enabled = active;
		var item = state?.Item;
		var hasItem = item != null;
		var isStackable = hasItem && item.IsStackable;

		_itemIcon.enabled = hasItem;
		_numbeiOfItems.enabled = isStackable;
		if (!hasItem) return;

		_itemIcon.sprite = item.UiSPrite;
		if (isStackable) _numbeiOfItems.SetText(state.NumOfItems.ToString());
	}

	void OnStateChanged(object sender, InventorySlotChangedArgs args)
	{
		UpdateViewState(args.NewState, args.Active);
	}
}

