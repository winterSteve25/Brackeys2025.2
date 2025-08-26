using System.Collections.Generic;
using ED.SC;
using UnityEngine;
using InventorySystem;
using InventorySystem.UI;
using KBCore.Refs;
using UnityEngine.InputSystem;
using Utils;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private TetrisInventory inventory;
        [SerializeField] private int selectedItem;
        [SerializeField] private List<ItemStack> hotbarItems;

        [Header("References")]
        [SerializeField, Anywhere] private TetrisInventoryUI inventoryUI;
        [SerializeField, Anywhere] private HotbarUI hotbarUI;
        [SerializeField] private InputActionReference scrollInput;
        
        private void Awake()
        {
            inventory = CarryOverDataManager.Instance.inventory;
            inventory.OnItemAdded += InventoryOnOnItemAdded;
            inventory.OnItemHeld += InventoryOnOnItemRemoved;
            inventory.OnItemRemoved += InventoryOnOnItemRemoved;
            selectedItem = -1;
        }

        private void OnDestroy()
        {
            inventory.OnItemAdded -= InventoryOnOnItemAdded;
            inventory.OnItemHeld -= InventoryOnOnItemRemoved;
            inventory.OnItemRemoved -= InventoryOnOnItemRemoved;
        }

        private void Start()
        {
            inventoryUI.Initialize(inventory);
            hotbarUI.Initialize(inventory, hotbarItems);
        }

        private void Update()
        {
            var scroll = scrollInput.action.ReadValue<float>();
            if (scroll != 0 && hotbarItems.Count > 0)
            {
                var previousSelected = selectedItem;
                WrapSelection(selectedItem + (int)scroll);
                if (previousSelected == selectedItem) return;
                hotbarUI.ChangeSelected(previousSelected, selectedItem);
            }
        }

        private void WrapSelection(int n)
        {
            var m = hotbarItems.Count;
            selectedItem = (n % m + m) % m;
        }

        public ItemStack GetItemHeld()
        {
            return selectedItem == -1 ? null : hotbarItems[selectedItem];
        }

        //  TODO: Hotbar hardcode
        private void InventoryOnOnItemRemoved(ItemStack obj)
        {
            if (obj.position.y >= 2) return;
            hotbarItems.Remove(obj);

            if (hotbarItems.Count == 0)
            {
                selectedItem = -1;
            }
            else
            {
                WrapSelection(selectedItem);
            }
        }

        private void InventoryOnOnItemAdded(ItemStack obj)
        {
            if (obj.position.y >= 2) return;
            hotbarItems.Add(obj);
            
            if (hotbarItems.Count == 1)
            {
                selectedItem = 0;
            }
        }

        [Command(MonoTargetType.Active)]
        private void AddItem(string path, int amount = 1)
        {
            var it = Resources.Load<ItemType>(path);
            inventory.AddAnywhere(new ItemStack(it, amount, Vector2Int.zero));
        }
    }
}