using KBCore.Refs;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace InventorySystem.UI
{
    public class InventoryUIController : MonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private bool isOpen;
        
        [Header("References")]
        [SerializeField, Child] private CanvasGroup group;
        [SerializeField] private InputActionReference openAction;
        [SerializeField] private InputActionReference exitAction;
        
        private void Awake()
        {
            group.alpha = 0;
            group.gameObject.SetActive(false);
        }

        public void Update()
        {
            if (isOpen && (exitAction.action.WasPressedThisFrame() || openAction.action.WasPressedThisFrame()))
            {
                isOpen = false;
                Tween.Alpha(group, 0, 0.2f)
                    .OnComplete(() => group.gameObject.SetActive(false));
            }
            
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (!isOpen && openAction.action.WasPressedThisFrame())
            {
                isOpen = true;
                group.gameObject.SetActive(true);
                Tween.Alpha(group, 1, 0.2f);
            }
        }
    }
}