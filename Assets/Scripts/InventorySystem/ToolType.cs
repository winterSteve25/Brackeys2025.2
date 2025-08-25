using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Tool Type", menuName = "Game/New Tool Type")]
    public class ToolType : ItemType
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float MiningSpeedMultiplier { get; private set; }
    }
}