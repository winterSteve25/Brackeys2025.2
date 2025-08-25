using UnityEngine;
using InventorySystem;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private ItemStack[] hotbar;
        [SerializeField] private TetrisInventory backpack;
        [SerializeField] private int selectedItem;
        
        private void Awake()
        {
            hotbar = new ItemStack[3];
            backpack = new TetrisInventory(10, 10);
        }

        public ItemStack GetItemHeld()
        {
            return hotbar[selectedItem];
        }
    }
}