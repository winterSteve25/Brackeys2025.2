using System;
using UnityEngine;

namespace Items
{
    public class ShotgunBullet : MonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private bool deleted;
        [SerializeField] private Vector2 direction;
        
        [Header("Parameters")]
        [SerializeField] private float lifetime;
        [SerializeField] private float damage;
        [SerializeField] private float speed;

        private void Awake()
        {
            Invoke(nameof(Delete), lifetime);
        }

        public void Init(Vector2 dir)
        {
            direction = dir;
            deleted = false;
        }

        private void Update()
        {
            transform.position += (Vector3) (direction * (speed * Time.deltaTime));
        }

        private void Delete()
        {
            if (deleted) return;
            deleted = true;
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Delete();

            if (other.TryGetComponent(out IHealthComponent health))
            {
                health.TakeDamage(damage);
            }
        }
    }
}