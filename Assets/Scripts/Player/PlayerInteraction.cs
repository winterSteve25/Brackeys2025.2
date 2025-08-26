using Objects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Utils;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private Collider2D[] thingsInRadius;
        [SerializeField] private ContactFilter2D everythingElse;
        [SerializeField] private InteractableObject wasShowing;

        private void Awake()
        {
            var avoidMask = LayerMask.NameToLayer("Player");
            var mask = 1 << avoidMask;
            mask = ~mask;
            thingsInRadius = new Collider2D[1];
            everythingElse = ContactFilter2D.noFilter;
            everythingElse.SetLayerMask(mask);
        }

        public void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            
            var mp = Mouse.current.position.ReadValue();
            var mpW = MainCamera.Current.ScreenToWorldPoint(mp);
            var amount = Physics2D.OverlapPoint(mpW, everythingElse, thingsInRadius);
            if (amount <= 0)
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
                obj.Interact();
            }
            
            if (wasShowing == obj) return;
            
            ToolTipManager.Instance.Show(obj.InteractableName, "");
            wasShowing = obj;
        }
    }
}