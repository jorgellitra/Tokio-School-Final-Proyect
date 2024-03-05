using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;
using UnityEngine.AI;

namespace TokioSchool.FinalProject.Enemy
{
    public class DragonAttackState : BaseState<DragonStateMachine.DragonState>
    {
        private DragonStateMachine enemy;
        private Animator anim;
        private NavMeshAgent navAgent;
        private PlayerController player;

        private bool isAttacking = false;
        private float attackColdownReset;

        public DragonAttackState(DragonStateMachine.DragonState key, DragonStateMachine enemy) :
            base(key, enemy)
        {
            this.enemy = enemy;
            this.anim = enemy.Anim;
            this.navAgent = enemy.NavAgent;
            this.player = enemy.Player;
        }

        public override void EnterState()
        {
            Debug.Log("EnterState Attack");
            navAgent.speed = 0;
            int indexAttack = Random.Range(1, 3);
            anim.SetTrigger("Attack" + indexAttack);
            isAttacking = true;
            attackColdownReset = anim.GetCurrentAnimatorStateInfo(0).length;
        }

        public override void ExitState()
        {
            Debug.Log("ExitState Attack");
            isAttacking = false;
            navAgent.speed = enemy.walkSpeed;
        }

        public override DragonStateMachine.DragonState GetNextState()
        {
            if (enemy.Controller.Dead)
            {
                return DragonStateMachine.DragonState.Dead;
            }

            return !isAttacking ? DragonStateMachine.DragonState.Chase : DragonStateMachine.DragonState.Attack;
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
