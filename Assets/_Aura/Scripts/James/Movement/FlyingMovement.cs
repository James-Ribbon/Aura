using UnityEngine;
using Aura.Core.Interfaces;

namespace Aura.Core.Movement
{
    public class FlyingMovement : BaseMovement, IMovement
    {
        [SerializeField] private float scaleAplitude = 0.5f;
        [SerializeField] private float scaleFrequency = 2f;

        private float originalY;
        public bool IsGrounded => false;

        protected override void Awake()
        {
            base.Awake();
            originalY = transform.position.y;
        }

        public override void Move(Vector2 direction)
        {
            float hoverOffset = Mathf.Sin(Time.time * scaleFrequency) * scaleAplitude;
            Vector2 newPosition = rb.position + direction * (moveSpeed * Time.deltaTime);
            newPosition.y = originalY + hoverOffset; // Maintain hover 

            rb.MovePosition(newPosition);
        }
    }
}
