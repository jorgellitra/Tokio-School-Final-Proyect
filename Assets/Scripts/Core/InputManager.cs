using UnityEngine;

namespace TokioSchool.FinalProject.Core
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInputs playerControls;

        private static InputManager instance;

        public static InputManager Instance { get => instance; set => instance = value; }

        private void Awake()
        {
            instance = this;
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
    }
}
