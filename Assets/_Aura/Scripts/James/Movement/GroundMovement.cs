using UnityEngine;
using Aura.Core.Interfaces;

namespace Aura.Core.Movement
{
    public class GroundMovement : BaseMovement, IMovement, IBoostable
    {
        [Header("Boost Settings")]
        [SerializeField] private float boostSpeed = 12f;

        [Header("Grounded Settings")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckDistance = 0.1f;

        [SerializeField] private bool isBoosting;
        [SerializeField] private bool isGrounded;

        public bool IsBoosting { get { return isBoosting; } }

        public bool IsGrounded { get { return isGrounded; } }
        
        public override void Move(Vector2 direction)
        {
            float speed = isBoosting ? boostSpeed : moveSpeed;
            Debug.Log($"Moving with speed: {speed}, Boosting: {isBoosting}");
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
            GroundedStatus();
        }

        public void ToggleBoost(bool enable) => isBoosting = enable;

        private void GroundedStatus()
        {
            RaycastHit2D hit2D = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
            isGrounded = hit2D.collider != null;
        }

        private void OnDrawGizmos()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(groundCheck.position,
                                groundCheck.position + Vector3.down * groundCheckDistance);
            }
        }
    }
}