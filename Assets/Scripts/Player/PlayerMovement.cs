using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerMovement : ValidatedMonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")] 
        [SerializeField] private Vector2 direction;
        [SerializeField] private bool grounded;
        [SerializeField] private Collider2D[] groundCollision;
        [SerializeField] private ContactFilter2D everythingElseLayerMask;
        [SerializeField] private bool jumpBuffered;
        [SerializeField] private float jumpBufferedTimeAmount;

        [Header("References")] 
        [SerializeField, Self] private Rigidbody2D rb;
        [SerializeField] private Transform groundTest;
        [SerializeField] private float groundTestRadius;

        [Header("Input")] 
        [SerializeField] private InputActionReference movement;
        [SerializeField] private InputActionReference jump;

        [Header("Stats")] 
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpBufferTime;

        private void Awake()
        {
            var avoidMask = LayerMask.NameToLayer("Player");
            var mask = 1 << avoidMask;
            mask = ~mask;
            groundCollision = new Collider2D[1];
            everythingElseLayerMask = ContactFilter2D.noFilter;
            everythingElseLayerMask.SetLayerMask(mask);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(groundTest.position, groundTestRadius);
        }

        private void Update()
        {
            var collisionCount = Physics2D.OverlapCircle(groundTest.position, groundTestRadius, everythingElseLayerMask, groundCollision);
            var wasGrounded = grounded;

            grounded = collisionCount > 0;
            direction = movement.action.ReadValue<Vector2>();

            if (jump.action.WasPressedThisFrame())
            {
                Jump(true);
            }

            if (jumpBuffered)
            {
                jumpBufferedTimeAmount += Time.deltaTime;
                if (!wasGrounded && grounded)
                {
                    Jump(false);
                    jumpBuffered = false;
                }
                else if (jumpBufferedTimeAmount >= jumpBufferTime)
                {
                    jumpBuffered = false;
                }
            }
        }

        private void FixedUpdate()
        {
            rb.linearVelocityX = direction.x * speed;
        }

        private void Jump(bool check)
        {
            if (grounded || !check)
            {
                rb.linearVelocityY = jumpForce;
                return;
            }

            jumpBuffered = true;
            jumpBufferedTimeAmount = 0;
        }
    }
}