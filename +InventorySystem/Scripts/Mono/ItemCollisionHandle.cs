using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemCollisionHandle : MonoBehaviour
{
	Inventory _inventory;

	private void Awake()
	{
		_inventory = GetComponentInParent<Inventory>();
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.TryGetComponent<GameItem>(out var gameItem) ||
		!_inventory.CanAcceptItem(gameItem.Stack)) return;

		_inventory.AddItem( gameItem.Pick());
	}
}

