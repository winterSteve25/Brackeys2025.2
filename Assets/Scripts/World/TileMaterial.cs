using System;

namespace World
{
    public enum TileMaterial
    {
        Stone,
        Unknown
    }

    public static class TileMaterialExtensions
    {
        public static float MiningDuration(this TileMaterial material)
        {
            return material switch
            {
                TileMaterial.Stone => 0.5f,
                TileMaterial.Unknown => 5,
                _ => throw new ArgumentOutOfRangeException(nameof(material), material, null)
            };
        }
    }
}