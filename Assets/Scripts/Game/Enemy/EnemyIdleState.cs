using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;

namespace TokioSchool.FinalProject.Enemy
{
    public class EnemyIdleState : BaseState<EnemyStateMachine.EnemyState>
    {
        [SerializeField] private float waitingTime;

        private float waitingTimeReset;
        private bool goPatrol;
        private bool playerFound;
        private EnemyStateMachine enemy;
        private Animator anim;
        private Rigidbody rb;
        private PlayerController player;

        public EnemyIdleState(EnemyStateMachine.EnemyState key, EnemyStateMachine enemy) :
            base(key, enemy)
        {
            this.enemy = enemy;
            this.anim = enemy.Anim;
            this.rb = enemy.Rb;
            this.player = enemy.Player;
        }

        public override void EnterState()
        {
            waitingTimeReset = waitingTime;
            goPatrol = false;

            anim.Play("Idle");
            rb.velocity = Vector3.zero;

            Debug.Log("EnterState idle");
        }

        public override void ExitState()
        {
            Debug.Log("ExitState idle");
        }

        public override EnemyStateMachine.EnemyState GetNextState()
        {
            if (playerFound)
            {
                return EnemyStateMachine.EnemyState.Chase;
            }

            if (goPatrol)
            {
                return EnemyStateMachine.EnemyState.Patrol;
            }

            return EnemyStateMachine.EnemyState.Idle;
        }

        public override void UpdateState()
        {
            waitingTimeReset -= Time.deltaTime;

            if (waitingTimeReset < 0)
            {
                goPatrol = true;
            }

            if (Vector3.Distance(player.transform.position, enemy.transform.position) < enemy.distanceToChasePlayer)
            {
                playerFound = true;
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
