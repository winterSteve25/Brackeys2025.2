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
        
        [SerializeField, Self] protected SpriteRenderer spriteRenderer;
        [SerializeField] private TileMaterial material;
        [SerializeField] private Sprite[] variations;
        
        [field: SerializeField] public Vector2Int Position { get; private set; }

        private void Awake()
        {
            SetTexture();
        }

        protected void SetTexture()
        {
            if (variations.Length <= 0) return;
            spriteRenderer.sprite = variations[Random.Range(0, variations.Length)];
        }

        public virtual void OnPlace(Vector2Int cell, Vector3 pos, WorldManager world)
        {
            transform.position = pos;
            Position = cell;
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