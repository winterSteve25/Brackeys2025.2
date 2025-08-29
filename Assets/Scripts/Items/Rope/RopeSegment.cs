using UnityEngine;
using World;

namespace Items.Rope
{
    public class RopeSegment : FragileTile
    {
        [field: Header("Debug Info DO NOT EDIT")]
        [field: SerializeField] public RopeMaster Master { get; private set; }
        [SerializeField] private int index;
        [SerializeField] private Sprite endSprite;

        public void Init(RopeMaster m, int i)
        {
            Master = m;
            index = i;
        }

        public void IsEnd(bool isEnd)
        {
            if (isEnd)
            {
                spriteRenderer.sprite = endSprite;
            }
            else
            {
                SetTexture();
            }
        }

        public override void OnRemove(Vector2Int cell, Vector3 pos, WorldManager world)
        {
            Master.RemoveSegment(index);
            base.OnRemove(cell, pos, world);
        }
    }
}