using System;
using InventorySystem;
using PrimeTween;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UpgradeStation
{
    public class UpgradeStationUIController : MonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private bool isOpen;
        
        [Header("References")]
        [SerializeField] private ListInventoryUI inventory;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private InputActionReference exitAction;
        
        private void Awake()
        {
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;
        }

        private void Update()
        {
            if (!isOpen || !exitAction.action.WasPressedThisFrame()) return;
            
            isOpen = false;
            Tween.Alpha(group, 0, 0.2f)
                .OnComplete(() =>
                {
                    group.interactable = false;
                    group.blocksRaycasts = false;
                });
        }

        public void Show(TetrisInventory inv)
        {
            if (isOpen) return;
            isOpen = true;
            inventory.Initialize(inv);
            group.interactable = true;
            group.blocksRaycasts = true;
            Tween.Alpha(group, 1, 0.2f);
        }
    }
}