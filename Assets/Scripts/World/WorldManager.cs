using System.Collections.Generic;
using JetBrains.Annotations;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace World
{
    public class WorldManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField, Child] private Tilemap tilemap;
        
        private Dictionary<Vector2Int, Tile> _tiles;

        private void Awake()
        {
            _tiles = new Dictionary<Vector2Int, Tile>();
            InitializeFromTilemap();
        }

        private void InitializeFromTilemap()
        {
            for (int i = tilemap.cellBounds.xMin; i < tilemap.cellBounds.xMax; i++)
            {
                for (int j = tilemap.cellBounds.yMin; j < tilemap.cellBounds.yMax; j++)
                {
                    if (tilemap.GetTile(new Vector3Int(i, j, 0)) == null) continue;
                    _tiles.Add(new Vector2Int(i, j), tilemap.GetTile(new Vector3Int(i, j, 0)));
                }
            }
        }

        public bool TryGetTile(Vector3 worldPosition, out Tile tile, out Vector2Int position)
        {
            var cellPosition = tilemap.WorldToCell(worldPosition);
            position = new Vector2Int(cellPosition.x, cellPosition.y);
            return _tiles.TryGetValue(position, out tile);
        }

        public void RemoveTile(Vector2Int position)
        {
            _tiles.Remove(position);
            tilemap.SetTile(new Vector3Int(position.x, position.y, 0), null);
        }

        public Vector2 CellToWorld(Vector2Int position)
        {
            return tilemap.CellToWorld(new Vector3Int(position.x, position.y, 0));
        }
    }
}