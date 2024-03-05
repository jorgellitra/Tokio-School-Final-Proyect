using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;
using UnityEngine.AI;

namespace TokioSchool.FinalProject.Enemy
{
    public class DragonChaseState : BaseState<DragonStateMachine.DragonState>
    {
        private DragonStateMachine enemy;
        private NavMeshAgent navAgent;
        private PlayerController player;

        public DragonChaseState(DragonStateMachine.DragonState key, DragonStateMachine enemy) :
            base(key, enemy)
        {
            this.enemy = enemy;
            this.navAgent = enemy.NavAgent;
            this.player = enemy.Player;
        }

        public override void EnterState()
        {
            //Debug.Log("EnterState Chase");
            navAgent.speed = enemy.runSpeed;
        }

        public override void ExitState()
        {
            //Debug.Log("ExitState Chase");
            navAgent.speed = enemy.walkSpeed;
        }

        public override DragonStateMachine.DragonState GetNextState()
        {
            if (!enemy.playerInRangeToChase)
            {
                return DragonStateMachine.DragonState.Idle;
            }

            if (enemy.Controller.Dead)
            {
                return DragonStateMachine.DragonState.Dead;
            }

            return enemy.playerInRangeToAttack ? DragonStateMachine.DragonState.Attack : DragonStateMachine.DragonState.Chase;
        }

        public override void UpdateState()
        {
            //navAgent.SetDestination(player.transform.position);
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
