using System.Collections.Generic;
using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UpgradeStation
{
    public class ListInventoryUI : MonoBehaviour
    {
        [SerializeField] private ListInventoryItem prefab;
        [SerializeField] private RectTransform container;
        [SerializeField] private TMP_Text sellOrBuyAmount;

        public void Initialize(TetrisInventory inventory)
        {
            var sorted = new List<ItemStack>(inventory.Items);
            sorted.Sort(new Vec2Comparer());

            for (int i = 0; i < container.transform.childCount; i++)
            {
                Destroy(container.transform.GetChild(i).gameObject);
            }

            foreach (var item in sorted)
            {
                var i = Instantiate(prefab, container);
                i.Initialize(inventory, item, sellOrBuyAmount);
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform) i.transform);
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform) container.transform);
        }
    }

    public class Vec2Comparer : IComparer<ItemStack>
    {
        public int Compare(ItemStack a, ItemStack b)
        {
            if (a == null)
            {
                return b == null ? 0 : -1;
            }

            if (b == null)
            {
                return 1;
            }
            
            var x = a.position;
            var y = b.position;
            
            var yComparison = x.y.CompareTo(y.y);
            if (yComparison != 0) return yComparison;
            
            var xComparison = x.x.CompareTo(y.x);
            if (xComparison != 0) return xComparison;

            return 0;
        }
    }
}