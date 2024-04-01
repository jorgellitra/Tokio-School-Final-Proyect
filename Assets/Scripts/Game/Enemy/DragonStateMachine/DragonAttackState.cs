using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;
using UnityEngine.AI;

namespace TokioSchool.FinalProject.Enemy
{
    public class DragonAttackState : BaseState<DragonStateMachine.DragonStates>
    {
        private DragonStateMachine enemy;
        private Animator anim;
        private NavMeshAgent navAgent;
        private PlayerController player;

        private bool isAttacking = false;
        private float attackColdownReset;

        public DragonAttackState(DragonStateMachine.DragonStates key, DragonStateMachine enemy) :
            base(key, enemy)
        {
            this.enemy = enemy;
            this.anim = enemy.Anim;
            this.navAgent = enemy.NavAgent;
            this.player = enemy.Player;
        }

        public override void EnterState()
        {
            if (enemy.isTransitioning)
            {
                return;
            }
            Debug.Log("EnterState Attack");
            navAgent.speed = 0;
            anim.SetTrigger(enemy.nextAttack.ToString());
            isAttacking = true;
            attackColdownReset = enemy.coldownAttacks[enemy.nextAttack];
        }

        public override void ExitState()
        {
            Debug.Log("ExitState Attack");
            isAttacking = false;
            navAgent.speed = enemy.walkSpeed;
        }

        public override DragonStateMachine.DragonStates GetNextState()
        {
            if (enemy.Controller.Dead)
            {
                return DragonStateMachine.DragonStates.Dead;
            }

            if (!isAttacking)
            {
                enemy.UpdateNextAttack();
                return DragonStateMachine.DragonStates.Chase;
            }

            return DragonStateMachine.DragonStates.Attack;
        }

        public override void UpdateState()
        {
            attackColdownReset -= Time.deltaTime;

            isAttacking = attackColdownReset > 0;
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
