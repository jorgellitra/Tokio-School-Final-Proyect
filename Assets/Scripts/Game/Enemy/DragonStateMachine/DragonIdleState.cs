using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;

namespace TokioSchool.FinalProject.Enemy
{
    public class DragonIdleState : BaseState<DragonStateMachine.DragonStates>
    {
        private DragonStateMachine enemy;
        private Animator anim;
        private PlayerController player;

        public DragonIdleState(DragonStateMachine.DragonStates key, DragonStateMachine enemy) :
            base(key, enemy)
        {
            this.enemy = enemy;
            this.anim = enemy.Anim;
            this.player = enemy.Player;
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
        }

        public override DragonStateMachine.DragonStates GetNextState()
        {
            if (enemy.Controller.Dead)
            {
                return DragonStateMachine.DragonStates.Dead;
            }

            if (enemy.playerInRangeToChase)
            {
                return DragonStateMachine.DragonStates.Chase;
            }

            if (enemy.playerInRangeToAttack)
            {
                return DragonStateMachine.DragonStates.Attack;
            }

            return DragonStateMachine.DragonStates.Idle;
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
