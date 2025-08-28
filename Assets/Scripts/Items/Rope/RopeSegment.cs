using UnityEngine;
using World;

namespace Items.Rope
{
    public class RopeSegment : FragileTile
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private RopeMaster master;

        public void Init(RopeMaster m)
        {
            master = m;
        }

        public override void OnRemove(Vector2Int cell, Vector3 pos, WorldManager world)
        {
            master.RemoveSegment(this);
            base.OnRemove(cell, pos, world);
        }
    }
}