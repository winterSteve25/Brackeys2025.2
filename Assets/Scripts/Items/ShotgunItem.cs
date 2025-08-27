using InventorySystem;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items
{
    public class ShotgunItem : UpdatableItem
    {
        [SerializeField] private ItemType shotgunAmmoType;
        
        public override void UseTick(PlayerInventory inventory)
        {
            if (!Mouse.current.leftButton.wasPressedThisFrame) return;
            
            var ammo = inventory.Inventory.GetItemOfType(shotgunAmmoType);
            if (ammo == null) return;
            
            inventory.Inventory.RemoveAmountFromPosition(ammo.position, 1);
            // do some damage
        }
    }
}