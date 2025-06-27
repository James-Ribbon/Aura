using UnityEngine;

namespace Aura.Core.Interfaces
{
    public interface IMovement
    {
        void Move(Vector2 direction);
        bool IsGrounded { get; }
    }
}