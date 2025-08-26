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
                TileMaterial.Stone => 0.3f,
                TileMaterial.Unknown => 0.5f,
                _ => throw new ArgumentOutOfRangeException(nameof(material), material, null)
            };
        }
    }
}