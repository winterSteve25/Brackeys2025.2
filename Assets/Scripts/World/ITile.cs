using UnityEngine;
using UnityEngine.Tilemaps;

namespace World
{
    public interface ITile
    {
        public TileMaterial Material { get; protected set; }

        public void OnPlace(Vector2Int cell, Vector3 pos, WorldManager world);
        public void OnRemove(Vector2Int cell, Vector3 pos, WorldManager world);
        public void OnNeighborUpdated(Vector2Int cell, WorldManager world);
    }
}