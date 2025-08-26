using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI
{
    public class HotbarSlot : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Transform child;
        
        [SerializeField] private float zoomFactor = 1.25f;
        [SerializeField] private float transitionTime = 0.2f;
        [SerializeField] private Ease easeIn;
        [SerializeField] private Ease easeOut;

        private Vector2 _startSize;
        private Vector2 _endSize;

        private void Awake()
        {
            _startSize = ((RectTransform) transform).sizeDelta;
            _endSize = _startSize * zoomFactor;
        }

        public void Initialize(ItemStack item)
        {
            image.sprite = item.itemType.Icon;
        }
        
        public void Select()
        {
            Tween.StopAll(child);
            Tween.Scale(child, new Vector3(zoomFactor, zoomFactor, 1), transitionTime, ease: easeIn);
        }
        
        public void Unselect()
        {
            Tween.StopAll(child);
            Tween.Scale(child, Vector3.one, transitionTime, ease: easeOut);
        }
    }
}