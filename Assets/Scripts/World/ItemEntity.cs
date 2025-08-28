using System;
using InventorySystem;
using KBCore.Refs;
using Player;
using UnityEngine;

namespace World
{
    public class ItemEntity : ValidatedMonoBehaviour
    {
        [SerializeField, Self] private SpriteRenderer spriteRenderer;
        [SerializeField, Self] private Rigidbody2D rb;

        private Action _onDelete;
        private ItemStack _itemStack;
        private float _despawnTime;
        private float _time;
        private bool _despawnAwaited;

        public void Init(Action onDelete, ItemStack item, float despawnTime)
        {
            _onDelete = onDelete;
            _itemStack = item;
            _despawnTime = despawnTime;
            _time = 0;
            _despawnAwaited = false;
            spriteRenderer.sprite = _itemStack.itemType.Icon;
        }

        public void MoveTo(Vector2 position, bool animate)
        {
            if (animate)
            {
                rb.MovePosition(position);
            }
            else
            {
                rb.position = position;
            }
        }

        private void Update()
        {
            if (_despawnTime < 0) return;
            _time += Time.deltaTime;

            if (_time >= _despawnTime)
            {
                _onDelete();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_despawnAwaited) return;
            if (!other.CompareTag("Player")) return;
            if (other.TryGetComponent(out PlayerInventory inventory))
            {
                inventory.Inventory.AddAnywhere(_itemStack);
                _onDelete();
                _despawnAwaited = true;
            }
        }
    }
}