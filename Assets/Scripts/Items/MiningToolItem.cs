using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using World;

namespace Items
{
    public class MiningToolItem : UpdatableItem
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private bool wasMining;
        [SerializeField] private Vector2Int wasMiningPosition;

        [Header("Parameters")]
        [SerializeField] private int damage;
        [SerializeField] private float miningSpeed;

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

            if (TryGetTileAtMouse(out var tile, out var position))
            {
                if (position != wasMiningPosition)
                {
                    breakManager.CancelBreak(wasMiningPosition);
                }

                wasMining = true;
                wasMiningPosition = position;
                var multiplier = inventory.GetItemHeld() == null
                    ? 0
                    : miningSpeed;

                if (breakManager.TickBreak(position, tile.Material.MiningDuration / multiplier))
                {
                    wasMining = false;
                }
            }
            else
            {
                if (wasMining)
                {
                    breakManager.CancelBreak(wasMiningPosition);
                }

                wasMining = false;
            }
        }
    }
}