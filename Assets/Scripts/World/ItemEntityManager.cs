using InventorySystem;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace World
{
    public class ItemEntityManager : MonoBehaviour
    {
        public static ItemEntityManager Current { get; private set; }
        
        [Header("References")]
        [SerializeField] private ItemEntity prefab;

        private IObjectPool<ItemEntity> _pool;

        private void Awake()
        {
            Current = this;
            _pool = new ObjectPool<ItemEntity>(
                () => Instantiate(prefab, transform),
                ib => ib.gameObject.SetActive(true),
                ib => ib.gameObject.SetActive(false),
                ib => Destroy(ib.gameObject),
                defaultCapacity: 16
            );
        }

        public ItemEntity Spawn(Vector2 pos, ItemStack item, float despawnTime = -1)
        {
            var itemBehaviour = _pool.Get();
            itemBehaviour.transform.position = pos;
            itemBehaviour.Init(() => Despawn(itemBehaviour), item, despawnTime);
            return itemBehaviour;
        }

        public void SpawnApproximatelyAt(Vector2 pos, ItemStack item)
        {
            var i = Spawn(pos, item);
            i.MoveTo(pos + Random.insideUnitCircle, true);
        }

        private void Despawn(ItemEntity groundItemBehaviour)
        {
            _pool.Release(groundItemBehaviour);
        }
    }
}