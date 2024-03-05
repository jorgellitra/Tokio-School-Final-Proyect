using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using UnityEngine;
using UnityEngine.AI;

namespace TokioSchool.FinalProject.Enemy
{
    public class EnemyDeadState : BaseState<EnemyStateMachine.EnemyState>
    {
        private EnemyStateMachine enemy;
        private Animator anim;
        private NavMeshAgent navAgent;

        public EnemyDeadState(EnemyStateMachine.EnemyState key, EnemyStateMachine enemy) :
            base(key, enemy)
        {
            this.enemy = enemy;
            this.anim = enemy.Anim;
            this.navAgent = enemy.NavAgent;
        }

        public override void EnterState()
        {
            navAgent.SetDestination(enemy.transform.position);
            navAgent.speed = 0;
            anim.SetTrigger(enemy.animDeath);
            EnemyLevelController.Instance.CheckEnemies();
            //Debug.Log("EnterState dead");
        }

        public override void ExitState()
        {
            //Debug.Log("ExitState dead");
        }

        public override EnemyStateMachine.EnemyState GetNextState()
        {
            return EnemyStateMachine.EnemyState.Dead;
        }

        public override void UpdateState()
        {
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
