using Aura.Core.Interfaces;
using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aura.PlayerInput
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [Header("Run Input")] //Replace with Config scriptable object
        [SerializeField] private KeyCode runKey = KeyCode.LeftShift;
        [SerializeField] private KeyCode jumpKey = KeyCode.Space;

        private IMovement movement;
        private IBoostable boostable;
        private IJumper jumper;

        void Awake()
        {
            movement = GetComponent<IMovement>();
            boostable = GetComponent<IBoostable>();
            jumper = GetComponent<IJumper>();   
        }

        void Update()
        {
            HandleMovementInput();
            HandleBoostInput();
            HandleJumpInput();
        }

        private void HandleMovementInput()
        {
            float horizontal = Input.GetAxis("Horizontal");
            movement.Move(new Vector2(horizontal, 0));
        }

        private void HandleBoostInput()
        {
            bool runPressed = Input.GetKey(runKey);
            boostable.ToggleBoost(runPressed);
        }

        private void HandleJumpInput()
        {
            if (Input.GetKeyDown(jumpKey))
            {
                jumper.Jump();
            }
        }
    }
}
