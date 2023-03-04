using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class InventorySlot
{

    public event EventHandler<InventorySlotChangedArgs> StateChanged;

    [SerializeField] ItemStack _state;

    bool _active; 

    public ItemStack State
	{
        get => _state;
		set
		{
			_state = value;
			NotifyAboutStateChange();
		}
	}
	public bool Active
	{
		get => _active;
		set
		{
			_active = value;
			NotifyAboutStateChange();
		}
	}
	public int NumberOfItems
	{
		get => _state.NumOfItems;
		set
		{
			_state.NumOfItems = value;
			NotifyAboutStateChange();
		}
	}
	/// <summary>
	/// 存储信息不为空
	/// </summary>
	public bool HasItem => _state?.Item != null;

	public ItemDefinitely Item => _state?.Item;

	public void Clear()
	{
		State = null;
	}

	void NotifyAboutStateChange()
	{
        StateChanged?.Invoke(this,new InventorySlotChangedArgs(_state,_active));
	}
}

