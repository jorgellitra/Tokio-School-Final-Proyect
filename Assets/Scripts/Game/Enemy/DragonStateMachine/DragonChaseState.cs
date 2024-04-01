using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;
using UnityEngine.AI;

namespace TokioSchool.FinalProject.Enemy
{
    public class DragonChaseState : BaseState<DragonStateMachine.DragonStates>
    {
        private DragonStateMachine enemy;
        private NavMeshAgent navAgent;
        private PlayerController player;

        public DragonChaseState(DragonStateMachine.DragonStates key, DragonStateMachine enemy) :
            base(key, enemy)
        {
            this.enemy = enemy;
            this.navAgent = enemy.NavAgent;
            this.player = enemy.Player;
        }

        public override void EnterState()
        {
            Debug.Log("EnterState Chase");
            navAgent.speed = enemy.runSpeed;
        }

        public override void ExitState()
        {
            Debug.Log("ExitState Chase");
            navAgent.speed = enemy.walkSpeed;
        }

        public override DragonStateMachine.DragonStates GetNextState()
        {
            if (enemy.playerInRangeToAttack)
            {
                return DragonStateMachine.DragonStates.Attack;
            }

            if (enemy.Controller.Dead)
            {
                return DragonStateMachine.DragonStates.Dead;
            }

            return DragonStateMachine.DragonStates.Chase;
        }

        public override void UpdateState()
        {
            navAgent.SetDestination(player.transform.position);
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
