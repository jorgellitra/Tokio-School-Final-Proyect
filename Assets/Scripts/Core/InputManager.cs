using System;
using TokioSchool.FinalProject.Singletons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TokioSchool.FinalProject.Core
{
    public class InputManager : Singleton<InputManager>
    {
        private PlayerInputs playerControls;

        private void Awake()
        {
            playerControls = new PlayerInputs();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        public Vector2 GetMouseDelta()
        {
            return playerControls.Player.Look.ReadValue<Vector2>();
        }

        public Vector2 GetPlayerMovement()
        {
            return playerControls.Player.Movement.ReadValue<Vector2>();
        }

        public bool PlayerJumped()
        {
            return playerControls.Player.Jump.triggered;
        }
        public bool PlayerAiming()
        {
            return playerControls.Player.Aim.IsPressed();
        }
        public bool PlayerAttack()
        {
            return playerControls.Player.Attack.triggered;
        }
        public bool PlayerHold()
        {
            return playerControls.Player.Hold.IsPressed();
        }
        public bool PlayerReload()
        {
            return playerControls.Player.Reload.triggered;
        }
        public bool PlayerSprinting()
        {
            return playerControls.Player.Sprint.IsPressed();
        }
        public bool PlayerCrouched()
        {
            return playerControls.Player.Crouch.triggered;
        }
        public bool PlayerChangeWeaponPressed()
        {
            return playerControls.Player.ChangeWeapon.triggered;
        }
        public InputAction GetChangeWeaponAction()
        {
            return playerControls.Player.ChangeWeapon;
        }
    }
}
