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

        private List<Weapon> weapons = new();
        private Weapon currentWeapon;
        private Animator animator;
        private PlayerController player;
        private bool canAttackWithoutAim;

        public bool CanAttackWithoutAim { get => canAttackWithoutAim; }

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            player = GetComponent<PlayerController>();

            var weaponList = Resources.LoadAll<Weapon>("");
            foreach (Weapon weapon in weaponList)
            {
                weapons.Add(weapon);
            }

            currentWeapon = defaultWeapon;
        }

        public void SetupWeapon(string weaponId)
        {
            currentWeapon = weapons.Find(w => w.Id == weaponId);
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
            animator.Play(currentWeapon.ActionAnimation.name);
            if (currentWeapon.ProjectileOnAction != null)
            {
                var caster = Camera.main.transform;
                Instantiate(currentWeapon.ProjectileOnAction.Prefab, caster.position + caster.forward, caster.rotation);
            }
        }

        public void HoldAnimation()
        {
            if (!player.Aiming && currentWeapon.HoldAnimation != null)
            {
                animator.Play(currentWeapon.HoldAnimation.name);
            }
        }
    }
}
