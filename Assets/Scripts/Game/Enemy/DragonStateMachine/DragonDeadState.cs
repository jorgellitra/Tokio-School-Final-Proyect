using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using UnityEngine;
using UnityEngine.AI;

namespace TokioSchool.FinalProject.Enemy
{
    public class DragonDeadState : BaseState<DragonStateMachine.DragonState>
    {
        private DragonStateMachine enemy;
        private Animator anim;
        private NavMeshAgent navAgent;

        public DragonDeadState(DragonStateMachine.DragonState key, DragonStateMachine enemy) :
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
            LevelManager.Instance.SaveTime();
            enemy.OnDeath();
            //Debug.Log("EnterState dead");
        }

        public override void ExitState()
        {
            //Debug.Log("ExitState dead");
        }

        public override DragonStateMachine.DragonState GetNextState()
        {
            return DragonStateMachine.DragonState.Dead;
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
