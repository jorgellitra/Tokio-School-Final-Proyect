using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Player;
using UnityEngine;
using UnityEngine.UI;

namespace TokioSchool.FinalProject.UI
{
    public class UIPlayerController : UIPanel
    {
        [SerializeField] private Slider sliderLife;
        [SerializeField] private Slider sliderStamina;

        [SerializeField] private PlayerController player;

        public override void StartScreen()
        {
            base.StartScreen();
            sliderLife.maxValue = player.Life;
            sliderStamina.maxValue = player.Stamina;
        }

        private void Update()
        {
            sliderStamina.value = player.CurrentStamina;
        }

        private void OnUpdateUI()
        {
            sliderStamina.value = player.CurrentStamina;
            sliderLife.value = player.CurrentLife;
        }
    }
}
