using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameItemSpawner : MonoBehaviour
{
	[SerializeField] GameObject _itemBasePrefab;

	public void SpawnItem(ItemStack itemStack)
	{
		if (_itemBasePrefab == null) return;
		var item = PrefabUtility.InstantiatePrefab(_itemBasePrefab) as GameObject;
		item.transform.position = transform.position;
		var gameItemScript = item.GetComponent<GameItem>();
		gameItemScript.SetStack(new ItemStack(itemStack.Item, itemStack.NumOfItems));
		gameItemScript.Throw(transform.localScale.x);
	}
}

