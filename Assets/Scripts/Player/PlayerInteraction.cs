using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private Collider2D[] thingsInRadius;
        [SerializeField] private ContactFilter2D everythingElse;
        
        [Header("Stats")]
        [SerializeField] private float radius;

        private void Awake()
        {
            var avoidMask = LayerMask.NameToLayer("Player");
            var mask = 1 << avoidMask;
            mask = ~mask;
            thingsInRadius = new Collider2D[1];
            everythingElse = ContactFilter2D.noFilter;
            everythingElse.SetLayerMask(mask);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public void Update()
        {
            var mp = Mouse.current.position.ReadValue();
            var mpW = MainCamera.Current.ScreenToWorldPoint(mp);
            var amount = Physics2D.OverlapPoint(mpW, everythingElse, thingsInRadius);
            if (amount <= 0) return;
            
            
        }
    }
}