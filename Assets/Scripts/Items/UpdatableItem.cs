using InventorySystem;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using World;

namespace Items
{
    public abstract class UpdatableItem : MonoBehaviour
    {
        public ItemStack item;
        
        public abstract void UseTick(PlayerInventory inventory);

        protected Vector2 GetMousePosInWorld()
        {
            var mp = Mouse.current.position.ReadValue();
            var mpW = MainCamera.Current.ScreenToWorldPoint(mp);
            return mpW;
        }

        protected Vector2Int GetMousePosInCell()
        {
            return WorldManager.Current.WorldToCell(GetMousePosInWorld());
        }

        protected bool TryGetTileAtMouse(out ITile tile, out Vector2Int position)
        {
            return WorldManager.Current.TryGetTile(GetMousePosInWorld(), out tile, out position);
        }
    }
}