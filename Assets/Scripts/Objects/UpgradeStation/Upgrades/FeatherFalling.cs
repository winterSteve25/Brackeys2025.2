using UnityEngine;
using Utils;

namespace Objects.UpgradeStation.Upgrades
{
    [CreateAssetMenu(fileName = "Fall Damage Upgrade", menuName = "Game/Upgrades/Fall")]
    public class FeatherFalling : Upgrade
    {
        [SerializeField] private float decreaseBy;

        public override string Description => $"Decrease fall damage by {decreaseBy:P1}";
        public override void Apply()
        {
            CarryOverDataManager.Instance.fallDamageMultiplier *= (1 - decreaseBy);
        }
    }
}