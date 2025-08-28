using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI
{
    public class HotbarUI : MonoBehaviour
    {
        private ItemStack _active;
        private Dictionary<ItemStack, HotbarSlot> _slots;
        private TetrisInventory _inventory;
        private PlayerInventory _playerInv;

        [Header("References")]
        [SerializeField] private HotbarSlot slotPrefab;
        [SerializeField] private RectTransform row;

        public void Initialize(TetrisInventory inventory, PlayerInventory playerInv)
        {
            _slots = new Dictionary<ItemStack, HotbarSlot>();
            _inventory = inventory;
            _playerInv = playerInv;
            _inventory.OnItemChanged += InventoryOnOnItemChanged;
            _playerInv.OnSelectedItemChanged += PlayerInvOnOnSelectedItemChanged;
        }
        
        private void OnDestroy()
        {
            _inventory.OnItemChanged -= InventoryOnOnItemChanged;
            _playerInv.OnSelectedItemChanged -= PlayerInvOnOnSelectedItemChanged;
        }
        
        private void PlayerInvOnOnSelectedItemChanged(ItemStack obj)
        {
            if (_active != null && _slots.TryGetValue(_active, out var slot))
            {
                slot.Unselect();
            }
            
            _active = obj;
            if (_active == null) return;
            _slots[_active].Select();
        }

        private void InventoryOnOnItemChanged(ItemStack obj)
        {
            if (obj.position.y >= 2) return;
            _slots[obj].Initialize(obj);
        }

        public void InventoryOnOnItemReplaced(ItemStack from, ItemStack to)
        {
            var slot = _slots[from];
            _slots.Remove(from);
            _slots.Add(to, slot);
            slot.Initialize(to);
        }

        public void InventoryOnOnItemRemoved(ItemStack obj)
        {
            Destroy(_slots[obj].gameObject);
            _slots.Remove(obj);
            LayoutRebuilder.ForceRebuildLayoutImmediate(row);
        }

        public void InventoryOnOnItemAdded(ItemStack obj)
        {
            var slot = Instantiate(slotPrefab, row);
            slot.Initialize(obj);
            _slots.Add(obj, slot);
            LayoutRebuilder.ForceRebuildLayoutImmediate(row);
        }
    }
}