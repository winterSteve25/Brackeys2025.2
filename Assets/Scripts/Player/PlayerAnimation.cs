using KBCore.Refs;
using UnityEngine;

namespace Player
{
    public class PlayerAnimation : ValidatedMonoBehaviour
    {
        private static readonly int Walking = Animator.StringToHash("Walking");
        private static readonly int Hurt1 = Animator.StringToHash("Hurt");
        private static readonly int Jump = Animator.StringToHash("Jumping");

        [Header("References")]
        [SerializeField, Child] private Animator animator;
        [SerializeField, Child] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform handAnchor;
        
        public void StartWalk()
        {
            animator.SetBool(Walking, true);
        }

        public void EndWalk()
        {
            animator.SetBool(Walking, false);
        }

        public void StartJump()
        {
            animator.SetBool(Jump, true);
        }

        public void EndJump()
        {
            animator.SetBool(Jump, false);
        }
        
        public bool IsJumping() => animator.GetBool(Jump);

        public void Hurt()
        {
            animator.SetTrigger(Hurt1);
        }

        public void ChangeDirection(float dirx)
        {
            spriteRenderer.flipX = dirx < 0;
            var scale = handAnchor.localScale;
            var pos = handAnchor.localPosition;
            var sign = Mathf.Sign(dirx);
            
            scale.x = sign;
            pos.x = Mathf.Abs(handAnchor.localPosition.x) * sign;
            handAnchor.localPosition = pos;
            handAnchor.localScale = scale;
        }
    }
}