using System;
using Audio;
using FMODUnity;
using InventorySystem;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace World
{
    public enum TileMaterialPreset
    {
        Stone,
        Bedrock,
        Unknown,
    }

    [Serializable]
    public class TileMaterial
    {
        [field: SerializeField] public float MiningDuration { get; private set; }
        [field: SerializeField] public ItemStack[] Loot { get; private set; }
        [field: SerializeField] public EventReference PlaceSound { get; private set; }
        [field: SerializeField] public EventReference BreakSound { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        
        public TileMaterial(float miningDuration, ItemStack[] loot, EventReference placeSound, EventReference breakSound, string name)
        {
            MiningDuration = miningDuration;
            Loot = loot;
            PlaceSound = placeSound;
            BreakSound = breakSound;
            Name = name;
        }

        public static implicit operator TileMaterial(TileMaterialPreset preset)
        {
            return new TileMaterial(preset.MiningDuration(), preset.Loot(), preset.PlaceSound(), preset.BreakSound(), preset.ToString());
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
        private static readonly Lazy<ItemType> StoneItemType = new(() => Resources.Load<ItemType>("Tiles/Item_Stone"));
        
        public static float MiningDuration(this TileMaterialPreset material)
        {
            return material switch
            {
                TileMaterialPreset.Stone => 0.3f,
                TileMaterialPreset.Unknown => 0.5f,
                TileMaterialPreset.Bedrock => -1,
                _ => 0,
            };
        }

        public static ItemStack[] Loot(this TileMaterialPreset material)
        {
            return material switch
            {
                TileMaterialPreset.Stone => new ItemStack[]
                {
                    new(StoneItemType.Value, 1, Vector2Int.zero)
                },
                _ => null,
            };
        }

        public static EventReference PlaceSound(this TileMaterialPreset material)
        {
            return material switch
            {
                TileMaterialPreset.Stone => FModEvents.Instance.StonePlace,
                TileMaterialPreset.Unknown => default,
                TileMaterialPreset.Bedrock => default,
                _ => default
            };
        }

        public static EventReference BreakSound(this TileMaterialPreset material)
        {
            return material switch
            {
                TileMaterialPreset.Stone => FModEvents.Instance.StoneBreak,
                TileMaterialPreset.Unknown => default,
                TileMaterialPreset.Bedrock => default,
                _ => default,
            };
        }
    }
}