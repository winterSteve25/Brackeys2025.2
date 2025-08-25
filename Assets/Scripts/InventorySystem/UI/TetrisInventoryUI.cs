using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI
{
    public class TetrisInventoryUI : ValidatedMonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private TetrisInventory inventory;
        [SerializeField] private InventoryItem heldItem;
        private Dictionary<Vector2Int, TetrisSlot> _slots;
        private Dictionary<Vector2Int, InventoryItem> _inventoryItems;

        [Header("References")]
        [SerializeField, Child] private GridLayoutGroup container;

        [SerializeField] private TetrisInventoryGridUI[] grids;
        [SerializeField] private Transform itemContainer;
        [SerializeField] private TetrisSlot slotPrefab;
        [SerializeField] private InventoryItem itemPrefab;

        [Header("Parameters")]
        [SerializeField] private Vector2 cellSize;

        public void Initialize(TetrisInventory inv)
        {
            _slots = new Dictionary<Vector2Int, TetrisSlot>();
            _inventoryItems = new Dictionary<Vector2Int, InventoryItem>();
            
            inventory = inv;
            inventory.OnItemAdded += InventoryOnOnItemAdded;
            inventory.OnItemChanged += InventoryOnOnItemChanged;
            inventory.OnItemRemoved += InventoryOnOnItemRemoved;
            inventory.OnItemHeld += InventoryOnOnItemHeld;
            inventory.OnHeldItemReleased += InventoryOnOnHeldItemReleased;
            
            foreach (var grid in grids)
            {
                grid.InitializeSlots(_slots, cellSize, inventory.Width, inventory.Height, slotPrefab, this);
            }
        }

        public void SlotClicked(Vector2Int pos)
        {
            var item = inventory.GetItemAtPosition(pos);
            if (item == null)
            {
                if (heldItem == null) return;
                heldItem.ItemStackStored.position = pos;

                if (inventory.AddItemAtPosition(heldItem.ItemStackStored))
                {
                    inventory.ClearHeldItem();
                    return;
                }

                heldItem.RefreshAmount();
                return;
            }

            if (heldItem != null)
            {
                var inPlaceItem = _inventoryItems[item.position];

                heldItem.ItemStackStored.position = pos;
                if (inventory.CanReplace(heldItem.ItemStackStored, item.position))
                {
                    inventory.ReplaceItemNoCheck(item.position, heldItem.ItemStackStored);
                    inPlaceItem.Set(heldItem.ItemStackStored, container.cellSize);
                    inPlaceItem.AnchorTo((RectTransform)_slots[pos].transform);
                    _inventoryItems.Remove(item.position);
                    _inventoryItems.Add(pos, inPlaceItem);
                    heldItem.Set(item, container.cellSize);
                }

                return;
            }

            inventory.HoldItem(item.position);
        }

        private void OnDestroy()
        {
            inventory.OnItemAdded -= InventoryOnOnItemAdded;
            inventory.OnItemChanged -= InventoryOnOnItemChanged;
            inventory.OnItemRemoved -= InventoryOnOnItemRemoved;
            inventory.OnItemHeld -= InventoryOnOnItemHeld;
            inventory.OnHeldItemReleased -= InventoryOnOnHeldItemReleased;
        }

        private void InventoryOnOnHeldItemReleased()
        {
            Destroy(heldItem.gameObject);
            heldItem = null;
        }

        private void InventoryOnOnItemHeld(ItemStack obj)
        {
            var inventoryItem = _inventoryItems[obj.position];
            _inventoryItems.Remove(obj.position);
            inventoryItem.FollowMouse();
            heldItem = inventoryItem;
        }

        private void InventoryOnOnItemChanged(ItemStack obj)
        {
            _inventoryItems[obj.position].Set(obj, container.cellSize);
        }

        private void InventoryOnOnItemRemoved(ItemStack obj)
        {
            Destroy(_inventoryItems[obj.position].gameObject);
            _inventoryItems.Remove(obj.position);
        }

        private void InventoryOnOnItemAdded(ItemStack obj)
        {
            var item = Instantiate(itemPrefab, itemContainer);
            item.Set(obj, container.cellSize);
            item.AnchorTo((RectTransform)_slots[obj.position].transform);
            _inventoryItems.Add(obj.position, item);
        }
    }
}