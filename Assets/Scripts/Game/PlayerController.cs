using Cinemachine;
using System;
using TMPro;
using TokioSchool.FinalProject.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TokioSchool.FinalProject.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float playerSpeed = 2.0f;
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float gravityValue = -9.81f;
        [SerializeField] private float reloadingTime = 1f;
        [SerializeField] private float stamina = 100f;
        [SerializeField] private float life = 100f;
        [SerializeField] private GameObject playerModel;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private Transform groundPlayerTransform;
        [SerializeField] private Transform followedCameraTransform;
        [SerializeField] private CinemachineVirtualCamera followedCamera;
        [SerializeField] private CinemachineVirtualCamera aimingCamera;

        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool isGrounded;
        private bool aiming;
        private bool isCrouched;
        private bool reloading;
        private bool lowStamina;
        private InputManager inputManager;
        private Transform cameraTransform;
        private Animator animator;
        private float reloadTimer;
        private int currentBullets;
        private float currentStamina;
        private float currentLife;

        private int animSpeed;
        private int animYSpeed;
        private int animXDirection;
        private int animZDirection;
        private int animIsGrounded;
        private int animIsCrouched;
        private int animAim;
        private int animAttack;
        private int animJump;


        public int CurrentBullets { get => currentBullets; }
        public float CurrentLife { get => currentLife; }
        public float CurrentStamina { get => currentStamina; }
        public bool IsReloading { get => reloading; set => reloading = value; }
        public float ReloadTimer { get => reloadTimer; set => reloadTimer = value; }
        public float Life { get => life; }
        public float Stamina { get => stamina; }

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
            inputManager = InputManager.Instance;
            cameraTransform = Camera.main.transform;
            reloadTimer = reloadingTime;
            currentStamina = stamina;
            currentLife = life;

            animSpeed = Animator.StringToHash("Speed");
            animYSpeed = Animator.StringToHash("YSpeed");
            animXDirection = Animator.StringToHash("XDirection");
            animZDirection = Animator.StringToHash("ZDirection");
            animIsGrounded = Animator.StringToHash("IsGrounded");
            animIsCrouched = Animator.StringToHash("IsCrouched");
            animAim = Animator.StringToHash("Aiming");
            animAttack = Animator.StringToHash("Attack");
            animJump = Animator.StringToHash("Jump");
        }

        void Update()
        {
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

            Reload();
            Aim();
            Attack();
            StaminaHandler();
            Crouch();
            ApplyPhysics();

            animator.SetFloat(animYSpeed, controller.velocity.y);
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

        private void Reload()
        {
            if (inputManager.PlayerReload())
            {
                reloading = true;
                //UIController.Instance.ActiveReloadUI(true);
            }

            if (reloading)
            {
                reloadTimer -= Time.deltaTime;
                if (reloadTimer < 0)
                {
                    reloadTimer = reloadingTime;

                    //int totalAmmo = 0;
                    //int totalInitialAmmo = 0;
                    //foreach (Weapon weapon in CurrentWeaponList)
                    //{
                    //    totalInitialAmmo += weapon.BulletInitialAmmount;
                    //    if (weapon.BulletAmmount < weapon.BulletInitialAmmount)
                    //    {
                    //        weapon.BulletAmmount++;
                    //        totalAmmo += weapon.BulletAmmount;
                    //    }
                    //}
                    //currentBullets = totalAmmo;
                    //UIController.Instance.UpdateUI();
                    //if (totalInitialAmmo <= totalAmmo)
                    //{
                    //    reloading = false;
                    //    UIController.Instance.ActiveReloadUI(false);
                    //}
                }
            }
        }

        private void Attack()
        {
            if (aiming && inputManager.PlayerAttack() && !reloading && currentBullets > 0)
            {
                //animator.SetTrigger(animAttack);
                //int totalAmmo = 0;
                //foreach (Weapon weapon in CurrentWeaponList)
                //{
                //    if (weapon.BulletAmmount > 0)
                //    {
                //        weapon.OnShoot();
                //        totalAmmo += weapon.BulletAmmount;
                //    }
                //}
                //currentBullets = totalAmmo;
                //UIController.Instance.UpdateUI();
            }
        }
        private void Crouch()
        {
            if (inputManager.PlayerCrouched())
            {
                isCrouched = !isCrouched;
                animator.SetBool(animIsCrouched, isCrouched);
                //animator.SetTrigger(animAttack);
                //int totalAmmo = 0;
                //foreach (Weapon weapon in CurrentWeaponList)
                //{
                //    if (weapon.BulletAmmount > 0)
                //    {
                //        weapon.OnShoot();
                //        totalAmmo += weapon.BulletAmmount;
                //    }
                //}
                //currentBullets = totalAmmo;
                //UIController.Instance.UpdateUI();
            }
        }

        private void Aim()
        {
            if (!reloading)
            {
                //animator.SetBool(animAim, groundedPlayer && inputManager.PlayerAiming());
                //
                //if (groundedPlayer && inputManager.PlayerAiming())
                //{
                //    aiming = true;
                //    aimingCamera.Priority = 1;
                //    followedCamera.Priority = 0;
                //}
                //else
                //{
                //    aiming = false;
                //    aimingCamera.Priority = 0;
                //    followedCamera.Priority = 1;
                //}
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

        //public void SetArsenal(EArsenalType type)
        //{
        //    equipment.SetArsenal(type);

        //    int totalAmmo = 0;
        //    foreach (Weapon weapon in CurrentWeaponList)
        //    {
        //        totalAmmo += weapon.BulletAmmount;
        //    }
        //    currentBullets = totalAmmo;
        //}
    }
}