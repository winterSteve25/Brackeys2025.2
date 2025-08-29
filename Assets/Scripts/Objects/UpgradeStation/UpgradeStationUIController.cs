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
                var inv = new TetrisInventory(64, 64, new []{ new RectInt(0, 0, 64, 64) });

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
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private bool isOpen;
        [SerializeField] private TetrisInventory[] computedPurchasableItems;

        [Header("References")]
        [SerializeField] private ListInventoryUI inventory;
        [SerializeField] private ListInventoryUI buyInventory;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private InputActionReference exitAction;

        [Header("Parameters")]
        [SerializeField] private List<DayItemPurchasables> purchasableItems;

        private void Awake()
        {
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;

            computedPurchasableItems = purchasableItems.Select(x => x.Computed).ToArray();
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
            buyInventory.Initialize(
                computedPurchasableItems[Mathf.Min(CarryOverDataManager.Day, computedPurchasableItems.Length)], inv);
            group.interactable = true;
            group.blocksRaycasts = true;
            Tween.Alpha(group, 1, 0.2f);
        }
    }
}