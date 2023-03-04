using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 调整收集物品信息
/// </summary>
public class GameItem : MonoBehaviour
{
	[SerializeField] ItemStack _stack;
	[SerializeField] SpriteRenderer _spriteRenderer;

	[Header("Throw Settings")]
	[SerializeField] float _colliderEnableAfter = 1f;

	[SerializeField] float _throwGravity = 2f;

	[SerializeField] float _minXForce = 3f;

	[SerializeField] float _maxXForce = 5f;

	[SerializeField] float _throwYForce = 5f;

	Collider2D _collider;
	Rigidbody2D _rb;

	public ItemStack Stack => _stack;

	private void Awake()
	{
		_collider = GetComponent<Collider2D>();
		_rb = GetComponent<Rigidbody2D>();
		_collider.enabled = false;
	}

	private void Start()
	{
		SetupGameObject();
		StartCoroutine(EnableCollider(_colliderEnableAfter));
	}

	private void OnValidate()
	{
		SetupGameObject();
	}
	void SetupGameObject()
	{
		if (_stack.Item == null) return;
		SetGameSprite();
		AdjustNumberOfItems();
		UpdateGameObjectName();
	}

	void SetGameSprite()
	{
		_spriteRenderer.sprite = _stack.Item.InGmaeSprite;
	}

	void UpdateGameObjectName()
	{
		var name = _stack.Item.Name;

		var number = _stack.IsStackable ? _stack.NumOfItems.ToString() : "ns";
		gameObject.name = $"{name}({number})";
	}

	void AdjustNumberOfItems()
	{
		_stack.NumOfItems = _stack.NumOfItems;
	}

	public ItemStack Pick()
	{
		//Destroy(gameObject);
		gameObject.SetActive(false);
		return _stack;
	}

	public void Throw(float xDir)
	{
		_rb.gravityScale = _throwGravity;
		var throwXForce = Random.Range(_minXForce, _maxXForce);
		_rb.velocity = new Vector2(Mathf.Sign(xDir) * throwXForce, _throwYForce);

		StartCoroutine(DisableGravity(_throwYForce));
	}

	IEnumerator DisableGravity(float atYVelocity)
	{
		yield return new WaitUntil(() => _rb.velocity.y < -atYVelocity);
		_rb.velocity = Vector2.zero;
		_rb.gravityScale = 0;
	}

	public IEnumerator EnableCollider(float afterTime)
	{
		yield return new WaitForSeconds(afterTime);
		_collider.enabled = true;
	}

	public void SetStack(ItemStack itemStack)
	{
		_stack = itemStack;
	}
}

