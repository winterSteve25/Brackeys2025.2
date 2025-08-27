using System;
using Items;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class ItemStack
    {
        public ItemType itemType;
        public int amount;
        public Vector2Int position;
        
        public bool IsTool => itemType is MiningTool;
        public MiningTool MiningTool => itemType as MiningTool;

        public ItemStack(ItemType itemType, int amount, Vector2Int position)
        {
            this.itemType = itemType;
            this.amount = amount;
            this.position = position;
        }

        public override string ToString()
        {
            return $"{itemType.name} x{amount} at {position}";
        }
    }
}