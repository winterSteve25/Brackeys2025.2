using KBCore.Refs;
using UnityEngine;
using UnityEngine.Tilemaps;
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
            spriteRenderer.sprite = variations[Random.Range(0, variations.Length)];
        }

        public void OnPlace(Vector2Int cell, Vector3 pos, Tilemap tilemap)
        {
            transform.position = pos;
        }

        public void OnRemove(Vector2Int cell, Vector3 pos, Tilemap tilemap)
        {
            Destroy(gameObject);
        }
    }
}