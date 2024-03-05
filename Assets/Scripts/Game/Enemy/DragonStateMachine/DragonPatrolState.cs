using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;
using UnityEngine.AI;

namespace TokioSchool.FinalProject.Enemy
{
    public class DragonPatrolState : BaseState<DragonStateMachine.DragonState>
    {
        private DragonStateMachine enemy;
        private Animator anim;
        private NavMeshAgent navAgent;
        private PlayerController player;

        private int indexPatrolPoint = 0;
        private bool patrolPointReached;
        private Transform currentPatrolPoint;

        public DragonPatrolState(DragonStateMachine.DragonState key, DragonStateMachine enemy) :
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
            patrolPointReached = false;
            if (enemy.patrolPoints.Length > 0)
            {
                currentPatrolPoint = enemy.patrolPoints[indexPatrolPoint];
            }
            if (currentPatrolPoint != null)
            {
                navAgent.SetDestination(currentPatrolPoint.position);
                navAgent.speed = enemy.walkSpeed;
            }
        }

        public override void ExitState()
        {
            //Debug.Log("ExitState Patrol");
            if (indexPatrolPoint == enemy.patrolPoints.Length - 1)
            {
                indexPatrolPoint = 0;
            }
            else
            {
                indexPatrolPoint++;
            }
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

            return patrolPointReached ? DragonStateMachine.DragonState.Idle : DragonStateMachine.DragonState.Patrol;
        }

        public override void UpdateState()
        {
            if (currentPatrolPoint)
            {
                float distanceBetweenNextPatrolPoint = Vector3.Distance(currentPatrolPoint.position, enemy.transform.position);
                if (distanceBetweenNextPatrolPoint < enemy.minDistanceToMoveNextPatrolPoint)
                {
                    patrolPointReached = true;
                }
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
