using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using World;

namespace Items
{
    public class TileItem : UpdatableItem
    {
        [SerializeField] private TileBase tile;

        public override void UseTick(PlayerInventory inventory)
        {
            if (!Mouse.current.leftButton.isPressed) return;
            if (TryGetTileAtMouse(out _, out var pos) ||
                WorldManager.Current.WorldToCell(inventory.transform.position) == pos)
            {
                return;
            }

            if (!WorldManager.Current.HasTile(pos + Vector2Int.left) &&
                !WorldManager.Current.HasTile(pos + Vector2Int.right) &&
                !WorldManager.Current.HasTile(pos + Vector2Int.up) &&
                !WorldManager.Current.HasTile(pos + Vector2Int.down)) return;
            
            WorldManager.Current.SetTile(pos, TilemapTile.FromTileBase(tile));
            inventory.Inventory.RemoveAmountFromPosition(item.position, 1);
        }
    }
}