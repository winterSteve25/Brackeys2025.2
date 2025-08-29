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
        [SerializeField] private ContactFilter2D raycastFilter;
        private RaycastHit2D[] _collisions;

        [Header("Parameters")]
        [SerializeField] private int damage;

        [SerializeField] private float miningSpeed;

        private void Awake()
        {
            raycastFilter = LayerMaskUtils.EverythingMask(false);
            _collisions = new RaycastHit2D[1];
        }

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

            Vector2 pickaxePosition = transform.position;
            var mp = GetMousePosInWorld();
            var dir = (mp - pickaxePosition).normalized;
            var amountHit = Physics2D.Raycast(pickaxePosition, dir, raycastFilter,
                _collisions);

            if (amountHit > 0)
            {
                mp = _collisions[0].point + dir * 0.5f;
            }
            
            if (WorldManager.Current.TryGetTile(mp, out var tile, out var position))
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