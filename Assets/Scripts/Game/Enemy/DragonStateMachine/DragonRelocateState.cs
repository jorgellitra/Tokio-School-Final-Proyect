using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;
using UnityEngine.AI;

namespace TokioSchool.FinalProject.Enemy
{
    public class DragonRelocateState : BaseState<DragonStateMachine.DragonStates>
    {
        private DragonStateMachine enemy;
        private Animator anim;
        private NavMeshAgent navAgent;
        private PlayerController player;

        private int indexPatrolPoint = 0;
        private bool relocatePointReached;
        private Transform currentPatrolPoint;

        public DragonRelocateState(DragonStateMachine.DragonStates key, DragonStateMachine enemy) :
            base(key, enemy)
        {
            this.enemy = enemy;
            this.anim = enemy.Anim;
            this.navAgent = enemy.NavAgent;
            this.player = enemy.Player;
        }

        public override void EnterState()
        {
            //Debug.Log("EnterState Patrol");
            relocatePointReached = false;
            //if (enemy.patrolPoints.Length > 0)
            //{
            //    currentPatrolPoint = enemy.patrolPoints[indexPatrolPoint];
            //}
            //if (currentPatrolPoint != null)
            //{
            //    navAgent.SetDestination(currentPatrolPoint.position);
            //    navAgent.speed = enemy.walkSpeed;
            //}
        }

        public override void ExitState()
        {
            //Debug.Log("ExitState Patrol");
            //if (indexPatrolPoint == enemy.patrolPoints.Length - 1)
            //{
            //    indexPatrolPoint = 0;
            //}
            //else
            //{
            //    indexPatrolPoint++;
            //}
        }

        public override DragonStateMachine.DragonStates GetNextState()
        {
            if (enemy.playerInRangeToChase)
            {
                return DragonStateMachine.DragonStates.Chase;
            }

            if (enemy.Controller.Dead)
            {
                return DragonStateMachine.DragonStates.Dead;
            }

            return relocatePointReached ? DragonStateMachine.DragonStates.Idle : DragonStateMachine.DragonStates.Relocate;
        }

        public override void UpdateState()
        {
            //if (currentPatrolPoint)
            //{
            //    float distanceBetweenNextPatrolPoint = Vector3.Distance(currentPatrolPoint.position, enemy.transform.position);
            //    if (distanceBetweenNextPatrolPoint < enemy.minDistanceToMoveNextPatrolPoint)
            //    {
            //        relocatePointReached = true;
            //    }
            //}
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
