using UnityEngine;
using Utils;

namespace Objects.UpgradeStation.Upgrades
{
    [CreateAssetMenu(fileName = "Shop Upgrade", menuName = "Game/Upgrades/Shop")]
    public class CheaperItems : Upgrade
    {
        [SerializeField] private float decreaseBy;

        public override string Description => $"Decrease item prices by {decreaseBy:P1} on future visits";
        public override void Apply()
        {
            CarryOverDataManager.Instance.targetShopItemPriceMultiplier *= (1 - decreaseBy);
        }
    }
}