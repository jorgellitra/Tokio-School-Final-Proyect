using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TokioSchool.FinalProject.UI
{
    public class UICredits : UIPanel
    {
        [SerializeField] private RectTransform creditsSection;
        [SerializeField] private float speed;

        public override void StartScreen()
        {
            base.StartScreen();
            Vector2 pos = creditsSection.position;
            pos.y = 0;
            creditsSection.position = pos;
        }

        void Update()
        {
            creditsSection.position += speed * Time.deltaTime * Vector3.up;
        }
    }
}
