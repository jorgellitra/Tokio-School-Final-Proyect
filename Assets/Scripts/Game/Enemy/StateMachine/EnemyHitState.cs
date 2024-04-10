using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;
using UnityEngine.AI;

namespace TokioSchool.FinalProject.Enemy
{
    public class EnemyHitState : BaseState<EnemyStateMachine.EnemyState>
    {
        private EnemyStateMachine enemy;
        private NavMeshAgent navAgent;
        private Animator anim;
        private PlayerController player;

        private bool canChangeState;
        private float hitColdownReset;

        public EnemyHitState(EnemyStateMachine.EnemyState key, EnemyStateMachine enemy) :
            base(key, enemy)
        {
            this.enemy = enemy;
            this.anim = enemy.Anim;
            this.navAgent = enemy.NavAgent;
            this.player = enemy.Player;
        }

        public override void EnterState()
        {
            navAgent.speed = 0;
            enemy.ChangeStatusWeaponColliders(false);
            //33% prob to trigger anim
            bool triggerAnimHit = Random.Range(0, 3) == 0;
            if (triggerAnimHit)
            {
                anim.SetTrigger("Hit");
            }
            hitColdownReset = 1.5f;
        }

        public override void ExitState()
        {
        }

        public override EnemyStateMachine.EnemyState GetNextState()
        {
            if (!canChangeState)
            {
                return EnemyStateMachine.EnemyState.Hit;
            }

            if (enemy.playerInRangeToAttack)
            {
                return EnemyStateMachine.EnemyState.Attack;
            }

            return EnemyStateMachine.EnemyState.Chase;
        }

        public override void UpdateState()
        {
            hitColdownReset -= Time.deltaTime;
            canChangeState = hitColdownReset < 0;
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
