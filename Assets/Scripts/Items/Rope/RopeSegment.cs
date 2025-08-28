using UnityEngine;
using World;

namespace Items.Rope
{
    public class RopeSegment : FragileTile
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private bool isMaster;
        [field: SerializeField] public RopeMaster Master { get; private set; }

        public void Init(RopeMaster m, bool isM)
        {
            Master = m;
            isMaster = isM;
        }

        public override void OnRemove(Vector2Int cell, Vector3 pos, WorldManager world)
        {
            Master.RemoveSegment(this);
            base.OnRemove(cell, pos, world);
        }

        public override void OnNeighborUpdated(Vector2Int cell, WorldManager world)
        {
            if (isMaster) return;
            base.OnNeighborUpdated(cell, world);
        }
    }
}