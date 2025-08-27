using Player;
using UnityEngine;

namespace Items
{
    public abstract class UpdatableItem : MonoBehaviour
    {
        public abstract void UseTick(PlayerInventory inventory);
    }
}