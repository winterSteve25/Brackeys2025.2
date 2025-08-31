using System;
using KBCore.Refs;
using Objects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Utils;

namespace Player
{
    public class PlayerInteraction : ValidatedMonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private Collider2D[] thingsInRadius;

        [SerializeField] private ContactFilter2D everythingElse;
        [SerializeField] private InteractableObject wasShowing;

        [Header("References")]
        [SerializeField, Self] private PlayerInventory inventory;
        [SerializeField, Self] private Collider2D playerCollider;

        [Header("Parameters")]
        [SerializeField] private float interactionDistance;

        private void Awake()
        {
            thingsInRadius = new Collider2D[1];
            everythingElse = LayerMaskUtils.EverythingMask(true);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, interactionDistance);
        }

        public void Update()
        {
            var mp = Mouse.current.position.ReadValue();
            var mpW = MainCamera.Current.ScreenToWorldPoint(mp);
            var amount = Physics2D.OverlapPoint(mpW, everythingElse, thingsInRadius);
            if (amount <= 0 || EventSystem.current.IsPointerOverGameObject() ||
                thingsInRadius[0].Distance(playerCollider).distance > interactionDistance ||
                !thingsInRadius[0].TryGetComponent(out InteractableObject obj) ||
                !obj.interactable)
            {
                if (wasShowing != null)
                {
                    wasShowing = null;
                    ToolTipManager.Instance.Hide();
                }

                return;
            }

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                obj.Interact(inventory);
            }

            if (wasShowing == obj) return;
            ToolTipManager.Instance.Show(obj.InteractableName, "", true);
            wasShowing = obj;
        }
    }
}