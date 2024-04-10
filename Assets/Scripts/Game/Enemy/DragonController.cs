using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.UI;
using UnityEngine;

namespace TokioSchool.FinalProject.Enemy
{
    public class DragonController : EnemyController
    {
        [SerializeField] private UIController uiController;
        [SerializeField] private UIPanel resultPanel;

        public override void HandleHit(float damage)
        {
            DragonStateMachine dragonStateMachine = GetComponent<DragonStateMachine>();
            if (dragonStateMachine.isTransitioning)
            {
                return;
            }

            base.HandleHit(damage);

            if (currentLife <= life * 0.4f && dragonStateMachine.CurrentPhase == DragonStateMachine.DragonPhases.Second)
            {
                StartCoroutine(dragonStateMachine.ThirdPhase());
            }

            if (currentLife <= life * 0.7f && dragonStateMachine.CurrentPhase == DragonStateMachine.DragonPhases.First)
            {
                StartCoroutine(dragonStateMachine.SecondPhase());
            }
        }

        public override void HandleDeath()
        {
            base.HandleDeath();

            uiController.SwitchScreens(resultPanel);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
