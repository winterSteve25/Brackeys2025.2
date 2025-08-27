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
        [Header("Debug Info DO NOT EDIT")]
        public ItemStack item;
        
        public abstract void UseTick(PlayerInventory inventory);

        public static Vector2 GetMousePosInWorld()
        {
            var mp = Mouse.current.position.ReadValue();
            var mpW = MainCamera.Current.ScreenToWorldPoint(mp);
            return mpW;
        }

        protected static Vector2Int GetMousePosInCell()
        {
            return WorldManager.Current.WorldToCell(GetMousePosInWorld());
        }

        protected static bool TryGetTileAtMouse(out ITile tile, out Vector2Int position)
        {
            return WorldManager.Current.TryGetTile(GetMousePosInWorld(), out tile, out position);
        }
    }
}