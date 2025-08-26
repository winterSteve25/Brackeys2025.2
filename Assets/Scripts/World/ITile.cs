using UnityEngine;
using UnityEngine.Tilemaps;

namespace World
{
    public interface ITile
    {
        public TileMaterial Material { get; protected set; }

        public abstract void OnPlace(Vector2Int cell, Vector3 pos, Tilemap tilemap);
        public abstract void OnRemove(Vector2Int cell, Vector3 pos, Tilemap tilemap);
    }
}