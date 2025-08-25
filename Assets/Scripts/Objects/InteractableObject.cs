using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class InteractableObject : MonoBehaviour
    {
        [field: SerializeField] public string InteractableName { get; private set; }
        [SerializeField] private UnityEvent onInteract;
        
        public void Interact()
        {
            onInteract?.Invoke();
        }
    }
}