using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 基础信息
/// name
/// sprite
/// isStakable
/// </summary>
[CreateAssetMenu(menuName ="Inventory/Item Definition" , fileName ="New Item Definition") ]
public class ItemDefinitely : ScriptableObject 
{
	[SerializeField] string _name;
	[SerializeField] bool _isStakable;
	[SerializeField] Sprite _inGameSprite;
	[SerializeField] Sprite _uiSprite;

	public string Name => _name;

	public bool IsStackable => _isStakable;

	public Sprite InGmaeSprite => _inGameSprite;

	public Sprite UiSPrite => _uiSprite;
}

