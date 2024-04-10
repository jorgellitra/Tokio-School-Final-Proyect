using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Equipments;
using TokioSchool.FinalProject.Player;
using TokioSchool.FinalProject.Tweens;
using UnityEngine;
using UnityEngine.UI;

namespace TokioSchool.FinalProject.UI
{
    public class UIPlayerController : UIPanel
    {
        [SerializeField] private Slider sliderLife;
        [SerializeField] private Slider sliderStamina;
        [SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private EquipmentSlot startEquipmentSlot;
        [SerializeField] private Weapon startWeapon;
        [SerializeField] private UIPanel gameOverPanel;
        [SerializeField] private Image bloodUI;
        [SerializeField] private GameObject proyectilesInfo;
        [SerializeField] private TextMeshProUGUI currentProjectilesText;
        [SerializeField] private TextMeshProUGUI currentNumberOfProjectilesText;

        [SerializeField] private TooManyCrosshairs.Crosshair crosshair;

        private PlayerController player;
        private EquipmentSlot currentEquipmentSlot;
        private Equipment playerEquipment;

        private void Awake()
        {
            player = FindFirstObjectByType<PlayerController>();
            playerEquipment = player.GetComponent<Equipment>();
        }

        private void OnEnable()
        {
            player.OnChangeWeapon += SetupWeaponUI;
            player.OnAttack += UpdateUI;
            player.OnHit += OnHitPlayer;
            player.OnDeath += SwitchToGameOverPanel;
            player.OnReload += ReloadCrosshair;
        }

        private void OnDisable()
        {
            player.OnChangeWeapon -= SetupWeaponUI;
            player.OnAttack -= UpdateUI;
            player.OnHit -= OnHitPlayer;
            player.OnDeath -= SwitchToGameOverPanel;
            player.OnReload -= ReloadCrosshair;
        }

        public override void StartScreen()
        {
            base.StartScreen();

            sliderLife.maxValue = player.Life;
            sliderStamina.maxValue = player.Stamina;
            SetupWeaponUI();
        }

        private void SetupCrosshair()
        {
            crosshair.SetReloadSpeed(1 / playerEquipment.CurrentWeapon.ReloadCooldown);
        }

        private void Update()
        {
            if (player.Dead)
            {
                return;
            }
            sliderStamina.value = player.CurrentStamina;
            if (!LevelManager.Instance.StopCountingTime)
            {
                TimeSpan time = TimeSpan.FromMilliseconds(LevelManager.Instance.miliseconds);

                timer.text = $"{time.Minutes:00}:{time.Seconds:00}:{time.Milliseconds:000}";
            }
        }

        public void UpdateUI()
        {
            sliderStamina.value = player.CurrentStamina;
            sliderLife.value = player.CurrentLife;

            proyectilesInfo.SetActive(playerEquipment.CurrentWeapon.HasProjectiles);
            currentNumberOfProjectilesText.text = playerEquipment.CurrentNumberOfProjectiles.ToString();
            currentProjectilesText.text = playerEquipment.CurrentProjectilesLoaded.ToString();
        }

        public void SetupWeaponUI()
        {
            EquipmentSlot[] equipmentSlots = GetComponentsInChildren<EquipmentSlot>();
            PlayerData playerData = PlayerPrefsManager.Instance.Load();

            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        equipmentSlots[i].ChangeStatusLocked(playerData.weapon1IsLocked);
                        break;
                    case 1:
                        equipmentSlots[i].ChangeStatusLocked(playerData.weapon2IsLocked);
                        break;
                    case 2:
                        equipmentSlots[i].ChangeStatusLocked(playerData.weapon3IsLocked);
                        break;
                    case 3:
                        equipmentSlots[i].ChangeStatusLocked(playerData.weapon4IsLocked);
                        break;
                }

                if (equipmentSlots[i].Weapon.Id == playerEquipment.CurrentWeapon.Id)
                {
                    currentEquipmentSlot?.ChangeStatus(false);
                    currentEquipmentSlot = equipmentSlots[i];
                    currentEquipmentSlot.ChangeStatus(true);

                    SetupCrosshair();
                    UpdateUI();
                }
            }
        }

        public void ReloadCrosshair()
        {
            crosshair.DoReload();
            UpdateUI();
        }

        public void SwitchToGameOverPanel()
        {
            UIController.Instance.SwitchScreens(gameOverPanel);
        }

        private void OnHitPlayer()
        {
            UpdateUI();
            TweenManager.Instance.DoSequence(new List<Tween>() {
                bloodUI.GetComponent<CanvasGroup>().DOFade(1, .5f),
                bloodUI.GetComponent<CanvasGroup>().DOFade(0, .5f)
            });
        }
    }
}
