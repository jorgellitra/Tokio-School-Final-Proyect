using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using UnityEngine;
using UnityEngine.AI;

namespace TokioSchool.FinalProject.Enemy
{
    public class DragonDeadState : BaseState<DragonStateMachine.DragonStates>
    {
        private DragonStateMachine enemy;
        private Animator anim;
        private NavMeshAgent navAgent;

        public DragonDeadState(DragonStateMachine.DragonStates key, DragonStateMachine enemy) :
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
            anim.SetTrigger(enemy.animDead);
            LevelManager.Instance.SaveTime();
            enemy.GetComponent<DragonController>().HandleDeath();
        }

        public override void ExitState()
        {
        }

        public override DragonStateMachine.DragonStates GetNextState()
        {
            return DragonStateMachine.DragonStates.Dead;
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
