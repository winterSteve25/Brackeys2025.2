using System.Collections.Generic;
using KBCore.Refs;
using Objects;
using Player;
using UnityEngine;
using Utils;
using World;

namespace Items.Rope
{
    public class RopeMaster : ValidatedMonoBehaviour, IPlayerMovementOverride
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private List<RopeSegment> segments;
        [SerializeField] private int length;
        [SerializeField] private PlayerMovement player;
        [SerializeField] private float originalGravityScale;
        private RaycastHit2D[] _collisions;

        [Header("References")]
        [SerializeField, Self] private BoxCollider2D col;
        [SerializeField, Self] private InteractableObject interactableObject;
        [SerializeField] private Transform upLimit;
        [SerializeField] private RopeSegment segmentPrefab;

        [Header("Parameters")]
        [SerializeField] private float ropeMovementSpeed;

        private void Awake()
        {
            _collisions = new RaycastHit2D[1];
        }

        public void AddSegment(bool isMaster = false)
        {
            var segment = Instantiate(segmentPrefab, transform);
            WorldManager.Current.SetTile(transform.position - new Vector3(0, length, 0), segment);
            
            segment.Init(this, isMaster);
            segments.Add(segment);
            length++;
            
            col.size = new Vector2(1, length);
            col.offset = new Vector2(0, -(length - 1) / 2f);
        }

        public void RemoveSegment(RopeSegment segment)
        {
            segments.Remove(segment);
            length--;
            
            col.size = new Vector2(1, length);
            col.offset = new Vector2(0, -(length - 1) / 2f);
        }

        public void GrabOntoRope(PlayerInventory inventory)
        {
            interactableObject.interactable = false;

            Vector2 playerPosition = inventory.transform.position;
            var closestPoint = col.ClosestPoint(playerPosition);
            Physics2D.Raycast(playerPosition, (closestPoint - playerPosition).normalized,
                LayerMaskUtils.EverythingMask(true), _collisions);

            if (_collisions[0] != col)
            {
                // todo: add cant grab feedback
                return;
            }

            player = inventory.GetComponent<PlayerMovement>();
            player.MovementOverride = this;

            var rb = player.GetComponent<Rigidbody2D>();
            rb.position = closestPoint;
            rb.linearVelocityX = 0;

            originalGravityScale = rb.gravityScale;
            rb.gravityScale = 0;
        }

        public void UpdateFrame(Vector2 position, ref Vector2 dir, ref bool grounded)
        {
            grounded = true;

            if (Mathf.Abs(dir.normalized.x) >= 1)
            {
                player.MovementOverride = null;
                player.GetComponent<Rigidbody2D>().gravityScale = originalGravityScale;
                interactableObject.interactable = true;
                player = null;
            }
        }

        public void UpdatePhysics(Rigidbody2D rb, Vector2 dir)
        {
            if (rb.position.y > upLimit.position.y ||
                Mathf.Approximately(rb.position.y, upLimit.position.y))
            {
                var pos = rb.position;
                pos.y = upLimit.position.y;
                rb.position = pos;
                if (dir.y > 0)
                {
                    dir.y = 0;
                }
            }

            if (rb.position.y < upLimit.position.y - col.size.y ||
                Mathf.Approximately(rb.position.y, upLimit.position.y - col.size.y))
            {
                var pos = rb.position;
                pos.y = upLimit.position.y - col.size.y;
                
                rb.position = pos;
                if (dir.y < 0)
                { 
                    dir.y = 0;
                }
            }

            rb.linearVelocityY = dir.y * ropeMovementSpeed;
        }
    }
}