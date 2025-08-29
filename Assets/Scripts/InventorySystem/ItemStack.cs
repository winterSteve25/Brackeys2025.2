using System;
using UnityEngine;
using Utils;

namespace InventorySystem
{
    [Serializable]
    public class ItemStack
    {
        public ItemType itemType;
        public int amount;
        public Vector2Int position;
        
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

        public int GetPrice(bool isBuy)
        {
            var shopMultiplier = isBuy ? CarryOverDataManager.Instance.shopItemPriceMultiplier : 1;
            return Mathf.FloorToInt(amount * shopMultiplier * itemType.BasePrice);
        }
    }
}