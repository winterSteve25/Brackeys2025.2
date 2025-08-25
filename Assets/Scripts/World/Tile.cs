using UnityEngine.Tilemaps;

namespace World
{
    public class Tile
    {
        public TileMaterial Material;

        public Tile(TileMaterial material)
        {
            Material = material;
        }

        public static implicit operator Tile(TileBase tile)
        {
            return new Tile(tile.name == "Stone" ? TileMaterial.Stone : TileMaterial.Unknown);
        }

        public override string ToString()
        {
            return Material.ToString();
        }
    }
}