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
        
        private void Awake()
        {
            thingsInRadius = new Collider2D[1];
            everythingElse = LayerMaskUtils.EverythingMask(true);
        }

        public void Update()
        {
            var mp = Mouse.current.position.ReadValue();
            var mpW = MainCamera.Current.ScreenToWorldPoint(mp);
            var amount = Physics2D.OverlapPoint(mpW, everythingElse, thingsInRadius);
            if (amount <= 0 || EventSystem.current.IsPointerOverGameObject())
            {
                if (wasShowing != null)
                {
                    wasShowing = null;
                    ToolTipManager.Instance.Hide();
                }

                return;
            }
            
            if (!thingsInRadius[0].TryGetComponent(out InteractableObject obj)) return;
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                obj.Interact(inventory);
            }
            
            if (wasShowing == obj) return;
            
            ToolTipManager.Instance.Show(obj.InteractableName, "");
            wasShowing = obj;
        }
    }
}