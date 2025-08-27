using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class InteractableObject : MonoBehaviour
    {
        [field: SerializeField] public string InteractableName { get; private set; }
        [SerializeField] private UnityEvent<PlayerInventory> onInteract;
        
        public void Interact(PlayerInventory inventory)
        {
            onInteract?.Invoke(inventory);
        }
    }
}