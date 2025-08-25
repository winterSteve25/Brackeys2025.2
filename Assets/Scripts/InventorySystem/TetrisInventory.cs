using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class TetrisInventory
    {
        [field: SerializeField] public List<ItemStack> Items { get; private set; }

        [SerializeField] private int width;
        [SerializeField] private int height;

        public TetrisInventory(int width, int height)
        {
            this.width = width;
            this.height = height;
            Items = new List<ItemStack>();
        }

        public void AddItem(ItemStack item)
        {
            if (!CanAdd(item)) return;
            Items.Add(item);
        }
        
        public void AddAnywhere(ItemStack item)
        {
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    item.position.x = i;
                    item.position.y = j;
                    if (!CanAdd(item)) continue;
                    Items.Add(item);
                    return;
                }
            }
        }

        private bool CanAdd(ItemStack item)
        {
            return Items.Any(x => Collide(x.position, x.itemType.Size, item.position, item.itemType.Size));
        }

        private static bool Collide(
            Vector2Int rect1TopLeft, Vector2Int rect1Size,
            Vector2Int rect2TopLeft, Vector2Int rect2Size)
        {
            // Calculate bottom-right corners
            Vector2Int rect1BottomRight = rect1TopLeft + new Vector2Int(rect1Size.x, -rect1Size.y);
            Vector2Int rect2BottomRight = rect2TopLeft + new Vector2Int(rect2Size.x, -rect2Size.y);

            // Check if one rectangle is to the left of the other
            if (rect1BottomRight.x <= rect2TopLeft.x || rect2BottomRight.x <= rect1TopLeft.x)
                return false;

            // Check if one rectangle is above the other
            if (rect1BottomRight.y >= rect2TopLeft.y || rect2BottomRight.y >= rect1TopLeft.y)
                return false;

            // Rectangles overlap
            return true;
        }
    }
}