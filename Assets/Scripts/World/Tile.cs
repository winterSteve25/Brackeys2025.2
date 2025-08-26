using UnityEngine.Tilemaps;

namespace World
{
    public class Tile
    {
        public readonly TileMaterial Material;

        public Tile(TileMaterial material)
        {
            Material = material;
        }

        public static implicit operator Tile(TileBase tile)
        {
            return new Tile(tile.name.Parse());
        }

        public override string ToString()
        {
            return Material.ToString();
        }
    }
}