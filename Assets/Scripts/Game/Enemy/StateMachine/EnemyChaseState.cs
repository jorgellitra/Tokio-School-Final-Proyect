using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;
using UnityEngine.AI;

namespace TokioSchool.FinalProject.Enemy
{
    public class EnemyChaseState : BaseState<EnemyStateMachine.EnemyState>
    {
        private EnemyStateMachine enemy;
        private NavMeshAgent navAgent;
        private PlayerController player;

        private float chasingTimeReset;
        private bool goIdleTimeout;

        public EnemyChaseState(EnemyStateMachine.EnemyState key, EnemyStateMachine enemy) :
            base(key, enemy)
        {
            this.enemy = enemy;
            this.navAgent = enemy.NavAgent;
            this.player = enemy.Player;
        }

        public override void EnterState()
        {
            chasingTimeReset = enemy.chasingTime;
            navAgent.speed = enemy.runSpeed;
            goIdleTimeout = false;
        }

        public override void ExitState()
        {
            navAgent.speed = enemy.walkSpeed;
        }

        public override EnemyStateMachine.EnemyState GetNextState()
        {
            if (!enemy.playerInRangeToChase && goIdleTimeout)
            {
                return EnemyStateMachine.EnemyState.Idle;
            }

            if (goIdleTimeout)
            {
                enemy.prioritisePatrol = true;
                return EnemyStateMachine.EnemyState.Idle;
            }

            if (enemy.Controller.Dead)
            {
                return EnemyStateMachine.EnemyState.Dead;
            }

            return enemy.playerInRangeToAttack ? EnemyStateMachine.EnemyState.Attack : EnemyStateMachine.EnemyState.Chase;
        }

        public override void UpdateState()
        {
            navAgent.destination = player.transform.position;

            chasingTimeReset -= Time.deltaTime;
            goIdleTimeout = chasingTimeReset < 0;
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
