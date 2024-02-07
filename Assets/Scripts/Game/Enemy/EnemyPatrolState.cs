using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;

namespace TokioSchool.FinalProject.Enemy
{
    public class EnemyPatrolState : BaseState<EnemyStateMachine.EnemyState>
    {

        private EnemyStateMachine enemy;
        private Animator anim;
        private Rigidbody rb;
        private PlayerController player;

        public EnemyPatrolState(EnemyStateMachine.EnemyState key, EnemyStateMachine enemy) :
            base(key, enemy)
        {
            this.enemy = enemy;
            this.anim = enemy.Anim;
            this.rb = enemy.Rb;
            this.player = enemy.Player;
        }

        public override void EnterState()
        {
            Debug.Log("EnterState Patrol");
        }

        public override void ExitState()
        {
            Debug.Log("ExitState Patrol");
        }

        public override EnemyStateMachine.EnemyState GetNextState()
        {
            return EnemyStateMachine.EnemyState.Patrol;
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
