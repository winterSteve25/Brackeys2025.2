using UnityEngine;
using Utils;

namespace Objects.UpgradeStation.Upgrades
{
    [CreateAssetMenu(fileName = "Oxygen Upgrade", menuName = "Game/Upgrades/OXG")]
    public class OxygenConsumptionRate : Upgrade
    {
        [SerializeField] private float decreaseBy;

        public override string Description => $"Decrease oxygen consumption rate by {decreaseBy:P1}";
        public override void Apply()
        {
            CarryOverDataManager.Instance.oxygenDepletionMultiplier *= (1 - decreaseBy);
        }
    }
}