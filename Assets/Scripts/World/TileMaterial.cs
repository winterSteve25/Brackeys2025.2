using System;
using InventorySystem;
using UnityEngine;

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

        public static TileMaterial Parse(this string material)
        {
            if (Enum.TryParse<TileMaterial>(material.Substring(5), true, out var result))
            {
                return result;
            }

            return TileMaterial.Unknown;
        }

        private static readonly Lazy<ItemType> StoneItemType = new(() => Resources.Load<ItemType>("Tiles/Item_Stone"));

        public static ItemStack[] Loot(this TileMaterial material)
        {
            return material switch
            {
                TileMaterial.Stone => new ItemStack[]
                {
                    new(StoneItemType.Value, 1, Vector2Int.zero)
                },
                TileMaterial.Unknown => null,
                _ => throw new ArgumentOutOfRangeException(nameof(material), material, null)
            };
        }
    }
}