using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using World;

namespace Items
{
    public class MiningToolItem : UpdatableItem
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private bool wasMining;
        [SerializeField] private Vector2Int wasMiningPosition;
        
        public override void UseTick(PlayerInventory inventory)
        {
            var breakManager = BreakManager.Current;
            
            if (!Mouse.current.leftButton.isPressed)
            {
                if (wasMining)
                {
                    breakManager.CancelBreak(wasMiningPosition);
                }

                wasMining = false;
                return;
            }

            var mp = Mouse.current.position.ReadValue();
            var mpW = MainCamera.Current.ScreenToWorldPoint(mp);

            if (WorldManager.Current.TryGetTile(mpW, out var tile, out var position))
            {
                if (position != wasMiningPosition)
                {
                    breakManager.CancelBreak(wasMiningPosition);
                }

                wasMining = true;
                wasMiningPosition = position;
                var multiplier = inventory.GetItemHeld() == null
                    ? 0
                    : inventory.GetItemHeld().MiningTool.MiningSpeedMultiplier;

                if (breakManager.TickBreak(position, tile.Material.MiningDuration / multiplier))
                {
                    wasMining = false;
                }
            }
        }
    }
}