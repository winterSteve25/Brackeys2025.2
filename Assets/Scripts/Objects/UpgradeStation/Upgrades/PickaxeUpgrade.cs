using InventorySystem;
using UnityEngine;
using Utils;

namespace Objects.UpgradeStation.Upgrades
{
    [CreateAssetMenu(fileName = "Pickaxe Upgrade", menuName = "Game/Upgrades/Pickaxe")]
    public class PickaxeUpgrade : Upgrade
    {
        [SerializeField] private ItemType laserType;
        [SerializeField] private ItemType pickaxeType;
        
        public override string Description => "Upgrade your good-ol pickaxe";
        public override void Apply()
        {
            CarryOverDataManager.Instance.Inventory.RemoveItemOfType(pickaxeType);
            CarryOverDataManager.Instance.Inventory.AddAnywhere(new ItemStack(laserType, 1, Vector2Int.zero));
        }
    }
}