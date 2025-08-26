using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace World
{
    public class WorldManager : MonoBehaviour
    {
        public static WorldManager Current { get; private set; }
        private Dictionary<Vector2Int, ITile> _tiles;

        [Header("References")]
        [SerializeField, Child] private Tilemap tilemap;

        private void Awake()
        {
            Current = this;
            _tiles = new Dictionary<Vector2Int, ITile>();
            InitializeFromTilemap();
        }

        private void InitializeFromTilemap()
        {
            for (int i = tilemap.cellBounds.xMin; i < tilemap.cellBounds.xMax; i++)
            {
                for (int j = tilemap.cellBounds.yMin; j < tilemap.cellBounds.yMax; j++)
                {
                    if (tilemap.GetTile(new Vector3Int(i, j, 0)) == null) continue;
                    _tiles.Add(new Vector2Int(i, j), TilemapTile.FromTileBase(tilemap.GetTile(new Vector3Int(i, j, 0))));
                }
            }
        }

        public bool TryGetTile(Vector3 worldPosition, out ITile tile, out Vector2Int position)
        {
            var cellPosition = tilemap.WorldToCell(worldPosition);
            position = new Vector2Int(cellPosition.x, cellPosition.y);
            return _tiles.TryGetValue(position, out tile);
        }

        public bool TryGetTile(Vector2Int position, out ITile tile)
        {
            return _tiles.TryGetValue(position, out tile);
        }

        public void RemoveTile(Vector3 position)
        {
            RemoveTile((Vector2Int) tilemap.WorldToCell(position));
        }

        public void RemoveTile(Vector2Int position)
        {
            if (_tiles.Remove(position, out ITile tile))
            {
                tile.OnRemove(position, tilemap.CellToWorld((Vector3Int)position), tilemap);
            }
        }

        public void SetTile(Vector3 worldPosition, ITile tile)
        {
            var cellPosition = tilemap.WorldToCell(worldPosition);
            var position = new Vector2Int(cellPosition.x, cellPosition.y);

            if (_tiles.TryGetValue(position, out var original))
            {
                original.OnRemove(position, worldPosition, tilemap);
            }

            tile.OnPlace(position, worldPosition, tilemap);
            _tiles[position] = tile;
        }

        public Vector2 CellToWorld(Vector2Int position)
        {
            return tilemap.CellToWorld(new Vector3Int(position.x, position.y, 0));
        }
    }
}