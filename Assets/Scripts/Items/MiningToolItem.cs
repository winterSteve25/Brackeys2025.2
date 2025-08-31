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
        [SerializeField] protected bool isMining;
        [SerializeField] protected Vector2Int wasMiningPosition;
        [SerializeField] private ContactFilter2D raycastFilter;
        private RaycastHit2D[] _collisions;

        [Header("Parameters")]
        [SerializeField] private int damage;
        [SerializeField] private float miningSpeed;
        [SerializeField] private float range;

        protected virtual void Awake()
        {
            raycastFilter = LayerMaskUtils.EverythingMask(true);
            _collisions = new RaycastHit2D[1];
        }

        public override void UseTick(PlayerInventory inventory)
        {
            var breakManager = BreakManager.Current;
            if (!Mouse.current.leftButton.isPressed)
            {
                Cancel(breakManager);
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

            if (range >= 0 && (mp - pickaxePosition).magnitude > range)
            {
                Cancel(breakManager);
                return;
            }

            if (WorldManager.Current.TryGetTile(mp, out var tile, out var position))
            {
                if (tile.Material.MiningDuration < 0)
                {
                    Cancel(breakManager);
                    return;
                }

                if (position != wasMiningPosition)
                {
                    breakManager.CancelBreak(wasMiningPosition);
                }

                isMining = true;
                wasMiningPosition = position;
                var multiplier = inventory.GetItemHeld() == null
                    ? 0
                    : miningSpeed;

                if (breakManager.TickBreak(position, tile.Material.MiningDuration / multiplier))
                {
                    isMining = false;
                }
            }
            else
            {
                Cancel(breakManager);
            }
        }

        private void Cancel(BreakManager breakManager)
        {
            if (isMining)
            {
                breakManager.CancelBreak(wasMiningPosition);
            }

            isMining = false;
        }
    }
}