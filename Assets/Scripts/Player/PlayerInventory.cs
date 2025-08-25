using System.Linq;
using ED.SC;
using UnityEngine;
using InventorySystem;
using InventorySystem.UI;
using KBCore.Refs;
using UnityEngine.Serialization;
using Utils;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private TetrisInventory inventory;
        [SerializeField] private int selectedItem;
        
        [Header("References")]
        [SerializeField, Anywhere] private TetrisInventoryUI inventoryUI;
        
        private void Awake()
        {
            inventory = CarryOverDataManager.Instance.inventory;
        }

        private void Start()
        {
            inventoryUI.Initialize(inventory);
        }

        public ItemStack GetItemHeld()
        {
            return null;
        }

        public ItemStack[] GetHotbarItems()
        {
            return inventory.Items.Where(x => x.position.y < 2).ToArray();
        }

        [Command(MonoTargetType.Active)]
        private void AddItem(string path, int amount)
        {
            var it = Resources.Load<ItemType>(path);
            inventory.AddAnywhere(new ItemStack(it, amount, Vector2Int.zero));
        }
    }
}