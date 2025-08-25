using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace InventorySystem
{
    [Serializable]
    public class ItemStack
    {
        public ItemType itemType;
        public int amount;
        public Vector2Int position;
        
        public bool IsTool => itemType is ToolType;
        public ToolType Tool => itemType as ToolType;

        public ItemStack(ItemType itemType, int amount, Vector2Int position)
        {
            this.itemType = itemType;
            this.amount = amount;
            this.position = position;
        }
    }
}