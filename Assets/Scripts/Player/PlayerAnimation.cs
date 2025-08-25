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

        public void Hurt()
        {
            animator.SetTrigger(Hurt1);
        }

        public void ChangeDirection(float dirx)
        {
            spriteRenderer.flipX = dirx < 0;
        }
    }
}