using KBCore.Refs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace World
{
    public class WorldTile : ValidatedMonoBehaviour, ITile
    {
        TileMaterial ITile.Material
        {
            get => material;
            set => material = value;
        }
        
        [SerializeField, Self] private SpriteRenderer spriteRenderer;
        [SerializeField] private TileMaterial material;
        [SerializeField] private Sprite[] variations;

        private void Awake()
        {
            if (variations.Length <= 0) return;
            spriteRenderer.sprite = variations[Random.Range(0, variations.Length)];
        }

        public virtual void OnPlace(Vector2Int cell, Vector3 pos, WorldManager world)
        {
            transform.position = pos;
        }

        public virtual void OnRemove(Vector2Int cell, Vector3 pos, WorldManager world)
        {
            Destroy(gameObject);
        }

        public virtual void OnNeighborUpdated(Vector2Int cell, WorldManager world)
        {
        }
    }
}