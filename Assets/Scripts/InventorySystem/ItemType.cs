using Items;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Item Type", menuName = "Game/New Item Type")]
    public class ItemType : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int StackSize { get; private set; }
        [field: SerializeField] public Vector2Int Size { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public GameObject VisualPrefab { get; private set; }
        [field: SerializeField] public UpdatableItem LogicPrefab { get; private set; }
    }
}