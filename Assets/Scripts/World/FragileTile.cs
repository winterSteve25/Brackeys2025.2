using System.Linq;
using UnityEngine;

namespace World
{
    public class FragileTile : WorldTile
    {
        [SerializeField] private Vector2Int[] requires;
        [SerializeField] private bool dropItem;
        
        public override void OnNeighborUpdated(Vector2Int cell, WorldManager world)
        {
            if (requires.Any(offset => !world.HasTile(cell + offset)))
            {
                BreakManager.Current.CompleteBreak(cell, dropItem);
            }
        }
    }
}