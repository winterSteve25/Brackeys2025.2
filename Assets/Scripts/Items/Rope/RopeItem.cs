using Audio;
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
                if (!ropeSegment.Master.TryAddSegment()) return;
                AudioManager.PlayOnce(FModEvents.Instance.RopePlace, Vector2.zero);
                inventory.Inventory.RemoveAmountFromPosition(item.position, 1);
                return;
            }

            if (WorldManager.Current.TryGetTile(pos + Vector2Int.up, out var upTile))
            {
                if (upTile is RopeSegment ropeSegment)
                {
                    if (!ropeSegment.Master.TryAddSegment()) return;
                    AudioManager.PlayOnce(FModEvents.Instance.RopePlace, Vector2.zero);
                    inventory.Inventory.RemoveAmountFromPosition(item.position, 1);
                    return;
                }
                
                var rope = Instantiate(ropePrefab, WorldManager.Current.CellToWorld(pos) + new Vector2(0.5f, 0.5f),
                    Quaternion.identity, WorldManager.Current.transform);
                rope.TryAddSegment();
                AudioManager.PlayOnce(FModEvents.Instance.RopePlace, Vector2.zero);
                inventory.Inventory.RemoveAmountFromPosition(item.position, 1);
            }
        }
    }
}