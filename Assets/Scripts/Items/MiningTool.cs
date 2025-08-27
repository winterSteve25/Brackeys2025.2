using InventorySystem;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Mining Tool", menuName = "Game/New Mining Tool")]
    public class MiningTool : ItemType
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float MiningSpeedMultiplier { get; private set; }
    }
}