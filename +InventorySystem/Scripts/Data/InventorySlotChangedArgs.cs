using System.Collections;
using UnityEngine;


public class InventorySlotChangedArgs
{
	public ItemStack NewState { get; }
	public bool Active { get; }

	public InventorySlotChangedArgs(ItemStack itemStack,bool actibe)
	{
		NewState = itemStack;
		Active = actibe;
	}
}
