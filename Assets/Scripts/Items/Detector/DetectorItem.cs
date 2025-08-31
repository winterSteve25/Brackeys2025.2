using Audio;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using World;

namespace Items.Detector
{
    public class DetectorItem : UpdatableItem
    {
        [SerializeField] private int radius;
        [SerializeField] private PingEffect ping;
        
        public override void UseTick(PlayerInventory inventory)
        {
            if (!Mouse.current.leftButton.wasPressedThisFrame) return;
            
            var pos = WorldManager.Current.WorldToCell(inventory.transform.position);
            var png = Instantiate(ping, inventory.transform.position, Quaternion.identity);
            png.Initialize(radius);

            for (int i = -radius; i <= radius; i++)
            {
                for (int j = -radius; j <= radius; j++)
                {
                    var offset = new Vector2Int(i, j);
                    if (offset.magnitude > radius) continue;
                    if (!WorldManager.Current.TryGetTile(pos + offset, out var tile)) continue;
                    if (tile.Material.Name != "Crystal") continue;
                    
                    var wp = WorldManager.Current.CellToWorld(pos + offset);
                    AudioManager.PlayDelayed(FModEvents.Instance.DectectorPing, wp, (wp - (Vector2) inventory.transform.position).magnitude * 0.25f);
                }
            }
        }
    }
}