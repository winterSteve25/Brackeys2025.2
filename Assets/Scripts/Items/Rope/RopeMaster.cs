using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Items.Rope
{
    public class RopeMaster : MonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private List<RopeSegment> segments;
        [SerializeField] private int length;

        [Header("References")]
        [SerializeField] private BoxCollider2D col;

        public void AddSegment(RopeSegment segment)
        {
            segment.Init(this);
            segments.Add(segment);
            length++;
        }

        public void RemoveSegment(RopeSegment segment)
        {
            segments.Remove(segment);
            length--;
        }

        public void GrabOntoRope(PlayerInventory inventory)
        {
        }
    }
}