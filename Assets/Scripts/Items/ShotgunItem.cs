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
        [SerializeField] private float effectiveAngle;
        [SerializeField] private float damage;
        [SerializeField] private int numOfBullets;
        [SerializeField] private ShotgunBullet prefab;

        private void OnDrawGizmosSelected()
        {
            if (gunFront == null) return;

            Gizmos.color = Color.red;

            var rotation = Quaternion.AngleAxis(effectiveAngle, Vector3.forward);
            var negativeRotation = Quaternion.AngleAxis(-effectiveAngle, Vector3.forward);
            var center = (gunFront.position - transform.position).normalized * 10;

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
            CameraEffects.Current.trauma += 0.15f;

            var delta = effectiveAngle * 2 / numOfBullets;
            var center = (gunFront.position - transform.position).normalized;
            
            for (float theta = -effectiveAngle; theta <= effectiveAngle; theta += delta)
            {
                var rotation = Quaternion.AngleAxis(theta, Vector3.forward);
                var dir = rotation * center;
                var bullet = Instantiate(prefab, 
                    gunFront.position,
                    Quaternion.AngleAxis(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, Vector3.forward));
                bullet.Init(dir);
            }
        }
    }
}