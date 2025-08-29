using System.Collections.Generic;
using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Objects.UpgradeStation
{
    public class ListInventoryUI : MonoBehaviour
    {
        [SerializeField] private ListInventoryItem prefab;
        [SerializeField] private RectTransform container;
        [SerializeField] private TMP_Text sellOrBuyAmount;

        private TetrisInventory _inv;
        private TetrisInventory _buyInto;

        public void Initialize(TetrisInventory inventory, TetrisInventory buyInto)
        {
            _inv = inventory;
            _inv.OnItemAdded += InvOnOnItemAdded;
            _buyInto = buyInto;

            var sorted = new List<ItemStack>(inventory.Items);
            sorted.Sort(new Vec2Comparer());

            for (int i = 0; i < container.transform.childCount; i++)
            {
                Destroy(container.transform.GetChild(i).gameObject);
            }

            foreach (var item in sorted)
            {
                InvOnOnItemAdded(item);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)container.transform);
        }

        private void OnDestroy()
        {
            if (_inv == null) return;
            _inv.OnItemAdded -= InvOnOnItemAdded;
        }

        private void InvOnOnItemAdded(ItemStack obj)
        {
            if (!obj.itemType.IsSellable) return;
            var i = Instantiate(prefab, container);
            i.Initialize(_inv, _buyInto, obj, sellOrBuyAmount);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)i.transform);
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