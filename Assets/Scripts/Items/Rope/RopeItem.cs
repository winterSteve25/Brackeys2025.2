using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using World;

namespace Items.Rope
{
    public class RopeItem : UpdatableItem
    {
        [SerializeField] private RopeMaster ropePrefab;

        public override void UseTick(PlayerInventory inventory)
        {
            if (!Mouse.current.leftButton.wasPressedThisFrame) return;
            if (TryGetTileAtMouse(out var tile, out var pos))
            {
                if (tile is not RopeSegment ropeSegment) return;
                ropeSegment.Master.TryAddSegment();
                inventory.Inventory.RemoveAmountFromPosition(item.position, 1);
                return;
            }

            if (!WorldManager.Current.HasTile(pos + Vector2Int.up)) return;

            var rope = Instantiate(ropePrefab, WorldManager.Current.CellToWorld(pos) + new Vector2(0.5f, 0.5f), Quaternion.identity, WorldManager.Current.transform);
            rope.TryAddSegment();

            inventory.Inventory.RemoveAmountFromPosition(item.position, 1);
        }
    }
}