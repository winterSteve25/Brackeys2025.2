using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class InteractableObject : MonoBehaviour
    {
        [field: SerializeField] public string InteractableName { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public bool IsRightclickable { get; private set; } = true;
        
        [SerializeField] private UnityEvent<PlayerInventory> onInteract;
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite onSprite;
        [SerializeField] private Sprite offSprite;
        [SerializeField] private Transform player;
        [SerializeField] private float onRange;
        
        private bool _changeSprite;
        private bool _wasOn;

        public bool interactable = true;

        private void Awake()
        {
            _changeSprite = spriteRenderer != null;
        }

        public void Interact(PlayerInventory inventory)
        {
            onInteract?.Invoke(inventory);
        }

        private void Update()
        {
            if (!_changeSprite) return;
            if ((player.position - transform.position).magnitude < onRange)
            {
                if (_wasOn) return;
                spriteRenderer.sprite = onSprite;
                _wasOn = true;
            }
            else
            {
                if (!_wasOn) return;
                spriteRenderer.sprite = offSprite;
                _wasOn = false;
            }
        }
    }
}