using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Damageables;
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

        private int currentNumberOfProjectiles;
        private int currentProjectilesLoaded;
        private Weapon currentWeapon;
        private Animator animator;
        private PlayerAnimEventController animEventController;
        private bool canAttackWithoutAim;
        public bool CanAttackWithoutAim { get => canAttackWithoutAim; }
        public Weapon CurrentWeapon { get => currentWeapon; }
        public int CurrentNumberOfProjectiles { get => currentNumberOfProjectiles; }
        public int CurrentProjectilesLoaded { get => currentProjectilesLoaded; }
        public bool NeedToReload { get => currentProjectilesLoaded == 0 && currentWeapon.HasProjectiles; }

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            animEventController = GetComponentInChildren<PlayerAnimEventController>();

            PlayerData playerData = PlayerPrefsManager.Instance.Load();
            if (playerData.weaponsData.Count == 0)
            {
                foreach (Weapon weapon in weapons)
                {
                    playerData.weaponsData.Add(weapon.Id, new ProjectilesWeaponData()
                    {
                        CurrentNumberOfProjectiles = weapon.NumberOfProjectiles - weapon.ProjectilesPerLoad,
                        CurrentProjectilesLoaded = weapon.ProjectilesPerLoad
                    });
                }

                PlayerPrefsManager.Instance.Save(playerData);
            }

            currentWeapon = defaultWeapon;
        }

        public void SetupWeapon(int weaponIndex)
        {
            PlayerData playerData = PlayerPrefsManager.Instance.Load();
            if ((playerData.weapon1IsLocked && weaponIndex == 0) ||
                (playerData.weapon2IsLocked && weaponIndex == 1) ||
                (playerData.weapon3IsLocked && weaponIndex == 2) ||
                (playerData.weapon4IsLocked && weaponIndex == 3))
            {
                return;
            }
            SaveWeaponData(playerData);

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

                LoadWeaponData(playerData);
            }
        }

        private void SaveWeaponData(PlayerData playerData)
        {
            if (currentWeapon.HasProjectiles)
            {
                var weaponData = playerData.weaponsData.GetValueOrDefault(currentWeapon.Id);

                weaponData.CurrentNumberOfProjectiles = currentNumberOfProjectiles;
                weaponData.CurrentProjectilesLoaded = currentProjectilesLoaded;

                PlayerPrefsManager.Instance.Save(playerData);
            }
        }

        private void LoadWeaponData(PlayerData playerData)
        {
            if (currentWeapon.HasProjectiles)
            {
                var weaponData = playerData.weaponsData.GetValueOrDefault(currentWeapon.Id);

                currentNumberOfProjectiles = weaponData.CurrentNumberOfProjectiles;
                currentProjectilesLoaded = weaponData.CurrentProjectilesLoaded;
            }
        }

        public void ActionAnimation()
        {
            animator.Play(currentWeapon.ActionAnimation.name);
            if (currentWeapon.HasProjectiles)
            {
                var caster = Camera.main.transform;
                Instantiate(currentWeapon.DamageableObjectOnAction.Prefab, caster.position + caster.forward, caster.rotation);
                currentProjectilesLoaded--;
                SaveWeaponData(PlayerPrefsManager.Instance.Load());
            }
            if (currentWeapon.DamageableObjectOnAction != null && !currentWeapon.DamageableObjectOnAction.IsProyectile)
            {
                StartCoroutine(ChangeCanDamageStateCoroutine(currentWeapon.ActionAnimation.length));
            }
        }

        private IEnumerator ChangeCanDamageStateCoroutine(float time)
        {
            ChangeCanDamageState(true);
            yield return new WaitForSeconds(time);
            ChangeCanDamageState(false);
        }

        private void ChangeCanDamageState(bool state)
        {
            foreach (Damageable item in rightHand.GetComponentsInChildren<Damageable>())
            {
                item.CanDamage = state;
            }
            foreach (Damageable item in leftHand.GetComponentsInChildren<Damageable>())
            {
                item.CanDamage = state;
            }
        }

        public void Reload()
        {
            var numberOfProjectiles = currentWeapon.ProjectilesPerLoad > currentNumberOfProjectiles ? 0 : currentNumberOfProjectiles - currentWeapon.ProjectilesPerLoad;
            currentProjectilesLoaded = currentWeapon.ProjectilesPerLoad > currentNumberOfProjectiles ?
                (currentNumberOfProjectiles - currentWeapon.ProjectilesPerLoad) <= 0 ? 0 : currentNumberOfProjectiles - currentWeapon.ProjectilesPerLoad :
                currentWeapon.ProjectilesPerLoad;
            currentNumberOfProjectiles = numberOfProjectiles;
        }

        public void HoldAnimation()
        {
            if (currentWeapon.HoldAnimation != null)
            {
                animator.Play(currentWeapon.HoldAnimation.name);
            }
        }
    }
}