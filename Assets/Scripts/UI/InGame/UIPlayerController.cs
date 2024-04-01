using DG.Tweening;
using Newtonsoft.Json.Bson;
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
            player.OnAttack += ReloadCrosshair;
            player.OnHit += OnHitPlayer;
            player.OnDeath += SwitchToGameOverPanel;
        }

        private void OnDisable()
        {
            player.OnChangeWeapon -= SetupWeaponUI;
            player.OnAttack -= ReloadCrosshair;
            player.OnHit -= OnHitPlayer;
            player.OnDeath -= SwitchToGameOverPanel;
        }

        public override void StartScreen()
        {
            base.StartScreen();
            SetupCrosshair();

            sliderLife.maxValue = player.Life;
            sliderStamina.maxValue = player.Stamina;
            UpdateUI();
        }

        private void SetupCrosshair()
        {
            crosshair.SetReloadSpeed(playerEquipment.CurrentWeapon.AttackCooldown);
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

                timer.text = $"{time.Minutes:00}:{time.Seconds:00}:{time.Milliseconds:00}";
            }
        }

        public void UpdateUI()
        {
            sliderStamina.value = player.CurrentStamina;
            sliderLife.value = player.CurrentLife;
        }

        public void SetupWeaponUI()
        {
            foreach (EquipmentSlot slot in GetComponentsInChildren<EquipmentSlot>())
            {
                if (slot.Weapon.Id == playerEquipment.CurrentWeapon.Id)
                {
                    currentEquipmentSlot?.ChangeStatus(false);
                    currentEquipmentSlot = slot;
                    currentEquipmentSlot.ChangeStatus(true);
                    SetupCrosshair();
                }
            }
        }

        public void ReloadCrosshair()
        {
            crosshair.DoReload();
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
