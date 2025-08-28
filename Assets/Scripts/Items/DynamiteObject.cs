using System.Collections.Generic;
using UnityEngine;
using World;

namespace Items
{
    public class DynamiteObject : MonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private float time;
        [SerializeField] private bool exploded;
        [SerializeField] private List<Collider2D> collisions;
        
        [Header("Parameters")]
        [SerializeField] private float detonationTime;
        [SerializeField] private float explosionRadius;
        [SerializeField] private float damage;
        [SerializeField] private float randomRadius;
        [SerializeField] private float knockbackForce;

        private void Update()
        {
            time += Time.deltaTime;

            if (time >= detonationTime && !exploded)
            {
                Explode();
            }
        }

        private void Explode()
        {
            exploded = true;
            var currentPos = WorldManager.Current.WorldToCell(transform.position);

            for (int i = -Mathf.RoundToInt(explosionRadius); i < explosionRadius; i++)
            {
                for (int j = -Mathf.RoundToInt(explosionRadius); j < explosionRadius; j++)
                {
                    var offset = new Vector2Int(i, j);
                    if (!(offset.magnitude < explosionRadius + Random.Range(-randomRadius, randomRadius))) continue;
                    BreakManager.Current.CompleteBreak(currentPos + offset);
                }
            }

            var numCol = Physics2D.OverlapCircle(transform.position, explosionRadius, ContactFilter2D.noFilter, collisions);
            for (int i = 0; i < numCol; i++)
            {
                var collision = collisions[i];
                if (collision.TryGetComponent(out IHealthComponent health))
                {
                    health.TakeDamage(damage);
                }

                if (collision.TryGetComponent(out Rigidbody2D rb))
                {
                    rb.AddForce((rb.position - (Vector2) transform.position).normalized * knockbackForce, ForceMode2D.Impulse);
                }
            }
            
            Destroy(gameObject);
        }
    }
}