using System;
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
        [SerializeField] private int selectedItem;
        [SerializeField] private List<ItemStack> hotbarItems;
        [field: SerializeField] public TetrisInventory Inventory { get; private set; }

        [Header("References")]
        [SerializeField, Anywhere] private TetrisInventoryUI inventoryUI;
        [SerializeField, Anywhere] private HotbarUI hotbarUI;
        [SerializeField] private InputActionReference scrollInput;

        public event Action<ItemStack> OnSelectedItemChanged; 
        
        private void Awake()
        {
            selectedItem = -1;
        }
        
        private void Start()
        {
            Inventory = CarryOverDataManager.Instance.inventory;
            Inventory.OnItemAdded += InventoryOnOnItemAdded;
            Inventory.OnItemReplaced += InventoryOnOnItemReplaced;
            Inventory.OnItemHeld += InventoryOnOnItemRemoved;
            Inventory.OnItemRemoved += InventoryOnOnItemRemoved;
            
            foreach (var item in Inventory.Items)
            {
                InventoryOnOnItemAdded(item);
            }
            
            inventoryUI.Initialize(Inventory);
            hotbarUI.Initialize(Inventory, hotbarItems);
        }
        
        
        private void OnDestroy()
        {
            Inventory.OnItemAdded -= InventoryOnOnItemAdded;
            Inventory.OnItemReplaced -= InventoryOnOnItemReplaced;
            Inventory.OnItemHeld -= InventoryOnOnItemRemoved;
            Inventory.OnItemRemoved -= InventoryOnOnItemRemoved;
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
            OnSelectedItemChanged?.Invoke(hotbarItems[selectedItem]);
        }

        public ItemStack GetItemHeld()
        {
            return selectedItem == -1 ? null : hotbarItems[selectedItem];
        }
        
        //  TODO: Hotbar hardcode
        private void InventoryOnOnItemReplaced(ItemStack from, ItemStack to)
        {
            if (from.position.y >= 2) return;
            int i = hotbarItems.IndexOf(from);
            hotbarItems[i] = to;
        }
        
        private void InventoryOnOnItemRemoved(ItemStack obj)
        {
            if (obj.position.y >= 2) return;
            hotbarItems.Remove(obj);

            if (hotbarItems.Count == 0)
            {
                selectedItem = -1;
                OnSelectedItemChanged?.Invoke(null);
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
                OnSelectedItemChanged?.Invoke(hotbarItems[0]);
            }
        }

        [Command(MonoTargetType.Active)]
        private void AddItem(string path, int amount = 1)
        {
            var it = Resources.Load<ItemType>(path);
            Inventory.AddAnywhere(new ItemStack(it, amount, Vector2Int.zero));
        }
    }
}