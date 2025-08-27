using System;
using System.Collections.Generic;
using InventorySystem;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Items
{
    public class ShotgunItem : UpdatableItem
    {
        [SerializeField] private ItemType shotgunAmmoType;
        [SerializeField] private Transform gunFront;
        [SerializeField] private float range;
        [SerializeField] private float effectiveAngle;
        [SerializeField] private float damage;

        private List<Collider2D> _collisions;

        private void Awake()
        {
            _collisions = new List<Collider2D>();
        }

        private void OnDrawGizmosSelected()
        {
            if (gunFront == null) return;

            Gizmos.color = Color.red;

            var rotation = Quaternion.AngleAxis(effectiveAngle, Vector3.forward);
            var negativeRotation = Quaternion.AngleAxis(-effectiveAngle, Vector3.forward);

            var center = (gunFront.position - transform.position).normalized * range;

            var a = rotation * center;
            var b = negativeRotation * center;

            a.z = 0;
            b.z = 0;

            Gizmos.DrawRay(transform.position, a);
            Gizmos.DrawRay(transform.position, b);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, center); // middle reference
        }

        public override void UseTick(PlayerInventory inventory)
        {
            if (!Mouse.current.leftButton.wasPressedThisFrame) return;

            var ammo = inventory.Inventory.GetItemOfType(shotgunAmmoType);
            if (ammo == null) return;

            inventory.Inventory.RemoveAmountFromPosition(ammo.position, 1);

            var numCol = Physics2D.OverlapCircle(transform.position, range, LayerMaskUtils.EverythingMask(true),
                _collisions);
            for (int i = 0; i < numCol; i++)
            {
                var col = _collisions[i];

                var toCol = col.transform.position - transform.position;
                var toFront = gunFront.position - transform.position;
                var angle = Vector2.Angle(toCol, toFront);
                if (angle > effectiveAngle) continue;

                if (_collisions[i].TryGetComponent(out IHealthComponent hp))
                {
                    hp.TakeDamage(damage);
                }
            }
        }
    }
}