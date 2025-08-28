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
        [SerializeField, Child] private TetrisInventoryUI inventoryUI;

        [SerializeField] private InputActionReference openAction;
        [SerializeField] private InputActionReference exitAction;

        private void Awake()
        {
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;
        }

        public void Update()
        {
            if (isOpen && exitAction.action.WasPressedThisFrame())
            {
                isOpen = false;
                inventoryUI.ReturnHeldItem();
                
                Tween.Alpha(group, 0, 0.2f)
                    .OnComplete(() =>
                    {
                        group.interactable = false;
                        group.blocksRaycasts = false;
                    });
            }

            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (!isOpen && openAction.action.WasPressedThisFrame())
            {
                isOpen = true;
                group.interactable = true;
                group.blocksRaycasts = true;
                Tween.Alpha(group, 1, 0.2f);
            }
        }
    }
}