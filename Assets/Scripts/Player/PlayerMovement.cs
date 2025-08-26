using KBCore.Refs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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
        [SerializeField] private bool wasWalking;
        [SerializeField] private float coyoteTimeCountDown;

        [Header("References")] 
        [SerializeField, Self] private Rigidbody2D rb;
        [SerializeField, Self] private PlayerAnimation animations;
        [SerializeField] private Transform groundTest;
        [SerializeField] private Transform groundTest2;
        [SerializeField] private float groundTestRadius;

        [Header("Input")] 
        [SerializeField] private InputActionReference movement;
        [SerializeField] private InputActionReference jump;

        [Header("Stats")] 
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpBufferTime;
        [SerializeField] private float coyoteTime;

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
            if (!grounded)
            {
                collisionCount = Physics2D.OverlapCircle(groundTest2.position, groundTestRadius, everythingElseLayerMask, groundCollision);
                grounded = collisionCount > 0;
            }
            
            direction = movement.action.ReadValue<Vector2>();
            UpdateVisuals(wasGrounded);
            HandleJump(wasGrounded);
        }

        private void FixedUpdate()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            rb.linearVelocityX = direction.x * speed;
        }

        private void UpdateVisuals(bool wasGrounded)
        {
            if (direction.sqrMagnitude > 0)
            {
                if (!wasWalking)
                {
                    animations.StartWalk();
                }
                
                animations.ChangeDirection(direction.x);
                wasWalking = true;
            }
            else
            {
                if (wasWalking)
                {
                    animations.EndWalk();
                }
                
                wasWalking = false;
            }

            if (!wasGrounded && grounded)
            {
                animations.EndJump();
            }
        }
        
        private void HandleJump(bool wasGrounded)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (wasGrounded && !grounded)
            {
                coyoteTimeCountDown = coyoteTime;
            }

            if (coyoteTimeCountDown > 0)
            {
                coyoteTimeCountDown -= Time.deltaTime;
                if (coyoteTimeCountDown <= 0)
                {
                    coyoteTimeCountDown = 0;
                }
            }

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

        private void Jump(bool check)
        {
            if (grounded || !check || coyoteTimeCountDown > 0)
            {
                animations.StartJump();
                rb.linearVelocityY = jumpForce;
                return;
            }

            jumpBuffered = true;
            jumpBufferedTimeAmount = 0;
        }
    }
}