using Cinemachine;
using System;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Enemy;
using TokioSchool.FinalProject.Enums;
using TokioSchool.FinalProject.Equipments;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace TokioSchool.FinalProject.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float playerSpeed = 2.0f;
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float gravityValue = -9.81f;
        [SerializeField] private float stamina = 100f;
        [SerializeField] private float life = 100f;
        [SerializeField] private GameObject playerModel;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private Transform groundPlayerTransform;
        [SerializeField] private Transform followedCameraTransform;
        [SerializeField] private CinemachineVirtualCamera deadCamera;

        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool isGrounded;
        private bool aiming;
        private bool isCrouched;
        private bool lowStamina;
        private InputManager inputManager;
        private Transform cameraTransform;
        private Equipment equipment;
        private Animator animator;
        private float currentStamina;
        private float currentLife;
        private PlayerData playerData;

        private int animSpeed;
        private int animYSpeed;
        private int animXDirection;
        private int animZDirection;
        private int animIsGrounded;
        private int animIsCrouched;
        private int animAim;
        private int animJump;
        private int animHit;
        private int animDead;

        public float CurrentLife { get => currentLife; }
        public float CurrentStamina { get => currentStamina; }
        public float Life { get => life; }
        public bool Dead { get => currentLife <= 0; }
        public float Stamina { get => stamina; }
        public bool Aiming { get => aiming; }
        public Quaternion Rotation { get => cameraTransform.rotation; }
        public UnityAction OnChangeWeapon { get; set; }
        public UnityAction OnAttack { get; set; }
        public UnityAction OnHit { get; set; }
        public UnityAction OnDeath { get; set; }

        private void OnEnable()
        {
            inputManager = InputManager.Instance;
            inputManager.GetChangeWeaponAction().performed += ChangeWeapon;
        }

        private void OnDisable()
        {
            inputManager.GetChangeWeaponAction().performed -= ChangeWeapon;
        }

        private void Start()
        {
            playerData = PlayerPrefsManager.Instance.Load();
            controller = GetComponent<CharacterController>();
            equipment = GetComponent<Equipment>();
            equipment.SetupWeapon(0);
            animator = GetComponentInChildren<Animator>();
            cameraTransform = Camera.main.transform;
            currentStamina = stamina;
            currentLife = LevelManager.Instance.CurrentLevel() != ELevels.Minotaur ? playerData.currentPlayerLife : life;

            animSpeed = Animator.StringToHash("Speed");
            animYSpeed = Animator.StringToHash("YSpeed");
            animXDirection = Animator.StringToHash("XDirection");
            animZDirection = Animator.StringToHash("ZDirection");
            animIsGrounded = Animator.StringToHash("IsGrounded");
            animIsCrouched = Animator.StringToHash("IsCrouched");
            animAim = Animator.StringToHash("Aiming");
            animJump = Animator.StringToHash("Jump");
            animHit = Animator.StringToHash("Hit");
            animDead = Animator.StringToHash("Dead");
        }

        void Update()
        {
            if (Dead)
            {
                return;
            }

            isGrounded = Physics.CheckSphere(groundPlayerTransform.position, .1f, groundLayerMask);
            lowStamina = currentStamina <= stamina * 0.25f;

            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            animator.SetBool(animIsGrounded, isGrounded);

            var speedMultiplier = 1f;
            if (inputManager.PlayerSprinting() && !lowStamina)
            {
                speedMultiplier = 1.5f;
            }

            Vector2 movement = inputManager.GetPlayerMovement();
            Vector3 move = new(movement.x, 0f, movement.y);
            move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
            move.y = 0f;
            controller.Move(playerSpeed * speedMultiplier * Time.deltaTime * move.normalized);

            playerModel.transform.rotation = Quaternion.Euler(0, cameraTransform.rotation.eulerAngles.y, 0);

            animator.SetFloat(animSpeed, controller.velocity.sqrMagnitude);
            animator.SetFloat(animXDirection, movement.x);
            animator.SetFloat(animZDirection, movement.y);

            Aim();
            Attack();
            StaminaHandler();
            Crouch();
            ApplyPhysics();

            animator.SetFloat(animYSpeed, controller.velocity.y);
        }

        private void ChangeWeapon(CallbackContext context)
        {
            equipment.SetupWeapon(context.action.GetBindingIndexForControl(context.control));
            OnChangeWeapon?.Invoke();
        }

        private void StaminaHandler()
        {
            if (inputManager.PlayerSprinting() && controller.velocity != Vector3.zero)
            {
                currentStamina -= 10 * Time.deltaTime;
            }
            else if (currentStamina < stamina)
            {
                currentStamina += 7 * Time.deltaTime;
            }
            if (currentStamina < 0)
            {
                currentStamina = 0;
            }
        }

        private void Attack()
        {
            bool canAttack = (aiming && inputManager.PlayerAttack()) || (equipment.CanAttackWithoutAim && inputManager.PlayerAttack());
            if (canAttack)
            {
                OnAttack?.Invoke();
                equipment.ActionAnimation();
            }
        }

        private void Crouch()
        {
            if (inputManager.PlayerCrouched())
            {
                isCrouched = !isCrouched;
                animator.SetBool(animIsCrouched, isCrouched);
            }
        }

        private void Aim()
        {
            animator.SetBool(animAim, isGrounded && inputManager.PlayerAiming());

            if (isGrounded && inputManager.PlayerAiming())
            {
                equipment.HoldAnimation();
                aiming = true;
            }
            else
            {
                aiming = false;
            }
        }

        private void ApplyPhysics()
        {
            if (inputManager.PlayerJumped() && isGrounded && !lowStamina)
            {
                animator.SetTrigger(animJump);
                currentStamina -= stamina * 0.25f;
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("hit");
            EnemyController enemy = other.GetComponentInParent<EnemyController>();
            animator.SetTrigger(animHit);
            currentLife -= enemy.Damage;
            OnHit?.Invoke();

            if (currentLife <= 0)
            {
                Cursor.lockState = CursorLockMode.Confined;
                animator.SetTrigger(animDead);
                controller.enabled = false;
                deadCamera.Priority = 11;
                OnDeath?.Invoke();
            }
        }
    }
}