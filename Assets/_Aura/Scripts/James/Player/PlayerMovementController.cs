using Aura.Core.Interfaces;
using Aura.Core.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aura.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementController : MonoBehaviour, IMovement, IJumper
    {
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float moveSpeed = 5f;

        private BaseMovement currentMovement;
        private bool isFacingRight = true;

        public bool IsGrounded => currentMovement is IMovement movement && movement.IsGrounded;
        public bool CanJump => IsGrounded;

        private void Awake()
        {
            currentMovement = GetComponent<GroundMovement>();

            if (currentMovement == null)
            {
                currentMovement = gameObject.AddComponent<GroundMovement>();
            }
        }

        public void Move(Vector2 direction)
        {
            currentMovement.Move(direction);
            FlipCharacter(direction.x);
        }

        private void FlipCharacter(float horizontalInput)
        {
            if ((horizontalInput > 0 && !isFacingRight) || (horizontalInput < 0 && isFacingRight))
            {
                isFacingRight = !isFacingRight;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }

        public void Jump()
        {
            if (CanJump)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}