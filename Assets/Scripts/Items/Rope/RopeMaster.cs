using System;
using System.Collections.Generic;
using Audio;
using FMOD.Studio;
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
        private EventInstance _sound;

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

        private void Start()
        {
            _sound = AudioManager.CreateInstance(FModEvents.Instance.RopeClimb);
        }

        private void OnDestroy()
        {
            _sound.stop(STOP_MODE.IMMEDIATE);
            _sound.release();
        }

        public void TryAddSegment()
        {
            if (segments.Count > 0)
            {
                if (WorldManager.Current.HasTile(segments[^1].Position + new Vector2Int(0, -1)))
                {
                    return;
                }
            }

            var segment = Instantiate(segmentPrefab, transform);
            WorldManager.Current.SetTile(transform.position - new Vector3(0, length, 0), segment);

            segment.Init(this, length);
            segments.Add(segment);
            segment.IsEnd(true);

            if (length - 1 >= 0)
            {
                segments[length - 1].IsEnd(false);
            }

            length++;

            col.size = new Vector2(1, length);
            col.offset = new Vector2(0, -(length - 1) / 2f);
        }

        public void RemoveSegment(int index)
        {
            if (index < 0 || index >= segments.Count)
            {
                return;
            }

            var removedAmount = segments.Count - index;
            segments.RemoveRange(index, removedAmount);
            length -= removedAmount;

            if (segments.Count > 0)
            {
                segments[^1].IsEnd(true);
            }

            col.size = new Vector2(1, length);
            col.offset = new Vector2(0, -(length - 1) / 2f);

            if (length <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void GrabOntoRope(PlayerInventory inventory)
        {
            Vector2 playerPosition = inventory.transform.position;
            var closestPoint = col.ClosestPoint(playerPosition);
            Physics2D.Raycast(playerPosition, (closestPoint - playerPosition).normalized,
                LayerMaskUtils.EverythingMask(true), _collisions);

            if (_collisions[0].collider != col)
            {
                return;
            }

            interactableObject.interactable = false;
            player = inventory.GetComponent<PlayerMovement>();
            player.MovementOverride = this;

            var rb = player.GetComponent<Rigidbody2D>();
            closestPoint.x = transform.position.x;
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
                AudioManager.PlayOnce(FModEvents.Instance.RopeClimbFinish, transform.position);
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

            if (Mathf.Abs(dir.y) > 0)
            {
                _sound.getPlaybackState(out var state);
                if (state is not PLAYBACK_STATE.PLAYING or PLAYBACK_STATE.STARTING)
                {
                    _sound.start();
                }
            }
            else
            {
                _sound.getPlaybackState(out var state);
                if (state is not PLAYBACK_STATE.STOPPED or PLAYBACK_STATE.STOPPING)
                {
                    _sound.stop(STOP_MODE.ALLOWFADEOUT);
                }
            }

            rb.linearVelocityY = dir.y * ropeMovementSpeed;
        }
    }
}