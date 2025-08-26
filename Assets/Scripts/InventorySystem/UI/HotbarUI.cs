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
            inventory.OnItemRemoved += InventoryOnOnItemRemoved;
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
            inventory.OnItemRemoved -= InventoryOnOnItemRemoved;
        }

        private void InventoryOnOnItemRemoved(ItemStack obj)
        {
            if (obj.position.y >= 2) return;
            Destroy(_slots[obj].gameObject);
            _slots.Remove(obj);
            LayoutRebuilder.ForceRebuildLayoutImmediate(row);
        }

        private void InventoryOnOnItemAdded(ItemStack obj)
        {
            if (obj.position.y >= 2) return;
            var slot = Instantiate(slotPrefab, row);
            slot.Initialize(obj);
            _slots.Add(obj, slot);
            LayoutRebuilder.ForceRebuildLayoutImmediate(row);
        }
    }
}