using System;
using System.Collections.Generic;
using System.Linq;
using InventorySystem;
using PrimeTween;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Objects.UpgradeStation
{
    [Serializable]
    public class DayItemPurchasables
    {
        [SerializeField] private List<ItemStack> items;

        public TetrisInventory Computed
        {
            get
            {
                var inv = new TetrisInventory(64, 64, new[] { new RectInt(0, 0, 64, 64) });

                foreach (var i in items)
                {
                    inv.AddAnywhere(i);
                }

                return inv;
            }
        }
    }

    public class UpgradeStationUIController : MonoBehaviour
    {
        private static HashSet<Upgrade> _appearedBefore;

        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private bool isOpen;
        [SerializeField] private TetrisInventory purchasableItemsForToday;
        [SerializeField] private List<Upgrade> upgradesForToday;

        [Header("References")]
        [SerializeField] private ListInventoryUI inventory;
        [SerializeField] private ListInventoryUI buyInventory;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private InputActionReference exitAction;
        [SerializeField] private UpgradeUI upgradeUI;

        [Header("Parameters")]
        [SerializeField] private List<DayItemPurchasables> purchasableItems;
        [SerializeField] private List<Upgrade> possibleUpgrades;

        private void Awake()
        {
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;

            _appearedBefore ??= new HashSet<Upgrade>();

            purchasableItemsForToday = purchasableItems[Mathf.Min(CarryOverDataManager.Day, purchasableItems.Count - 1)]
                .Computed;
            upgradesForToday = possibleUpgrades.Where(x =>
            {
                if (x.CanOnlyAppearOnce && _appearedBefore.Contains(x))
                {
                    return false;
                }

                return CarryOverDataManager.Day > x.AppearAfterDay;
            }).ToList();

            if (upgradesForToday.Count > 3)
            {
                upgradesForToday = Sample(upgradesForToday, 3);
            }
        }

        private void Update()
        {
            if (!isOpen || !exitAction.action.WasPressedThisFrame()) return;

            isOpen = false;
            Tween.Alpha(group, 0, 0.2f)
                .OnComplete(() =>
                {
                    group.interactable = false;
                    group.blocksRaycasts = false;
                });
        }

        public void Show(TetrisInventory inv)
        {
            if (isOpen) return;
            isOpen = true;

            inventory.Initialize(inv, null);
            buyInventory.Initialize(purchasableItemsForToday, inv);
            upgradeUI.Initialize(upgradesForToday);

            group.interactable = true;
            group.blocksRaycasts = true;
            Tween.Alpha(group, 1, 0.2f);
        }

        // Selects n unique random elements from a list
        private static List<T> Sample<T>(List<T> list, int n)
        {
            if (n > list.Count)
                throw new ArgumentException("n must be <= list.Count");

            List<T> copy = new List<T>(list); // clone to avoid modifying original
            int count = copy.Count;

            // Fisher–Yates partial shuffle
            for (int i = 0; i < n; i++)
            {
                int j = UnityEngine.Random.Range(i, count); // pick index between i and last
                (copy[i], copy[j]) = (copy[j], copy[i]); // swap
            }

            return copy.GetRange(0, n); // take first n
        }
    }
}