using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils
{
    public class FollowMouseUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Canvas parentCanvas;

        [Header("Parameters")]
        [SerializeField] private Vector2 offset;
        [SerializeField] private bool changePivot;

        private RectTransform _rectTransform;
        private Transform _transform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _transform = transform;
            parentCanvas = GetComponentInParent<Canvas>();
        }

        private void Update()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            var mousePosition = Mouse.current.position.ReadValue();
            _transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            
            if (!changePivot) return;

            var pivotX = mousePosition.x / Screen.width;
            var pivotY = mousePosition.y / Screen.height;
            var targetPivot = new Vector2(Mathf.RoundToInt(pivotX), Mathf.RoundToInt(pivotY));
            
            _rectTransform.pivot = Vector2.Lerp(_rectTransform.pivot, targetPivot, 0.1f);
        }
    }
}