using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TokioSchool.FinalProject.Enemy
{
    public class DragonController : EnemyController
    {
        protected override void HandleHit(float damage)
        {
            base.HandleHit(damage);

            DragonStateMachine dragonStateMachine = GetComponent<DragonStateMachine>();

            if (currentLife <= life * 0.4f && !dragonStateMachine.isTransitioning &&
                dragonStateMachine.CurrentPhase == DragonStateMachine.DragonPhases.Second)
            {
                StartCoroutine(dragonStateMachine.ThirdPhase());
            }

            if (currentLife <= life * 0.7f && !dragonStateMachine.isTransitioning &&
                dragonStateMachine.CurrentPhase == DragonStateMachine.DragonPhases.First)
            {
                StartCoroutine(dragonStateMachine.SecondPhase());
            }
        }
    }
}
