using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;

namespace TokioSchool.FinalProject.Enemy
{
    public class EnemyIdleState : BaseState<EnemyStateMachine.EnemyState>
    {
        private float waitingTimeReset;
        private bool goPatrol;
        private EnemyStateMachine enemy;
        private Animator anim;
        private PlayerController player;

        public EnemyIdleState(EnemyStateMachine.EnemyState key, EnemyStateMachine enemy) :
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

        public override EnemyStateMachine.EnemyState GetNextState()
        {
            if (enemy.playerInRangeToChase)
            {
                return EnemyStateMachine.EnemyState.Chase;
            }

            if (enemy.Controller.Dead)
            {
                return EnemyStateMachine.EnemyState.Dead;
            }

            if (goPatrol)
            {
                return EnemyStateMachine.EnemyState.Patrol;
            }

            return EnemyStateMachine.EnemyState.Idle;
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
