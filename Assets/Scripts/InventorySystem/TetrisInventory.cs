using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class TetrisInventory
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private ItemStack heldItem;
        [SerializeField] private RectInt[] areas;
        [field: SerializeField] public List<ItemStack> Items { get; private set; }

        [Header("Parameters")]
        [field: SerializeField] public int Width { get; private set; }
        [field: SerializeField] public int Height { get; private set; }

        public event Action<ItemStack> OnItemAdded;
        public event Action<ItemStack> OnItemChanged;
        public event Action<ItemStack> OnItemRemoved;
        public event Action<ItemStack> OnItemHeld;
        public event Action OnHeldItemReleased;

        public TetrisInventory(int width, int height)
        {
            Width = width;
            Height = height;
            Items = new List<ItemStack>();
        }

        /// <summary>
        /// Tries to add item to the location specified in the ItemStack
        /// </summary>
        /// <param name="item">The item to be added</param>
        /// <returns>True if was fully added, false if not added or partially added - item parameter is modified to leftover amount</returns>
        public bool AddItemAtPosition(ItemStack item)
        {
            var itemAtPosition = GetItemAtPosition(item.position);
            if (itemAtPosition == null)
            {
                if (!CanAddItemToSlot(item)) return false;

                if (item.amount > item.itemType.StackSize)
                {
                    var itemStack = new ItemStack(item.itemType, item.itemType.StackSize, item.position);
                    Items.Add(itemStack);
                    OnItemAdded?.Invoke(itemStack);
                    item.amount -= item.itemType.StackSize;
                    return false;
                }

                Items.Add(item);
                OnItemAdded?.Invoke(item);
                return true;
            }

            if (itemAtPosition.itemType == item.itemType)
            {
                itemAtPosition.amount += item.amount;
                if (itemAtPosition.amount <= item.itemType.StackSize)
                {
                    OnItemChanged?.Invoke(itemAtPosition);
                    return true;
                }

                var diff = itemAtPosition.amount - item.itemType.StackSize;
                itemAtPosition.amount = item.itemType.StackSize;
                OnItemChanged?.Invoke(itemAtPosition);
                item.amount = diff;
            }

            return false;
        }

        public bool AddAnywhere(ItemStack item)
        {
            for (var j = 0; j < Height; j++)
            {
                for (var i = 0; i < Width; i++)
                {
                    item.position.x = i;
                    item.position.y = j;
                    if (AddItemAtPosition(item)) return true;
                }
            }

            return false;
        }

        public ItemStack RemoveItem(Vector2Int position)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].position != position &&
                    !Collide(Items[i].position, Items[i].itemType.Size, position, Vector2Int.one)) continue;
                
                var item = Items[i];
                Items.RemoveAt(i);
                OnItemRemoved?.Invoke(item);
                return item;
            }

            return null;
        }

        // Input position must be top left corner
        public void HoldItem(Vector2Int positionStrict)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                var itemStack = Items[i];
                if (itemStack.position == positionStrict)
                {
                    heldItem = itemStack;
                    Items.RemoveAt(i);
                    OnItemHeld?.Invoke(itemStack);
                    return;
                }
            }
        }

        public void ReturnHeldItem()
        {
            if (heldItem == null) return;
            OnHeldItemReleased?.Invoke();
            
            if (AddItemAtPosition(heldItem))
            {
                return;
            }

            if (AddAnywhere(heldItem))
            {
                return;
            }
                
            // TODO: Drop excess
        }

        public void ClearHeldItem()
        {
            if (heldItem == null) return;
            heldItem = null;
            OnHeldItemReleased?.Invoke();
        }
        
        public void ReplaceItemNoCheck(Vector2Int position, ItemStack item)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                var original = Items[i];
                if (original.position == position)
                {
                    Items[i] = item;
                }
            }
        }
        
        public ItemStack GetItemAtPosition(Vector2Int position)
        {
            return Items.FirstOrDefault(x => Collide(x.position, x.itemType.Size, position, Vector2Int.one));
        }

        public bool CanReplace(ItemStack item, Vector2Int ignore)
        {
            if (FindAreaCodeOfItem(item) == -1) return false;
            return !Items.Any(x =>
            {
                if (x.position == ignore) return false;
                return Collide(x.position, x.itemType.Size, item.position, item.itemType.Size);
            });
        }

        private bool CanAddItemToSlot(ItemStack item)
        {
            if (FindAreaCodeOfItem(item) == -1) return false;
            return !Items.Any(x => Collide(x.position, x.itemType.Size, item.position, item.itemType.Size));
        }

        // -1 if not valid
        private int FindAreaCodeOfItem(ItemStack item)
        {
            var tl = FindAreaCodeOfPoint(item.position);
            var br = FindAreaCodeOfPoint(item.position + item.itemType.Size - Vector2Int.one);

            if (tl != br)
            {
                return -1;
            }

            return tl;
        }

        // -1 if not belong anywhere
        // should not be possible
        private int FindAreaCodeOfPoint(Vector2Int point)
        {
            for (var i = 0; i < areas.Length; i++)
            {
                var a = areas[i];
                if (a.Contains(point)) return i;
            }

            return -1;
        }

        private static bool Collide(
            Vector2Int rect1TopLeft, Vector2Int rect1Size,
            Vector2Int rect2TopLeft, Vector2Int rect2Size)
        {
            if (rect2TopLeft.x >= rect1TopLeft.x && rect2TopLeft.x < rect1TopLeft.x + rect1Size.x &&
                rect2TopLeft.y >= rect1TopLeft.y && rect2TopLeft.y < rect1TopLeft.y + rect1Size.y)
            {
                return true;
            }


            if (rect1TopLeft.x >= rect2TopLeft.x && rect1TopLeft.x < rect2TopLeft.x + rect2Size.x &&
                rect1TopLeft.y >= rect2TopLeft.y && rect1TopLeft.y < rect2TopLeft.y + rect2Size.y)
            {
                return true;
            }

            return false;
        }
    }
}