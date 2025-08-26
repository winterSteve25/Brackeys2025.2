using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI
{
    public class HotbarUI : MonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private TetrisInventory inventory;
        [SerializeField] private List<ItemStack> items;
        private Dictionary<ItemStack, HotbarSlot> _slots;

        [Header("References")]
        [SerializeField] private HotbarSlot slotPrefab;

        [SerializeField] private RectTransform row;

        public void Initialize(TetrisInventory inv, List<ItemStack> hotbarItems)
        {
            _slots = new Dictionary<ItemStack, HotbarSlot>();
            inventory = inv;
            items = hotbarItems;
            inventory.OnItemAdded += InventoryOnOnItemAdded;
            inventory.OnItemHeld += InventoryOnOnItemRemoved;
            inventory.OnItemReplaced += InventoryOnOnItemReplaced;
            inventory.OnItemRemoved += InventoryOnOnItemRemoved;

            foreach (var obj in hotbarItems)
            {
                InventoryOnOnItemAdded(obj);
            }
        }

        public void ChangeSelected(int oldIndex, int newIndex)
        {
            if (oldIndex == newIndex) return;
            if (oldIndex >= 0 && oldIndex < items.Count)
            {
                _slots[items[oldIndex]].Unselect();
            }

            if (newIndex >= 0 && newIndex < items.Count)
            {
                _slots[items[newIndex]].Select();
            }
        }

        private void OnDestroy()
        {
            inventory.OnItemAdded -= InventoryOnOnItemAdded;
            inventory.OnItemHeld -= InventoryOnOnItemRemoved;
            inventory.OnItemReplaced -= InventoryOnOnItemReplaced;
            inventory.OnItemRemoved -= InventoryOnOnItemRemoved;
        }

        private void InventoryOnOnItemReplaced(ItemStack from, ItemStack to)
        {
            if (from.position.y >= 2) return;
            var slot = _slots[from];
            _slots.Remove(from);
            _slots.Add(to, slot);
            slot.Initialize(to);
        }

        private void InventoryOnOnItemRemoved(ItemStack obj)
        {
            if (obj.position.y >= 2) return;
            Destroy(_slots[obj].gameObject);
            _slots.Remove(obj);
            ChangeSelected(-1, 0);
            LayoutRebuilder.ForceRebuildLayoutImmediate(row);
        }

        private void InventoryOnOnItemAdded(ItemStack obj)
        {
            if (obj.position.y >= 2) return;
            var slot = Instantiate(slotPrefab, row);
            slot.Initialize(obj);
            _slots.Add(obj, slot);

            if (_slots.Count == 1)
            {
                ChangeSelected(-1, 0);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(row);
        }
    }
}