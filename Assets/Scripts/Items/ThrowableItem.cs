using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items
{
    public class ThrowableItem : UpdatableItem
    {
        [Header("Parameters")]
        [SerializeField] private Rigidbody2D prefab;
        [SerializeField] private float throwForce;
        
        public override void UseTick(PlayerInventory inventory)
        {
            if (!Mouse.current.leftButton.wasPressedThisFrame) return;
            inventory.Inventory.RemoveAmountFromPosition(item.position, 1);
            
            var dir = GetDirectionFromHandToMouse();
            var dynamite = Instantiate(prefab);
            
            dynamite.position = handAnchor.position;
            dynamite.AddForce(dir * throwForce, ForceMode2D.Impulse);
        }
    }
}