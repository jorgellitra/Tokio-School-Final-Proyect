using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;

namespace TokioSchool.FinalProject.Enemy
{
    public class DragonIdleState : BaseState<DragonStateMachine.DragonState>
    {
        private float waitingTimeReset;
        private bool goPatrol;
        private DragonStateMachine enemy;
        private Animator anim;
        private PlayerController player;

        public DragonIdleState(DragonStateMachine.DragonState key, DragonStateMachine enemy) :
            base(key, enemy)
        {
            this.enemy = enemy;
            this.anim = enemy.Anim;
            this.player = enemy.Player;
        }

        public override void EnterState()
        {
            waitingTimeReset = enemy.waitingTimeIdle;
            goPatrol = false;

            //Debug.Log("EnterState idle");
        }

        public override void ExitState()
        {
            //Debug.Log("ExitState idle");
        }

        public override DragonStateMachine.DragonState GetNextState()
        {
            if (enemy.playerInRangeToChase)
            {
                return DragonStateMachine.DragonState.Chase;
            }

            if (enemy.Controller.Dead)
            {
                return DragonStateMachine.DragonState.Dead;
            }

            if (goPatrol)
            {
                return DragonStateMachine.DragonState.Patrol;
            }

            return DragonStateMachine.DragonState.Idle;
        }

        public override void UpdateState()
        {
            waitingTimeReset -= Time.deltaTime;

            if (waitingTimeReset < 0)
            {
                goPatrol = true;
            }
        }

        public override void OnTriggerEnter(Collider other)
        {
        }

        public override void OnTriggerExit(Collider other)
        {
        }

        public override void OnTriggerStay(Collider other)
        {
        }
    }
}
