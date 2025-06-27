using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aura.Core.Movement
{
    public abstract class BaseMovement : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed = 5f;
        protected Rigidbody2D rb;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

#if UNITY_EDITOR
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component is missing from the GameObject.");
            }
#endif
        }

        public abstract void Move(Vector2 direction);
    }
}