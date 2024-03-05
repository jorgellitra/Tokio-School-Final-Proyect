using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Equipments;
using TokioSchool.FinalProject.Player;
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

        [SerializeField] private Crosshair crosshair; //put the UI crosshair object into this field in the inspector
        [SerializeField] private float gunRecoil;
        [SerializeField] private float reloadSpeed;
        [SerializeField] private float settleSpeed;
        [SerializeField] private float maxCrossHairSize;
        [SerializeField] private float shotsPerSecond; //how fast this gun shoots

        public Color specialColor;

        private float shotRate;
        private float nextShotTime;

        private PlayerController player;
        private EquipmentSlot currentEquipmentSlot;
        private Equipment playerEquipment;

        private void Awake()
        {
            player = FindFirstObjectByType<PlayerController>();
            playerEquipment = player.GetComponent<Equipment>();
        }

        public override void StartScreen()
        {
            base.StartScreen();

            sliderLife.maxValue = player.Life;
            sliderStamina.maxValue = player.Stamina;
            UpdateUI();
        }

        private void Update()
        {
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
            SetupWeaponUI();
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
                }
            }
        }
    }
}
