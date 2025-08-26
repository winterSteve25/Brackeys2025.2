using System;
using InventorySystem;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace World
{
    public enum TileMaterialPreset
    {
        Stone,
        Unknown,
    }

    [Serializable]
    public class TileMaterial
    {
        [field: SerializeField] public float MiningDuration { get; private set; }
        [field: SerializeField] public ItemStack[] Loot { get; private set; }
        private readonly TileMaterialPreset _preset;

        public TileMaterial(float miningDuration, ItemStack[] loot)
        {
            MiningDuration = miningDuration;
            Loot = loot;
        }

        public static implicit operator TileMaterial(TileMaterialPreset preset)
        {
            return new TileMaterial(preset.MiningDuration(), preset.Loot());
        }

        public static implicit operator TileMaterial(TileBase tileBase)
        {
            return Enum.TryParse<TileMaterialPreset>(tileBase.name[5..], true, out var result)
                ? result
                : TileMaterialPreset.Unknown;
        }
    }

    public static class TileMaterialExtensions
    {
        public static float MiningDuration(this TileMaterialPreset material)
        {
            return material switch
            {
                TileMaterialPreset.Stone => 0.3f,
                TileMaterialPreset.Unknown => 0.5f,
                _ => throw new ArgumentOutOfRangeException(nameof(material), material, null)
            };
        }

        private static readonly Lazy<ItemType> StoneItemType = new(() => Resources.Load<ItemType>("Tiles/Item_Stone"));

        public static ItemStack[] Loot(this TileMaterialPreset material)
        {
            return material switch
            {
                TileMaterialPreset.Stone => new ItemStack[]
                {
                    new(StoneItemType.Value, 1, Vector2Int.zero)
                },
                TileMaterialPreset.Unknown => null,
                _ => throw new ArgumentOutOfRangeException(nameof(material), material, null)
            };
        }
    }
}