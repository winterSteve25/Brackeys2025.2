using System.Linq;
using UnityEngine;

namespace World
{
    public class FragileTile : WorldTile
    {
        [SerializeField] protected Vector2Int[] requires;
        [SerializeField] protected bool dropItem;
        
        public override void OnNeighborUpdated(Vector2Int cell, WorldManager world)
        {
            if (requires.Any(offset => !world.HasTile(cell + offset)))
            {
                BreakManager.Current.CompleteBreak(cell, dropItem);
                Debug.Log(cell);
            }
        }
    }
}