using UnityEngine;
using Utils;

namespace Objects.UpgradeStation.Upgrades
{
    [CreateAssetMenu(fileName = "Ore Spawn Upgrade", menuName = "Game/Upgrades/Ore")]
    public class OreSpawnRate : Upgrade
    {
        [SerializeField] private float increaseBy;

        public override string Description => $"Increase ore spawn rate by {increaseBy:P1}";
        public override void Apply()
        {
            CarryOverDataManager.Instance.oreSpawnRateMultiplier *= (1 + increaseBy);
        }
    }
}