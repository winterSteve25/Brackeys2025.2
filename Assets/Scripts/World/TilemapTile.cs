using UnityEngine;
using UnityEngine.Tilemaps;

namespace World
{
    public class TilemapTile : ITile
    {
        TileMaterial ITile.Material
        {
            get => _material;
            set => _material = value;
        }
        
        public readonly TileBase TileBase;
        private TileMaterial _material;

        public TilemapTile(TileMaterial material, TileBase tileBase)
        {
            _material = material;
            TileBase = tileBase;
        }

        public void OnPlace(Vector2Int cell, Vector3 pos, WorldManager world)
        {
            world.Tilemap.SetTile((Vector3Int)cell, TileBase);
        }

        public void OnRemove(Vector2Int cell, Vector3 pos, WorldManager world)
        {
            world.Tilemap.SetTile((Vector3Int)cell, null);
        }

        public void OnNeighborUpdated(Vector2Int cell, WorldManager world)
        {
        }

        public static TilemapTile FromTileBase(TileBase tileBase)
        {
            return new TilemapTile(tileBase, tileBase);
        }
    }
}