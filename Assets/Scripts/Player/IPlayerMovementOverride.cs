using UnityEngine;

namespace Player
{
    public interface IPlayerMovementOverride
    {
        void UpdateFrame(Vector2 position, ref Vector2 dir, ref bool grounded);
        void UpdatePhysics(Rigidbody2D rb, Vector2 dir);
    }
}