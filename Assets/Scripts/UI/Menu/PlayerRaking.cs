using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TokioSchool.FinalProject.UI
{
    public class PlayerRaking : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI timeText;

        public void Setup(string name, long miliseconds)
        {
            TimeSpan time = TimeSpan.FromMilliseconds(miliseconds);

            nameText.text = name;
            timeText.text = $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}";
        }
    }
}