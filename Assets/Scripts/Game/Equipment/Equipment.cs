using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace TokioSchool.FinalProject.Equipments
{
    public class Equipment : MonoBehaviour
    {
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;
        [SerializeField] private Weapon defaultWeapon;
        [SerializeField] private List<Weapon> weapons;

        private Weapon currentWeapon;
        private Animator animator;
        private bool canAttackWithoutAim;
        private bool attackOnCooldown;
        private bool holdingAnimationState;

        public bool AttackOnCooldown { get => attackOnCooldown; }
        public bool CanAttackWithoutAim { get => canAttackWithoutAim; }
        public Weapon CurrentWeapon { get => currentWeapon; }

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();

            currentWeapon = defaultWeapon;
        }

        public void SetupWeapon(int weaponIndex)
        {
            currentWeapon = weapons[weaponIndex];
            rightHand.DestroyChildren();
            leftHand.DestroyChildren();

            canAttackWithoutAim = currentWeapon.CanAttackWithoutAim;

            if (currentWeapon != null)
            {
                if (currentWeapon.LeftHandPrefab != null)
                {
                    var weaponInstantiated = Instantiate(currentWeapon.LeftHandPrefab, leftHand);
                    weaponInstantiated.SetActive(true);
                }
                if (currentWeapon.RightHandPrefab != null)
                {
                    var weaponInstantiated = Instantiate(currentWeapon.RightHandPrefab, rightHand);
                    weaponInstantiated.SetActive(true);
                }
            }
        }

        public void ActionAnimation()
        {
            if (!attackOnCooldown)
            {
                StartCoroutine(AttackCooldownCoroutine(currentWeapon.AttackCooldown));
                animator.Play(currentWeapon.ActionAnimation.name);
                if (currentWeapon.ProjectileOnAction != null)
                {
                    var caster = Camera.main.transform;
                    Instantiate(currentWeapon.ProjectileOnAction.Prefab, caster.position + caster.forward, caster.rotation);
                }
            }
        }

        public void HoldAnimation()
        {
            if (!holdingAnimationState && currentWeapon.HoldAnimation != null && !attackOnCooldown)
            {
                holdingAnimationState = true;
                animator.Play(currentWeapon.HoldAnimation.name);
            }
        }

        IEnumerator AttackCooldownCoroutine(float seconds)
        {
            attackOnCooldown = true;
            yield return new WaitForSeconds(seconds);
            attackOnCooldown = false;
            holdingAnimationState = false;
        }
    }
}