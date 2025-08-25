using KBCore.Refs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Utils;
using World;

namespace Player
{
    public class PlayerMining : ValidatedMonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private bool wasMining;
        [SerializeField] private Vector2Int wasMiningPosition;
        
        [Header("References")]
        [SerializeField, Self] private PlayerInventory inventory;
        [SerializeField, Anywhere] private WorldManager worldManager;
        [SerializeField, Anywhere] private BreakManager breakManager;

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
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

            if (worldManager.TryGetTile(mpW, out var tile, out var position))
            {
                if (position != wasMiningPosition)
                {
                    breakManager.CancelBreak(wasMiningPosition);
                }
                
                wasMining = true;
                wasMiningPosition = position;
                var multiplier = inventory.GetItemHeld() == null ? 0 : inventory.GetItemHeld().Tool.MiningSpeedMultiplier;
                
                if (breakManager.TickBreak(position, tile.Material.MiningDuration() / multiplier))
                {
                    wasMining = false;
                    worldManager.RemoveTile(position);
                }
            }
        }
    }
}