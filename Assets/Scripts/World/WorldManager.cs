using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

namespace World
{
    public class WorldManager : MonoBehaviour
    {
        public static WorldManager Current { get; private set; }
        private Dictionary<Vector2Int, ITile> _tiles;

        [Header("References")]
        [field: SerializeField, Child] public Tilemap Tilemap { get; private set; }

        private void Awake()
        {
            Current = this;
            _tiles = new Dictionary<Vector2Int, ITile>();
            InitializeFromTilemap();
        }

        private void InitializeFromTilemap()
        {
            for (int i = Tilemap.cellBounds.xMin; i < Tilemap.cellBounds.xMax; i++)
            {
                for (int j = Tilemap.cellBounds.yMin; j < Tilemap.cellBounds.yMax; j++)
                {
                    if (Tilemap.GetTile(new Vector3Int(i, j, 0)) == null) continue;
                    _tiles.Add(new Vector2Int(i, j),
                        TilemapTile.FromTileBase(Tilemap.GetTile(new Vector3Int(i, j, 0))));
                }
            }
        }

        public bool TryGetTile(Vector3 worldPosition, out ITile tile, out Vector2Int position)
        {
            var cellPosition = Tilemap.WorldToCell(worldPosition);
            position = new Vector2Int(cellPosition.x, cellPosition.y);
            return _tiles.TryGetValue(position, out tile);
        }

        public bool TryGetTile(Vector2Int position, out ITile tile)
        {
            return _tiles.TryGetValue(position, out tile);
        }

        public bool HasTile(Vector2Int position)
        {
            return _tiles.ContainsKey(position);
        }

        public void RemoveTile(Vector3 position)
        {
            RemoveTile((Vector2Int)Tilemap.WorldToCell(position));
        }

        public void RemoveTile(Vector2Int position)
        {
            if (_tiles.Remove(position, out ITile tile))
            {
                tile.OnRemove(position, Tilemap.CellToWorld((Vector3Int)position), this);
                NotifyNeighbors(position);
            }
        }

        public void SetTile(Vector3 worldPosition, ITile tile)
        {
            var cellPosition = Tilemap.WorldToCell(worldPosition);
            var position = new Vector2Int(cellPosition.x, cellPosition.y);

            if (_tiles.TryGetValue(position, out var original))
            {
                original.OnRemove(position, worldPosition, this);
            }

            tile.OnPlace(position, worldPosition, this);
            _tiles[position] = tile;
            NotifyNeighbors(position);
        }

        private void NotifyNeighbors(Vector2Int position)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    var pos = new Vector2Int(i, j) + position;
                    if (_tiles.TryGetValue(pos, out var t))
                    {
                        t.OnNeighborUpdated(pos, this);
                    }
                }
            }
        }

        public void SetTile(Vector2Int position, ITile tile)
        {
            SetTile(Tilemap.CellToWorld((Vector3Int)position), tile);
        }

        public Vector2 CellToWorld(Vector2Int position)
        {
            return Tilemap.CellToWorld(new Vector3Int(position.x, position.y, 0));
        }

        public Vector2Int WorldToCell(Vector2 worldPosition)
        {
            return (Vector2Int)Tilemap.WorldToCell(worldPosition);
        }
    }
}