using System;
using ED.SC;
using UnityEngine;
using InventorySystem;
using InventorySystem.UI;
using KBCore.Refs;
using Utils;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private ItemStack[] hotbar;
        [SerializeField] private TetrisInventory backpack;
        [SerializeField] private int selectedItem;
        
        [Header("References")]
        [SerializeField, Anywhere] private TetrisInventoryUI inventoryUI;
        
        private void Awake()
        {
            hotbar = new ItemStack[3];
            backpack = CarryOverDataManager.Instance.backpack;
        }

        private void Start()
        {
            inventoryUI.Initialize(backpack);
        }

        public ItemStack GetItemHeld()
        {
            return hotbar[selectedItem];
        }

        [Command(MonoTargetType.Active)]
        private void AddItem(string path, int amount)
        {
            var it = Resources.Load<ItemType>(path);
            backpack.AddAnywhere(new ItemStack(it, amount, Vector2Int.zero));
        }
    }
}